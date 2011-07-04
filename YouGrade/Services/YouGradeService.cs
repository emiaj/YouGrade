using System.Collections.Generic;
using System.Linq;
using YouGrade.Dtos;
using YouGrade.Model;

namespace YouGrade.Services
{
    public interface IYouGradeService
    {
        ExamDef GetExamDef();
        int SaveExamTake(ExamTakeDto examTakeDto);
    }

    public class YouGradeService : IYouGradeService
    {
        private readonly YouGradeEntities _youGradeEntities;

        public YouGradeService(YouGradeEntities youGradeEntities)
        {
            _youGradeEntities = youGradeEntities;
        }

        public ExamDef GetExamDef()
        {
            return _youGradeEntities.ExamDef.Include("QuestionDef.Alternative").First();
        }

        public int SaveExamTake(ExamTakeDto examTakeDto)
        {
            var newExamTake = new ExamTake
            {
                Id = 0,
                ExamId = examTakeDto.ExamId,
                UserId = examTakeDto.UserId,
                StartDateTime = examTakeDto.StartDateTime,
                Duration = examTakeDto.Duration,
                Grade = examTakeDto.Grade,
                Status = examTakeDto.Status,
            };

            _youGradeEntities.ExamTake.AddObject(newExamTake);
            _youGradeEntities.SaveChanges();

            saveAnswers(examTakeDto.Answers, newExamTake.Id);

            return getCorrectAnswers(newExamTake);
        }

        private int getCorrectAnswers(ExamTake newExamTake)
        {
            var grade = 0;
            var questions = _youGradeEntities.ExamDef.First(x => x.Id == newExamTake.ExamId).QuestionDef;
            foreach (var question in questions)
            {
                var local = question;
                var markedAnswers = newExamTake.Answer
                    .Where(x => x.IsChecked).Where(x => x.QuestionId == local.Id)
                    .Select(x => x.AlternativeId).ToList();
                var answers = question.Alternative.Where(x => x.Correct).Select(x => x.Id);
                grade += answers.All(markedAnswers.Contains) ? 1 : 0;
            }
            return grade;
        }

        private void saveAnswers(IEnumerable<AnswerDto> answers, int examTakeId)
        {
            foreach (var answer in answers)
            {
                var newAnswer = new Answer
                {
                    ExamTakeId = examTakeId,
                    QuestionId = answer.QuestionId,
                    AlternativeId = answer.AlternativeId,
                    IsChecked = answer.IsChecked,
                };
                _youGradeEntities.Answer.AddObject(newAnswer);
            }
            _youGradeEntities.SaveChanges();
        }
    }
}