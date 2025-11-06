using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class CommentTests
    {
        private readonly string _validContent = "Esta receita ficou incrível! Adorei o sabor e a textura.";
        private readonly int _validIdUser = 1;
        private readonly int _validIdRecipe = 1;
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "user123";
        private readonly string _validLastModifiedBy = "user123";

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateComment()
        {
            // Act
            var comment = new Comment(
                _validContent, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            comment.Content.Should().Be(_validContent);
            comment.IdUser.Should().Be(_validIdUser);
            comment.IdRecipe.Should().Be(_validIdRecipe);
            comment.ParentCommentId.Should().BeNull();
            comment.IsReply().Should().BeFalse();
            comment.CreatedAt.Should().Be(_validCreatedAt);
            comment.UpdatedAt.Should().Be(_validUpdatedAt);
            comment.CreatedBy.Should().Be(_validCreatedBy);
            comment.LastModifiedBy.Should().Be(_validLastModifiedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateCommentWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var comment = new Comment(
                id, _validContent, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            comment.Id.Should().Be(id);
            comment.Content.Should().Be(_validContent);
            comment.IdUser.Should().Be(_validIdUser);
            comment.IdRecipe.Should().Be(_validIdRecipe);
        }

        [Fact]
        public void Constructor_WithParentComment_ShouldCreateReply()
        {
            // Arrange
            var parentCommentId = 1;

            // Act
            var comment = new Comment(
                _validContent, _validIdUser, _validIdRecipe, parentCommentId, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            comment.ParentCommentId.Should().Be(parentCommentId);
            comment.IsReply().Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidContent_ShouldThrowDomainException(string invalidContent)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Comment(
                    invalidContent, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithLongContent_ShouldThrowDomainException()
        {
            // Arrange
            var longContent = new string('A', 522);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Comment(
                    longContent, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidUserId_ShouldThrowDomainException(int invalidIdUser)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Comment(
                    _validContent, invalidIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidRecipeId_ShouldThrowDomainException(int invalidIdRecipe)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Comment(
                    _validContent, _validIdUser, invalidIdRecipe, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidParentCommentId_ShouldThrowDomainException(int invalidParentCommentId)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Comment(
                    _validContent, _validIdUser, _validIdRecipe, invalidParentCommentId, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void UpdateContent_WithValidContent_ShouldUpdateContent()
        {
            // Arrange
            var comment = CreateValidComment();
            var newContent = "Atualizando meu comentário: a receita ficou ainda melhor na segunda tentativa!";
            var modifiedBy = "user456";
            var originalUpdatedAt = comment.UpdatedAt;

            // Act
            comment.UpdateContent(newContent, modifiedBy);

            // Assert
            comment.Content.Should().Be(newContent);
            comment.LastModifiedBy.Should().Be(modifiedBy);
            comment.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Fact]
        public void UpdateContent_WithInvalidContent_ShouldThrowDomainException()
        {
            // Arrange
            var comment = CreateValidComment();
            var invalidContent = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                comment.UpdateContent(invalidContent, "user456")
            );
        }

        [Fact]
        public void UpdateContent_WithInvalidModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var comment = CreateValidComment();
            var invalidModifiedBy = "ab"; // Menos de 3 caracteres

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                comment.UpdateContent("Novo conteúdo válido", invalidModifiedBy)
            );
        }

        [Fact]
        public void AddReply_WithValidReply_ShouldAddReply()
        {
            // Arrange
            var comment = CreateValidComment();
            var reply = new Comment(
                "Concordo com seu comentário!", _validIdUser, _validIdRecipe, comment.Id,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );
            var modifiedBy = "user456";

            // Act
            comment.AddReply(reply, modifiedBy);

            // Assert
            comment.Replies.Should().Contain(reply);
            comment.GetRepliesCount().Should().Be(1);
            comment.LastModifiedBy.Should().Be(modifiedBy);
        }

        [Fact]
        public void AddReply_WithNullReply_ShouldThrowDomainException()
        {
            // Arrange
            var comment = CreateValidComment();

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                comment.AddReply(null, "user456")
            );
        }

        [Fact]
        public void AddReply_WithDifferentRecipeId_ShouldThrowDomainException()
        {
            // Arrange
            var comment = CreateValidComment();

            var replyWithDifferentRecipe = new Comment(
                "Concordo!", _validIdUser, 999, comment.Id, 
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                comment.AddReply(replyWithDifferentRecipe, "user456")
            );
        }

        [Fact]
        public void RemoveReply_WithValidReply_ShouldRemoveReply()
        {
            // Arrange
            var comment = CreateValidComment();
            var reply = new Comment(
                "Concordo com seu comentário!", _validIdUser, _validIdRecipe, comment.Id,
                _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy
            );
            comment.AddReply(reply, "user456");

            // Act
            comment.RemoveReply(reply, "user789");

            // Assert
            comment.Replies.Should().NotContain(reply);
            comment.GetRepliesCount().Should().Be(0);
        }

        [Fact]
        public void IsReply_WithParentCommentId_ShouldReturnTrue()
        {
            // Arrange
            var comment = new Comment(
                _validContent, _validIdUser, _validIdRecipe, 1, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Act & Assert
            comment.IsReply().Should().BeTrue();
        }

        [Fact]
        public void IsReply_WithoutParentCommentId_ShouldReturnFalse()
        {
            // Arrange
            var comment = CreateValidComment();

            // Act & Assert
            comment.IsReply().Should().BeFalse();
        }

        [Fact]
        public void GetRepliesCount_WithNoReplies_ShouldReturnZero()
        {
            // Arrange
            var comment = CreateValidComment();

            // Act
            var count = comment.GetRepliesCount();

            // Assert
            count.Should().Be(0);
        }

        [Fact]
        public void GetLikesCount_WithNoLikes_ShouldReturnZero()
        {
            // Arrange
            var comment = CreateValidComment();

            // Act
            var count = comment.GetLikesCount();

            // Assert
            count.Should().Be(0);
        }

        [Fact]
        public void Constructor_WithSpacesInContent_ShouldTrimContent()
        {
            // Arrange
            var contentWithSpaces = "  Este é um comentário com espaços.  ";

            // Act
            var comment = new Comment(
                contentWithSpaces, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            comment.Content.Should().Be("Este é um comentário com espaços.");
        }

        [Fact]
        public void Constructor_WithValidBoundaryValues_ShouldCreateComment()
        {
            // Arrange
            var minContent = "A";
            var maxContent = new string('A', 521);

            // Act & Assert
            var comment1 = new Comment(
                minContent, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );
            var comment2 = new Comment(
                maxContent, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            comment1.Content.Should().Be(minContent);
            comment2.Content.Should().Be(maxContent);
        }

        [Fact]
        public void Constructor_WithInvalidId_ShouldThrowDomainException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new Comment(
                    invalidId, _validContent, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        private Comment CreateValidComment()
        {
            return new Comment(1,
                _validContent, _validIdUser, _validIdRecipe, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );
        }
    }
}