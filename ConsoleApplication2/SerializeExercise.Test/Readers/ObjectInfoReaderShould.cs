using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializeExercise.Readers;

namespace SerializeExercise.Test.Readers
{
    [TestClass]
    public class ObjectInfoReaderShould
    {
        private ObjectInfoReader _target;

        [TestInitialize]
        public void Init()
        {
            _target = new ObjectInfoReader();
        }

        //[TestMethod]
        //public void ReadTheObjectType()
        //{

        //    BinaryReader br;
        //    BinaryWriter
        //    var result =  target.Read(br, type);
        //}
    }
}
