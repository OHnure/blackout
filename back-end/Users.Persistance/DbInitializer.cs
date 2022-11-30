using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain;

namespace Users.Persistance
{
    public class DbInitializer
    {
        public static void Initialize(Sql8580971Context context)
        {
            context.Database.EnsureCreated();
        }
    }
}
