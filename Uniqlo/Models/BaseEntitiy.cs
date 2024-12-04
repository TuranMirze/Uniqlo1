namespace Uniqlo.Models
{
    public class BaseEntitiy
    {
        public int Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
