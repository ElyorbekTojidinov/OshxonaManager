using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models.Models;

[Table("orders")]
public class Orders
{
    [Column("order_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    [Column("order_table")]
    public int OrderTable { get; set; }

    [Column("order_data")]
    public DateTime OrderDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [JsonIgnore]
    public List<Products> Products { get; set; }

    [JsonIgnore]
    public List<Waiter> Waiters { get; set;}
}
