using System.IO;

namespace SerializeExercise
{
    public class ObjectReader
    {
        private readonly BinaryReader _br;

        public ObjectReader(BinaryReader br)
        {
            _br = br;
        }

        public T Read<T>()
        {
            ICreationInfo info = ReadInfo<T>();
            return (T)info.GetInstance(new ObjectCreator());
        }

       

        private ICreationInfo ReadInfo<T>()
        {
            var type = typeof (T);
            InfoReader infoReader = new InfoReader();
            return infoReader.Read<T>(_br);

        }
    }
}