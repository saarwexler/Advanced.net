using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace SerializeExercise
{
    public class ObjectWriter : IDisposable
    {
        private readonly BinaryWriter _bw;
        private readonly Dictionary<object, int> _graph;
        private int _key;

        public ObjectWriter(BinaryWriter bw)
        {
            _bw = bw;
            _graph = new Dictionary<object, int>(new ReferenceComperer());
            _key = 0;
        }

        public void Write(object value)
        {
            int oldKey;
            var fieldType = value.GetType();
            if (fieldType.IsPrimitive || fieldType == typeof (string))
                _bw.Write((dynamic) value);
            else if (fieldType.IsArray)
                WriteArray((Array) value);
            else if (IsAlreadyWriten(value, out oldKey))
                WriteReference(oldKey);
            else
                WriteNewObj(value);
        }




        private bool IsAlreadyWriten(object o, out int oldKey)
            {
                return _graph.TryGetValue(o, out oldKey);
            }

            private void WriteReference(int oldKey)
            {
                _bw.Write("!R");
                _bw.Write(oldKey);
            }
       
            private void WriteNewObj<T>(T o)
            {
                AddObjectToGraph(o);
                var type = o.GetType();
                WriteQualifiedName(type);
                WriteFields(o,type);
                _bw.Write(Serializer.EndOfSection);
            }

                private void AddObjectToGraph(object o)
                {
                    _graph.Add(o, _key++);
                }

                private void WriteQualifiedName(Type type)
                {
                    _bw.Write(type.AssemblyQualifiedName);
                }

                private void WriteFields(object o, Type type)
                {
                    var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                    WriteFields(o, fields);
                }

                    private void WriteFields(object o, FieldInfo[] fields)
                    {
                        foreach (var field in fields)
                            WriteField(o, field);
                    }

                        private void WriteField(object o, FieldInfo field)
                        {
                            var value = field.GetValue(o);
                            if (value == null) return; 
                             _bw.Write(field.Name);
                            Write(value);
                        }

                       

                            private void WriteArray(Array arr)
                            {
                                var arrayType = arr.GetType().GetElementType();
                               _bw.Write(arr.Length);

                                Action<object> write;
                                    if(arrayType.IsPrimitive || arrayType == typeof (string))
                                write =  x => _bw.Write((dynamic) x);
                                    else
                                        write = Write;
                                    foreach (var value  in arr)
                                       write(value);
                            }

        public void Dispose()
        {
            _bw.Dispose();
        }

        private class ReferenceComperer : IEqualityComparer<object>
        {
            bool IEqualityComparer<object>.Equals(object x, object y)
            {
                return ReferenceEquals(x, y);
            }

            public int GetHashCode(object obj)
            {
               return obj.GetHashCode();
            }
        }

        
    }

    
}