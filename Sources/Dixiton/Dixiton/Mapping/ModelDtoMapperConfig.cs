using Dixiton.Common.Interfaces;
using Spring.Context.Support;

namespace Dixiton.Mapping
{
    /// <summary>
    /// Configuration Model->Dto и Dto->Model
    /// </summary>
    public class ModelDtoMapperConfig
    {
        public static void Configure()
        {
            var resourceProvider = ContextRegistry.GetContext().GetObject<IResourceProvider>();
            new ModelDtoMapping(resourceProvider);
        }
    }
}