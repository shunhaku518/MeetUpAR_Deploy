namespace MeetUpAR.Models.DataModels
{
    public class MeetUpSettings
    {
        public string BaseURL {  get; set; } = string.Empty;
        public string GoogleMapKey {  get; set; } = string.Empty;
        public string SiteName {  get; set; } = string.Empty;
        public string SiteLogo { get; set; } = string.Empty;
        public string MainPic {  get; set; } = string.Empty;
        public string GooglePinPic {  get; set; } = string.Empty;
        public string CopyLocationLink {  get; set; } = string.Empty;
        public bool IsUsedCurrentLocation {  get; set; }
        public double DefaultLatitude { get; set; } 
        public double DefaultLongitude { get; set; }
        public string LinkPreviewURL {  get; set; } = string.Empty;
        public string LinkPreviewImage {  get; set; } = string.Empty;
        public string LinkPreviewDescription {  get; set; } = string.Empty;

        public List<ARPicture> ARobjs { get; set; } = new List<ARPicture>();
    }
}
