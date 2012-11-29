namespace YouGrade.Domain.Services
{
    public interface IQuizTakeService
    {
        void Save(QuizTake quizTake);
        QuizTake Get(string takeId);
    }
}