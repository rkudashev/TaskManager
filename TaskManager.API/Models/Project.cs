namespace TaskManager.API.Models
{
    public class Project : CommonObject
    {
        public int Id { get; set; }
        public List<User> AllUsers { get; set; }
        public List<Desk> AllDesks { get; set; }
    }
}
