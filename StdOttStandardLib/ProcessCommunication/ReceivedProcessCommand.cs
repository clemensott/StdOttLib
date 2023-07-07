namespace StdOttStandard.ProcessCommunication
{
    public class ReceivedProcessCommand
    {
        public string Name { get; }

        public string Data { get; }

        public ReceivedProcessCommand(string name, string data)
        {
            Name = name;
            Data = data;
        }

        public T DeserializeData<T>()
        {
            return StdUtils.XmlDeserializeText<T>(Data);
        }
    }
}
