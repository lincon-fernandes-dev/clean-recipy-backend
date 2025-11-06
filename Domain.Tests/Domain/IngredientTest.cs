using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class IngredientTests
    {
        private readonly string _validName = "2 xícaras de farinha de trigo";
        private readonly int _validIdRecipe = 1;
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "chef_john";
        private readonly string _validLastModifiedBy = "chef_john";

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateIngredient()
        {
            // Act
            var ingredient = new Ingredient(
                _validName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            ingredient.Name.Should().Be(_validName);
            ingredient.IdRecipe.Should().Be(_validIdRecipe);
            ingredient.CreatedAt.Should().Be(_validCreatedAt);
            ingredient.UpdatedAt.Should().Be(_validUpdatedAt);
            ingredient.CreatedBy.Should().Be(_validCreatedBy);
            ingredient.LastModifiedBy.Should().Be(_validLastModifiedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateIngredientWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var ingredient = new Ingredient(
                id, _validName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            ingredient.Id.Should().Be(id);
            ingredient.Name.Should().Be(_validName);
            ingredient.IdRecipe.Should().Be(_validIdRecipe);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidName_ShouldThrowDomainException(string invalidName)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Ingredient(
                    invalidName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithShortName_ShouldThrowDomainException()
        {
            // Arrange
            var shortName = "A";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Ingredient(
                    shortName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithLongName_ShouldThrowDomainException()
        {
            // Arrange
            var longName = new string('A', 201);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Ingredient(
                    longName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
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
                new Ingredient(
                    _validName, invalidIdRecipe, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithSpacesInName_ShouldTrimName()
        {
            // Arrange
            var nameWithSpaces = "  2 xícaras de farinha de trigo  ";

            // Act
            var ingredient = new Ingredient(
                nameWithSpaces, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            ingredient.Name.Should().Be("2 xícaras de farinha de trigo");
        }

        [Fact]
        public void UpdateName_WithValidName_ShouldUpdateName()
        {
            // Arrange
            var ingredient = CreateValidIngredient();
            var newName = "3 colheres de sopa de açúcar";
            var modifiedBy = "chef_maria";
            var originalUpdatedAt = ingredient.UpdatedAt;

            // Act
            ingredient.UpdateName(newName, modifiedBy);

            // Assert
            ingredient.Name.Should().Be(newName);
            ingredient.LastModifiedBy.Should().Be(modifiedBy);
            ingredient.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Fact]
        public void UpdateName_WithInvalidName_ShouldThrowDomainException()
        {
            // Arrange
            var ingredient = CreateValidIngredient();
            var invalidName = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                ingredient.UpdateName(invalidName, "chef_maria")
            );
        }

        [Fact]
        public void UpdateRecipe_WithValidIdRecipe_ShouldUpdateIdRecipe()
        {
            // Arrange
            var ingredient = CreateValidIngredient();
            var newIdRecipe = 2;
            var modifiedBy = "chef_maria";
            var originalUpdatedAt = ingredient.UpdatedAt;

            // Act
            ingredient.UpdateRecipe(newIdRecipe, modifiedBy);

            // Assert
            ingredient.IdRecipe.Should().Be(newIdRecipe);
            ingredient.LastModifiedBy.Should().Be(modifiedBy);
            ingredient.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Fact]
        public void UpdateRecipe_WithInvalidIdRecipe_ShouldThrowDomainException()
        {
            // Arrange
            var ingredient = CreateValidIngredient();
            var invalidIdRecipe = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                ingredient.UpdateRecipe(invalidIdRecipe, "chef_maria")
            );
        }

        [Fact]
        public void UpdateName_WithInvalidModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var ingredient = CreateValidIngredient();
            var invalidModifiedBy = "ab"; // Menos de 3 caracteres

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                ingredient.UpdateName("Novo nome válido", invalidModifiedBy)
            );
        }

        [Fact]
        public void UpdateName_WithLongModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var ingredient = CreateValidIngredient();
            var longModifiedBy = new string('A', 101); // Mais de 100 caracteres

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                ingredient.UpdateName("Novo nome válido", longModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithValidBoundaryValues_ShouldCreateIngredient()
        {
            // Arrange
            var minName = "Sal";
            var maxName = new string('A', 200); // 200 caracteres

            // Act & Assert
            var ingredient1 = new Ingredient(
                minName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );
            var ingredient2 = new Ingredient(
                maxName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            ingredient1.Name.Should().Be(minName);
            ingredient2.Name.Should().Be(maxName);
        }

        [Fact]
        public void Constructor_WithInvalidId_ShouldThrowDomainException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Ingredient(
                    invalidId, _validName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
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
                new Ingredient(
                    _validName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                    invalidCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithSpacesInAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var createdByWithSpaces = "  chef_john  ";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Ingredient(
                    _validName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                    createdByWithSpaces, _validLastModifiedBy
                )
            );
        }

        private Ingredient CreateValidIngredient()
        {
            return new Ingredient(
                _validName, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );
        }
    }
}