// MIT License

using System.Threading.Tasks;

namespace DnnReactDemo.Data.IRepositories
{
    /// <summary>
    /// Interface for managing repositories, provides access to repositories and methods for saving changes synchronously and asynchronously.
    /// </summary>
    public interface IRepositoryManager
    {
        IMissionRepository Mission { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
