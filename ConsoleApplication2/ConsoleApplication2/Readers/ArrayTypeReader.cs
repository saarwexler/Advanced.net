using System;
using System.IO;

namespace SerializeExercise.Readers
{
    public class ArrayTypeReader : IReader
    {

        public ICreationInfo Read(BinaryReader br, Type type, InfoReader infoReader)
        {
            var array = ReadArray(br, type, infoReader);
            return new ArrayInfo
            {
                ElementType = type.GetElementType(),

                Array = array
            };
        }

        private ICreationInfo[] ReadArray(BinaryReader br, Type arrayType, InfoReader infoReader)
        {
            var elementType = arrayType.GetElementType();
            var length = br.ReadInt32();
            var arr = new ICreationInfo[length];
            for (int i = 0; i < length; i++)
                arr[i] = infoReader.Read(br, elementType);
            return arr;
        }
       
    }
}
