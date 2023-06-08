using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ирида
{
    partial class Reservations
    {
        public override string ToString()
        {
            return Clients.Name + " " + Clients.LastName;
        }
    }
}
