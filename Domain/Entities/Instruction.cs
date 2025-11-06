namespace Domain.Entities
{
    public class Instruction : Entity
    {
        public int IdRecipe { get; private set; }
        public string Content { get; private set; } = string.Empty;
        public int StepNumber { get; private set; }
        public Recipe Recipe { get; private set; }

        private Instruction() { }

        public Instruction(int idRecipe, string content, int stepNumber, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(idRecipe, content, stepNumber);

            IdRecipe = idRecipe;
            Content = content.Trim();
            StepNumber = stepNumber;
        }

        public Instruction(int id, int idRecipe, string content, int stepNumber, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");
            Validate(idRecipe, content, stepNumber);

            Id = id;
            IdRecipe = idRecipe;
            Content = content.Trim();
            StepNumber = stepNumber;
        }

        public void UpdateContent(string newContent, string modifiedBy)
        {
            ValidateContent(newContent);
            ValidateModifiedBy(modifiedBy);

            Content = newContent;
            MarkAsModified(modifiedBy);
        }

        public void UpdateStepNumber(int newStepNumber, string modifiedBy)
        {
            ValidateStepNumber(newStepNumber);
            ValidateModifiedBy(modifiedBy);

            StepNumber = newStepNumber;
            MarkAsModified(modifiedBy);
        }

        public void UpdateInstruction(string newContent, int newStepNumber, string modifiedBy)
        {
            ValidateContent(newContent);
            ValidateStepNumber(newStepNumber);
            ValidateModifiedBy(modifiedBy);

            Content = newContent;
            StepNumber = newStepNumber;
            MarkAsModified(modifiedBy);
        }

        private static void Validate(int idRecipe, string content, int stepNumber)
        {
            ValidateIdRecipe(idRecipe);
            ValidateContent(content);
            ValidateStepNumber(stepNumber);
        }

        private static void ValidateIdRecipe(int idRecipe)
        {
            ValidateDomain(idRecipe < 1, "Id da receita inválido. Id deve ser um número inteiro e positivo.");
        }

        private static void ValidateContent(string content)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(content), "O conteúdo da instrução é obrigatório.");
            ValidateDomain(content.Trim().Length < 8, "A instrução deve conter pelo menos 8 caracteres.");
            ValidateDomain(content.Trim().Length > 500, "A instrução não pode ultrapassar 500 caracteres.");
        }

        private static void ValidateStepNumber(int stepNumber)
        {
            ValidateDomain(stepNumber < 1, "O número do passo deve ser pelo menos 1.");
            ValidateDomain(stepNumber > 100, "O número do passo não pode ser maior que 100.");
        }

        private static void ValidateModifiedBy(string modifiedBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(modifiedBy), "Para atualizar a instrução é necessário fornecer o nome do usuário que está modificando.");
            ValidateDomain(modifiedBy.Trim().Length < 3, "O nome de usuário para modificação deve ter pelo menos 3 caracteres.");
        }
    }
}