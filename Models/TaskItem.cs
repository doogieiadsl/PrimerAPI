namespace PrimerAPI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = default;
        public string IsCompleted { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
