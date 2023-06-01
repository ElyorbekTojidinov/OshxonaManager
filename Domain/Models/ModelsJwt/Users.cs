using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.ModelsJwt
{

    [Table("users")]
    public class Users
    {
        [Column("user_id")]
        [JsonPropertyName("user_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsersId { get; set; }

        [Column("username")]
        public string UserName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [JsonIgnore]
        public virtual ICollection<Roles>? Role { get; set; }

        [NotMapped]
        public int[] Roles { get; set; }

    }
}
