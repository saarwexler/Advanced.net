using System;
using System.Collections.Generic;

namespace SerializeExercise
{


    public class ObjectInfo : ICreationInfo
    {
        public ObjectInfo()
        {
            Fields = new Dictionary<string, ICreationInfo>();
        }

        public Type ObjectType { get; set; }
        public IDictionary<string, ICreationInfo> Fields { get; set; }


        public object GetInstance(IObjectCreator objectCreator)
        {
            return objectCreator.CreateObject(this);
        }
    }
}