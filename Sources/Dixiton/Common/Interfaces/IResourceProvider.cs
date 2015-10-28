namespace Dixiton.Common.Interfaces
{
    /// <summary>
    /// Defines a method to gets resources.
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Gets string from resource.
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <returns>Value</returns>
        string GetResourceString(string resourceId);

        /// <summary>
        /// Gets string from resource. If no resource found, resourceId is returned
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <returns>Found value, otherwise resourceId</returns>
        string GetResourceStringSafe(string resourceId);

        /// <summary>
        /// Gets string from resource.
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <param name="classKey">Resource class key (e.g. 'SR', or 'CodeSR')</param>
        /// <returns>Value</returns>
        string GetResourceString(string resourceId, string classKey);

        /// <summary>
        /// Get formatted resource string using passed arguments
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        string GetFormattedResourceString(string resourceId, params object[] args);

        /// <summary>
        /// Gets string from resource.
        /// </summary>
        /// <param name="resourceId">Resource ID</param>
        /// <returns>Value</returns>
        string GetResourceImagePath(string resourceId);
    }
}
