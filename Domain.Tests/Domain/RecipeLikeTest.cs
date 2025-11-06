using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class RecipeLikeTest
    {
        private readonly int _validIdUser = 1;
        private readonly int _validIdRecipe = 1;
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "admin";
        private readonly string _validLastModifiedBy = "admin";

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateRecipeLike()
        {
            // Act
            var recipeLike = new RecipeLike(_validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            recipeLike.IdUser.Should().Be(_validIdUser);
            recipeLike.IdRecipe.Should().Be(_validIdRecipe);
            recipeLike.CreatedAt.Should().Be(_validCreatedAt);
            recipeLike.UpdatedAt.Should().Be(_validUpdatedAt);
            recipeLike.CreatedBy.Should().Be(_validCreatedBy);
            recipeLike.LastModifiedBy.Should().Be(_validLastModifiedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateRecipeLikeWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var recipeLike = new RecipeLike(id, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            recipeLike.Id.Should().Be(id);
            recipeLike.IdUser.Should().Be(_validIdUser);
            recipeLike.IdRecipe.Should().Be(_validIdRecipe);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidIdUser_ShouldThrowDomainException(int invalidIdUser)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeLike(invalidIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidIdRecipe_ShouldThrowDomainException(int invalidIdRecipe)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeLike(_validIdUser, invalidIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithInvalidId_ShouldThrowDomainException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeLike(invalidId, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void UpdateRecipeLike_WithValidParameters_ShouldUpdateProperties()
        {
            // Arrange
            var recipeLike = new RecipeLike(_validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var newIdUser = 2;
            var newIdRecipe = 3;
            var modifiedBy = "modifier";
            var originalUpdatedAt = recipeLike.UpdatedAt;

            // Act
            recipeLike.UpdateRecipeLike(newIdUser, newIdRecipe, modifiedBy);

            // Assert
            recipeLike.IdUser.Should().Be(newIdUser);
            recipeLike.IdRecipe.Should().Be(newIdRecipe);
            recipeLike.LastModifiedBy.Should().Be(modifiedBy);
            recipeLike.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        [InlineData(0, 0)]
        public void UpdateRecipeLike_WithInvalidParameters_ShouldThrowDomainException(int invalidIdUser, int invalidIdRecipe)
        {
            // Arrange
            var recipeLike = new RecipeLike(_validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                recipeLike.UpdateRecipeLike(invalidIdUser, invalidIdRecipe, "modifier")
            );
        }

        [Fact]
        public void UpdateRecipeLike_WithInvalidModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var recipeLike = new RecipeLike(_validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var invalidModifiedBy = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                recipeLike.UpdateRecipeLike(2, 3, invalidModifiedBy)
            );
        }

        [Fact]
        public void Equals_WithSameIdUserAndIdRecipe_ShouldReturnTrue()
        {
            // Arrange
            var recipeLike1 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeLike2 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeLike1.Equals(recipeLike2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentIdUser_ShouldReturnFalse()
        {
            // Arrange
            var recipeLike1 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeLike2 = new RecipeLike(2, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeLike1.Equals(recipeLike2).Should().BeFalse();
        }

        [Fact]
        public void Equals_WithDifferentIdRecipe_ShouldReturnFalse()
        {
            // Arrange
            var recipeLike1 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeLike2 = new RecipeLike(1, 2, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeLike1.Equals(recipeLike2).Should().BeFalse();
        }

        [Fact]
        public void Equals_WithNull_ShouldReturnFalse()
        {
            // Arrange
            var recipeLike = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeLike.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void Equals_WithDifferentType_ShouldReturnFalse()
        {
            // Arrange
            var recipeLike = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var differentObject = new object();

            // Act & Assert
            recipeLike.Equals(differentObject).Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_WithSameIdUserAndIdRecipe_ShouldReturnSameHash()
        {
            // Arrange
            var recipeLike1 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeLike2 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeLike1.GetHashCode().Should().Be(recipeLike2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WithDifferentIdUser_ShouldReturnDifferentHash()
        {
            // Arrange
            var recipeLike1 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeLike2 = new RecipeLike(2, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeLike1.GetHashCode().Should().NotBe(recipeLike2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WithDifferentIdRecipe_ShouldReturnDifferentHash()
        {
            // Arrange
            var recipeLike1 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeLike2 = new RecipeLike(1, 2, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeLike1.GetHashCode().Should().NotBe(recipeLike2.GetHashCode());
        }

        [Fact]
        public void Constructor_WithInvalidAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var invalidCreatedBy = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeLike(_validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt, invalidCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithInvalidDates_ShouldThrowDomainException()
        {
            // Arrange
            var futureCreatedAt = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeLike(_validIdUser, _validIdRecipe, futureCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithSpacesInAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var createdByWithSpaces = "  admin  ";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeLike(_validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt, createdByWithSpaces, _validLastModifiedBy)
            );
        }

        [Fact]
        public void RecipeLike_ShouldNotAllowDuplicateUserAndRecipeCombination()
        {
            // Arrange
            var recipeLike1 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeLike2 = new RecipeLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeLike1.Equals(recipeLike2).Should().BeTrue();
            recipeLike1.GetHashCode().Should().Be(recipeLike2.GetHashCode());
        }
    }
}