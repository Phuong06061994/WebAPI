using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebAPI.Entities
{
    public class User : IUserStore<AppUser, AppRole, AppDbContext, int, AppUserClaim, AppUserRole, AppUserLogin, AppUserToken, AppRoleClaim>
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
       
        [JsonIgnore]
        public string Password { get; set; }

        public string Token { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }


    }
}
