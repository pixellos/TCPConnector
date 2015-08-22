using System.IO;

namespace Common_Files
{
    public class Comunicator
    {
        Stream newStream;
        public BinaryReader BinaryReader { get; private set; }
        public BinaryWriter BinaryWriter { get; private set; }
        public Comunicator(Stream IOStream)
        {
            ChangeParametrs(IOStream);
        }

        public void ChangeParametrs(Stream IOStream)
        {
            newStream = IOStream;
            BinaryWriter = new BinaryWriter(newStream);
            BinaryReader = new BinaryReader(newStream);
        }
    }
}
