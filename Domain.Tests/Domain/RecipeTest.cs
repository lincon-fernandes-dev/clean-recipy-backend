using Domain.Entities;
using Domain.Validation;
using System;
using FluentAssertions;
using Xunit;
using Domain.Enums;

namespace Domain.Tests.Entities
{
    public class RecipeTests
    {
        private readonly string _validName = "Bolo de Chocolate Caseiro";
        private readonly string _validDescription = "Um delicioso bolo de chocolate feito com cacau em pó e cobertura cremosa. Perfeito para festas e lanches da tarde.";
        private readonly string _validInstructions = "1. Pré-aqueça o forno a 180°C. 2. Misture os ingredientes secos. 3. Adicione os líquidos e bata até ficar homogêneo. 4. Asse por 40 minutos. 5. Deixe esfriar antes de servir.";
        private readonly int _validUserId = 1;
        private readonly string _validCreatedBy = "chef_john";

        #region Constructor Tests

        [Fact(DisplayName = "Deve criar receita válida com Id especificado")]
        public void CreateRecipe_WithValidParametersAndId_ShouldSucceed()
        {
            // Arrange
            var id = 1;

            // Act
            var recipe = new Recipe(id, _validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Id.Should().Be(id);
            recipe.Name.Should().Be(_validName);
            recipe.Description.Should().Be(_validDescription);
            recipe.Instructions.Should().Be(_validInstructions);
            recipe.UserId.Should().Be(_validUserId);
            recipe.CreatedBy.Should().Be(_validCreatedBy);
            recipe.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve criar receita válida sem Id")]
        public void CreateRecipe_WithoutId_ShouldSucceed()
        {
            // Act
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Id.Should().Be(0);
            recipe.Name.Should().Be(_validName);
            recipe.Description.Should().Be(_validDescription);
            recipe.Instructions.Should().Be(_validInstructions);
            recipe.UserId.Should().Be(_validUserId);
            recipe.CreatedBy.Should().Be(_validCreatedBy);
        }

        [Theory(DisplayName = "Deve lançar exceção quando Id é inválido")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void CreateRecipe_WithInvalidId_ShouldThrowException(int invalidId)
        {
            // Act
            Action act = () => new Recipe(invalidId, _validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Id inválido, Id deve ser um numero inteiro e positivo");
        }

        #endregion

        #region Name Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando nome é inválido")]
        [InlineData(null, "O nome da receita é obrigatório.")]
        [InlineData("", "O nome da receita é obrigatório.")]
        [InlineData("   ", "O nome da receita é obrigatório.")]
        [InlineData("Bo", "O nome da receita deve ter pelo menos 3 caracteres.")]
        [InlineData("B", "O nome da receita deve ter pelo menos 3 caracteres.")]
        public void CreateRecipe_WithInvalidName_ShouldThrowException(string invalidName, string expectedMessage)
        {
            // Act
            Action act = () => new Recipe(invalidName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Deve lançar exceção quando nome excede 100 caracteres")]
        public void CreateRecipe_WithLongName_ShouldThrowException()
        {
            // Arrange
            var longName = new string('A', 101);

            // Act
            Action act = () => new Recipe(longName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("O nome da receita não pode ultrapassar 100 caracteres.");
        }

        [Theory(DisplayName = "Deve aceitar nomes válidos")]
        [InlineData("Bolo")]
        [InlineData("Bolo de Chocolate")]
        [InlineData("Lasanha de Carne com Queijo")]
        [InlineData("Sopa de Legumes Cremosa")]
        public void CreateRecipe_WithValidName_ShouldSucceed(string validName)
        {
            // Act
            var recipe = new Recipe(validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Name.Should().Be(validName);
        }

        [Fact(DisplayName = "Deve aceitar nome com exatamente 3 caracteres")]
        public void CreateRecipe_WithNameExactly3Characters_ShouldSucceed()
        {
            // Arrange
            var exactName = "Bol";

            // Act
            var recipe = new Recipe(exactName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Name.Should().Be(exactName);
        }

        [Fact(DisplayName = "Deve aceitar nome com exatamente 100 caracteres")]
        public void CreateRecipe_WithNameExactly100Characters_ShouldSucceed()
        {
            // Arrange
            var exactName = new string('A', 100);

            // Act
            var recipe = new Recipe(exactName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Name.Should().Be(exactName);
        }

        #endregion

        #region Description Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando descrição é inválida")]
        [InlineData(null, "A descrição é obrigatória.")]
        [InlineData("", "A descrição é obrigatória.")]
        [InlineData("   ", "A descrição é obrigatória.")]
        public void CreateRecipe_WithInvalidDescription_ShouldThrowException(string invalidDescription, string expectedMessage)
        {
            // Act
            Action act = () => new Recipe(_validName, invalidDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Deve lançar exceção quando descrição é muito curta")]
        public void CreateRecipe_WithShortDescription_ShouldThrowException()
        {
            // Arrange
            var shortDescription = "Descrição muito curta";

            // Act
            Action act = () => new Recipe(_validName, shortDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("A descrição deve ter pelo menos 25 caracteres.");
        }

        [Fact(DisplayName = "Deve lançar exceção quando descrição excede 524 caracteres")]
        public void CreateRecipe_WithLongDescription_ShouldThrowException()
        {
            // Arrange
            var longDescription = new string('A', 525);

            // Act
            Action act = () => new Recipe(_validName, longDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("A descrição não pode ultrapassar 524 caracteres.");
        }

        [Fact(DisplayName = "Deve aceitar descrição com exatamente 25 caracteres")]
        public void CreateRecipe_WithDescriptionExactly25Characters_ShouldSucceed()
        {
            // Arrange
            var exactDescription = new string('A', 25);

            // Act
            var recipe = new Recipe(_validName, exactDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Description.Should().Be(exactDescription);
        }

        [Fact(DisplayName = "Deve aceitar descrição com exatamente 524 caracteres")]
        public void CreateRecipe_WithDescriptionExactly524Characters_ShouldSucceed()
        {
            // Arrange
            var exactDescription = new string('A', 524);

            // Act
            var recipe = new Recipe(_validName, exactDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Description.Should().Be(exactDescription);
        }

        #endregion

        #region Instructions Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando instruções são inválidas")]
        [InlineData(null, "As instruções são obrigatórias.")]
        [InlineData("", "As instruções são obrigatórias.")]
        [InlineData("   ", "As instruções são obrigatórias.")]
        public void CreateRecipe_WithInvalidInstructions_ShouldThrowException(string invalidInstructions, string expectedMessage)
        {
            // Act
            Action act = () => new Recipe(_validName, _validDescription, invalidInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Deve lançar exceção quando instruções são muito curtas")]
        public void CreateRecipe_WithShortInstructions_ShouldThrowException()
        {
            // Arrange
            var shortInstructions = "Instruções curtas";

            // Act
            Action act = () => new Recipe(_validName, _validDescription, shortInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("As instruções devem ter pelo menos 25 caracteres.");
        }

        [Fact(DisplayName = "Deve lançar exceção quando instruções excedem 600 caracteres")]
        public void CreateRecipe_WithLongInstructions_ShouldThrowException()
        {
            // Arrange
            var longInstructions = new string('A', 601);

            // Act
            Action act = () => new Recipe(_validName, _validDescription, longInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("As instruções não podem ultrapassar 600 caracteres.");
        }

        [Fact(DisplayName = "Deve aceitar instruções com exatamente 25 caracteres")]
        public void CreateRecipe_WithInstructionsExactly25Characters_ShouldSucceed()
        {
            // Arrange
            var exactInstructions = new string('A', 25);

            // Act
            var recipe = new Recipe(_validName, _validDescription, exactInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Instructions.Should().Be(exactInstructions);
        }

        [Fact(DisplayName = "Deve aceitar instruções com exatamente 600 caracteres")]
        public void CreateRecipe_WithInstructionsExactly600Characters_ShouldSucceed()
        {
            // Arrange
            var exactInstructions = new string('A', 600);

            // Act
            var recipe = new Recipe(_validName, _validDescription, exactInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Instructions.Should().Be(exactInstructions);
        }

        #endregion

        #region UserId Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando UserId é inválido")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void CreateRecipe_WithInvalidUserId_ShouldThrowException(int invalidUserId)
        {
            // Act
            Action act = () => new Recipe(_validName, _validDescription, _validInstructions, invalidUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("UserId inválido, Id deve ser um numero inteiro e positivo");
        }

        [Theory(DisplayName = "Deve aceitar UserId válido")]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(999)]
        public void CreateRecipe_WithValidUserId_ShouldSucceed(int validUserId)
        {
            // Act
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, validUserId, _validCreatedBy);

            // Assert
            recipe.UserId.Should().Be(validUserId);
        }

        #endregion

        #region CreatedBy Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando createdBy é inválido")]
        [InlineData(null, "O nome de usuario é obrigatorio")]
        [InlineData("", "O nome de usuario é obrigatorio")]
        [InlineData("Jo", "O nome de usuario deve ter pelo menos 4 caracteres")]
        [InlineData("J", "O nome de usuario deve ter pelo menos 4 caracteres")]
        public void CreateRecipe_WithInvalidCreatedBy_ShouldThrowException(string invalidCreatedBy, string expectedMessage)
        {
            // Act
            Action act = () => new Recipe(_validName, _validDescription, _validInstructions, _validUserId, invalidCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Theory(DisplayName = "Deve aceitar createdBy válido")]
        [InlineData("john")]
        [InlineData("chef_maria")]
        [InlineData("user123")]
        [InlineData("cook")]
        public void CreateRecipe_WithValidCreatedBy_ShouldSucceed(string validCreatedBy)
        {
            // Act
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, validCreatedBy);

            // Assert
            recipe.CreatedBy.Should().Be(validCreatedBy);
        }

        [Fact(DisplayName = "Deve aceitar createdBy com exatamente 4 caracteres")]
        public void CreateRecipe_WithCreatedByExactly4Characters_ShouldSucceed()
        {
            // Arrange
            var exactCreatedBy = "John";

            // Act
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, exactCreatedBy);

            // Assert
            recipe.CreatedBy.Should().Be(exactCreatedBy);
        }

        #endregion

        #region Update Tests

        [Fact(DisplayName = "Deve atualizar receita com dados válidos")]
        public void Update_WithValidData_ShouldSucceed()
        {
            // Arrange
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);
            var newName = "Bolo de Chocolate Premium";
            var newDescription = "Uma versão premium do bolo de chocolate com ingredientes selecionados e técnicas especiais de preparo.";
            var newInstructions = "1. Pré-aqueça o forno a 180°C. 2. Peneire a farinha e o cacau. 3. Bata as claras em neve. 4. Misture cuidadosamente. 5. Asse por 45 minutos.";
            var modifiedBy = "chef_updated";

            // Act
            recipe.Update(newName, newDescription, newInstructions, modifiedBy);

            // Assert
            recipe.Name.Should().Be(newName);
            recipe.Description.Should().Be(newDescription);
            recipe.Instructions.Should().Be(newInstructions);
            recipe.LastModifiedBy.Should().Be(modifiedBy);
            recipe.LastModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory(DisplayName = "Deve lançar exceção ao atualizar com modifiedBy inválido")]
        [InlineData(null, "Para Poder atualizar o Ingrediente é necessario fornacer o nome do usuario que esta o modificando")]
        [InlineData("", "Para Poder atualizar o Ingrediente é necessario fornacer o nome do usuario que esta o modificando")]
        [InlineData("Jo", "O nome de usuario para modificação deve ter pelo menos 4 caracteres")]
        [InlineData("J", "O nome de usuario para modificação deve ter pelo menos 4 caracteres")]
        public void Update_WithInvalidModifiedBy_ShouldThrowException(string invalidModifiedBy, string expectedMessage)
        {
            // Arrange
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Act
            Action act = () => recipe.Update(_validName, _validDescription, _validInstructions, invalidModifiedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Deve lançar exceção ao atualizar com nome inválido")]
        public void Update_WithInvalidName_ShouldThrowException()
        {
            // Arrange
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);
            var invalidName = "Bo";

            // Act
            Action act = () => recipe.Update(invalidName, _validDescription, _validInstructions, "valid_user");

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("O nome da receita deve ter pelo menos 3 caracteres.");
        }

        #endregion

        #region Navigation Properties Tests

        [Fact(DisplayName = "Deve inicializar coleções vazias")]
        public void Recipe_ShouldInitializeEmptyCollections()
        {
            // Act
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Ingredients.Should().NotBeNull();
            recipe.Ingredients.Should().BeEmpty();
            recipe.Votes.Should().NotBeNull();
            recipe.Votes.Should().BeEmpty();
        }

        [Fact(DisplayName = "Deve inicializar User como nulo")]
        public void Recipe_ShouldInitializeUserAsNull()
        {
            // Act
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.User.Should().BeNull();
        }

        [Fact(DisplayName = "Deve permitir adicionar ingredientes")]
        public void Recipe_ShouldAllowAddingIngredients()
        {
            // Arrange
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);
            var ingredient = new RecipeIngredient(1, 1, 2.0m, UnidadeMedidaEnum.Xicara, _validCreatedBy);

            // Act
            recipe.Ingredients.Add(ingredient);

            // Assert
            recipe.Ingredients.Should().ContainSingle();
            recipe.Ingredients.First().Should().Be(ingredient);
        }

        [Fact(DisplayName = "Deve permitir adicionar votos")]
        public void Recipe_ShouldAllowAddingVotes()
        {
            // Arrange
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);
            var vote = new Vote(_validUserId, recipe.Id, true, _validCreatedBy);

            // Act
            recipe.Votes.Add(vote);

            // Assert
            recipe.Votes.Should().ContainSingle();
            recipe.Votes.First().Should().Be(vote);
        }

        [Fact(DisplayName = "Deve permitir definir User via reflection")]
        public void Recipe_ShouldAllowSettingUser()
        {
            // Arrange
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);
            var user = new User("Test User", "test@email.com", "hash", UserStatus.Active, _validCreatedBy);

            // Act
            recipe.GetType().GetProperty("User")!.SetValue(recipe, user);

            // Assert
            recipe.User.Should().Be(user);
        }

        #endregion

        #region Edge Cases

        [Fact(DisplayName = "Deve manter UserId original após update")]
        public void Update_ShouldNotChangeUserId()
        {
            // Arrange
            var originalUserId = 5;
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, originalUserId, _validCreatedBy);

            // Act
            recipe.Update("Novo Nome", "Nova Descrição minimo 25 caractere", "Novas Instruções minimo 25 caractere", "modifier");

            // Assert
            recipe.UserId.Should().Be(originalUserId);
        }

        [Fact(DisplayName = "Deve manter CreatedBy original após update")]
        public void Update_ShouldNotChangeCreatedBy()
        {
            // Arrange
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Act
            recipe.Update("Novo Nome", "Nova Descrição minimo 25 caractere", "Novas Instruções minimo 25 caractere", "modifier");

            // Assert
            recipe.CreatedBy.Should().Be(_validCreatedBy);
        }

        #endregion
    }
}