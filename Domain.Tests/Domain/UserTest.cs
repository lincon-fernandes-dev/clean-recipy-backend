using Domain.Entities;
using Domain.Validation;
using System;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class UserTests
    {
        private readonly string _validName = "John Doe Test";
        private readonly string _validEmail = "john@test.com";
        private readonly string _validPasswordHash = "hashed_password";
        private readonly string _validCreatedBy = "joe doe";

        #region Constructor Tests

        [Fact(DisplayName = "Deve criar usuário válido com Id especificado")]
        public void CreateUser_WithValidParametersAndId_ShouldSucceed()
        {
            // Arrange
            var id = 1;

            // Act
            var user = new User(id, _validName, _validEmail, _validPasswordHash, _validCreatedBy);

            // Assert
            user.Id.Should().Be(id);
            user.Name.Should().Be(_validName);
            user.Email.Should().Be(_validEmail);
            user.PasswordHash.Should().Be(_validPasswordHash);
            user.CreatedBy.Should().Be(_validCreatedBy);
            user.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            user.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve criar usuário válido sem Id")]
        public void CreateUser_WithoutId_ShouldSucceed()
        {
            // Act
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);

            // Assert
            user.Id.Should().Be(0);
            user.Name.Should().Be(_validName);
            user.Email.Should().Be(_validEmail);
            user.PasswordHash.Should().Be(_validPasswordHash);
            user.CreatedBy.Should().Be(_validCreatedBy);
        }

        [Theory(DisplayName = "Deve lançar exceção quando Id é inválido")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void CreateUser_WithInvalidId_ShouldThrowException(int invalidId)
        {
            // Act
            Action act = () => new User(invalidId, _validName, _validEmail, _validPasswordHash, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("Id inválido. Id deve ser um número inteiro e positivo.");
        }

        #endregion

        #region Name Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando nome é inválido")]
        [InlineData(null, "O nome é obrigatório.")]
        [InlineData("", "O nome é obrigatório.")]
        [InlineData("   ", "O nome é obrigatório.")]
        [InlineData("John", "O nome deve ter pelo menos 5 caracteres.")]
        [InlineData("Jo", "O nome deve ter pelo menos 5 caracteres.")]
        [InlineData("A", "O nome deve ter pelo menos 5 caracteres.")]
        public void CreateUser_WithInvalidName_ShouldThrowException(string invalidName, string expectedMessage)
        {
            // Act
            Action act = () => new User(invalidName, _validEmail, _validPasswordHash, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Theory(DisplayName = "Deve aceitar nomes válidos")]
        [InlineData("John Doe")]
        [InlineData("Maria Silva Santos")]
        [InlineData("Ana Maria de Souza")]
        [InlineData("João Pedro")]
        public void CreateUser_WithValidName_ShouldSucceed(string validName)
        {
            // Act
            var user = new User(validName, _validEmail, _validPasswordHash, _validCreatedBy);

            // Assert
            user.Name.Should().Be(validName);
        }

        #endregion

        #region Email Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando email é inválido")]
        [InlineData(null, "O e-mail é obrigatório.")]
        [InlineData("", "O e-mail é obrigatório.")]
        [InlineData("   ", "O e-mail é obrigatório.")]
        [InlineData("invalid-email", "O e-mail deve ser válido.")]
        public void CreateUser_WithInvalidEmail_ShouldThrowException(string invalidEmail, string expectedMessage)
        {
            // Act
            Action act = () => new User(_validName, invalidEmail, _validPasswordHash, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        [Theory(DisplayName = "Deve aceitar emails válidos")]
        [InlineData("user@example.com")]
        [InlineData("user.name@example.com")]
        [InlineData("user_name@example.com")]
        [InlineData("user+tag@example.com")]
        [InlineData("user@sub.example.com")]
        public void CreateUser_WithValidEmail_ShouldSucceed(string validEmail)
        {
            // Act
            var user = new User(_validName, validEmail, _validPasswordHash, _validCreatedBy);

            // Assert
            user.Email.Should().Be(validEmail);
        }

        #endregion

        #region Password Validation Tests

        [Theory(DisplayName = "Deve lançar exceção quando senha é inválida")]
        [InlineData(null, "A senha é obrigatória.")]
        [InlineData("", "A senha é obrigatória.")]
        [InlineData("   ", "A senha é obrigatória.")]
        public void CreateUser_WithInvalidPassword_ShouldThrowException(string invalidPassword, string expectedMessage)
        {
            // Act
            Action act = () => new User(_validName, _validEmail, invalidPassword, _validCreatedBy);

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage(expectedMessage);
        }

        #endregion

        #region UpdateProfile Tests

        [Fact(DisplayName = "Deve atualizar perfil com dados válidos")]
        public void UpdateProfile_WithValidData_ShouldSucceed()
        {
            // Arrange
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);
            var newName = "Jane Smith Updated";
            var newEmail = "jane.updated@test.com";
            var modifiedBy = "admin";

            // Act
            user.UpdateProfile(newName, newEmail, modifiedBy);

            // Assert
            user.Name.Should().Be(newName);
            user.Email.Should().Be(newEmail);
            user.ModifiedBy.Should().Be(modifiedBy);
            user.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve lançar exceção ao atualizar perfil com nome inválido")]
        public void UpdateProfile_WithInvalidName_ShouldThrowException()
        {
            // Arrange
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);
            var invalidName = "Jo";

            // Act
            Action act = () => user.UpdateProfile(invalidName, _validEmail, "admin");

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("O nome deve ter pelo menos 5 caracteres.");
        }

        [Fact(DisplayName = "Deve lançar exceção ao atualizar perfil com email inválido")]
        public void UpdateProfile_WithInvalidEmail_ShouldThrowException()
        {
            // Arrange
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);
            var invalidEmail = "invalid-email";

            // Act
            Action act = () => user.UpdateProfile(_validName, invalidEmail, "admin");

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("O e-mail deve ser válido.");
        }

        #endregion

        #region ChangePassword Tests

        [Fact(DisplayName = "Deve alterar senha com hash válido")]
        public void ChangePassword_WithValidHash_ShouldSucceed()
        {
            // Arrange
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);
            var newPasswordHash = "new_hashed_password";
            var modifiedBy = "admin";

            // Act
            user.ChangePassword(newPasswordHash, modifiedBy);

            // Assert
            user.PasswordHash.Should().Be(newPasswordHash);
            user.ModifiedBy.Should().Be(modifiedBy);
            user.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Theory(DisplayName = "Deve lançar exceção ao alterar senha com hash inválido")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ChangePassword_WithInvalidHash_ShouldThrowException(string invalidPasswordHash)
        {
            // Arrange
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);

            // Act
            Action act = () => user.ChangePassword(invalidPasswordHash, "admin");

            // Assert
            act.Should().Throw<DomainExceptionValidation>()
                .WithMessage("A senha não pode ser vazia.");
        }

        #endregion

        #region Collections Tests

        [Fact(DisplayName = "Deve inicializar coleções vazias")]
        public void User_ShouldInitializeEmptyCollections()
        {
            // Act
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);

            // Assert
            user.Recipes.Should().NotBeNull();
            user.Recipes.Should().BeEmpty();
            user.Votes.Should().NotBeNull();
            user.Votes.Should().BeEmpty();
        }

        [Fact(DisplayName = "Deve permitir adicionar receitas")]
        public void User_ShouldAllowAddingRecipes()
        {
            // Arrange
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);
            var recipe = new Recipe("Test Recipe", "Description com 25 caracteres minimo", "instructions com 25 caracteres minimo", 30, _validCreatedBy);

            // Act
            user.Recipes.Add(recipe);

            // Assert
            user.Recipes.Should().ContainSingle();
            user.Recipes.First().Should().Be(recipe);
        }

        [Fact(DisplayName = "Deve permitir adicionar votos")]
        public void User_ShouldAllowAddingVotes()
        {
            // Arrange
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);
            var vote = new Vote(1, 1, true, _validCreatedBy);

            // Act
            user.Votes.Add(vote);

            // Assert
            user.Votes.Should().ContainSingle();
            user.Votes.First().Should().Be(vote);
        }

        #endregion

        #region Audit Fields Tests

        [Fact(DisplayName = "Deve permitir alterar datas e usuários de modificação")]
        public void SetAuditFields_ShouldAllowSettingCreatedAndModified()
        {
            // Arrange
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);
            var createdDate = new DateTime(2025, 10, 20);
            var modifiedBy = "Admin";

            // Act
            user.GetType().GetProperty("CreatedDate")!
                .SetValue(user, createdDate);
            user.GetType().GetProperty("ModifiedBy")!
                .SetValue(user, modifiedBy);

            // Assert
            user.CreatedDate.Should().Be(createdDate);
            user.ModifiedBy.Should().Be(modifiedBy);
        }

        [Fact(DisplayName = "Deve marcar como modificado corretamente")]
        public void MarkAsModified_ShouldUpdateAuditFields()
        {
            // Arrange
            var user = new User(_validName, _validEmail, _validPasswordHash, _validCreatedBy);
            var originalModifiedDate = user.ModifiedDate;
            var modifier = "NewAdmin";

            // Act
            user.MarkAsModified(modifier);

            // Assert
            user.ModifiedBy.Should().Be(modifier);
            user.ModifiedDate.Should().BeAfter(originalModifiedDate);
            user.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        #endregion

        #region Edge Cases

        [Fact(DisplayName = "Deve criar usuário com nome exatamente com 5 caracteres")]
        public void CreateUser_WithNameExactly5Characters_ShouldSucceed()
        {
            // Arrange
            var exactName = "Maria";

            // Act
            var user = new User(exactName, _validEmail, _validPasswordHash, _validCreatedBy);

            // Assert
            user.Name.Should().Be(exactName);
        }

        [Fact(DisplayName = "Deve criar usuário com email contendo múltiplos @")]
        public void CreateUser_WithEmailContainingMultipleAtSymbols_ShouldSucceed()
        {
            // Arrange
            var emailWithMultipleAt = "user@name@domain.com";

            // Act
            var user = new User(_validName, emailWithMultipleAt, _validPasswordHash, _validCreatedBy);

            // Assert
            user.Email.Should().Be(emailWithMultipleAt);
        }

        #endregion
    }
}