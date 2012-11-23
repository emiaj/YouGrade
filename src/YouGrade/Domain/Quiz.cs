using System.Collections.Generic;

namespace YouGrade.Domain
{
    public class Quiz
    {
        protected Quiz()
        {
        }

        public Quiz(string id, string title, string description)
        {
            Questions = new List<Question>();
            Id = id;
            Title = title;
            Description = description;
        }
        public string Id { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public List<Question> Questions { get; protected set; }
    }
}