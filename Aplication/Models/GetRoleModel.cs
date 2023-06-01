using Domain.Models.ModelsJwt;
using System.Text.Json.Serialization;

namespace Domain.Models.JwtNotCreateDb
{
    public class GetRoleModel
    {
        [JsonPropertyName("role_id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public Permission[] Permission { get; set; }
    }
}
