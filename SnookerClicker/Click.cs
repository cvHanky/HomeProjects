using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnookerClicker
{
    public class Click
    {
        public string Purpose { get; set; }

        public Click(string purpose)
        {
            Purpose = purpose;
        }
    }
}
