using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using SerializeExercise.Readers;

namespace SerializeExercise
{
    public class InfoReader 
    {
        private ReaderFactory _readerFactory = new ReaderFactory();

        public ICreationInfo Read<T>(BinaryReader br)
        {
            var type = typeof (T);
           return  Read(br, type);
        }

      


        public ICreationInfo Read(BinaryReader br, Type type)
        {
            var reader = _readerFactory.GetReader(type);
            return reader.Read(br, type, this);
            
        }
      
       
    }
}