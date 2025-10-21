using Domain.Entities;
using Domain.Validation;
using System;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class IngredientTests
    {
        private readonly string _validName = "Farinha de Trigo";
        private readonly string _validCreatedBy = "chef_john";

        #region Constructor Tests

        [Fact(DisplayName = "Deve criar ingrediente válido com Id especificado")]
        public void CreateIngredient_WithValidParametersAndId_ShouldSucceed()
        {
            // Arrange
            var id = 1;

            // Act
            var ingredient = new Ingredient(id, _validName, _validCreatedBy);

            // Assert
            ingredient.Id.Should().Be(id);
            ingredient.Name.Should().Be(_validName);
            ingredient.CreatedBy.Should().Be(_validCreatedBy);
            ingredient.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve criar ingrediente válido sem Id")]
        public void CreateIngredient_WithoutId_ShouldSucceed()
        {
            // Act
            var ingredient = new Ingredient(_validName, _validCreatedBy);

            // Assert
            ingredient.Id.Should().Be(0);
            ingredient.Name.Should().Be(_validName);
            ingredient.CreatedBy.Should().Be(_validCreatedBy);
        }

        [Theory(DisplayName = "Deve lançar exceção quando Id é inválido")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void CreateIngredient_WithInvalidId_ShouldThrowException(int invalidId)
        {
            // Act
            Action act = () => new Ingredient(invalidId, _validName, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Id inválido, Id deve ser um numero inteiro e positivo");
        }

        #endregion

        #region Name Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando nome é inválido")]
        [InlineData(null, "O nome do ingrediente é obrigatório.")]
        [InlineData("", "O nome do ingrediente é obrigatório.")]
        [InlineData("   ", "O nome do ingrediente é obrigatório.")]
        [InlineData("A", "O nome do ingrediente deve ter pelo menos 2 caracteres.")]
        public void CreateIngredient_WithInvalidName_ShouldThrowException(string invalidName, string expectedMessage)
        {
            // Act
            Action act = () => new Ingredient(invalidName, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Deve lançar exceção quando nome excede 100 caracteres")]
        public void CreateIngredient_WithLongName_ShouldThrowException()
        {
            // Arrange
            var longName = new string('A', 101);

            // Act
            Action act = () => new Ingredient(longName, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("O nome do ingrediente não pode ultrapassar 100 caracteres.");
        }

        [Theory(DisplayName = "Deve aceitar nomes válidos")]
        [InlineData("Sal")]
        [InlineData("Açúcar")]
        [InlineData("Farinha de Trigo Integral")]
        [InlineData("Leite Condensado")]
        [InlineData("Chocolate em Pó 50% Cacau")]
        public void CreateIngredient_WithValidName_ShouldSucceed(string validName)
        {
            // Act
            var ingredient = new Ingredient(validName, _validCreatedBy);

            // Assert
            ingredient.Name.Should().Be(validName);
        }

        [Fact(DisplayName = "Deve aceitar nome com exatamente 2 caracteres")]
        public void CreateIngredient_WithNameExactly2Characters_ShouldSucceed()
        {
            // Arrange
            var exactName = "Sal";

            // Act
            var ingredient = new Ingredient(exactName, _validCreatedBy);

            // Assert
            ingredient.Name.Should().Be(exactName);
        }

        [Fact(DisplayName = "Deve aceitar nome com exatamente 100 caracteres")]
        public void CreateIngredient_WithNameExactly100Characters_ShouldSucceed()
        {
            // Arrange
            var exactName = new string('A', 100);

            // Act
            var ingredient = new Ingredient(exactName, _validCreatedBy);

            // Assert
            ingredient.Name.Should().Be(exactName);
        }

        #endregion

        #region CreatedBy Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando createdBy é inválido")]
        [InlineData(null, "O nome de usuario é obrigatorio")]
        [InlineData("", "O nome de usuario é obrigatorio")]
        [InlineData("Jo", "O nome de usuario deve ter pelo menos 4 caracteres")]
        [InlineData("J", "O nome de usuario deve ter pelo menos 4 caracteres")]
        public void CreateIngredient_WithInvalidCreatedBy_ShouldThrowException(string invalidCreatedBy, string expectedMessage)
        {
            // Act
            Action act = () => new Ingredient(_validName, invalidCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Theory(DisplayName = "Deve aceitar createdBy válido")]
        [InlineData("john")]
        [InlineData("chef_maria")]
        [InlineData("user123")]
        [InlineData("cook")]
        public void CreateIngredient_WithValidCreatedBy_ShouldSucceed(string validCreatedBy)
        {
            // Act
            var ingredient = new Ingredient(_validName, validCreatedBy);

            // Assert
            ingredient.CreatedBy.Should().Be(validCreatedBy);
        }

        [Fact(DisplayName = "Deve aceitar createdBy com exatamente 4 caracteres")]
        public void CreateIngredient_WithCreatedByExactly4Characters_ShouldSucceed()
        {
            // Arrange
            var exactCreatedBy = "John";

            // Act
            var ingredient = new Ingredient(_validName, exactCreatedBy);

            // Assert
            ingredient.CreatedBy.Should().Be(exactCreatedBy);
        }

        #endregion

        #region UpdateName Tests

        [Fact(DisplayName = "Deve atualizar nome do ingrediente com dados válidos")]
        public void UpdateName_WithValidData_ShouldSucceed()
        {
            // Arrange
            var ingredient = new Ingredient(_validName, _validCreatedBy);
            var newName = "Farinha de Trigo Integral";
            var modifiedBy = "chef_updated";

            // Act
            ingredient.UpdateName(newName, modifiedBy);

            // Assert
            ingredient.Name.Should().Be(newName);
            ingredient.ModifiedBy.Should().Be(modifiedBy);
            ingredient.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory(DisplayName = "Deve lançar exceção ao atualizar com modifiedBy inválido")]
        [InlineData(null, "Para Poder atualizar o Ingrediente é necessario fornacer o nome do usuario que esta o modificando")]
        [InlineData("", "Para Poder atualizar o Ingrediente é necessario fornacer o nome do usuario que esta o modificando")]
        [InlineData("Jo", "O nome de usuario para modificação deve ter pelo menos 4 caracteres")]
        [InlineData("J", "O nome de usuario para modificação deve ter pelo menos 4 caracteres")]
        public void UpdateName_WithInvalidModifiedBy_ShouldThrowException(string invalidModifiedBy, string expectedMessage)
        {
            // Arrange
            var ingredient = new Ingredient(_validName, _validCreatedBy);

            // Act
            Action act = () => ingredient.UpdateName(_validName, invalidModifiedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Deve lançar exceção ao atualizar com nome inválido")]
        public void UpdateName_WithInvalidName_ShouldThrowException()
        {
            // Arrange
            var ingredient = new Ingredient(_validName, _validCreatedBy);
            var invalidName = "A";

            // Act
            Action act = () => ingredient.UpdateName(invalidName, "valid_user");

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("O nome do ingrediente deve ter pelo menos 2 caracteres.");
        }

        [Fact(DisplayName = "Deve lançar exceção ao atualizar com nome muito longo")]
        public void UpdateName_WithLongName_ShouldThrowException()
        {
            // Arrange
            var ingredient = new Ingredient(_validName, _validCreatedBy);
            var longName = new string('A', 101);

            // Act
            Action act = () => ingredient.UpdateName(longName, "valid_user");

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("O nome do ingrediente não pode ultrapassar 100 caracteres.");
        }

        [Fact(DisplayName = "Deve manter CreatedBy original após update")]
        public void UpdateName_ShouldNotChangeCreatedBy()
        {
            // Arrange
            var ingredient = new Ingredient(_validName, _validCreatedBy);

            // Act
            ingredient.UpdateName("Novo Nome do Ingrediente", "modifier");

            // Assert
            ingredient.CreatedBy.Should().Be(_validCreatedBy);
        }

        [Fact(DisplayName = "Deve atualizar múltiplas vezes corretamente")]
        public void UpdateName_MultipleTimes_ShouldSucceed()
        {
            // Arrange
            var ingredient = new Ingredient(_validName, _validCreatedBy);
            var firstNewName = "Farinha Integral";
            var secondNewName = "Farinha de Trigo Especial";
            var firstModifier = "chef_1";
            var secondModifier = "chef_2";

            // Act - Primeira atualização
            ingredient.UpdateName(firstNewName, firstModifier);
            var firstModifiedDate = ingredient.ModifiedDate;

            // Aguardar um pouco para garantir que o tempo mude
            System.Threading.Thread.Sleep(10);

            // Act - Segunda atualização
            ingredient.UpdateName(secondNewName, secondModifier);

            // Assert
            ingredient.Name.Should().Be(secondNewName);
            ingredient.ModifiedBy.Should().Be(secondModifier);
            ingredient.ModifiedDate.Should().BeAfter(firstModifiedDate);
        }

        #endregion

        #region Edge Cases

        [Fact(DisplayName = "Deve aceitar nome com caracteres especiais")]
        public void CreateIngredient_WithSpecialCharacters_ShouldSucceed()
        {
            // Arrange
            var specialName = "Açúcar Mascavo Orgânico (Light)";

            // Act
            var ingredient = new Ingredient(specialName, _validCreatedBy);

            // Assert
            ingredient.Name.Should().Be(specialName);
        }

        [Fact(DisplayName = "Deve aceitar nome com números")]
        public void CreateIngredient_WithNumbers_ShouldSucceed()
        {
            // Arrange
            var nameWithNumbers = "Farinha 000";

            // Act
            var ingredient = new Ingredient(nameWithNumbers, _validCreatedBy);

            // Assert
            ingredient.Name.Should().Be(nameWithNumbers);
        }

        [Fact(DisplayName = "Deve atualizar para o mesmo nome")]
        public void UpdateName_ToSameName_ShouldSucceed()
        {
            // Arrange
            var ingredient = new Ingredient(_validName, _validCreatedBy);
            var modifier = "chef_same";

            // Act
            ingredient.UpdateName(_validName, modifier);

            // Assert
            ingredient.Name.Should().Be(_validName);
            ingredient.ModifiedBy.Should().Be(modifier);
            ingredient.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        #endregion

        #region Audit Fields Tests

        [Fact(DisplayName = "Deve herdar e funcionar campos de auditoria da Entity")]
        public void Ingredient_ShouldInheritAuditFieldsFromEntity()
        {
            // Arrange
            var createdDate = new DateTime(2025, 10, 20);
            var ingredient = new Ingredient(_validName, _validCreatedBy);

            // Act
            ingredient.GetType().GetProperty("CreatedDate")!.SetValue(ingredient, createdDate);
            ingredient.MarkAsModified("modifier");

            // Assert
            ingredient.CreatedDate.Should().Be(createdDate);
            ingredient.CreatedBy.Should().Be(_validCreatedBy);
            ingredient.ModifiedBy.Should().Be("modifier");
            ingredient.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve atualizar ModifiedDate ao chamar UpdateName")]
        public void UpdateName_ShouldUpdateModifiedDate()
        {
            // Arrange
            var ingredient = new Ingredient(_validName, _validCreatedBy);
            var initialModifiedDate = ingredient.ModifiedDate;

            // Aguardar um pouco para garantir que o tempo mude
            System.Threading.Thread.Sleep(10);

            // Act
            ingredient.UpdateName("Novo Nome", "modifier");

            // Assert
            ingredient.ModifiedDate.Should().BeAfter(initialModifiedDate);
        }

        #endregion

        #region Consistency Tests

        [Fact(DisplayName = "Deve manter consistência dos dados após criação")]
        public void Ingredient_ShouldMaintainDataConsistency()
        {
            // Arrange
            var originalName = "Farinha de Aveia";
            var originalCreatedBy = "chef_original";

            // Act
            var ingredient = new Ingredient(originalName, originalCreatedBy);

            // Assert
            ingredient.Name.Should().Be(originalName);
            ingredient.CreatedBy.Should().Be(originalCreatedBy);
            ingredient.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve validar todos os campos no UpdateName")]
        public void UpdateName_ShouldValidateAllFields()
        {
            // Arrange
            var ingredient = new Ingredient(_validName, _validCreatedBy);
            var validNewName = "Novo Ingrediente Válido";
            var validModifier = "valid_mod";

            // Act
            Action act = () => ingredient.UpdateName(validNewName, validModifier);

            // Assert
            act.Should().NotThrow();
            ingredient.Name.Should().Be(validNewName);
            ingredient.ModifiedBy.Should().Be(validModifier);
        }

        #endregion
    }
}