// MIT License

using DnnReactDemo.Dto.Missions;
using DnnReactDemo.Services;
using DnnReactDemo.Utilities.API;
using DotNetNuke.Security;
using DotNetNuke.Web.Api;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DnnReactDemo.Controllers
{
    [DnnAuthorize(StaticRoles = "Registered Users")]
    public class MissionController : ModuleApiController
    {

        private IServiceManager _serviceManager;
        public MissionController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        /// <summary>
        /// Add Mission 
        /// </summary>
        [HttpPost]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public async Task<ApiResult<MissionSelectDto>> Add(AddOrUpdateMissionDto dto)
        {
            var result = await _serviceManager.Mission.AddAsync(dto, UserInfo);
            return result;
        }

        /// <summary>
        /// Update Mission 
        /// </summary>
        [HttpPut]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public async Task<ApiResult<MissionSelectDto>> Update(int id, [FromBody] AddOrUpdateMissionDto dto)
        {
            var result = await _serviceManager.Mission.UpdateAsync(id, dto, UserInfo);
            return result;
        }
        /// <summary>
        /// Get Mission By User
        /// </summary>
        [HttpGet]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public async Task<ApiResult<List<MissionSelectDto>>> Get()
        {
            var result = await _serviceManager.Mission.GetByUserAsync(UserInfo.UserID);
            return result;
        }


        /// <summary>
        /// Delete Mission By MissionId
        /// </summary>
        [HttpDelete]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        public async Task<ApiResult> Delete(int id)
        {
            await _serviceManager.Mission.DeleteAsync(id, UserInfo);
            return Ok();
        }

    }
}
