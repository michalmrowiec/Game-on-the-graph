using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konkurs
{
    public class ComunicationService
    {
        private readonly Stream _stream;
        public ComunicationService(Stream str)
        {
            _stream = str;
        }

        public string GetData()
        {
            byte[] by = new byte[10000];
            _stream.Read(by, 0, 10000);

            return ASCIIEncoding.ASCII.GetString(by);
        }

        public void SendData(string message)
        {
            ASCIIEncoding asenE = new();
            byte[] by = asenE.GetBytes(message);

            _stream.Write(by, 0, by.Length);
        }

    }
}
