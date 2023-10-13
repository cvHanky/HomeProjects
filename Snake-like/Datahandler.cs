using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_like
{
    
    public class Datahandler
    {
        public string DataFileName { get; set; }
        public Datahandler(string dataFileName)
        {
            DataFileName = dataFileName;
        }
    }
}
