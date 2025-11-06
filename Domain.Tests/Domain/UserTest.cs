using Domain.Entities;
using Domain.Validation;
using System;
using FluentAssertions;
using Xunit;
using Domain.Enums;

namespace Domain.Tests.Entities
{
    public class UserTests
    {
        private readonly string _validName = "John Doe Test";
        private readonly string _validEmail = "john@test.com";
        private readonly string _validPasswordHash = "hashed_password";
        private readonly string _validCreatedBy = "joe doe";
        private readonly string _validAvatar = "https://example.com/avatar.jpg";
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateUser()
        {
            // Act
            var user = new User(
                _validName,
                _validEmail,
                _validPasswordHash,
                _validAvatar,
                false,
                UserStatus.Active,
                _validCreatedAt,
                _validUpdatedAt,
                _validCreatedBy,
                _validCreatedBy
            );

            // Assert
            user.Name.Should().Be(_validName);
            user.Email.Should().Be(_validEmail);
            user.PasswordHash.Should().Be(_validPasswordHash);
            user.Avatar.Should().Be(_validAvatar);
            user.IsVerified.Should().BeFalse();
            user.Status.Should().Be(UserStatus.Active);
            user.CreatedAt.Should().Be(_validCreatedAt);
            user.UpdatedAt.Should().Be(_validUpdatedAt);
            user.CreatedBy.Should().Be(_validCreatedBy);
            user.LastModifiedBy.Should().Be(_validCreatedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateUserWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var user = new User(
                id,
                _validName,
                _validEmail,
                _validPasswordHash,
                _validAvatar,
                true,
                UserStatus.Inactive,
                _validCreatedAt,
                _validUpdatedAt,
                _validCreatedBy,
                _validCreatedBy
            );

            // Assert
            user.Id.Should().Be(id);
            user.Name.Should().Be(_validName);
            user.Email.Should().Be(_validEmail);
            user.IsVerified.Should().BeTrue();
            user.Status.Should().Be(UserStatus.Inactive);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidName_ShouldThrowDomainExceptionValidation(string invalidName)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    invalidName,
                    _validEmail,
                    _validPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Theory]
        [InlineData("Jo")]
        [InlineData("A")]
        [InlineData("Ab")]
        public void Constructor_WithShortName_ShouldThrowDomainExceptionValidation(string shortName)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    shortName,
                    _validEmail,
                    _validPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithLongName_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var longName = new string('A', 101);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    longName,
                    _validEmail,
                    _validPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidEmail_ShouldThrowDomainExceptionValidation(string invalidEmail)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    _validName,
                    invalidEmail,
                    _validPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Theory]
        [InlineData("invalid-email")]
        [InlineData("invalid@")]
        [InlineData("@gmail.com")]
        [InlineData("test @gmail.com")]
        [InlineData("test@ gmail.com")]
        [InlineData("test@gmail .com")]
        public void Constructor_WithInvalidEmailFormat_ShouldThrowDomainExceptionValidation(string invalidEmail)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    _validName,
                    invalidEmail,
                    _validPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Theory]
        [InlineData("test@domain")]
        [InlineData("test@domain.")]
        [InlineData("test@.com")]
        public void Constructor_WithInvalidEmailDomain_ShouldThrowDomainExceptionValidation(string invalidEmail)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    _validName,
                    invalidEmail,
                    _validPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidPasswordHash_ShouldThrowDomainExceptionValidation(string invalidPasswordHash)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    _validName,
                    _validEmail,
                    invalidPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithShortPasswordHash_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var shortPasswordHash = "short";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    _validName,
                    _validEmail,
                    shortPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithLongPasswordHash_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var longPasswordHash = new string('A', 501);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    _validName,
                    _validEmail,
                    longPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithInvalidId_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new User(
                    invalidId,
                    _validName,
                    _validEmail,
                    _validPasswordHash,
                    _validAvatar,
                    false,
                    UserStatus.Active,
                    _validCreatedAt,
                    _validUpdatedAt,
                    _validCreatedBy,
                    _validCreatedBy
                )
            );
        }

        [Fact]
        public void UpdateProfile_WithValidParameters_ShouldUpdateProperties()
        {
            // Arrange
            var user = CreateValidUser();
            var newName = "Jane Doe Updated";
            var newEmail = "jane.updated@test.com";
            var modifiedBy = "admin";

            // Act
            user.UpdateProfile(newName, newEmail, modifiedBy);

            // Assert
            user.Name.Should().Be(newName);
            user.Email.Should().Be(newEmail);
            user.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void UpdateProfile_WithInvalidName_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var user = CreateValidUser();
            var invalidName = "A";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                user.UpdateProfile(invalidName, _validEmail, "admin")
            );
        }

        [Fact]
        public void UpdateProfile_WithInvalidEmail_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var user = CreateValidUser();
            var invalidEmail = "invalid-email";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                user.UpdateProfile(_validName, invalidEmail, "admin")
            );
        }

        [Fact]
        public void ChangePassword_WithValidPasswordHash_ShouldUpdatePassword()
        {
            // Arrange
            var user = CreateValidUser();
            var newPasswordHash = "new_hashed_password";
            var modifiedBy = "admin";

            // Act
            user.ChangePassword(newPasswordHash, modifiedBy);

            // Assert
            user.PasswordHash.Should().Be(newPasswordHash);
            user.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void ChangePassword_WithInvalidPasswordHash_ShouldThrowDomainExceptionValidation(string invalidPasswordHash)
        {
            // Arrange
            var user = CreateValidUser();

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                user.ChangePassword(invalidPasswordHash, "admin")
            );
        }

        [Fact]
        public void UpdateAvatar_WithValidAvatar_ShouldUpdateAvatar()
        {
            // Arrange
            var user = CreateValidUser();
            var newAvatar = "https://example.com/new-avatar.jpg";
            var modifiedBy = "admin";

            // Act
            user.UpdateAvatar(newAvatar, modifiedBy);

            // Assert
            user.Avatar.Should().Be(newAvatar);
            user.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void UpdateAvatar_WithLongUrl_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var user = CreateValidUser();
            var longAvatar = new string('A', 501);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                user.UpdateAvatar(longAvatar, "admin")
            );
        }

        [Fact]
        public void UpdateAvatar_WithInvalidUrl_ShouldThrowDomainExceptionValidation()
        {
            // Arrange
            var user = CreateValidUser();
            var invalidAvatar = "http.invalid-url";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                user.UpdateAvatar(invalidAvatar, "admin")
            );
        }

        [Fact]
        public void ChangeStatus_ShouldUpdateStatus()
        {
            // Arrange
            var user = CreateValidUser();
            var newStatus = UserStatus.Inactive;
            var modifiedBy = "admin";

            // Act
            user.ChangeStatus(newStatus, modifiedBy);

            // Assert
            user.Status.Should().Be(newStatus);
            user.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void VerifyAccount_ShouldSetIsVerifiedToTrue()
        {
            // Arrange
            var user = CreateValidUser();
            var modifiedBy = "admin";

            // Act
            user.VerifyAccount(modifiedBy);

            // Assert
            user.IsVerified.Should().BeTrue();
            user.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void Constructor_WithValidLongEmail_ShouldCreateUser()
        {
            // Arrange
            var longEmail = new string('a', 230) + "@test.com";

            // Act & Assert
            var user = new User(
                _validName,
                longEmail,
                _validPasswordHash,
                _validAvatar,
                false,
                UserStatus.Active,
                _validCreatedAt,
                _validUpdatedAt,
                _validCreatedBy,
                _validCreatedBy
            );

            user.Email.Should().Be(longEmail);
        }

        [Fact]
        public void Constructor_WithEmptyAvatar_ShouldCreateUser()
        {
            // Act
            var user = new User(
                _validName,
                _validEmail,
                _validPasswordHash,
                "",
                false,
                UserStatus.Active,
                _validCreatedAt,
                _validUpdatedAt,
                _validCreatedBy,
                _validCreatedBy
            );

            // Assert
            user.Avatar.Should().BeEmpty();
        }

        private User CreateValidUser()
        {
            return new User(
                _validName,
                _validEmail,
                _validPasswordHash,
                _validAvatar,
                false,
                UserStatus.Active,
                _validCreatedAt,
                _validUpdatedAt,
                _validCreatedBy,
                _validCreatedBy
            );
        }
    }
}