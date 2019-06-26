#if NET40
using System;
using System.IO;

namespace MathNet.Numerics.TestData
{
    public class TempFile : IDisposable
    {

        private bool disposedValue = false;

        public string FileName { get; private set; }

        public TempFile(string name)
        {
            var stream = Data.ReadStream(name);
            FileName = Path.GetTempFileName();

            using (FileStream fs = File.OpenWrite(FileName))
            {
                stream.CopyTo(fs);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if(FileName != null) File.Delete(FileName);
                    FileName = null;
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}

#endif
