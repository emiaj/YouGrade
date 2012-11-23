using System.Collections.Generic;
using System.Linq;

namespace YouGrade.Domain
{
    public class Question
    {
        protected Question()
        {
        }
        public Question(int questionNumber, string questionText, bool multiSelect, string thumbnail)
        {
            QuestionNumber = questionNumber;
            QuestionText = questionText;
            MultiSelect = multiSelect;
            Thumbnail = thumbnail;
            Alternatives = new List<Alternative>();
        }

        public int QuestionNumber { get; protected set; }
        public string QuestionText { get; protected set; }
        public bool MultiSelect { get; protected set; }
        public string Thumbnail { get; protected set; }

        public List<Alternative> Alternatives { get; protected set; }

        public bool IsValidAnswer(int[] numbers)
        {
            return numbers.All(number => Alternatives.Any(x => x.IsCorrect && x.AlternativeNumber == number));
        }
    }
}