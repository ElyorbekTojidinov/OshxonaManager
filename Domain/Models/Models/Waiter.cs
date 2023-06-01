using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.Models
{
    [Table("waiter")]
    public class Waiter
    {
        [Column("waiter_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int waiterId { get; set; }

        [Column("waiter_name")]
        [StringLength(50)]
        public required string WaiterName { get; set; }

        [Column("waiter_phone")]
        [StringLength(20)]
        public string? WaiterPhone { get; set; }

        [JsonIgnore]
        public List<Orders> Orders { get; set; }
    }
}
