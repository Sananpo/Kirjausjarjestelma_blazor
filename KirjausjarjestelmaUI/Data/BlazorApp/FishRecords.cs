using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KirjausjarjestelmaUI.Data.BlazorApp
{
    public class FishRecords
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; }
        public float Weight { get; set; }
        public float Length { get; set; }
        public DateTime Created { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string UserName { get; set; }
    }
}
