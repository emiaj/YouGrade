namespace YouGrade.Domain
{
    public class Answer
    {
        protected Answer()
        {
        }
        public Answer(int questionNumber, bool correct, int alternative)
        {
            QuestionNumber = questionNumber;
            Correct = correct;
            Alternative = alternative;
        }

        public int QuestionNumber { get; protected set; }
        public bool Correct { get; protected set; }
        public int Alternative { get; protected set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Answer)) return false;
            return Equals((Answer) obj);
        }

        public bool Equals(Answer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.QuestionNumber == QuestionNumber;
        }

        public override int GetHashCode()
        {
            return QuestionNumber;
        }
    }
}