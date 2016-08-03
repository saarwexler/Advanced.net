using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializeExercise.Readers;

namespace SerializeExercise.Test
{
    [TestClass]
    public class InfoReaderShould
    {
        [TestMethod]
        public void ReadObjectTypefromStream()
        {
            InfoReader ir = new InfoReader();
           using(Stream stream = new MemoryStream())
            using(ObjectWriter wr = new ObjectWriter(new BinaryWriter(stream)))
            using (BinaryReader br = new BinaryReader(stream))
            {
                wr.Write(new SampleClass());
                stream.Seek(0, SeekOrigin.Begin);
                var info = ir.Read<SampleClass>(br);
                Assert.AreSame(typeof(SampleClass), ((ObjectInfo) info).ObjectType);
            }
        }

        [TestMethod]
        public void ReadPublicFieldNamefromStream()
        {
            InfoReader ir = new InfoReader();
            using (Stream stream = new MemoryStream())
            using (ObjectWriter wr = new ObjectWriter(new BinaryWriter(stream)))
            using (BinaryReader br = new BinaryReader(stream))
            {
                wr.Write(new SampleClass());
                stream.Seek(0, SeekOrigin.Begin);
                var info = ir.Read<SampleClass>(br) as ObjectInfo;
                string expectedName = "simpleMember";
                Assert.IsTrue( info.Fields.ContainsKey(expectedName));
            }
        }

        [TestMethod]
        public void ReadPublicIntFieldValuefromStream()
        {
            InfoReader ir = new InfoReader();
            using (Stream stream = new MemoryStream())
            using (ObjectWriter wr = new ObjectWriter(new BinaryWriter(stream)))
            using (BinaryReader br = new BinaryReader(stream))
            {
                const int five = 5;
                var simpleObject = new SampleClass {simpleMember = five};
                wr.Write(simpleObject);
                stream.Seek(0, SeekOrigin.Begin);
                var info = ir.Read<SampleClass>(br) as ObjectInfo;
                Assert.AreEqual(five, info.Fields["simpleMember"].GetInstance(null));
            }
        }

        [TestMethod]
        public void ReadSeconedPublicIntFieldValuefromStream()
        {
            InfoReader ir = new InfoReader();
            using (Stream stream = new MemoryStream())
            using (ObjectWriter wr = new ObjectWriter(new BinaryWriter(stream)))
            using (BinaryReader br = new BinaryReader(stream))
            {
                const int five = 5;
                const int six  =6;
                var simpleObject = new SampleClass { simpleMember = five,seconedMember = six};
                wr.Write(simpleObject);
                stream.Seek(0, SeekOrigin.Begin);
                var info = ir.Read<SampleClass> (br) as ObjectInfo ;
                Assert.AreEqual(six, info.Fields["seconedMember"].GetInstance(null));
            }
        }

        [TestMethod]
        public void ReadPublicStringFieldValuefromStream()
        {
           const int five = 5;
           const int six = 6;
           string blabla = "blabla";
           var simpleObject = new SampleClass { stringField = blabla };
           var info =(ObjectInfo) WriteObjectAndReadInfo(simpleObject);
           Assert.AreEqual(blabla, info.Fields["stringField"].GetInstance(null));
        }

        [TestMethod]
        public void ReadFieldReference()
        {
            const int five = 5;
            const int six = 6;
            string blabla = "blabla";
            AnotherClass refClass = new AnotherClass();
            var simpleObject = new SampleClass { stringField = blabla ,AnotherClassField = refClass, AnotherClassRef = refClass};
            var info =(ObjectInfo) WriteObjectAndReadInfo(simpleObject);
            var value = info.Fields["AnotherClassRef"];
            Assert.IsInstanceOfType(value, typeof(ReferenceInfo));

            int refKey = ((ReferenceInfo) value).RefKey;
            Assert.AreEqual(1, refKey);
        }

        [TestMethod]
        public void ReadIntArray()
        {
            const int five = 5;
            const int six = 6;
            int[] expected = {1, 2, 3, 4, 5};
            var simpleObject = new SampleClass {arr = expected};
            var info =(ObjectInfo) WriteObjectAndReadInfo(simpleObject);
            var value = (ArrayInfo)info.Fields["arr"];
            
            Assert.IsTrue(expected.SequenceEqual(value.Array.Select(o=>(int)o.GetInstance(null))));
        }
        
        [TestMethod]
        public void ReadStringArray()
        {
            const int five = 5;
            const int six = 6;
            string[] expected = { "1", "2", "3", "4", "5" };
            var simpleObject = new SampleClass { strArr =  expected };
            var info = (ObjectInfo)WriteObjectAndReadInfo(simpleObject);
            var value = (ArrayInfo) info.Fields["strArr"];
            

            Assert.IsTrue(expected.SequenceEqual(value.Array.Select(o=>(string)o.GetInstance(null) )));
        }

        [TestMethod]
        public void ReadObjectArray()
        {
            const int five = 5;
            const int six = 6;
            int[] expectedSequence = {five, six};
            var first = new SampleClass { simpleMember = five};
            var sec = new SampleClass {simpleMember = six};
            var info = (ArrayInfo)WriteObjectAndReadInfo(new[]{first,sec});
            Assert.AreSame(info.ElementType, typeof(SampleClass));
            
            var result =  info.Array.Cast<ObjectInfo>().Select(o=>o.Fields["simpleMember"].GetInstance(null)). Cast<int>();
            Assert.IsTrue(result.SequenceEqual(expectedSequence));

        }

     

        private static ICreationInfo WriteObjectAndReadInfo<T>(T objectToWrite)
        {
            ICreationInfo info;
            using (Stream stream = new MemoryStream())
            using (ObjectWriter wr = new ObjectWriter(new BinaryWriter(stream)))
            using (BinaryReader br = new BinaryReader(stream))
            {
                wr.Write(objectToWrite);
                stream.Seek(0, SeekOrigin.Begin);
                InfoReader ir = new InfoReader();
                info = ir.Read<T>(br);
            }
            return info;
        }
    }

   
}
