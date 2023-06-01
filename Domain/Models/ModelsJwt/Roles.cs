using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.ModelsJwt
{
    [Table("roles")]
    public class Roles
    {
        [Column("role_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("role_id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

       
        [JsonIgnore]
        public virtual ICollection<Users>? Users { get; set; }

        [JsonPropertyName("Permission_names")]
        [NotMapped]
        public int[]? Permissions { get; set; }

        [JsonIgnore]
        public List<Permission> Permission { get; set; }
    }

}
