using DnnReactDemo.Entities;

namespace DnnReactDemo.Dto.Missions
{
    public class MissionSelectDto : BaseDto<MissionSelectDto, Mission>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
