namespace User_Registartion.Entity
{
    public class User
    {
        public string UserId { get; set; } = Guid.NewGuid().ToString().Substring(6);
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? Password { get; set; }
        public string? Phone { get; set; }

        public ICollection<Organisation>? Organisations { get; set; }



    }
}
