using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class RecipeTagTest
    {
        private readonly int _validIdTag = 1;
        private readonly int _validIdRecipe = 1;
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "admin";
        private readonly string _validLastModifiedBy = "admin";

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateRecipeTag()
        {
            // Act
            var recipeTag = new RecipeTag(_validIdTag, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            recipeTag.IdTag.Should().Be(_validIdTag);
            recipeTag.IdRecipe.Should().Be(_validIdRecipe);
            recipeTag.CreatedAt.Should().Be(_validCreatedAt);
            recipeTag.UpdatedAt.Should().Be(_validUpdatedAt);
            recipeTag.CreatedBy.Should().Be(_validCreatedBy);
            recipeTag.LastModifiedBy.Should().Be(_validLastModifiedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateRecipeTagWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var recipeTag = new RecipeTag(id, _validIdTag, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            recipeTag.Id.Should().Be(id);
            recipeTag.IdTag.Should().Be(_validIdTag);
            recipeTag.IdRecipe.Should().Be(_validIdRecipe);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidIdTag_ShouldThrowDomainException(int invalidIdTag)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeTag(invalidIdTag, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
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
                new RecipeTag(_validIdTag, invalidIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithInvalidId_ShouldThrowDomainException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeTag(invalidId, _validIdTag, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void UpdateRecipeTag_WithValidParameters_ShouldUpdateProperties()
        {
            // Arrange
            var recipeTag = new RecipeTag(_validIdTag, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var newIdTag = 2;
            var newIdRecipe = 3;
            var modifiedBy = "modifier";
            var originalUpdatedAt = recipeTag.UpdatedAt;

            // Act
            recipeTag.UpdateRecipeTag(newIdTag, newIdRecipe, modifiedBy);

            // Assert
            recipeTag.IdTag.Should().Be(newIdTag);
            recipeTag.IdRecipe.Should().Be(newIdRecipe);
            recipeTag.LastModifiedBy.Should().Be(modifiedBy);
            recipeTag.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        [InlineData(0, 0)]
        public void UpdateRecipeTag_WithInvalidParameters_ShouldThrowDomainException(int invalidIdTag, int invalidIdRecipe)
        {
            // Arrange
            var recipeTag = new RecipeTag(_validIdTag, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                recipeTag.UpdateRecipeTag(invalidIdTag, invalidIdRecipe, "modifier")
            );
        }

        [Fact]
        public void UpdateRecipeTag_WithInvalidModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var recipeTag = new RecipeTag(_validIdTag, _validIdRecipe, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var invalidModifiedBy = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                recipeTag.UpdateRecipeTag(2, 3, invalidModifiedBy)
            );
        }

        [Fact]
        public void Equals_WithSameIdTagAndIdRecipe_ShouldReturnTrue()
        {
            // Arrange
            var recipeTag1 = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeTag2 = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeTag1.Equals(recipeTag2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentIdTag_ShouldReturnFalse()
        {
            // Arrange
            var recipeTag1 = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeTag2 = new RecipeTag(2, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeTag1.Equals(recipeTag2).Should().BeFalse();
        }

        [Fact]
        public void Equals_WithDifferentIdRecipe_ShouldReturnFalse()
        {
            // Arrange
            var recipeTag1 = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeTag2 = new RecipeTag(1, 2, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeTag1.Equals(recipeTag2).Should().BeFalse();
        }

        [Fact]
        public void Equals_WithNull_ShouldReturnFalse()
        {
            // Arrange
            var recipeTag = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeTag.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void Equals_WithDifferentType_ShouldReturnFalse()
        {
            // Arrange
            var recipeTag = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var differentObject = new object();

            // Act & Assert
            recipeTag.Equals(differentObject).Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_WithSameIdTagAndIdRecipe_ShouldReturnSameHash()
        {
            // Arrange
            var recipeTag1 = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeTag2 = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeTag1.GetHashCode().Should().Be(recipeTag2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WithDifferentIdTag_ShouldReturnDifferentHash()
        {
            // Arrange
            var recipeTag1 = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeTag2 = new RecipeTag(2, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeTag1.GetHashCode().Should().NotBe(recipeTag2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WithDifferentIdRecipe_ShouldReturnDifferentHash()
        {
            // Arrange
            var recipeTag1 = new RecipeTag(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var recipeTag2 = new RecipeTag(1, 2, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            recipeTag1.GetHashCode().Should().NotBe(recipeTag2.GetHashCode());
        }

        [Fact]
        public void Constructor_WithInvalidAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var invalidCreatedBy = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeTag(_validIdTag, _validIdRecipe, _validCreatedAt, _validUpdatedAt, invalidCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithInvalidDates_ShouldThrowDomainException()
        {
            // Arrange
            var futureCreatedAt = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeTag(_validIdTag, _validIdRecipe, futureCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithSpacesInAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var createdByWithSpaces = "  admin  ";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new RecipeTag(_validIdTag, _validIdRecipe, _validCreatedAt, _validUpdatedAt, createdByWithSpaces, _validLastModifiedBy)
            );
        }
    }
}