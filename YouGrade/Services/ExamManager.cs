using System.Linq;
using AutoMapper;
using YouGrade.Dtos;

namespace YouGrade.Services
{
    public class ExamManager : IExamManager
    {
        private readonly IYouGradeService _service;
        private readonly IExamTakeContext _examTakeContext;

        public ExamManager(IYouGradeService service, IExamTakeContext examTakeContext)
        {
            _service = service;
            _examTakeContext = examTakeContext;
        }

        public ExamDefDto GetExam()
        {
            var examDef = _service.GetExamDef();
            var examDefDto = new ExamDefDto();
            Mapper.Map(examDef, examDefDto);
            return examDefDto;
        }

        public QuestionDefDto GetQuestion()
        {
            var examDef = _service.GetExamDef();
            var questionDef = examDef.QuestionDef.Where(e => e.Id == _examTakeContext.QuestionId).First();
            var questionDefDto = new QuestionDefDto();
            Mapper.Map(questionDef, questionDefDto);
            foreach (var alternative in questionDefDto.Alternatives)
            {
                var alternativeId = alternative.Id;
                alternative.IsChecked = _examTakeContext.Exam.Answers
                    .Where(x => x.AlternativeId == alternativeId)
                    .Where(x => x.QuestionId == _examTakeContext.QuestionId)
                    .Where(x => x.IsChecked)
                    .Any();
            }
            return questionDefDto;
        }

        public void SaveAnswer(int questionId, string answers)
        {
            _examTakeContext.Exam.Answers.RemoveAll(x => x.QuestionId == questionId);
            foreach (var option in answers.ToCharArray())
            {
                var answer = new AnswerDto
                {
                    AlternativeId = option.ToString(),
                    ExamTakeId = _examTakeContext.Exam.ExamId,
                    QuestionId = questionId,
                    IsChecked = true
                };
                _examTakeContext.Exam.Answers.Add(answer);
            }
        }

        public bool MoveToPreviousQuestion()
        {
            bool canMove = false;

            if (_examTakeContext.QuestionId > 1)
            {
                _examTakeContext.QuestionId--;
                canMove = true;
            }

            return canMove;
        }

        public bool MoveToNextQuestion()
        {
            bool canMove = false;
            var examDef = _service.GetExamDef();

            if (examDef.QuestionDef.Count > _examTakeContext.QuestionId)
            {
                _examTakeContext.QuestionId++;
                canMove = true;
            }

            return canMove;
        }

        public int EndExam()
        {
            var examDef = _service.GetExamDef();
            var result = _service.SaveExamTake(_examTakeContext.Exam);
            return (100 * result) / examDef.QuestionDef.Count;
        }
    }

    public interface IExamManager
    {
        ExamDefDto GetExam();
        QuestionDefDto GetQuestion();
        void SaveAnswer(int questionId, string answers);
        bool MoveToPreviousQuestion();
        bool MoveToNextQuestion();
        int EndExam();
    }
}