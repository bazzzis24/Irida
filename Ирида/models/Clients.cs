using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ирида
{
    partial class Clients
    {
        public override string ToString()
        {
            return LastName + " " + Name + " " + Pasport;
        }
    }
}
