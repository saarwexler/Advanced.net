using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerializeExercise.Test
{
    [TestClass]
    public class ObjectWriterShloud
    {
        private SampleClass _sampleClass;

        [TestInitialize]
        public void Init()
        {
            _sampleClass = new SampleClass { stringField = "mystr", AnotherClassField = new AnotherClass(), IntProp = 7};
        }

        [TestMethod]
        public void WriteFullyQualifiedName()
        {
          
            using (MemoryStream ms = new MemoryStream())
            using ( BinaryWriter bw = new BinaryWriter(ms))
             using( ObjectWriter target = new ObjectWriter(bw))
            {
               
               var o = new SampleClass();
               target.Write( o);
                ms.Seek(0, SeekOrigin.Begin);
                BinaryReader br = new BinaryReader(ms);
                var objectTypeStr = br.ReadString();
                var fullName =   typeof (SampleClass).AssemblyQualifiedName;
                Assert.AreEqual(fullName,objectTypeStr);
            }
        }

        [TestMethod]
        public void WriteMemberName()
        {
            Action<BinaryReader> verify = br =>
            {
                var objectTypeStr = br.ReadString();
                AssertFieldName(s =>s.simpleMember, br);
            };
            TestWriter(new SampleClass(), verify);
        }

       

        [TestMethod]
        public void WriteIntMemberValue()
        {
            const int five = 5;
            var o = new SampleClass { simpleMember = five };
            Action<BinaryReader> verify = br =>
            {
                var objectTypeStr = br.ReadString();
                var memberName = br.ReadString();
                var memberValue = br.ReadInt32();
                Assert.AreEqual(five, memberValue);
            };
            TestWriter(o,verify);
        }

        [TestMethod]
        public void WriteStringFieldValue()
        {
            string blabla = "blabla";
            var o = new SampleClass { stringField = blabla };
            Action<BinaryReader> asssert = br=>
            {
                var result = ReadSimpleObject(br);
                Assert.AreEqual(blabla, result.stringField);
            };
            TestWriter(o, asssert);
        }

        [TestMethod]
        public void WriteEndFieldSection()
        {
            var o = new SampleClass { stringField = "mystr" };
            TestWriter(o, GoToEndOfFieldsSection);
        }

        private static void GoToEndOfFieldsSection(BinaryReader br)
        {
            while (br.ReadString() != Serializer.EndOfSection){}
        }


        [TestMethod]
        public void WriteObjectField()
        {
            Action<BinaryReader> assert = br =>
            {
                ReadSimpleObject(br);
                var objFieldName = br.ReadString();
                Assert.AreEqual("AnotherClassField", objFieldName);

                var objFieldType = br.ReadString();
                Assert.AreEqual(typeof(AnotherClass).AssemblyQualifiedName, objFieldType);
            };
            TestWriter(_sampleClass,assert);
        }

        [TestMethod]
        public void WriteSimpleProperty()
        {
            var o = new SampleClass { stringField = "mystr", IntProp = 7 };
            Action<BinaryReader> assert = br =>
            {
                ReadSimpleObject(br);
                br.ReadString();
                var propResult = br.ReadInt32();
                Assert.AreEqual(7, propResult);
            };
            TestWriter(o,assert);
        }

        [TestMethod]
        public void WriteIntArray()
        {
            int[] expected = {1, 2, 3, 4, 5};
            var obj = new SampleClass {stringField = "mystr", arr = expected};
            Action<BinaryReader> assert = br =>
            {
                ReadSimpleObject(br);

               AssertFieldName<SampleClass>(x =>x.arr, br );
                var arrLength = br.ReadInt32();
                Assert.AreEqual(5, arrLength);

                int[] result= new int[5];
                for (int i = 0; i < 5; i++)
                    result[i] = br.ReadInt32();
                Assert.IsTrue(expected.SequenceEqual(result));
            };
            TestWriter(obj,assert);
            
        }

        [TestMethod]
        public void WriteStringArray()
        {
            string[] expected = { "1", "2", "3", "4", "5" };
            var obj = new SampleClass { stringField = "mystr", strArr = expected };
            Action<BinaryReader> assert = br =>
            {
                ReadSimpleObject(br);

                AssertFieldName<SampleClass>(x => x.strArr, br);
                var arrLength = br.ReadInt32();
                Assert.AreEqual(5, arrLength);

                string[] result = new string[5];
                for (int i = 0; i < 5; i++)
                    result[i] = br.ReadString();
                Assert.IsTrue(expected.SequenceEqual(result));
            };
            TestWriter(obj, assert);

        }

       

     

        [TestMethod]
        public void WriteCrossReferenceField()
        {
            const int five = 5;
            _sampleClass.AnotherClassRef = _sampleClass.AnotherClassField;
            Action<BinaryReader> assert = br =>
            {
                ReadSimpleObject(br);
                AssertFieldName(o=>o.AnotherClassField,br);

                var objFieldType = br.ReadString();
                Assert.AreEqual(typeof(AnotherClass).AssemblyQualifiedName, objFieldType);
                GoToEndOfFieldsSection(br);

                AssertFieldName(o=> o.AnotherClassRef,br);

                var referenceSign = br.ReadString();
                string crossRefStr = "!R";
                Assert.AreEqual(crossRefStr, referenceSign);

                var referenceIndex = br.ReadInt32();
                Assert.AreEqual(1, referenceIndex);
            };
            TestWriter(_sampleClass,assert);
        }

        [TestMethod]
        public void WriteCircularReferenceField()
        {
            const int five = 5;
            _sampleClass.AnotherClassField.Reference = _sampleClass;
            Action<BinaryReader> assert = br =>
            {
                ReadSimpleObject(br);
                AssertFieldName(o => o.AnotherClassField, br);

                var objFieldType = br.ReadString();
                Assert.AreEqual(typeof(AnotherClass).AssemblyQualifiedName, objFieldType);

                var referenceName = br.ReadString();
                Assert.AreEqual("<Reference>k__BackingField", referenceName);

                var referenceValue = br.ReadString();
                string circularRefStr = "!R";
                Assert.AreEqual(circularRefStr, referenceValue);

                var referenceKey = br.ReadInt32();
                Assert.AreEqual(0, referenceKey);
            };
        }

        private static void TestWriter(object toRight, Action<BinaryReader> assertMethod)
        {
             using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            using (ObjectWriter target = new ObjectWriter(bw))
            using (BinaryReader br = new BinaryReader(ms))
            {
                target.Write(toRight);
                ms.Seek(0, SeekOrigin.Begin);
                assertMethod(br);
            }
        }

        private static SampleClass ReadSimpleObject(BinaryReader br)
        {
            br.BaseStream.Seek(0, SeekOrigin.Begin);
            var objectTypeStr = br.ReadString();
            var memberName = br.ReadString();
            var memberValue = br.ReadInt32();
            var secmemberName = br.ReadString();
            var sevalue = br.ReadInt32();
            var thrdName = br.ReadString();
            var thrValue = br.ReadString();
            return new SampleClass
            {
                simpleMember = memberValue,
                seconedMember = sevalue,
                stringField = thrValue
            };
        }

        private void AssertFieldName(Expression<Func<SampleClass, dynamic>> field, BinaryReader br)
        {
            AssertFieldName<SampleClass>(field,br);
        }

        private void AssertFieldName<TIn>(Expression<Func<TIn, dynamic>> field, BinaryReader br)
        {
            var fieldName = GetFieldName(field);
            AssertString(fieldName, br);
        }

        private string GetFieldName<TIn>(Expression<Func<TIn, dynamic>> exp)
        {
            var unary  =exp.Body as UnaryExpression;
            MemberExpression memberExpression = exp.Body as MemberExpression ??(MemberExpression) (((UnaryExpression) exp.Body).Operand);
          
            return memberExpression.Member.Name;
        }

        private static void AssertString(string expected, BinaryReader br)
        {
            var fieldName = br.ReadString();
            Assert.AreEqual(expected, fieldName);
        }

      


       
    }
}
