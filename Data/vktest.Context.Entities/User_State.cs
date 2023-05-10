
using vktest.Common.Enums;
using vktest.Context.Entities.Common;


namespace vktest.Context.Entities
{
    public class User_State : BaseEntity
    {
        public virtual ICollection<User> Users { get; set; }
        public string Description { get; set; }
        public UserState Code { get; set; }
    }
}
