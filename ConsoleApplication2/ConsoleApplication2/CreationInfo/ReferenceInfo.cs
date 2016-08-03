using System;
using System.Collections;
using System.Collections.Generic;

namespace SerializeExercise
{
    public class ReferenceInfo : ICreationInfo
    {
        public ReferenceInfo(int index)
        {
            RefKey = index;
        }

        public int RefKey { get; private  set; }

        public object GetInstance(IObjectCreator objectCreator)
        {
            return objectCreator.GetObjectReference(this);
        }

    }
}