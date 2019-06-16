using System.IO;

namespace Task3.BL
{
    public class AttachmentFile
    {
        public int ContentLength { get; set; }
        public string FileName { get; set; }
        public Stream InputStream { get; set; }
        public string ContentType { get; set; }
    }
}