// MIT License

using DnnReactModule.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnnReactModule.Data.IRepositories
{
    public interface IMissionRepository : IRepository<Mission>
    {
        Task<Mission> GetMissionByUserIdAsync(int missionId, int userId);
        Task<IList<Mission>> GetMissionByUserIdAsync(int userId);
    }
}
