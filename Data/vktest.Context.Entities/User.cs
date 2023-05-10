
using vktest.Common.Enums;
using vktest.Context.Entities.Common;

namespace vktest.Context.Entities
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Created_Date { get; set; }
        public int? GroupId { get; set; }
        public virtual User_Group Group { get; set; }
        public int? StateId { get; set; }
        public virtual User_State State { get; set; }


    }
}
