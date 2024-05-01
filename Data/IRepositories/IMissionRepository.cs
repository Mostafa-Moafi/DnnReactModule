// MIT License

using DnnReactDemo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnnReactDemo.Data.IRepositories
{
    public interface IMissionRepository : IRepository<Mission>
    {
        Task<Mission> GetMissionByUserIdAsync(int missionId, int userId);
        Task<IList<Mission>> GetMissionByUserIdAsync(int userId);
    }
}
