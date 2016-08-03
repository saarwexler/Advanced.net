using System;
using System.IO;

namespace SerializeExercise.Readers
{
    public interface IReader
    {
        ICreationInfo Read(BinaryReader br, Type type, InfoReader infoReader);
    }
}