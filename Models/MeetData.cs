namespace MeetUpAR.Models
{
    public class MeetData
    {
        public string GoogleMapLatitude { get; set; } = string.Empty;
        public string GoogleMapLongitude { get; set; } = string.Empty;

        public ARPicture? ARPicture { get; set; }
    }
}
