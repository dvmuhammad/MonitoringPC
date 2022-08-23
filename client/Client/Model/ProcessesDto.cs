namespace Client.Model
{
    public class ProcessesDto
    {
        public ProcessesDto(string processes, string cpu)
        {
            Processes = processes;
            Cpu = cpu;
        }

        public string Processes { get; set; }
        public string Cpu { get; set; }
    }
}