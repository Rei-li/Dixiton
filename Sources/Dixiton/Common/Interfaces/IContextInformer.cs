namespace Dixiton.Common.Interfaces
{
    /// <summary>
    /// Execution context
    /// </summary>
    public interface IContextInformer
    {
        string GetFullPath(string relPath);  
    }
}