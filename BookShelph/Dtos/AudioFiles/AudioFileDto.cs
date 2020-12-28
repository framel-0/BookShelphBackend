namespace BookShelph.Dtos.AudioFiles
{
    public class AudioFileDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal FileSize { get; set; }
        public string File { get; set; }
    }
}
