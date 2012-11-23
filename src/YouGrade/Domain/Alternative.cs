namespace YouGrade.Domain
{
    public class Alternative
    {
        protected Alternative()
        {
        }
        public Alternative(int alternativeNumber, string alternativeText, bool isCorrect)
        {
            AlternativeNumber = alternativeNumber;
            AlternativeText = alternativeText;
            IsCorrect = isCorrect;
        }

        public int AlternativeNumber { get; protected set; }
        public string AlternativeText { get; protected set; }
        public bool IsCorrect { get; protected set; }
    }
}