using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vktest.Context.Settings
{
    public class DbSettings
    {
        public DbType Type { get; private set; }
        public string ConnectionString { get; private set; }
    }
}
