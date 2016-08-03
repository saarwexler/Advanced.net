namespace SerializeExercise
{
    public interface ICreationInfo
    {
        object GetInstance(IObjectCreator objectCreator);
    }
}