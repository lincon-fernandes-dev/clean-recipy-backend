using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class TagTest
    {
        private readonly string _validTitle = "Sobremesa";
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "admin";
        private readonly string _validLastModifiedBy = "admin";

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateTag()
        {
            // Act
            var tag = new Tag(_validTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            tag.Title.Should().Be(_validTitle);
            tag.CreatedAt.Should().Be(_validCreatedAt);
            tag.UpdatedAt.Should().Be(_validUpdatedAt);
            tag.CreatedBy.Should().Be(_validCreatedBy);
            tag.LastModifiedBy.Should().Be(_validLastModifiedBy);
            tag.RecipeTags.Should().NotBeNull();
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateTagWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var tag = new Tag(id, _validTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            tag.Id.Should().Be(id);
            tag.Title.Should().Be(_validTitle);
            tag.CreatedAt.Should().Be(_validCreatedAt);
            tag.UpdatedAt.Should().Be(_validUpdatedAt);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidTitle_ShouldThrowDomainException(string invalidTitle)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Tag(invalidTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithShortTitle_ShouldThrowDomainException()
        {
            // Arrange
            var shortTitle = "A";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Tag(shortTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithLongTitle_ShouldThrowDomainException()
        {
            // Arrange
            var longTitle = new string('A', 129);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Tag(longTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Theory]
        [InlineData("Tag@Invalid")]
        [InlineData("Tag#Invalid")]
        [InlineData("Tag$Invalid")]
        [InlineData("Tag%Invalid")]
        public void Constructor_WithInvalidCharacters_ShouldThrowDomainException(string invalidTitle)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Tag(invalidTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithSpacesAroundTitle_ShouldTrimSpaces()
        {
            // Arrange
            var titleWithSpaces = "  Sobremesa  ";

            // Act
            var tag = new Tag(titleWithSpaces, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            tag.Title.Should().Be("Sobremesa");
        }

        [Fact]
        public void UpdateTitle_WithValidTitle_ShouldUpdateTitleAndAuditFields()
        {
            // Arrange
            var tag = new Tag(_validTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var newTitle = "Massas";
            var modifiedBy = "modifier";
            var originalUpdatedAt = tag.UpdatedAt;

            // Act
            tag.UpdateTitle(newTitle, modifiedBy);

            // Assert
            tag.Title.Should().Be(newTitle);
            tag.LastModifiedBy.Should().Be(modifiedBy);
            tag.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Fact]
        public void UpdateTitle_WithInvalidTitle_ShouldThrowDomainException()
        {
            // Arrange
            var tag = new Tag(_validTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var invalidTitle = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                tag.UpdateTitle(invalidTitle, "modifier")
            );
        }

        [Fact]
        public void UpdateTitle_WithInvalidModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var tag = new Tag(_validTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var invalidModifiedBy = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                tag.UpdateTitle("Valid Title", invalidModifiedBy)
            );
        }

        [Fact]
        public void GetNormalizedTitle_ShouldReturnLowerCaseTrimmedTitle()
        {
            // Arrange
            var tag = new Tag("  SOBREMESA Brasileira  ", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act
            var normalized = tag.GetNormalizedTitle();

            // Assert
            normalized.Should().Be("sobremesa brasileira");
        }

        [Fact]
        public void Equals_WithSameNormalizedTitle_ShouldReturnTrue()
        {
            // Arrange
            var tag1 = new Tag("Sobremesa", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var tag2 = new Tag("SOBREMESA", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            tag1.Equals(tag2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentTitles_ShouldReturnFalse()
        {
            // Arrange
            var tag1 = new Tag("Sobremesa", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var tag2 = new Tag("Massas", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            tag1.Equals(tag2).Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_ShouldReturnSameHashForSameNormalizedTitle()
        {
            // Arrange
            var tag1 = new Tag("Sobremesa", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var tag2 = new Tag("SOBREMESA", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            tag1.GetHashCode().Should().Be(tag2.GetHashCode());
        }

        [Fact]
        public void Constructor_WithInvalidId_ShouldThrowDomainException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Tag(invalidId, _validTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Theory]
        [InlineData("Massas Italianas")]
        [InlineData("Sobremesa-Brasileira")]
        [InlineData("Low_Carb")]
        [InlineData("Vegetariano123")]
        [InlineData("Comida Mexicana")]
        public void Constructor_WithValidFormats_ShouldCreateTag(string validTitle)
        {
            // Act
            var tag = new Tag(validTitle, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            tag.Title.Should().Be(validTitle.Trim());
        }

        [Fact]
        public void Constructor_WithInvalidAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var invalidCreatedBy = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Tag(_validTitle, _validCreatedAt, _validUpdatedAt, invalidCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithInvalidDates_ShouldThrowDomainException()
        {
            // Arrange
            var futureCreatedAt = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Tag(_validTitle, futureCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Tag_WithSameNormalizedTitleButDifferentIds_ShouldBeConsideredEqual()
        {
            // Arrange
            var tag1 = new Tag(1, "Sobremesa", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var tag2 = new Tag(2, "SOBREMESA", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            tag1.Equals(tag2).Should().BeTrue();
        }
    }
}