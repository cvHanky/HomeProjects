using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnookerClicker
{
    public class Click
    {
        public Click? PreviousClick { get; set; }

        public Click(Click? previousClick)
        {
            PreviousClick = previousClick;
        }
        public Click() { }


        public void Pot()
        {

        }
    }
}
