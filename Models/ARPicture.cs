namespace MeetUpAR.Models
{
    public class ARPicture
    {
        public int Id { get; set; }
        public string ImageTitle { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}
