using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem_CSharp
{
    class Folder
    {
        public string Name { get; set; }
        public string DateCreated { get; set; }

        public Folder() { }

        public Folder(string Name, string DateCreated)
        {
            this.Name = Name;
            this.DateCreated = DateCreated;
        }
    }
}