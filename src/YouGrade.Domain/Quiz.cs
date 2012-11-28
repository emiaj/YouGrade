using System.Collections.Generic;

namespace YouGrade.Domain
{
    public class Quiz
    {
        protected Quiz()
        {
        }

        public Quiz(int id, string title, string description, string language, string thumbnail)
        {
            Questions = new List<Question>();
            Id = id;
            Title = title;
            Description = description;
            Language = language;
            Thumbnail = thumbnail;
        }

        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public List<Question> Questions { get; protected set; }
        public string Language { get; protected set; }
        public string Thumbnail { get; protected set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Quiz)) return false;
            return Equals((Quiz) obj);
        }

        public bool Equals(Quiz other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}