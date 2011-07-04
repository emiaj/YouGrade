using System;
using System.Collections.Generic;

namespace YouGrade.Dtos
{
    public class ExamDefDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinimumOfCorrectAnswers { get; set; }
        public TimeSpan Duration { get; set; }
        public List<QuestionDefDto> Questions { get; set; }
    }
}