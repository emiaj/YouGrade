using System;
using System.Collections.Generic;

namespace YouGrade.Dtos
{
    public class ExamTakeDto
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public int Grade { get; set; }
        public string Status { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }

    public enum ExamTakeStatus
    {
        Pending,
        Open,
        Paused,
        Finished,
        Canceled
    }
}