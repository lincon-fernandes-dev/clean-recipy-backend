using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class CommentLikeTests
    {
        private readonly int _validIdComment = 1;
        private readonly int _validIdUser = 1;
        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "user123";
        private readonly string _validLastModifiedBy = "user123";

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateCommentLike()
        {
            // Act
            var commentLike = new CommentLike(
                _validIdComment, _validIdUser, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            commentLike.IdComment.Should().Be(_validIdComment);
            commentLike.IdUser.Should().Be(_validIdUser);
            commentLike.CreatedAt.Should().Be(_validCreatedAt);
            commentLike.UpdatedAt.Should().Be(_validUpdatedAt);
            commentLike.CreatedBy.Should().Be(_validCreatedBy);
            commentLike.LastModifiedBy.Should().Be(_validLastModifiedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateCommentLikeWithId()
        {
            // Arrange
            var id = 1;

            // Act
            var commentLike = new CommentLike(
                id, _validIdComment, _validIdUser, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );

            // Assert
            commentLike.Id.Should().Be(id);
            commentLike.IdComment.Should().Be(_validIdComment);
            commentLike.IdUser.Should().Be(_validIdUser);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidIdComment_ShouldThrowDomainException(int invalidIdComment)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new CommentLike(
                    invalidIdComment, _validIdUser, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithInvalidIdUser_ShouldThrowDomainException(int invalidIdUser)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new CommentLike(
                    _validIdComment, invalidIdUser, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithInvalidId_ShouldThrowDomainException()
        {
            // Arrange
            var invalidId = 0;

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new CommentLike(
                    invalidId, _validIdComment, _validIdUser, _validCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void UpdateCommentLike_WithValidParameters_ShouldUpdateProperties()
        {
            // Arrange
            var commentLike = CreateValidCommentLike();
            var newIdComment = 2;
            var newIdUser = 3;
            var modifiedBy = "user456";
            var originalUpdatedAt = commentLike.UpdatedAt;

            // Act
            commentLike.UpdateCommentLike(newIdComment, newIdUser, modifiedBy);

            // Assert
            commentLike.IdComment.Should().Be(newIdComment);
            commentLike.IdUser.Should().Be(newIdUser);
            commentLike.LastModifiedBy.Should().Be(modifiedBy);
            commentLike.UpdatedAt.Should().BeAfter(originalUpdatedAt);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(-1, 1)]
        [InlineData(1, 0)]
        [InlineData(1, -1)]
        [InlineData(0, 0)]
        public void UpdateCommentLike_WithInvalidParameters_ShouldThrowDomainException(int invalidIdComment, int invalidIdUser)
        {
            // Arrange
            var commentLike = CreateValidCommentLike();

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                commentLike.UpdateCommentLike(invalidIdComment, invalidIdUser, "user456")
            );
        }

        [Fact]
        public void UpdateCommentLike_WithInvalidModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var commentLike = CreateValidCommentLike();
            var invalidModifiedBy = "ab"; // Menos de 3 caracteres

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                commentLike.UpdateCommentLike(2, 3, invalidModifiedBy)
            );
        }

        [Fact]
        public void Equals_WithSameIdCommentAndIdUser_ShouldReturnTrue()
        {
            // Arrange
            var commentLike1 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var commentLike2 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            commentLike1.Equals(commentLike2).Should().BeTrue();
        }

        [Fact]
        public void Equals_WithDifferentIdComment_ShouldReturnFalse()
        {
            // Arrange
            var commentLike1 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var commentLike2 = new CommentLike(2, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            commentLike1.Equals(commentLike2).Should().BeFalse();
        }

        [Fact]
        public void Equals_WithDifferentIdUser_ShouldReturnFalse()
        {
            // Arrange
            var commentLike1 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var commentLike2 = new CommentLike(1, 2, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            commentLike1.Equals(commentLike2).Should().BeFalse();
        }

        [Fact]
        public void Equals_WithNull_ShouldReturnFalse()
        {
            // Arrange
            var commentLike = CreateValidCommentLike();

            // Act & Assert
            commentLike.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void Equals_WithDifferentType_ShouldReturnFalse()
        {
            // Arrange
            var commentLike = CreateValidCommentLike();
            var differentObject = new object();

            // Act & Assert
            commentLike.Equals(differentObject).Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_WithSameIdCommentAndIdUser_ShouldReturnSameHash()
        {
            // Arrange
            var commentLike1 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var commentLike2 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            commentLike1.GetHashCode().Should().Be(commentLike2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WithDifferentIdComment_ShouldReturnDifferentHash()
        {
            // Arrange
            var commentLike1 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var commentLike2 = new CommentLike(2, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            commentLike1.GetHashCode().Should().NotBe(commentLike2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_WithDifferentIdUser_ShouldReturnDifferentHash()
        {
            // Arrange
            var commentLike1 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var commentLike2 = new CommentLike(1, 2, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            commentLike1.GetHashCode().Should().NotBe(commentLike2.GetHashCode());
        }

        [Fact]
        public void Constructor_WithInvalidAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var invalidCreatedBy = "";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new CommentLike(
                    _validIdComment, _validIdUser, _validCreatedAt, _validUpdatedAt,
                    invalidCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithInvalidDates_ShouldThrowDomainException()
        {
            // Arrange
            var futureCreatedAt = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new CommentLike(
                    _validIdComment, _validIdUser, futureCreatedAt, _validUpdatedAt,
                    _validCreatedBy, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void Constructor_WithSpacesInAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var createdByWithSpaces = "  user123  ";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new CommentLike(
                    _validIdComment, _validIdUser, _validCreatedAt, _validUpdatedAt,
                    createdByWithSpaces, _validLastModifiedBy
                )
            );
        }

        [Fact]
        public void CommentLike_ShouldNotAllowDuplicateUserAndCommentCombination()
        {
            // Arrange
            var commentLike1 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var commentLike2 = new CommentLike(1, 1, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            commentLike1.Equals(commentLike2).Should().BeTrue();
            commentLike1.GetHashCode().Should().Be(commentLike2.GetHashCode());
        }

        private CommentLike CreateValidCommentLike()
        {
            return new CommentLike(
                _validIdComment, _validIdUser, _validCreatedAt, _validUpdatedAt,
                _validCreatedBy, _validLastModifiedBy
            );
        }
    }
}