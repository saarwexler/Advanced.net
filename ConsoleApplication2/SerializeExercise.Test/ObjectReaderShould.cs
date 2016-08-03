using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerializeExercise.Test
{
    [TestClass]
    public class ObjectReaderShould
    {
        [TestMethod]
        public void CreateSimpleObject()
        {
            using(Stream stream = new MemoryStream())
            using(ObjectWriter wr = new ObjectWriter(new BinaryWriter(stream)))
            using (BinaryReader br = new BinaryReader(stream))
            {
                wr.Write(new SampleClass());
                stream.Seek(0, SeekOrigin.Begin);
                ObjectReader target = new ObjectReader(br);
                var result = target.Read<SampleClass>();
                Assert.IsInstanceOfType(result,typeof(SampleClass));
            }
        }

        [TestMethod]
        public void CreateSimpleObjectWithIntMember()
        {
            using (Stream stream = new MemoryStream())
            using (ObjectWriter wr = new ObjectWriter(new BinaryWriter(stream)))
            using (BinaryReader br = new BinaryReader(stream))
            {
                const int five =5;
                var simpleObject = new SampleClass {simpleMember = five};
                wr.Write(simpleObject);
                stream.Seek(0, SeekOrigin.Begin);
                ObjectReader target = new ObjectReader(br);
                var result = target.Read<SampleClass>();
                Assert.AreEqual (five, result.simpleMember);
            }
        }


    }
}
