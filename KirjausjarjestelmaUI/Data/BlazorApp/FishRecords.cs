using System;

namespace KirjausjarjestelmaUI.Data.BlazorApp
{
    public class FishRecords
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public DateTime Created { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string UserName { get; set; }
    }
}
