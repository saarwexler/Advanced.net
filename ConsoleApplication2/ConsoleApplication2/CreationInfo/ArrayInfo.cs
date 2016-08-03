using System;

namespace SerializeExercise.Readers
{
    public class ArrayInfo : ICreationInfo
    {
        public object GetInstance(IObjectCreator objectCreator)
        {
            return objectCreator.CreateArray(this);
        }

        public Type ElementType { get; set; }
        public ICreationInfo[] Array { get; set; }
    }
}