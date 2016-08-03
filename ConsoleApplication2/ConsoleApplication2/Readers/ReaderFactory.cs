using System;
using System.IO;

namespace SerializeExercise.Readers
{
    public class ReaderFactory
    {
        public IReader GetReader(Type type)
        {
            if(type.IsArray)
                return new ArrayTypeReader();
            if(type.IsPrimitive || type == typeof(string))
            return new SimpleTypeReader();
            return new ObjectInfoReader();

        }
    }
}