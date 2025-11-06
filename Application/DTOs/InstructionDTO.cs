namespace Application.DTOs
{
    public class InstructionDTO
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int StepNumber { get; set; }
    }
}