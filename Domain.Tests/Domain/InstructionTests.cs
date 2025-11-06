using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Domain.Tests.Entities
{
    public class InstructionTests
    {
        private readonly int _validIdRecipe = 1;
        private readonly string _validContent = "Misture todos os ingredientes secos em uma tigela grande.";
        private readonly int _validStepNumber = 1;
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "chef_john";
        private readonly string _validLastModifiedBy = "chef_john";

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateInstruction()
        {
            // Act
            var instruction = new Instruction(
                _validIdRecipe, _validContent, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            instruction.IdRecipe.Should().Be(_validIdRecipe);
            instruction.Content.Should().Be(_validContent);
            instruction.StepNumber.Should().Be(_validStepNumber);
            instruction.CreatedAt.Should().Be(_validCreatedAt);
            instruction.UpdatedAt.Should().Be(_validUpdatedAt);
            instruction.CreatedBy.Should().Be(_validCreatedBy);
            instruction.LastModifiedBy.Should().Be(_validLastModifiedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateInstructionWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var instruction = new Instruction(
                id, _validIdRecipe, _validContent, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            instruction.Id.Should().Be(id);
            instruction.IdRecipe.Should().Be(_validIdRecipe);
            instruction.Content.Should().Be(_validContent);
            instruction.StepNumber.Should().Be(_validStepNumber);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidIdRecipe_ShouldThrowDomainException(int invalidIdRecipe)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Instruction(
                    invalidIdRecipe, _validContent, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidContent_ShouldThrowDomainException(string invalidContent)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Instruction(
                    _validIdRecipe, invalidContent, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithShortContent_ShouldThrowDomainException()
        {
            // Arrange
            var shortContent = "Misture";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Instruction(
                    _validIdRecipe, shortContent, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithLongContent_ShouldThrowDomainException()
        {
            // Arrange
            var longContent = new string('A', 501);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Instruction(
                    _validIdRecipe, longContent, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidStepNumber_ShouldThrowDomainException(int invalidStepNumber)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Instruction(
                    _validIdRecipe, _validContent, invalidStepNumber, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithHighStepNumber_ShouldThrowDomainException()
        {
            // Arrange
            var highStepNumber = 101;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Instruction(
                    _validIdRecipe, _validContent, highStepNumber, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void UpdateContent_WithValidContent_ShouldUpdateContent()
        {
            // Arrange
            var instruction = CreateValidInstruction();
            var newContent = "Misture os ingredientes secos e depois adicione os líquidos.";
            var modifiedBy = "chef_maria";
            var originalUpdatedAt = instruction.UpdatedAt;

            // Act
            instruction.UpdateContent(newContent, modifiedBy);

            // Assert
            instruction.Content.Should().Be(newContent);
            instruction.LastModifiedBy.Should().Be(modifiedBy);
            instruction.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Fact]
        public void UpdateContent_WithInvalidContent_ShouldThrowDomainException()
        {
            // Arrange
            var instruction = CreateValidInstruction();
            var invalidContent = "Misture";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                instruction.UpdateContent(invalidContent, "chef_maria")
            );
        }

        [Fact]
        public void UpdateStepNumber_WithValidStepNumber_ShouldUpdateStepNumber()
        {
            // Arrange
            var instruction = CreateValidInstruction();
            var newStepNumber = 2;
            var modifiedBy = "chef_maria";
            var originalUpdatedAt = instruction.UpdatedAt;

            // Act
            instruction.UpdateStepNumber(newStepNumber, modifiedBy);

            // Assert
            instruction.StepNumber.Should().Be(newStepNumber);
            instruction.LastModifiedBy.Should().Be(modifiedBy);
            instruction.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Fact]
        public void UpdateStepNumber_WithInvalidStepNumber_ShouldThrowDomainException()
        {
            // Arrange
            var instruction = CreateValidInstruction();
            var invalidStepNumber = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                instruction.UpdateStepNumber(invalidStepNumber, "chef_maria")
            );
        }

        [Fact]
        public void UpdateInstruction_WithValidParameters_ShouldUpdateBothContentAndStepNumber()
        {
            // Arrange
            var instruction = CreateValidInstruction();
            var newContent = "Pré-aqueça o forno a 180 graus Celsius por 15 minutos.";
            var newStepNumber = 3;
            var modifiedBy = "chef_maria";
            var originalUpdatedAt = instruction.UpdatedAt;

            // Act
            instruction.UpdateInstruction(newContent, newStepNumber, modifiedBy);

            // Assert
            instruction.Content.Should().Be(newContent);
            instruction.StepNumber.Should().Be(newStepNumber);
            instruction.LastModifiedBy.Should().Be(modifiedBy);
            instruction.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Fact]
        public void UpdateContent_WithInvalidModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var instruction = CreateValidInstruction();
            var invalidModifiedBy = "ab"; // Menos de 3 caracteres

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                instruction.UpdateContent("Novo conteúdo válido", invalidModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithSpacesInContent_ShouldTrimContent()
        {
            // Arrange
            var contentWithSpaces = "  Misture os ingredientes em uma tigela.  ";

            // Act
            var instruction = new Instruction(
                _validIdRecipe, contentWithSpaces, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            instruction.Content.Should().Be("Misture os ingredientes em uma tigela.");
        }

        [Fact]
        public void Constructor_WithValidBoundaryValues_ShouldCreateInstruction()
        {
            // Arrange
            var minContent = "Misture todos os ingredientes necessários para o preparo da receita.";
            var maxStepNumber = 100;

            // Act
            var instruction = new Instruction(
                _validIdRecipe, minContent, maxStepNumber, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            instruction.Content.Should().Be(minContent);
            instruction.StepNumber.Should().Be(maxStepNumber);
        }

        [Fact]
        public void Constructor_WithInvalidId_ShouldThrowDomainException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Instruction(
                    invalidId, _validIdRecipe, _validContent, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithInvalidAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var invalidCreatedBy = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Instruction(
                    _validIdRecipe, _validContent, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                    invalidCreatedBy, _validLastModifiedBy
                )
            );
        }

        private Instruction CreateValidInstruction()
        {
            return new Instruction(
                _validIdRecipe, _validContent, _validStepNumber, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );
        }
    }
}