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
            if (id == null) throw new ArgumentNullException("id");
            if (userId == null) throw new ArgumentNullException("userId");
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

        public void UpdateAnswer(Answer answer)
        {
            lock (Answers)
            {
                if (Answers.Contains(answer))
                {
                    Answers.Remove(answer);
                }
                Answers.Add(answer);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (QuizTake)) return false;
            return Equals((QuizTake) obj);
        }

        public bool Equals(QuizTake other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}