namespace YouGrade.Domain
{
    public class Answer
    {
        protected Answer()
        {
        }
        public Answer(int questionNumber, bool correct, string[] alternatives)
        {
            QuestionNumber = questionNumber;
            Correct = correct;
            Alternatives = alternatives;
        }

        public int QuestionNumber { get; protected set; }
        public bool Correct { get; protected set; }
        public string[] Alternatives { get; protected set; }
    }
}