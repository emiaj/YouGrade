using System.Collections.Generic;
using AutoMapper;
using Bottles;
using Bottles.Diagnostics;
using YouGrade.Dtos;
using YouGrade.Model;

namespace YouGrade
{
    public class AutoMapperActivator : IActivator
    {
        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            Mapper.CreateMap<ExamDef, ExamDefDto>()
                .ForMember(e => e.Questions, options => options.MapFrom(e => e.QuestionDef));

            Mapper.CreateMap<QuestionDef, QuestionDefDto>()
                .ForMember(e => e.Alternatives, options => options.MapFrom(e => e.Alternative));
            Mapper.CreateMap<Alternative, AlternativeDto>();
            Mapper.CreateMap<ExamTake, ExamTakeDto>();
            Mapper.CreateMap<Answer, AnswerDto>();
        }
    }
}