using SerializeExercise.Readers;

namespace SerializeExercise
{
    public interface IObjectCreator
    {
        object CreateObject(ObjectInfo target);
        object GetObjectReference(ReferenceInfo target);
        object CreateArray(ArrayInfo arrayInfo);
    }
}