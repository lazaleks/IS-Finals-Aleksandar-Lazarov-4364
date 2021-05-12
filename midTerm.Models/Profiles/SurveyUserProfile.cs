using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.SurveyUser;

namespace midTerm.Models.Profiles
{
    class SurveyUserProfile : Profile
    {
        public SurveyUserProfile()
        {
            CreateMap<SurveyUser, SurveyUserBaseModel>()
                .ReverseMap();
            CreateMap<SurveyUser, SurveyUserExtended>()
                .ReverseMap();

            CreateMap<SurveyUserCreate, SurveyUser>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Answers, opt => opt.Ignore());
            CreateMap<SurveyUserUpdate, SurveyUser>()
                .ForMember(x => x.Answers, opt => opt.Ignore());
        }

    }
}