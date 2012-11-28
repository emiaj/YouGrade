using System;
using System.Collections.Generic;
using System.Linq;

namespace YouGrade.Domain
{
    public class QuizTake
    {
        protected QuizTake()
        {
        }

        public QuizTake(string id, int quizId, string userId, int questions)
        {
            Id = id;
            QuizId = quizId;
            UserId = userId;
            Questions = questions;
            DateTime = DateTime.UtcNow;
            Answers = new List<Answer>();
        }

        public string Id { get; protected set; }
        public int QuizId { get; protected set; }
        public string UserId { get; protected set; }
        public DateTime DateTime { get; protected set; }
        public int Questions { get; protected set; }
        public List<Answer> Answers { get; protected set; }
        public int CorrectAnswers { get { return Answers.Count(x => x.Correct); } }
        public int IncorrectAnswers { get { return Answers.Count(x => !x.Correct); } }

        public QuizTake Reset()
        {
            return new QuizTake
                {
                    Id = Id,
                    Questions = Questions,
                    QuizId = QuizId,
                    UserId = UserId
                };
        }
    }
}