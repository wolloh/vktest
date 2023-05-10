using vktest.Context.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vktest.Common.Enums;

namespace vktest.Context.Entities
{
    public  class User_Group:BaseEntity
    {
        public virtual ICollection<User> Users { get; set; }
        public string Description{ get; set; }
        public UserType Code { get; set; }
    }
}
