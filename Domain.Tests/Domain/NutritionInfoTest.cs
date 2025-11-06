using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class NutritionInfoTest
    {
        private readonly int _validIdRecipe = 1;
        private readonly int _validCalories = 500;
        private readonly int _validProteins = 25;
        private readonly int _validCarbs = 60;
        private readonly int _validFat = 15;
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "admin";
        private readonly string _validLastModifiedBy = "admin";

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateNutritionInfo()
        {
            // Act
            var nutritionInfo = new NutritionInfo(
                _validIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            nutritionInfo.IdRecipe.Should().Be(_validIdRecipe);
            nutritionInfo.Calories.Should().Be(_validCalories);
            nutritionInfo.Proteins.Should().Be(_validProteins);
            nutritionInfo.Carbs.Should().Be(_validCarbs);
            nutritionInfo.Fat.Should().Be(_validFat);
            nutritionInfo.CreatedAt.Should().Be(_validCreatedAt);
            nutritionInfo.UpdatedAt.Should().Be(_validUpdatedAt);
            nutritionInfo.CreatedBy.Should().Be(_validCreatedBy);
            nutritionInfo.LastModifiedBy.Should().Be(_validLastModifiedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateNutritionInfoWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var nutritionInfo = new NutritionInfo(
                id, _validIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            nutritionInfo.Id.Should().Be(id);
            nutritionInfo.IdRecipe.Should().Be(_validIdRecipe);
            nutritionInfo.Calories.Should().Be(_validCalories);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidIdRecipe_ShouldThrowDomainException(int invalidIdRecipe)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    invalidIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithNegativeCalories_ShouldThrowDomainException(int invalidCalories)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    _validIdRecipe, invalidCalories, _validProteins, _validCarbs, _validFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithNegativeProteins_ShouldThrowDomainException(int invalidProteins)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    _validIdRecipe, _validCalories, invalidProteins, _validCarbs, _validFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithNegativeCarbs_ShouldThrowDomainException(int invalidCarbs)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    _validIdRecipe, _validCalories, _validProteins, invalidCarbs, _validFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithNegativeFat_ShouldThrowDomainException(int invalidFat)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    _validIdRecipe, _validCalories, _validProteins, _validCarbs, invalidFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithZeroValues_ShouldCreateNutritionInfo()
        {
            // Act
            var nutritionInfo = new NutritionInfo(
                _validIdRecipe, 0, 0, 0, 0,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            nutritionInfo.Calories.Should().Be(0);
            nutritionInfo.Proteins.Should().Be(0);
            nutritionInfo.Carbs.Should().Be(0);
            nutritionInfo.Fat.Should().Be(0);
        }

        [Fact]
        public void Constructor_WithHighButValidValues_ShouldCreateNutritionInfo()
        {
            // Arrange
            var highCalories = 5000;
            var highProteins = 500;
            var highCarbs = 1000;
            var highFat = 500;

            // Act
            var nutritionInfo = new NutritionInfo(
                _validIdRecipe, highCalories, highProteins, highCarbs, highFat,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            nutritionInfo.Calories.Should().Be(highCalories);
            nutritionInfo.Proteins.Should().Be(highProteins);
            nutritionInfo.Carbs.Should().Be(highCarbs);
            nutritionInfo.Fat.Should().Be(highFat);
        }

        [Fact]
        public void Constructor_WithExcessiveCalories_ShouldThrowDomainException()
        {
            // Arrange
            var excessiveCalories = 10001;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    _validIdRecipe, excessiveCalories, _validProteins, _validCarbs, _validFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithExcessiveProteins_ShouldThrowDomainException()
        {
            // Arrange
            var excessiveProteins = 1001;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    _validIdRecipe, _validCalories, excessiveProteins, _validCarbs, _validFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithExcessiveCarbs_ShouldThrowDomainException()
        {
            // Arrange
            var excessiveCarbs = 2001;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    _validIdRecipe, _validCalories, _validProteins, excessiveCarbs, _validFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithExcessiveFat_ShouldThrowDomainException()
        {
            // Arrange
            var excessiveFat = 1001;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    _validIdRecipe, _validCalories, _validProteins, _validCarbs, excessiveFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void UpdateNutritionInfo_WithValidParameters_ShouldUpdateProperties()
        {
            // Arrange
            var nutritionInfo = new NutritionInfo(
                _validIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );
            var newCalories = 600;
            var newProteins = 30;
            var newCarbs = 70;
            var newFat = 20;
            var modifiedBy = "modifier";
            var originalUpdatedAt = nutritionInfo.UpdatedAt;

            // Act
            nutritionInfo.UpdateNutritionInfo(newCalories, newProteins, newCarbs, newFat, modifiedBy);

            // Assert
            nutritionInfo.Calories.Should().Be(newCalories);
            nutritionInfo.Proteins.Should().Be(newProteins);
            nutritionInfo.Carbs.Should().Be(newCarbs);
            nutritionInfo.Fat.Should().Be(newFat);
            nutritionInfo.LastModifiedBy.Should().Be(modifiedBy);
            nutritionInfo.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Fact]
        public void UpdateRecipe_WithValidIdRecipe_ShouldUpdateIdRecipe()
        {
            // Arrange
            var nutritionInfo = new NutritionInfo(
                _validIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );
            var newIdRecipe = 2;
            var modifiedBy = "modifier";

            // Act
            nutritionInfo.UpdateRecipe(newIdRecipe, modifiedBy);

            // Assert
            nutritionInfo.IdRecipe.Should().Be(newIdRecipe);
            nutritionInfo.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void UpdateRecipe_WithInvalidIdRecipe_ShouldThrowDomainException()
        {
            // Arrange
            var nutritionInfo = new NutritionInfo(
                _validIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );
            var invalidIdRecipe = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                nutritionInfo.UpdateRecipe(invalidIdRecipe, "modifier")
            );
        }

        [Fact]
        public void GetTotalCalories_ShouldReturnCaloriesValue()
        {
            // Arrange
            var nutritionInfo = new NutritionInfo(
                _validIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );

            // Act
            var totalCalories = nutritionInfo.GetTotalCalories();

            // Assert
            totalCalories.Should().Be(_validCalories);
        }

        [Fact]
        public void GetNutritionSummary_ShouldReturnFormattedString()
        {
            // Arrange
            var nutritionInfo = new NutritionInfo(
                _validIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );

            // Act
            var summary = nutritionInfo.GetNutritionSummary();

            // Assert
            summary.Should().Be($"Calorias: {_validCalories}kcal, Proteínas: {_validProteins}g, Carboidratos: {_validCarbs}g, Gorduras: {_validFat}g");
        }

        [Fact]
        public void Constructor_WithInvalidId_ShouldThrowDomainException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new NutritionInfo(
                    invalidId, _validIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                    _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
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
                new NutritionInfo(
                    _validIdRecipe, _validCalories, _validProteins, _validCarbs, _validFat,
                    _validCreatedAt, _validUpdatedAt, invalidCreatedBy, _validLastModifiedBy
                )
            );
        }
    }
}