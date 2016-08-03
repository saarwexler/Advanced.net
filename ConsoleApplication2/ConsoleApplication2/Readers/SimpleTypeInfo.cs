using System;

namespace SerializeExercise.Readers
{
    public class SimpleTypeInfo : ICreationInfo
    {
        private readonly object _value;

        public SimpleTypeInfo(object value)
        {
            _value = value;
        }

        public object GetInstance(IObjectCreator objectCreator)
        {
            return _value;
        }
    }
}