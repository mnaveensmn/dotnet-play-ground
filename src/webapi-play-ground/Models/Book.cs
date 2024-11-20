namespace webapi_play_ground.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string BookType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}