using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializeExercise
{
    public class Serializer
    {
        public static readonly string EndOfSection = "!!!END!!!";

        public static void Serialize<T>(T o, Stream stream)
        {
            var bw = new BinaryWriter(stream);
            Write(o, bw);
            bw.Flush();
        }

        private static void Write<T>(T o , BinaryWriter bw)
        {
            var ow = new ObjectWriter(bw);
            ow.Write(o);
        }

        public static T Deserialize<T>(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            var or = new ObjectReader(br);
           return or.Read<T>();
        }
    }
}