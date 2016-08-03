using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SerializeExercise.Readers
{
    public class ObjectInfoReader:IReader
    {


        public ICreationInfo Read(BinaryReader br, Type type, InfoReader infoReader)
        {
            var prefix = br.ReadString();
            if (prefix == "!R")
                return new ReferenceInfo(br.ReadInt32());
            return Read(prefix,br, infoReader);
        }

        private ObjectInfo Read(string prefix, BinaryReader br, InfoReader infoReader)
        {
            Type type = Type.GetType(prefix, true);
            var info = new ObjectInfo
            {
                ObjectType = type,
                Fields = ReadFields(type,br, infoReader),
            };
            return info;
        }
        private IDictionary<string, ICreationInfo> ReadFields(Type type, BinaryReader br, InfoReader infoReader)
        {
            var dic = new Dictionary<string, ICreationInfo>();
            for (string fieldName = br.ReadString(); fieldName != "!!!END!!!"; fieldName = br.ReadString())
                dic[fieldName] = ReadField(type, br, fieldName, infoReader);

            return dic;
        }

        private ICreationInfo ReadField(Type type, BinaryReader br, string fieldName, InfoReader infoReader)
        {
            BindingFlags allInstanceFields = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            var fieldInfo = type.GetField(fieldName, allInstanceFields);
            return Read(fieldInfo, br, infoReader);
        }

        private ICreationInfo Read(FieldInfo fieldInfo, BinaryReader br, InfoReader infoReader)
        {
            var type = fieldInfo.FieldType;
            return infoReader.Read(br, type);
        }

       
    }
}
