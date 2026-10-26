using System.ComponentModel.DataAnnotations;

namespace StoreMobileV2.Models
{
    public class UserModel
    {
        [Key]
        public int ID_user { get; set; } = 0;
        public string? email { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string ID_role { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
