namespace Application.DTOs
{
    public class InstructionDTO
    {
        public int IdInstruction { get; set; }
        public string Content { get; set; } = string.Empty;
        public int StepNumber { get; set; }
    }
}