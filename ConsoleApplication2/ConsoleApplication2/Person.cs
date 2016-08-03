using System.Runtime.Serialization.Formatters.Binary;

namespace SerializeExercise
{
    class Person
    {
        protected bool Equals(Person other)
        {
            return Name.Equals(other.Name) && Age == other.Age && Equals(Address, other.Address);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Person)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Age;
                hashCode = (hashCode * 397) ^ (Address != null ? Address.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Person left, Person right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !Equals(left, right);
        }

        public FullName Name { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
        public Person Spouse { get; set; }
    }
}