using System.Collections.Generic;

namespace YouGrade.Dtos
{
    public class QuestionDefDto
    {
        public int Id {get;set;}
        public string Text { get; set; }
        public string Url { get; set; }
        public int StartSeconds { get; set; }
        public string Explanation { get; set; }
        public string Mark { get; set; }
        public bool IsMultiSelect { get; set; }
        public List<AlternativeDto> Alternatives { get; set; }
    }
}