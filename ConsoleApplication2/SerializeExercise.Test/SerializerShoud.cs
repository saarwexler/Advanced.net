using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerializeExercise.Test
{
    [TestClass]
    public class SerializerShoud
    {
        private readonly string Somthing = "Somthing good";
        private const int Seven = 7;
        private const int Five = 5;
        private const int Six = 6;

        [TestMethod]
        public void SerializeAndDeserialaizeInt()
        {
            int result = SerializeAndDeserialize(5);
            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void SerializeAndDeserialaizeString()
        {
            var result = SerializeAndDeserialize("5");
            Assert.AreEqual("5", result);

        }


        [TestMethod] 
        public void SerializeAndDeserialaizeObjectWithNoMemebers()
        {
            var result = SerializeAndDeserialize(new SampleClass());
                Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SerializeAndDeserialaizeObjectWithOneIntPublicMember()
        {
            var simpleObject = new SampleClass() { simpleMember = Five };
           var result =  SerializeAndDeserialize(simpleObject);
                Assert.AreEqual(Five,result.simpleMember);
        }

        [TestMethod]
        public void SerializeAndDeserialaizeObjectWithTwoIntPublicField()
        {
            var simpleObject = new SampleClass { simpleMember = Five, seconedMember = Six };
           var result = SerializeAndDeserialize(simpleObject);
            Assert.AreEqual(Six, result.seconedMember);
        }

        [TestMethod]
        public void SerializeAndDeserialaizeObjectWithIntAndStringPublicField()
        {
            var simpleObject = new SampleClass { simpleMember = Five, seconedMember = Six, stringField = Somthing };
            var result = SerializeAndDeserialize(simpleObject);
            Assert.AreEqual(Somthing, result.stringField);
        }

        [TestMethod]
        public void SerializeAndDeserialaizeObjectWithClassPublicField()
        {
            string anotherOneBustToDust = "anotherOneBustToDust";
            AnotherClass anotherClass = new AnotherClass {Name = anotherOneBustToDust};
            var simpleObject = new SampleClass { simpleMember = Five, seconedMember = Six, stringField = Somthing, AnotherClassField  = anotherClass };
            var result = SerializeAndDeserialize(simpleObject);
            Assert.AreEqual(anotherOneBustToDust, result.AnotherClassField.Name);
        }

        [TestMethod]
        public void SerializeAndDeserialaizeIntProperty()
        {
            string anotherOneBustToDust = "anotherOneBustToDust";
            AnotherClass anotherClass = new AnotherClass { Name = anotherOneBustToDust };
            var simpleObject = new SampleClass { simpleMember = Five, seconedMember = Six, stringField = Somthing, AnotherClassField = anotherClass, IntProp = Seven};
            var result = SerializeAndDeserialize(simpleObject);
            Assert.AreEqual(7, result.IntProp);
        }

        [TestMethod]
        public void HandleCrossReference()
        {
            string anotherOneBustToDust = "anotherOneBustToDust";
            AnotherClass anotherClass = new AnotherClass { Name = anotherOneBustToDust };
            var simpleObject = new SampleClass { simpleMember = Five, seconedMember = Six, stringField = Somthing, AnotherClassField = anotherClass, IntProp = Seven, AnotherClassRef = anotherClass };
            var result = SerializeAndDeserialize(simpleObject);
            Assert.AreSame(result.AnotherClassField, result.AnotherClassRef);
        }

        [TestMethod]
        public void HandleCircularReference()
        {
            string anotherOneBustToDust = "anotherOneBustToDust";
            AnotherClass anotherClass = new AnotherClass { Name = anotherOneBustToDust };
            var simpleObject = new SampleClass { simpleMember = Five, seconedMember = Six, stringField = Somthing, AnotherClassField = anotherClass, IntProp = Seven };
            anotherClass.Reference = simpleObject;
            var result = SerializeAndDeserialize(simpleObject);
            Assert.AreSame(result, result.AnotherClassField.Reference);
        }

        private static T SerializeAndDeserialize <T>(T objectToSerialize)
        {
            SampleClass result;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(objectToSerialize, stream);
                stream.Seek(0, SeekOrigin.Begin);
                return Serializer.Deserialize<T>(stream);
            }
        }

        [TestMethod]
        public void SerializeAndDeserialaizeObjectWithArrayOfInt()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var arr = new[] { 1, 2, 3 };
                Serializer.Serialize(new SampleClass{arr = arr} , stream);
                stream.Seek(0, SeekOrigin.Begin);
                var result = Serializer.Deserialize<SampleClass>(stream);
                Assert.IsTrue(arr.SequenceEqual(result.arr));
            }
        }

        [TestMethod]
        public void SerializeAndDeserialaizeArrayOfInt()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var arr = new[] { 1, 2, 3 };
                Serializer.Serialize(arr, stream);
                stream.Seek(0, SeekOrigin.Begin);
                var result = Serializer.Deserialize<int[]>(stream);
                Assert.IsTrue(arr.SequenceEqual(result));
            }
        }

        [TestMethod]
        public void SerializeAndDeserialaizeArrayOfStrings()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                var arr = new[] { "1", "2", "3" };
                Serializer.Serialize(arr, stream);
                stream.Seek(0, SeekOrigin.Begin);
                var result = Serializer.Deserialize<string[]>(stream);
                Assert.IsTrue(arr.SequenceEqual(result));
            }
        }

        [TestMethod]
        public void SerializeAndDeserialaizeArrayOfObjects()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                
                var arr = new[] { new SampleClass{simpleMember = 1}, new SampleClass{simpleMember  =2} ,new SampleClass {simpleMember = 3} };
                Serializer.Serialize(arr, stream);
                stream.Seek(0, SeekOrigin.Begin);
                var result = Serializer.Deserialize<SampleClass[]>(stream);
                var expected = arr.Select(o => o.simpleMember);
                Assert.IsTrue(expected.SequenceEqual(result.Select(o=>o.simpleMember)));
            }
        }
    }

    public class AnotherClass
    {
        public string Name;
        public SampleClass Reference { get; set; }
    }
}
