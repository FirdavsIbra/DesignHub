namespace Domain.Models
{
    public interface ISketch
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FilePath { get; set; }
    }
}
