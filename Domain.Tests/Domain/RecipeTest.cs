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
        private readonly string _validDescription = "Um delicioso bolo de chocolate feito com cacau em p� e cobertura cremosa. Perfeito para festas e lanches da tarde.";
        private readonly string _validInstructions = "1. Pr�-aque�a o forno a 180�C. 2. Misture os ingredientes secos. 3. Adicione os l�quidos e bata at� ficar homog�neo. 4. Asse por 40 minutos. 5. Deixe esfriar antes de servir.";
        private readonly int _validUserId = 1;
        private readonly string _validCreatedBy = "chef_john";

        #region Constructor Tests

        [Fact(DisplayName = "Deve criar receita v�lida com Id especificado")]
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

        [Fact(DisplayName = "Deve criar receita v�lida sem Id")]
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

        [Theory(DisplayName = "Deve lan�ar exce��o quando Id � inv�lido")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void CreateRecipe_WithInvalidId_ShouldThrowException(int invalidId)
        {
            // Act
            Action act = () => new Recipe(invalidId, _validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Id inv�lido, Id deve ser um numero inteiro e positivo");
        }

        #endregion

        #region Name Validation Tests

        [Theory(DisplayName = "Deve lan�ar exce��o quando nome � inv�lido")]
        [InlineData(null, "O nome da receita � obrigat�rio.")]
        [InlineData("", "O nome da receita � obrigat�rio.")]
        [InlineData("   ", "O nome da receita � obrigat�rio.")]
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

        [Fact(DisplayName = "Deve lan�ar exce��o quando nome excede 100 caracteres")]
        public void CreateRecipe_WithLongName_ShouldThrowException()
        {
            // Arrange
            var longName = new string('A', 101);

            // Act
            Action act = () => new Recipe(longName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("O nome da receita n�o pode ultrapassar 100 caracteres.");
        }

        [Theory(DisplayName = "Deve aceitar nomes v�lidos")]
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

        [Theory(DisplayName = "Deve lan�ar exce��o quando descri��o � inv�lida")]
        [InlineData(null, "A descri��o � obrigat�ria.")]
        [InlineData("", "A descri��o � obrigat�ria.")]
        [InlineData("   ", "A descri��o � obrigat�ria.")]
        public void CreateRecipe_WithInvalidDescription_ShouldThrowException(string invalidDescription, string expectedMessage)
        {
            // Act
            Action act = () => new Recipe(_validName, invalidDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Deve lan�ar exce��o quando descri��o � muito curta")]
        public void CreateRecipe_WithShortDescription_ShouldThrowException()
        {
            // Arrange
            var shortDescription = "Descri��o muito curta";

            // Act
            Action act = () => new Recipe(_validName, shortDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("A descri��o deve ter pelo menos 25 caracteres.");
        }

        [Fact(DisplayName = "Deve lan�ar exce��o quando descri��o excede 524 caracteres")]
        public void CreateRecipe_WithLongDescription_ShouldThrowException()
        {
            // Arrange
            var longDescription = new string('A', 525);

            // Act
            Action act = () => new Recipe(_validName, longDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("A descri��o n�o pode ultrapassar 524 caracteres.");
        }

        [Fact(DisplayName = "Deve aceitar descri��o com exatamente 25 caracteres")]
        public void CreateRecipe_WithDescriptionExactly25Characters_ShouldSucceed()
        {
            // Arrange
            var exactDescription = new string('A', 25);

            // Act
            var recipe = new Recipe(_validName, exactDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Description.Should().Be(exactDescription);
        }

        [Fact(DisplayName = "Deve aceitar descri��o com exatamente 524 caracteres")]
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

        [Theory(DisplayName = "Deve lan�ar exce��o quando instru��es s�o inv�lidas")]
        [InlineData(null, "As instru��es s�o obrigat�rias.")]
        [InlineData("", "As instru��es s�o obrigat�rias.")]
        [InlineData("   ", "As instru��es s�o obrigat�rias.")]
        public void CreateRecipe_WithInvalidInstructions_ShouldThrowException(string invalidInstructions, string expectedMessage)
        {
            // Act
            Action act = () => new Recipe(_validName, _validDescription, invalidInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Fact(DisplayName = "Deve lan�ar exce��o quando instru��es s�o muito curtas")]
        public void CreateRecipe_WithShortInstructions_ShouldThrowException()
        {
            // Arrange
            var shortInstructions = "Instru��es curtas";

            // Act
            Action act = () => new Recipe(_validName, _validDescription, shortInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("As instru��es devem ter pelo menos 25 caracteres.");
        }

        [Fact(DisplayName = "Deve lan�ar exce��o quando instru��es excedem 600 caracteres")]
        public void CreateRecipe_WithLongInstructions_ShouldThrowException()
        {
            // Arrange
            var longInstructions = new string('A', 601);

            // Act
            Action act = () => new Recipe(_validName, _validDescription, longInstructions, _validUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("As instru��es n�o podem ultrapassar 600 caracteres.");
        }

        [Fact(DisplayName = "Deve aceitar instru��es com exatamente 25 caracteres")]
        public void CreateRecipe_WithInstructionsExactly25Characters_ShouldSucceed()
        {
            // Arrange
            var exactInstructions = new string('A', 25);

            // Act
            var recipe = new Recipe(_validName, _validDescription, exactInstructions, _validUserId, _validCreatedBy);

            // Assert
            recipe.Instructions.Should().Be(exactInstructions);
        }

        [Fact(DisplayName = "Deve aceitar instru��es com exatamente 600 caracteres")]
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

        [Theory(DisplayName = "Deve lan�ar exce��o quando UserId � inv�lido")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void CreateRecipe_WithInvalidUserId_ShouldThrowException(int invalidUserId)
        {
            // Act
            Action act = () => new Recipe(_validName, _validDescription, _validInstructions, invalidUserId, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("UserId inv�lido, Id deve ser um numero inteiro e positivo");
        }

        [Theory(DisplayName = "Deve aceitar UserId v�lido")]
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

        [Theory(DisplayName = "Deve lan�ar exce��o quando createdBy � inv�lido")]
        [InlineData(null, "O nome de usuario � obrigatorio")]
        [InlineData("", "O nome de usuario � obrigatorio")]
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

        [Theory(DisplayName = "Deve aceitar createdBy v�lido")]
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

        [Fact(DisplayName = "Deve atualizar receita com dados v�lidos")]
        public void Update_WithValidData_ShouldSucceed()
        {
            // Arrange
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);
            var newName = "Bolo de Chocolate Premium";
            var newDescription = "Uma vers�o premium do bolo de chocolate com ingredientes selecionados e t�cnicas especiais de preparo.";
            var newInstructions = "1. Pr�-aque�a o forno a 180�C. 2. Peneire a farinha e o cacau. 3. Bata as claras em neve. 4. Misture cuidadosamente. 5. Asse por 45 minutos.";
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

        [Theory(DisplayName = "Deve lan�ar exce��o ao atualizar com modifiedBy inv�lido")]
        [InlineData(null, "Para Poder atualizar o Ingrediente � necessario fornacer o nome do usuario que esta o modificando")]
        [InlineData("", "Para Poder atualizar o Ingrediente � necessario fornacer o nome do usuario que esta o modificando")]
        [InlineData("Jo", "O nome de usuario para modifica��o deve ter pelo menos 4 caracteres")]
        [InlineData("J", "O nome de usuario para modifica��o deve ter pelo menos 4 caracteres")]
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

        [Fact(DisplayName = "Deve lan�ar exce��o ao atualizar com nome inv�lido")]
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

        [Fact(DisplayName = "Deve inicializar cole��es vazias")]
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

        [Fact(DisplayName = "Deve manter UserId original ap�s update")]
        public void Update_ShouldNotChangeUserId()
        {
            // Arrange
            var originalUserId = 5;
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, originalUserId, _validCreatedBy);

            // Act
            recipe.Update("Novo Nome", "Nova Descri��o minimo 25 caractere", "Novas Instru��es minimo 25 caractere", "modifier");

            // Assert
            recipe.UserId.Should().Be(originalUserId);
        }

        [Fact(DisplayName = "Deve manter CreatedBy original ap�s update")]
        public void Update_ShouldNotChangeCreatedBy()
        {
            // Arrange
            var recipe = new Recipe(_validName, _validDescription, _validInstructions, _validUserId, _validCreatedBy);

            // Act
            recipe.Update("Novo Nome", "Nova Descri��o minimo 25 caractere", "Novas Instru��es minimo 25 caractere", "modifier");

            // Assert
            recipe.CreatedBy.Should().Be(_validCreatedBy);
        }

        #endregion
    }
}