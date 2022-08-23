using System.Collections.Generic;

namespace monitoringMVC.Models
{
    public class Disk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FreeSizeDisk { get; set; }
        public string SizeDisk { get; set; }
    }
}