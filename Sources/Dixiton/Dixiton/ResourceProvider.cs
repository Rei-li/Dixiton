using System;
using System.Collections.Generic;
using System.Resources;
using Dixiton.Common.Interfaces;

namespace Dixiton.Web
{
    public class ResourceProvider : Dixiton.Common.Interfaces.IResourceProvider
    {
        private const string RESOURCE_ASSEMBLY = "App_GlobalResources";

        /// <summary>
        /// Gets or sets ImageResources
        /// </summary>
        /// <value>string object</value>
        public List<string> ImageResources { get; set; }

        /// <summary>
        /// Gets or sets String Resources
        /// </summary>
        /// <value>string object</value>
        public List<string> StringResources { get; set; }

        private readonly Dictionary<string, ResourceManager> _resourceManagers = new Dictionary<string, ResourceManager>();
        private readonly Dictionary<string, ResourceManager> _imageResourceManagers = new Dictionary<string, ResourceManager>();

        public ResourceProvider()
        {
            StringResources = new List<string>();
            ImageResources = new List<string>();
        }

        /// <summary>
        /// Init method
        /// </summary>
        public void Init()
        {
            foreach (string str in StringResources)
            {
                _resourceManagers.Add(str, new ResourceManager(str, System.Reflection.Assembly.Load(RESOURCE_ASSEMBLY)));
            }
            foreach (string str in ImageResources)
            {
                _imageResourceManagers.Add(str, new ResourceManager(str, System.Reflection.Assembly.Load(RESOURCE_ASSEMBLY)));
            }
        }

        /// <summary>
        /// Gets string from resources.
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <returns>Value.</returns>
        public virtual string GetResourceString(string resourceId)
        {
            if (string.IsNullOrEmpty(resourceId))
                return string.Empty;
            return GetResource(resourceId, _resourceManagers, false);
        }

        public virtual string GetResourceStringSafe(string resourceId)
        {
            return GetResource(resourceId, _resourceManagers, true);
        }

        /// <summary>
        /// Gets string from resource.
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <param name="classKey">Resource class key (e.g. 'SR', or 'CodeSR')</param>
        /// <returns>Value</returns>
        public string GetResourceString(string resourceId, string classKey)
        {
            if (!_resourceManagers.ContainsKey(classKey))
                throw new Exception(string.Format("No resource manager for classKey='{0}' is found", classKey));

            string str = _resourceManagers[classKey].GetString(resourceId);
            if (!String.IsNullOrEmpty(str))
            {
                return str;
            }
            throw new Exception(String.Format("Resource with id='{0}' with classKey='{1}' cannot be found", resourceId, classKey));
        }

        /// <summary>
        /// Gets string from resources.
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <returns>Value.</returns>
        public virtual string GetFormattedResourceString(string resourceId, params object[] args)
        {
            return String.Format(GetResource(resourceId, _resourceManagers, false), args);
        }

        /// <summary>
        /// Gets string from resource.
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <returns>Value</returns>
        public virtual string GetResourceImagePath(string resourceId)
        {
            return GetResource(resourceId, _imageResourceManagers, false);
        }

        protected virtual string GetResource(string resourceId, Dictionary<string, ResourceManager> resourceManagers, bool isSafe)
        {
            foreach (ResourceManager rm in resourceManagers.Values)
            {
                string str = rm.GetString(resourceId);
                if (!String.IsNullOrEmpty(str))
                {
                    return str;
                }
            }

            if (isSafe)
            {
                return resourceId;
            }

            throw new Exception(String.Format("Resource with id='{0}' cannot be found", resourceId));
        }
    }
}