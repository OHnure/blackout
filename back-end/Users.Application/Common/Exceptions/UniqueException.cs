using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Common.Exceptions
{
    internal class UniqueException : Exception
    {
        public UniqueException()
            : base($"User with this mail address already exists")
        {
        }
    }
}
