namespace YouGrade.Domain.Services
{
    public interface IQuizTakeFactory
    {
        QuizTake GetOrCreate(string takeId, int quizId, string userId);
    }
}