using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using SerializeExercise.Readers;

namespace SerializeExercise
{
    public class ObjectCreator : IObjectCreator
    {
        private readonly List<object> _exisitingObjects = new List<object>();
        private const BindingFlags Binding = BindingFlags.NonPublic| BindingFlags.Public|BindingFlags.Instance;

       

        public object GetObjectReference(ReferenceInfo target)
        {
            return _exisitingObjects[target.RefKey];
        }

        public object CreateArray(ArrayInfo arrayInfo)
        {
            var elementType = arrayInfo.ElementType;
            var objArray = arrayInfo.Array.Cast<ICreationInfo>().Select(o => o.GetInstance(this)).ToArray();
          var targetArray =   Array.CreateInstance(elementType, arrayInfo.Array.Length);
            objArray.CopyTo(targetArray,0);
            return targetArray;
        }
         
        public object CreateObject(ObjectInfo info)
        {
            var instance =  Activator.CreateInstance(info.ObjectType);
            _exisitingObjects.Add(instance);
            foreach (var field in info.Fields)
            {
                var value = field.Value.GetInstance(this);
                info.ObjectType.GetField(field.Key,Binding).SetValue(instance, value);
            }
            return instance;
        }

       

       
    }
}