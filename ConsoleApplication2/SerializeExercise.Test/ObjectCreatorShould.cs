using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializeExercise.Readers;

namespace SerializeExercise.Test
{
    [TestClass]
    public class ObjectCreatorShould
    {
        private readonly int[] _simpleIntArray = {1, 2, 3, 4, 5};
        private ObjectCreator _target;

        private const int Five = 5;

        [TestInitialize]
        public void Init()
        {
            _target = new ObjectCreator();
        }

        [TestMethod]
        public void CreateInstanceOfSimpleObject()
        {
            ObjectCreator target = new ObjectCreator();
            ObjectInfo info = CreateSimpleInfo();
            var result = target.CreateObject(info);
            Assert.IsInstanceOfType(result, typeof (SampleClass));

        }

        [TestMethod]
        public void SetPulicIntFieldValue()
        {
            ObjectCreator target = new ObjectCreator();
            ObjectInfo info = CreateSimpleInfo();
            var result = (SampleClass) target.CreateObject(info);
            Assert.AreEqual(Five, result.simpleMember);
        }

        [TestMethod]
        public void SetRefField()
        {
            ObjectCreator target = new ObjectCreator();
            ObjectInfo info = CreateCrossRefInfo();
            var result = (SampleClass )target.CreateObject(info);
            Assert.AreSame(result.AnotherClassField, result.AnotherClassRef);
        }

        [TestMethod]
        public void CreateArrayOfInt()
        {
            ObjectCreator target = new ObjectCreator();
            ArrayInfo info = CreateIntArrayInfo();
            var result = (int[]) target.CreateArray(info);
            Assert.IsTrue(result.SequenceEqual(_simpleIntArray));
        }

        [TestMethod]
        public void CreateArrayOfSampleObjects()
        {
            ObjectCreator target = new ObjectCreator();
            var info = CreateSampleClassArrayInfo();
            var result = (SampleClass[])target.CreateArray(info);
            Assert.AreEqual(2,result.Length);
            Assert.AreEqual(Five, result[1].simpleMember);
        }

        [TestMethod]
        public void CreateArrayWithReferenceToSameObject()
        {
            ArrayInfo info = CreateSampleClassArrayInfoWithRefs();
            var result = (SampleClass[])_target.CreateArray(info);
            Assert.AreEqual(3, result.Length);
            Assert.AreSame(result[1],result[2]);
            Assert.AreNotSame(result[1],result[0]);
        }

        [TestMethod]
        public void GetObjectReference()
        {
            ObjectInfo objectInfo = CreateSimpleInfo();
            ReferenceInfo referenceInfo =  new ReferenceInfo(0);
            var expected = (SampleClass)_target.CreateObject(objectInfo);
            var result = _target.GetObjectReference(referenceInfo);
            Assert.AreSame(expected,result);

        }

        private ArrayInfo CreateSampleClassArrayInfoWithRefs()
        {
            ICreationInfo[] array = { CreateSimpleInfo(), CreateSimpleInfo(),new  ReferenceInfo(1)};
            return new ArrayInfo
            {
                ElementType = typeof(SampleClass),
                Array = array
            };

            
        }

        private ArrayInfo CreateSampleClassArrayInfo()
        {
            ICreationInfo[] arr = {CreateSimpleInfo(), CreateSimpleInfo()};
            var info =  new ArrayInfo
            {
                ElementType = typeof(SampleClass),
                Array = arr
            };
          
            return info;
        }

        private ArrayInfo CreateIntArrayInfo()
        {
            var arr = _simpleIntArray.Select(o => (ICreationInfo) new SimpleTypeInfo(o)). ToArray();
            var info =  new ArrayInfo
            {
                ElementType = typeof (int),
                Array = arr
            };
            return info;
        }


        private ObjectInfo CreateCrossRefInfo()
        {
            var simpleInfo = CreateSimpleInfo();

            simpleInfo.Fields["AnotherClassField"] = new ObjectInfo
            {
                ObjectType = typeof (AnotherClass),
            };
            simpleInfo.Fields["AnotherClassRef"] = new ReferenceInfo(1);

            return simpleInfo;
        }

        private ObjectInfo CreateSimpleInfo()
        {
            var info = new ObjectInfo()
            {
                ObjectType = typeof (SampleClass),
            };
            info.Fields["simpleMember"] = new SimpleTypeInfo(Five);
            return info;
        }


    }
}
