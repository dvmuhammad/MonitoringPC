namespace Client.Model
{
    public class RamDto
    {
        public RamDto(string totalRam, string freeRam)
        {
            TotalRam = totalRam;
            FreeRam = freeRam;
        }

        public string TotalRam { get; set; }
        public string FreeRam { get; set; }
    }
}