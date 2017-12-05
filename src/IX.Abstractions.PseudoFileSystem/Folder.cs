using System;
using System.Collections.Generic;
using System.Text;

namespace IX.Abstractions.PseudoFileSystem
{
    internal class Folder : FileSystemContainerBase
    {
        internal Folder(string pathSeparator)
            : base(pathSeparator)
        { }

        public override IFileSystemContainer Parent => throw new NotImplementedException();

        public override IFileSystemContainer Navigate(string path)
        {
            throw new NotImplementedException();
        }
    }
}
