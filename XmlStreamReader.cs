using System.IO;
using System.Xml;

namespace DiscoverDotnet
{
    public class XmlStreamReader : StreamReader
    {
        public XmlStreamReader(Stream stream)
            : base(stream)
        {
        }

        public override int Peek()
        {
            int ch = 0;
            while (ch != -1)
            {
                ch = base.Peek();
                if (XmlConvert.IsXmlChar((char)ch))
                {
                    return ch;
                }
                base.Read();
            }
            return ch;
        }

        public override int Read()
        {
            int ch = 0;
            while (ch != -1)
            {
                ch = base.Read();
                if (XmlConvert.IsXmlChar((char)ch))
                {
                    return ch;
                }
            }
            return ch;
        }

        public override int Read(char[] buffer, int index, int count)
        {
            int i;
            for (i = index; i < count; i++)
            {
                int c = Read();
                if (c == -1)
                {
                    break;
                }
                buffer[i] = (char)c;
            }
            return i - index;
        }
    }
}
