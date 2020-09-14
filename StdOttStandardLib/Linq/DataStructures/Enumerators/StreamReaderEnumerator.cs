using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StdOttStandard.Linq.DataStructures.Enumerators
{
    class StreamReaderEnumerator : IEnumerator<string>
    {
        private readonly StreamReader reader;

        public bool EndOfStream => reader.EndOfStream;

        public string Current { get; private set; }

        object IEnumerator.Current => Current;

        public StreamReaderEnumerator(StreamReader reader)
        {
            this.reader = reader;
        }

        public bool MoveNext()
        {
            return ReadLine() != null;
        }

        public string ReadLine()
        {
            return Current = reader.ReadLine();
        }

        public async Task<bool> MoveNextAsync()
        {
            return (await ReadLineAsync()) != null;
        }

        public async Task<string> ReadLineAsync()
        {
            return Current = await reader.ReadLineAsync();
        }

        public void Reset()
        {
            throw new InvalidOperationException();
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
