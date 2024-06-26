﻿// MIT License

using DnnReactModule.Dto.Missions;
using DotNetNuke.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnnReactModule.Services
{
    public interface IMissionService
    {
        Task<MissionSelectDto> AddAsync(AddOrUpdateMissionDto dto, UserInfo creator);
        Task<MissionSelectDto> UpdateAsync(int id, AddOrUpdateMissionDto dto, UserInfo modifier);
        Task<List<MissionSelectDto>> GetByUserAsync(int userId);
        Task DeleteAsync(int id, UserInfo modifier);
    }
}
