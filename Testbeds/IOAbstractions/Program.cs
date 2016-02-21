using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOAbstractions
{
    class Program
    {
        static void Main(string[] args)
        {
            System.IO.Abstractions.IFileSystem fileSystem = new System.IO.Abstractions.FileSystem();
        }
    }
}
