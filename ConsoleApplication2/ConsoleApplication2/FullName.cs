namespace SerializeExercise
{
    class FullName
    {
        public bool Equals(FullName other)
        {
            return string.Equals(_firstName, other._firstName) && string.Equals(_lastName, other._lastName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FullName && Equals((FullName)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_firstName != null ? _firstName.GetHashCode() : 0) * 397) ^ (_lastName != null ? _lastName.GetHashCode() : 0);
            }
        }

        public static bool operator ==(FullName left, FullName right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FullName left, FullName right)
        {
            return !left.Equals(right);
        }

        private string _firstName;
        private string _lastName;

        public FullName()
        {

        }

        public FullName(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
    }
}