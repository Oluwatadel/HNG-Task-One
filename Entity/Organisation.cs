namespace User_Registartion.Entity
{
    public class Organisation
    {
        public string? OrgId { get; set; } = Guid.NewGuid().ToString().Substring(6);
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}