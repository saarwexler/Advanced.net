using System;
using System.IO;

namespace SerializeExercise.Readers
{
    public class SimpleTypeReader : IReader
    {
       
        public ICreationInfo Read(BinaryReader br, Type type, InfoReader infoReader)
        {
            var value = Read(br, type);
            return new SimpleTypeInfo(value);

        }

        private object Read(BinaryReader br, Type type)
        {
            if (type == typeof(string))
                return br.ReadString();
            if (type == typeof (Int32))
                return br.ReadInt32();
            throw new NotImplementedException("not yet");
        }
    }
}
