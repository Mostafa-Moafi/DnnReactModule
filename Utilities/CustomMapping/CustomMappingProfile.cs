// MIT License

using AutoMapper;
using System.Collections.Generic;

namespace DnnReactModule.Utilities.CustomMapping
{
    public class CustomMappingProfile : Profile
    {
        public CustomMappingProfile(IEnumerable<IHaveCustomMapping> haveCustomMappings)
        {
            foreach (var item in haveCustomMappings)
                item.CreateMappings(this);
        }
    }
}
