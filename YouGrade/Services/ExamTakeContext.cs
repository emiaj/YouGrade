using System;
using System.Collections.Generic;
using YouGrade.Dtos;

namespace YouGrade.Services
{
    public class ExamTakeContext : IExamTakeContext
    {
        public ExamTakeContext()
        {
            QuestionId = 1;
            Exam = new ExamTakeDto
                       {
                           Answers = new List<AnswerDto>(),
                           // TODO: User must be able to pick which exam he/she wants to take
                           ExamId = 1,
                           StartDateTime = DateTime.Now,
                           Status = ExamTakeStatus.Open.ToString(),
                       };
        }
        public ExamTakeDto Exam { get; set; }
        public int QuestionId { get; set; }

        private int? _userId;
        public int UserId
        {
            get { return _userId.GetValueOrDefault(); }
            set
            {
                _userId = value;
                Exam.UserId = value;
            }
        }

        public bool IsSet()
        {
            return _userId.HasValue;
        }

    }
    public interface IExamTakeContext
    {
        ExamTakeDto Exam { get; }
        int QuestionId { get; set; }
        int UserId { get; set; }
        bool IsSet();
    }
}