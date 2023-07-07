using System;

namespace StdOttStandard.ProcessCommunication
{
    public struct ProcessCommandInfo
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public ProcessCommandInfo(string name, string data) : this()
        {
            ID = Guid.NewGuid().ToString();
            Name = name;
            Data = data;
        }
    }
}
