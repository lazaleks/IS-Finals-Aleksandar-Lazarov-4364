using AutoMapper;
using midTerm.Data.Entities;
using midTerm.Models.Models.Answers;

namespace midTerm.Models.Profiles
{
    class AnswersProfile : Profile
    {
        public AnswersProfile()
        {
            CreateMap<Answers, AnswersBaseModel>()
                .ReverseMap();
            CreateMap<Answers, AnswersExtended>()
                .ReverseMap();

            CreateMap<AnswerCreateModel, Answers>()
                .ForMember(x => x.Id,opt => opt.Ignore())
                .ForMember(x => x.Option,opt => opt.Ignore())
                .ForMember(x => x.User,opt => opt.Ignore());
            CreateMap<AnswersUpdateModel, Answers>()
                .ForMember(x => x.Option, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore());
        }

    }
}