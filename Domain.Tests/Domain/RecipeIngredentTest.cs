using Domain.Entities;
using Domain.Enums;
using Domain.Validation;
using System;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class RecipeIngredientTests
    {
        private readonly int _validRecipeId = 1;
        private readonly int _validIngredientId = 1;
        private readonly decimal _validQuantity = 2.5m;
        private readonly UnidadeMedidaEnum _validUnidadeMedida = UnidadeMedidaEnum.Xicara;
        private readonly string _validCreatedBy = "chef_john";

        #region Constructor Tests

        [Fact(DisplayName = "Deve criar RecipeIngredient válido com Id especificado")]
        public void CreateRecipeIngredient_WithValidParametersAndId_ShouldSucceed()
        {
            // Arrange
            var id = 1;

            // Act
            var recipeIngredient = new RecipeIngredient(id, _validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            recipeIngredient.Id.Should().Be(id);
            recipeIngredient.RecipeId.Should().Be(_validRecipeId);
            recipeIngredient.IngredientId.Should().Be(_validIngredientId);
            recipeIngredient.Quantity.Should().Be(_validQuantity);
            recipeIngredient.UnidadeMedida.Should().Be(_validUnidadeMedida);
            recipeIngredient.CreatedBy.Should().Be(_validCreatedBy);
            recipeIngredient.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve criar RecipeIngredient válido sem Id")]
        public void CreateRecipeIngredient_WithoutId_ShouldSucceed()
        {
            // Act
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            recipeIngredient.Id.Should().Be(0);
            recipeIngredient.RecipeId.Should().Be(_validRecipeId);
            recipeIngredient.IngredientId.Should().Be(_validIngredientId);
            recipeIngredient.Quantity.Should().Be(_validQuantity);
            recipeIngredient.UnidadeMedida.Should().Be(_validUnidadeMedida);
            recipeIngredient.CreatedBy.Should().Be(_validCreatedBy);
        }

        [Theory(DisplayName = "Deve lançar exceção quando Id é inválido")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void CreateRecipeIngredient_WithInvalidId_ShouldThrowException(int invalidId)
        {
            // Act
            Action act = () => new RecipeIngredient(invalidId, _validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Id inválido, Id deve ser um numero inteiro e positivo");
        }

        #endregion

        #region RecipeId Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando RecipeId é inválido")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void CreateRecipeIngredient_WithInvalidRecipeId_ShouldThrowException(int invalidRecipeId)
        {
            // Act
            Action act = () => new RecipeIngredient(_validRecipeId, invalidRecipeId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Id inválido, Id do ingrediente deve ser um numero inteiro e positivo");
        }

        [Theory(DisplayName = "Deve aceitar RecipeId válido")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999)]
        public void CreateRecipeIngredient_WithValidRecipeId_ShouldSucceed(int validRecipeId)
        {
            // Act
            var recipeIngredient = new RecipeIngredient(validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            recipeIngredient.RecipeId.Should().Be(validRecipeId);
        }

        #endregion

        #region IngredientId Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando IngredientId é inválido")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void CreateRecipeIngredient_WithInvalidIngredientId_ShouldThrowException(int invalidIngredientId)
        {
            // Act
            Action act = () => new RecipeIngredient(_validRecipeId, invalidIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Id inválido, Id do ingrediente deve ser um numero inteiro e positivo");
        }

        [Theory(DisplayName = "Deve aceitar IngredientId válido")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999)]
        public void CreateRecipeIngredient_WithValidIngredientId_ShouldSucceed(int validIngredientId)
        {
            // Act
            var recipeIngredient = new RecipeIngredient(_validRecipeId, validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            recipeIngredient.IngredientId.Should().Be(validIngredientId);
        }

        #endregion

        #region Quantity Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando Quantity é inválida")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-0.5)]
        [InlineData(-100.5)]
        public void CreateRecipeIngredient_WithInvalidQuantity_ShouldThrowException(decimal invalidQuantity)
        {
            // Act
            Action act = () => new RecipeIngredient(_validRecipeId, _validIngredientId, invalidQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("A quantidade deve ser maior que zero.");
        }

        [Theory(DisplayName = "Deve aceitar Quantity válida")]
        [InlineData(0.1)]
        [InlineData(1)]
        [InlineData(2.5)]
        [InlineData(100)]
        [InlineData(999.99)]
        public void CreateRecipeIngredient_WithValidQuantity_ShouldSucceed(decimal validQuantity)
        {
            // Act
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            recipeIngredient.Quantity.Should().Be(validQuantity);
        }

        [Fact(DisplayName = "Deve aceitar Quantity com valor decimal pequeno")]
        public void CreateRecipeIngredient_WithSmallDecimalQuantity_ShouldSucceed()
        {
            // Arrange
            var smallQuantity = 0.001m;

            // Act
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, smallQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            recipeIngredient.Quantity.Should().Be(smallQuantity);
        }

        #endregion

        #region UnidadeMedida Validation Tests

        [Theory(DisplayName = "Deve aceitar todas as unidades de medida válidas")]
        [InlineData(UnidadeMedidaEnum.NaoInformada)]
        [InlineData(UnidadeMedidaEnum.Grama)]
        [InlineData(UnidadeMedidaEnum.Quilograma)]
        [InlineData(UnidadeMedidaEnum.Miligrama)]
        [InlineData(UnidadeMedidaEnum.Litro)]
        [InlineData(UnidadeMedidaEnum.Xicara)]
        [InlineData(UnidadeMedidaEnum.ColherSopa)]
        [InlineData(UnidadeMedidaEnum.ColherCha)]
        [InlineData(UnidadeMedidaEnum.Pitada)]
        [InlineData(UnidadeMedidaEnum.Unidade)]
        [InlineData(UnidadeMedidaEnum.Folha)]
        [InlineData(UnidadeMedidaEnum.Dente)]
        public void CreateRecipeIngredient_WithValidUnidadeMedida_ShouldSucceed(UnidadeMedidaEnum validUnidadeMedida)
        {
            // Act
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, validUnidadeMedida, _validCreatedBy);

            // Assert
            recipeIngredient.UnidadeMedida.Should().Be(validUnidadeMedida);
        }

        [Fact(DisplayName = "Deve lançar exceção quando UnidadeMedida é inválida")]
        public void CreateRecipeIngredient_WithInvalidUnidadeMedida_ShouldThrowException()
        {
            // Arrange
            var invalidUnidadeMedida = (UnidadeMedidaEnum)999;

            // Act
            Action act = () => new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, invalidUnidadeMedida, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Unidade de medida inválida.");
        }

        #endregion

        #region CreatedBy Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando createdBy é inválido")]
        [InlineData(null, "O nome de usuario é obrigatorio")]
        [InlineData("", "O nome de usuario é obrigatorio")]
        [InlineData("Jo", "O nome de usuario deve ter pelo menos 4 caracteres")]
        [InlineData("J", "O nome de usuario deve ter pelo menos 4 caracteres")]
        public void CreateRecipeIngredient_WithInvalidCreatedBy_ShouldThrowException(string invalidCreatedBy, string expectedMessage)
        {
            // Act
            Action act = () => new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, invalidCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Theory(DisplayName = "Deve aceitar createdBy válido")]
        [InlineData("john")]
        [InlineData("chef_maria")]
        [InlineData("user123")]
        [InlineData("cook")]
        public void CreateRecipeIngredient_WithValidCreatedBy_ShouldSucceed(string validCreatedBy)
        {
            // Act
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, validCreatedBy);

            // Assert
            recipeIngredient.CreatedBy.Should().Be(validCreatedBy);
        }

        [Fact(DisplayName = "Deve aceitar createdBy com exatamente 4 caracteres")]
        public void CreateRecipeIngredient_WithCreatedByExactly4Characters_ShouldSucceed()
        {
            // Arrange
            var exactCreatedBy = "John";

            // Act
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, exactCreatedBy);

            // Assert
            recipeIngredient.CreatedBy.Should().Be(exactCreatedBy);
        }

        #endregion

        #region UpdateQuantity Tests

        [Fact(DisplayName = "Deve atualizar quantidade com dados válidos")]
        public void UpdateQuantity_WithValidData_ShouldSucceed()
        {
            // Arrange
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);
            var newQuantity = 3.75m;
            var modifiedBy = "chef_updated";

            // Act
            recipeIngredient.UpdateQuantity(newQuantity, modifiedBy);

            // Assert
            recipeIngredient.Quantity.Should().Be(newQuantity);
            recipeIngredient.ModifiedBy.Should().Be(modifiedBy);
            recipeIngredient.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory(DisplayName = "Deve lançar exceção ao atualizar com quantidade inválida")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-0.5)]
        public void UpdateQuantity_WithInvalidQuantity_ShouldThrowException(decimal invalidQuantity)
        {
            // Arrange
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Act
            Action act = () => recipeIngredient.UpdateQuantity(invalidQuantity, "valid_user");

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("A quantidade deve ser maior que zero.");
        }

        [Fact(DisplayName = "Deve manter outros campos inalterados após update da quantidade")]
        public void UpdateQuantity_ShouldNotChangeOtherFields()
        {
            // Arrange
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);
            var newQuantity = 4.2m;

            // Act
            recipeIngredient.UpdateQuantity(newQuantity, "modifier");

            // Assert
            recipeIngredient.RecipeId.Should().Be(_validRecipeId);
            recipeIngredient.IngredientId.Should().Be(_validIngredientId);
            recipeIngredient.UnidadeMedida.Should().Be(_validUnidadeMedida);
            recipeIngredient.CreatedBy.Should().Be(_validCreatedBy);
        }

        [Fact(DisplayName = "Deve atualizar quantidade múltiplas vezes")]
        public void UpdateQuantity_MultipleTimes_ShouldSucceed()
        {
            // Arrange
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);
            var firstNewQuantity = 3.0m;
            var secondNewQuantity = 5.5m;

            // Act - Primeira atualização
            recipeIngredient.UpdateQuantity(firstNewQuantity, "chef_1");
            var firstModifiedDate = recipeIngredient.ModifiedDate;

            // Aguardar um pouco para garantir que o tempo mude
            System.Threading.Thread.Sleep(10);

            // Act - Segunda atualização
            recipeIngredient.UpdateQuantity(secondNewQuantity, "chef_2");

            // Assert
            recipeIngredient.Quantity.Should().Be(secondNewQuantity);
            recipeIngredient.ModifiedBy.Should().Be("chef_2");
            recipeIngredient.ModifiedDate.Should().BeAfter(firstModifiedDate);
        }

        #endregion

        #region Navigation Properties Tests

        [Fact(DisplayName = "Deve inicializar propriedades de navegação como nulas")]
        public void RecipeIngredient_ShouldInitializeNavigationPropertiesAsNull()
        {
            // Act
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            recipeIngredient.Recipe.Should().BeNull();
            recipeIngredient.Ingredient.Should().BeNull();
        }

        [Fact(DisplayName = "Deve permitir definir propriedades de navegação via reflection")]
        public void RecipeIngredient_ShouldAllowSettingNavigationProperties()
        {
            // Arrange
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);
            var recipe = new Recipe("Test Recipe", "Description minima de 25 caracteres", "Instructions minima de 25 caracteres", 1, _validCreatedBy);
            var ingredient = new Ingredient("Test Ingredient", _validCreatedBy);

            // Act
            recipeIngredient.GetType().GetProperty("Recipe")!.SetValue(recipeIngredient, recipe);
            recipeIngredient.GetType().GetProperty("Ingredient")!.SetValue(recipeIngredient, ingredient);

            // Assert
            recipeIngredient.Recipe.Should().Be(recipe);
            recipeIngredient.Ingredient.Should().Be(ingredient);
        }

        #endregion

        #region Audit Fields Tests

        [Fact(DisplayName = "Deve herdar e funcionar campos de auditoria da Entity")]
        public void RecipeIngredient_ShouldInheritAuditFieldsFromEntity()
        {
            // Arrange
            var createdDate = new DateTime(2025, 10, 20);
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Act
            recipeIngredient.GetType().GetProperty("CreatedDate")!.SetValue(recipeIngredient, createdDate);
            recipeIngredient.MarkAsModified("modifier");

            // Assert
            recipeIngredient.CreatedDate.Should().Be(createdDate);
            recipeIngredient.CreatedBy.Should().Be(_validCreatedBy);
            recipeIngredient.ModifiedBy.Should().Be("modifier");
            recipeIngredient.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve atualizar ModifiedDate ao chamar UpdateQuantity")]
        public void UpdateQuantity_ShouldUpdateModifiedDate()
        {
            // Arrange
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);
            var initialModifiedDate = recipeIngredient.ModifiedDate;

            // Aguardar um pouco para garantir que o tempo mude
            System.Threading.Thread.Sleep(10);

            // Act
            recipeIngredient.UpdateQuantity(3.0m, "modifier");

            // Assert
            recipeIngredient.ModifiedDate.Should().BeAfter(initialModifiedDate);
        }

        #endregion

        #region Edge Cases

        [Fact(DisplayName = "Deve criar com valores extremos de quantidade")]
        public void CreateRecipeIngredient_WithExtremeQuantityValues_ShouldSucceed()
        {
            // Arrange
            var verySmallQuantity = 0.0001m;
            var veryLargeQuantity = 999999.9999m;

            // Act & Assert - Quantidade muito pequena
            var smallIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, verySmallQuantity, _validUnidadeMedida, _validCreatedBy);
            smallIngredient.Quantity.Should().Be(verySmallQuantity);

            // Act & Assert - Quantidade muito grande
            var largeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, veryLargeQuantity, _validUnidadeMedida, _validCreatedBy);
            largeIngredient.Quantity.Should().Be(veryLargeQuantity);
        }

        [Fact(DisplayName = "Deve manter consistência entre RecipeId e IngredientId")]
        public void RecipeIngredient_ShouldMaintainIdConsistency()
        {
            // Arrange
            var recipeId = 5;
            var ingredientId = 10;

            // Act
            var recipeIngredient = new RecipeIngredient(recipeId, ingredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Assert
            recipeIngredient.RecipeId.Should().Be(recipeId);
            recipeIngredient.IngredientId.Should().Be(ingredientId);
            recipeIngredient.RecipeId.Should().NotBe(recipeIngredient.IngredientId);
        }

        [Fact(DisplayName = "Deve validar todas as regras no UpdateQuantity")]
        public void UpdateQuantity_ShouldValidateAllRules()
        {
            // Arrange
            var recipeIngredient = new RecipeIngredient(_validRecipeId, _validIngredientId, _validQuantity, _validUnidadeMedida, _validCreatedBy);

            // Act
            Action act = () => recipeIngredient.UpdateQuantity(0, "invalid");

            // Assert - Deve falhar na validação da quantidade primeiro
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("A quantidade deve ser maior que zero.");
        }

        #endregion
    }
}