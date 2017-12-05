using IX.System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace IX.Abstractions.PseudoFileSystem
{
    public sealed class FileShim : IFile
    {
        internal FileShim()
        { }

        public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public void AppendAllText(string path, string contents, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public StreamWriter AppendText(string path)
        {
            throw new NotImplementedException();
        }

        public void Copy(string sourceFileName, string destFileName, bool overwrite = false)
        {
            throw new NotImplementedException();
        }

        public Stream Create(string path, int bufferSize = 4096)
        {
            throw new NotImplementedException();
        }

        public StreamWriter CreateText(string path)
        {
            throw new NotImplementedException();
        }

        public void Delete(string path)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetCreationTime(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastAccessTime(string path)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastWriteTime(string path)
        {
            throw new NotImplementedException();
        }

        public void Move(string sourceFileName, string destFileName)
        {
            throw new NotImplementedException();
        }

        public Stream OpenRead(string path)
        {
            throw new NotImplementedException();
        }

        public StreamReader OpenText(string path)
        {
            throw new NotImplementedException();
        }

        public Stream OpenWrite(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadAllBytes(string path)
        {
            throw new NotImplementedException();
        }

        public string[] ReadAllLines(string path, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public string ReadAllText(string path, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ReadLines(string path, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public void SetCreationTime(string path, DateTime creationTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            throw new NotImplementedException();
        }

        public void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            throw new NotImplementedException();
        }

        public void WriteAllBytes(string path, byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }

        public void WriteAllText(string path, string contents, Encoding encoding = null)
        {
            throw new NotImplementedException();
        }
    }
}
