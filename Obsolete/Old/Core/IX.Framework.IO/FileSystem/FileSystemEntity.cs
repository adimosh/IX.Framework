using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IX.Framework.IO.FileSystem
{
    [DataContract(Namespace= "http://framework.ixiancorp.com/")]
    public class FileSystemEntity
    {
        [DataMember]
        public string ResourceName { get; set; }

        [DataMember]
        public string ResourcePath { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public DateTime ModifiedDate { get; set; }

        [DataMember]
        public long Size { get; set; }

        [DataMember]
        public bool IsContainer { get; set; }
    }
}
