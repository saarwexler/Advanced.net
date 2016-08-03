using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializeExercise.Readers;
using SerializeExercise;

namespace SerializeExercise.Test.Readers
{
    [TestClass]
    public class ReaderFactoryShould
    {
        private ReaderFactory _target;

        [TestInitialize]
        public void Init()
        {
            _target = new ReaderFactory();
        }

        [TestMethod]
        public void ReturnSimpleTypeReaderOnInt()
        {
            var result = _target.GetReader(typeof (int));
            Assert.IsInstanceOfType(result, typeof(SimpleTypeReader));
        }

        [TestMethod]
        public void ReturnSimpleTypeReaderOnstring()
        {
            var result = _target.GetReader(typeof(string));
            Assert.IsInstanceOfType(result, typeof(SimpleTypeReader));
        }

        [TestMethod]
        public void ReturnArrayTypeReaderOnArray()
        {
            var result = _target.GetReader(typeof(int[]));
            Assert.IsInstanceOfType(result, typeof(ArrayTypeReader));
        }

        [TestMethod]
        public void ReturnObjectReaderOnEvrerythingElse()
        {
            var result = _target.GetReader(typeof(SampleClass));
            Assert.IsInstanceOfType(result, typeof(ObjectInfoReader));
        }
    }

    
}
