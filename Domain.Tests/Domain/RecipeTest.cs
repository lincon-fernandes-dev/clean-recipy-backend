using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Domain.Tests.Entities
{
    public class RecipeTests
    {
        private readonly string _validTitle = "Bolo de Chocolate Delicioso";
        private readonly string _validDescription = "Um bolo de chocolate incrível com cacau 100% e cobertura cremosa. Perfect for celebrations and family gatherings.";
        private readonly IEnumerable<Instruction> _validInstructions;
        private readonly int _validUserId = 1;
        private readonly string _validImageUrl = "https://example.com/bolo-chocolate.jpg";
        private readonly int _validPreparationTime = 45;
        private readonly int _validServings = 8;
        private readonly string _validDifficulty = "Fácil";
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "chef_john";
        private readonly string _validLastModifiedBy = "chef_john";

        public RecipeTests()
        {
            _validInstructions = new List<Instruction>
            {
                new Instruction(1, "Pré-aqueça o forno a 180°C", 1, DateTime.UtcNow, DateTime.UtcNow, "system", "system"),
                new Instruction(2, "Misture os ingredientes secos", 2, DateTime.UtcNow, DateTime.UtcNow, "system", "system"),
                new Instruction(3, "Adicione os ingredientes líquidos", 3, DateTime.UtcNow, DateTime.UtcNow, "system", "system")
            };
        }

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateRecipe()
        {
            // Act
            var recipe = new Recipe(
                _validTitle, _validDescription, _validUserId, _validImageUrl,
                _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            recipe.Title.Should().Be(_validTitle);
            recipe.Description.Should().Be(_validDescription);
            recipe.IdUser.Should().Be(_validUserId);
            recipe.ImageUrl.Should().Be(_validImageUrl);
            recipe.PreparationTime.Should().Be(_validPreparationTime);
            recipe.Servings.Should().Be(_validServings);
            recipe.Difficulty.Should().Be(_validDifficulty);
            recipe.CreatedAt.Should().Be(_validCreatedAt);
            recipe.UpdatedAt.Should().Be(_validUpdatedAt);
            recipe.CreatedBy.Should().Be(_validCreatedBy);
            recipe.LastModifiedBy.Should().Be(_validLastModifiedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateRecipeWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var recipe = new Recipe(
                id, _validTitle, _validDescription, _validUserId, _validImageUrl,
                _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            recipe.Id.Should().Be(id);
            recipe.Title.Should().Be(_validTitle);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidTitle_ShouldThrowDomainException(string invalidTitle)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    invalidTitle, _validDescription, _validUserId, _validImageUrl,
                    _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithShortTitle_ShouldThrowDomainException()
        {
            // Arrange
            var shortTitle = "Ab";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    shortTitle, _validDescription, _validUserId, _validImageUrl,
                    _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithLongTitle_ShouldThrowDomainException()
        {
            // Arrange
            var longTitle = new string('A', 101);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    longTitle, _validDescription, _validUserId, _validImageUrl,
                    _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidDescription_ShouldThrowDomainException(string invalidDescription)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    _validTitle, invalidDescription, _validUserId, _validImageUrl,
                    _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithShortDescription_ShouldThrowDomainException()
        {
            // Arrange
            var shortDescription = "Descrição muito curta";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    _validTitle, shortDescription, _validUserId, _validImageUrl,
                    _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithLongDescription_ShouldThrowDomainException()
        {
            // Arrange
            var longDescription = new string('A', 525);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    _validTitle, longDescription, _validUserId, _validImageUrl,
                    _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidUserId_ShouldThrowDomainException(int invalidUserId)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    _validTitle, _validDescription, invalidUserId, _validImageUrl,
                    _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1441)]
        [InlineData(10000)]
        public void Constructor_WithInvalidPreparationTime_ShouldThrowDomainException(int invalidPreparationTime)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    _validTitle, _validDescription, _validUserId, _validImageUrl,
                    invalidPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(101)]
        [InlineData(1000)]
        public void Constructor_WithInvalidServings_ShouldThrowDomainException(int invalidServings)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    _validTitle, _validDescription, _validUserId, _validImageUrl,
                    _validPreparationTime, invalidServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        [InlineData("Muito Fácil")]
        [InlineData("Hard")]
        [InlineData("Expert")]
        public void Constructor_WithInvalidDifficulty_ShouldThrowDomainException(string invalidDifficulty)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    _validTitle, _validDescription, _validUserId, _validImageUrl,
                    _validPreparationTime, _validServings, invalidDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Update_WithValidParameters_ShouldUpdateProperties()
        {
            // Arrange
            var recipe = CreateValidRecipe();
            var newTitle = "Bolo de Chocolate Premium";
            var newDescription = "Uma versão premium do bolo de chocolate com ingredientes selecionados.";
            var newInstructions = new List<Instruction>
            {
                new Instruction(1, "Novo passo 1", 1, DateTime.UtcNow, DateTime.UtcNow, "system", "system"),
                new Instruction(2, "Novo passo 2", 2, DateTime.UtcNow, DateTime.UtcNow, "system", "system")
            };
            var newImageUrl = "https://example.com/novo-bolo.jpg";
            var newPreparationTime = 60;
            var newServings = 10;
            var newDifficulty = "Médio";
            var modifiedBy = "chef_maria";
            var originalUpdatedAt = recipe.UpdatedAt;

            // Act
            recipe.Update(newTitle, newDescription, newInstructions, newImageUrl, newPreparationTime, newServings, newDifficulty, modifiedBy);

            // Assert
            recipe.Title.Should().Be(newTitle);
            recipe.Description.Should().Be(newDescription);
            recipe.Instructions.Should().HaveCount(2);
            recipe.ImageUrl.Should().Be(newImageUrl);
            recipe.PreparationTime.Should().Be(newPreparationTime);
            recipe.Servings.Should().Be(newServings);
            recipe.Difficulty.Should().Be(newDifficulty);
            recipe.LastModifiedBy.Should().Be(modifiedBy);
            recipe.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Fact]
        public void Update_WithInvalidModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var recipe = CreateValidRecipe();
            var invalidModifiedBy = "abc"; // Menos de 4 caracteres

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                recipe.Update(_validTitle, _validDescription, _validInstructions, _validImageUrl,
                    _validPreparationTime, _validServings, _validDifficulty, invalidModifiedBy)
            );
        }

        [Fact]
        public void UpdateTitle_WithValidTitle_ShouldUpdateTitle()
        {
            // Arrange
            var recipe = CreateValidRecipe();
            var newTitle = "Novo Título da Receita";
            var modifiedBy = "chef_maria";

            // Act
            recipe.UpdateTitle(newTitle, modifiedBy);

            // Assert
            recipe.Title.Should().Be(newTitle);
            recipe.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void UpdateNutritionInfo_WithValidNutritionInfo_ShouldUpdateNutritionInfo()
        {
            // Arrange
            var recipe = CreateValidRecipe();
            var nutritionInfo = new NutritionInfo(
                1, 500, 25, 60, 15, DateTime.UtcNow, DateTime.UtcNow, "system", "system"
            );
            var modifiedBy = "chef_maria";

            // Act
            recipe.UpdateNutritionInfo(nutritionInfo, modifiedBy);

            // Assert
            recipe.NutritionInfo.Should().Be(nutritionInfo);
            recipe.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void AddIngredient_ShouldAddIngredientToRecipe()
        {
            // Arrange
            var recipe = CreateValidRecipe();
            var ingredient = new Ingredient(
                "2 xícaras de Farinha", 1, DateTime.UtcNow, DateTime.UtcNow, "system", "system"
            );
            var modifiedBy = "chef_maria";

            // Act
            recipe.AddIngredient(ingredient, modifiedBy);

            // Assert
            recipe.Ingredients.Should().Contain(ingredient);
            recipe.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void RemoveIngredient_ShouldRemoveIngredientFromRecipe()
        {
            // Arrange
            var recipe = CreateValidRecipe();
            var ingredient = new Ingredient(
                "2 xícaras de Farinha", 1, DateTime.UtcNow, DateTime.UtcNow, "system", "system"
            );
            var modifiedBy = "chef_maria";
            recipe.AddIngredient(ingredient, modifiedBy);

            // Act
            recipe.RemoveIngredient(ingredient, modifiedBy);

            // Assert
            recipe.Ingredients.Should().NotContain(ingredient);
        }

        [Fact]
        public void GetLikesCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var recipe = CreateValidRecipe();

            // Act
            var likesCount = recipe.GetLikesCount();

            // Assert
            likesCount.Should().Be(0);
        }

        [Fact]
        public void GetCommentsCount_ShouldReturnCorrectCount()
        {
            // Arrange
            var recipe = CreateValidRecipe();

            // Act
            var commentsCount = recipe.GetCommentsCount();

            // Assert
            commentsCount.Should().Be(0);
        }

        [Fact]
        public void HasTags_WithNoTags_ShouldReturnFalse()
        {
            // Arrange
            var recipe = CreateValidRecipe();

            // Act
            var hasTags = recipe.HasTags();

            // Assert
            hasTags.Should().BeFalse();
        }

        [Fact]
        public void HasIngredients_WithNoIngredients_ShouldReturnFalse()
        {
            // Arrange
            var recipe = CreateValidRecipe();

            // Act
            var hasIngredients = recipe.HasIngredients();

            // Assert
            hasIngredients.Should().BeFalse();
        }

        [Fact]
        public void Constructor_WithEmptyImageUrl_ShouldCreateRecipe()
        {
            // Arrange
            var emptyImageUrl = "";

            // Act
            var recipe = new Recipe(
                _validTitle, _validDescription, _validUserId, emptyImageUrl,
                _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            recipe.ImageUrl.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_WithInvalidImageUrl_ShouldThrowDomainException()
        {
            // Arrange
            var invalidImageUrl = "http.invalid-url";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Recipe(
                    _validTitle, _validDescription, _validUserId, invalidImageUrl,
                    _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        private Recipe CreateValidRecipe()
        {
            return new Recipe(
                _validTitle, _validDescription, _validUserId, _validImageUrl,
                _validPreparationTime, _validServings, _validDifficulty, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );
        }
    }
}