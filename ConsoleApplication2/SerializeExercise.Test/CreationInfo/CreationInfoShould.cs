using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SerializeExercise.Test.CreationInfo
{
    [TestClass]
    public class CreationInfoShould
    {

        [TestMethod]
        public void CreateNewInstance()
        {
            Mock<IObjectCreator> creatorMock = new Mock<IObjectCreator>();
            var target = new ObjectInfo {ObjectType = typeof(SampleClass)};
            SampleClass sampleObject= new SampleClass();
            creatorMock.Setup(o => o.CreateObject(target)).Returns(sampleObject);
            var result =  target.GetInstance(creatorMock.Object);
            Assert.AreSame(sampleObject, result);
        }

        [TestMethod]
        public void CreateNewRefernce()
        {
            Mock<IObjectCreator> creatorMock = new Mock<IObjectCreator>();
            var target = new ReferenceInfo(5);
            SampleClass sampleObject = new SampleClass();
            creatorMock.Setup(o => o.GetObjectReference(target)).Returns(sampleObject);
            var result = target.GetInstance(creatorMock.Object);
            Assert.IsInstanceOfType(result, typeof(SampleClass));
        }
    }
}
