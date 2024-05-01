using System.ComponentModel.DataAnnotations;

namespace DnnReactDemo.Dto.Missions
{
    public class AddOrUpdateMissionDto : BaseDto<AddOrUpdateMissionDto, Entities.Mission>
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
