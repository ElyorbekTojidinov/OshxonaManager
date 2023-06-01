namespace Aplication.DTOs.Roles
{
    public class RoleUpdateDTO : RoleBaseDTO
    {
        public int Id { get; set; }
        public int[]? Permissions { get; set; }
    }
}
