using System;

namespace YouGrade.Domain
{
    public class Language
    {
        public Language(string id, string nativeName, string neutralName)
        {
            if (id == null) throw new ArgumentNullException("id");
            if (nativeName == null) throw new ArgumentNullException("nativeName");
            if (neutralName == null) throw new ArgumentNullException("neutralName");
            Id = id;
            NativeName = nativeName;
            NeutralName = neutralName;
        }


        public string Id { get; protected set; }
        public string NativeName { get; protected set; }
        public string NeutralName { get; protected set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Language)) return false;
            return Equals((Language)obj);
        }

        public bool Equals(Language other)
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