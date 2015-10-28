using Dixiton.Common.Interfaces;
using NHibernate.Validator.Engine;
using Spring.Context.Support;

namespace Dixiton.Web.Resources
{
    public class CustomMessageInterpolator : IMessageInterpolator
    {
        private volatile IResourceProvider resourceProvider;
        private readonly object sync = new object();

        private IResourceProvider ResourceProvider
        {
            get
            {
                if (resourceProvider == null)
                {
                    lock (sync)
                    {
                        if (resourceProvider == null)
                        {
                            resourceProvider = ContextRegistry.GetContext().GetObject<IResourceProvider>();
                        }
                    }
                }
                return resourceProvider;
            }
        }

        public string Interpolate(InterpolationInfo info)
        {
            var message = info.Message;
            if (!message.StartsWith("{") && !message.EndsWith("}"))
            {
                return message;
            }
            var resource = message.Substring(1, message.Length - 2);
            var translatedMessage = ResourceProvider.GetResourceStringSafe(resource);
            info.Message = translatedMessage;
            var interpolatedMessage = info.DefaultInterpolator.Interpolate(info);
            return interpolatedMessage;
        }
    }
}