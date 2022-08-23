namespace Client.Model
{
    public class DiskDto
    {
        public DiskDto(string name, string freeSizeDisk, string sizeDisk)
        {
            Name = name;
            FreeSizeDisk = freeSizeDisk;
            SizeDisk = sizeDisk;

        }

        public string Name { get; set; }
        public string FreeSizeDisk { get; set; }
        public string SizeDisk { get; set; }
    }
}