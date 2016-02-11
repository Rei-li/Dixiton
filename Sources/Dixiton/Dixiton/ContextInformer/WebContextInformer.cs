using System.Diagnostics;
using System.IO;
using System.Web;
using Dixiton.Common.Interfaces;

namespace Dixiton.ContextInformer
{
    /// <summary>
    /// Web environment context
    /// </summary>
    public class WebContextInformer : IContextInformer
    {
        /// <summary>
        /// Get root directory path
        /// </summary>
        /// <returns>Path</returns>
        private static string GetRootDirectory()
        {
            Debug.Assert(HttpContext.Current != null, "There is no HttpContext!");
            string rootPath = HttpContext.Current.Server.MapPath(@"~\");
            return rootPath;
        }

        public string GetFullPath(string relPath)
        {
            return Path.Combine(GetRootDirectory(), relPath);
        }
    }
}