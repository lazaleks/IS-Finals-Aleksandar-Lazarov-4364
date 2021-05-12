using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.Option;

namespace midTerm.Models.Profiles
{
    class OptionProfile : Profile
    {
        public OptionProfile()
        {
            CreateMap<Option, OptionBaseModel>()
                .ReverseMap();
            CreateMap<Option, OptionModelExtended>()
                .ReverseMap();

            CreateMap<OptionCreateModel, Option>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Question, opt => opt.Ignore());
            CreateMap<OptionUpdateModel, Option>()
                .ForMember(x => x.Question, opt => opt.Ignore())
                .ForMember(x => x.QuestionId,opt => opt.Ignore());
        }
       
    }
}
