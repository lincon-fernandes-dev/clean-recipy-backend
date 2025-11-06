using Domain.Entities;
using Domain.Validation;
using FluentAssertions;
using Xunit;

namespace Domain.Tests.Entities
{
    public class EntityBaseTest
    {
        private class TestEntity : Entity
        {
            public string TestProperty { get; private set; }

            public TestEntity(string testProperty, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
                : base(createdAt, updatedAt, createdBy, lastModifiedBy)
            {
                TestProperty = testProperty;
            }

            public TestEntity(int id, string testProperty, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
                : base(createdAt, updatedAt, createdBy, lastModifiedBy)
            {
                Id = id;
                TestProperty = testProperty;
            }

            public void UpdateTestProperty(string newValue, string modifiedBy)
            {
                TestProperty = newValue;
                MarkAsModified(modifiedBy);
            }
        }

        private readonly DateTime _validCreatedAt = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _validUpdatedAt = DateTime.UtcNow;
        private readonly string _validCreatedBy = "admin";
        private readonly string _validLastModifiedBy = "admin";

        [Fact]
        public void Constructor_WithValidParameters_ShouldCreateEntity()
        {
            // Arrange
            var testProperty = "Test Value";

            // Act
            var entity = new TestEntity(testProperty, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            entity.TestProperty.Should().Be(testProperty);
            entity.CreatedAt.Should().Be(_validCreatedAt);
            entity.UpdatedAt.Should().Be(_validUpdatedAt);
            entity.CreatedBy.Should().Be(_validCreatedBy);
            entity.LastModifiedBy.Should().Be(_validLastModifiedBy);
        }

        [Fact]
        public void Constructor_WithIdAndValidParameters_ShouldCreateEntityWithId()
        {
            // Arrange
            var id = 1;
            var testProperty = "Test Value";

            // Act
            var entity = new TestEntity(id, testProperty, _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Assert
            entity.Id.Should().Be(id);
            entity.TestProperty.Should().Be(testProperty);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidCreatedBy_ShouldThrowDomainException(string invalidCreatedBy)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new TestEntity("test", _validCreatedAt, _validUpdatedAt, invalidCreatedBy, _validLastModifiedBy)
            );
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void Constructor_WithInvalidLastModifiedBy_ShouldThrowDomainException(string invalidLastModifiedBy)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new TestEntity("test", _validCreatedAt, _validUpdatedAt, _validCreatedBy, invalidLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithLongCreatedBy_ShouldThrowDomainException()
        {
            // Arrange
            var longCreatedBy = new string('A', 101);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new TestEntity("test", _validCreatedAt, _validUpdatedAt, longCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithLongLastModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var longLastModifiedBy = new string('A', 101);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new TestEntity("test", _validCreatedAt, _validUpdatedAt, _validCreatedBy, longLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithFutureCreatedAt_ShouldThrowDomainException()
        {
            // Arrange
            var futureCreatedAt = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new TestEntity("test", futureCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithFutureUpdatedAt_ShouldThrowDomainException()
        {
            // Arrange
            var futureUpdatedAt = DateTime.UtcNow.AddDays(1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new TestEntity("test", _validCreatedAt, futureUpdatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithUpdatedAtBeforeCreatedAt_ShouldThrowDomainException()
        {
            // Arrange
            var createdAt = DateTime.UtcNow;
            var updatedAt = createdAt.AddDays(-1);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new TestEntity("test", createdAt, updatedAt, _validCreatedBy, _validLastModifiedBy)
            );
        }

        [Fact]
        public void MarkAsModified_WithValidModifiedBy_ShouldUpdateProperties()
        {
            // Arrange
            var entity = new TestEntity("test", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var modifiedBy = "modifier";
            var originalUpdatedAt = entity.UpdatedAt;

            // Act
            entity.UpdateTestProperty("new value", modifiedBy);

            // Assert
            entity.LastModifiedBy.Should().Be(modifiedBy);
            entity.UpdatedAt.Should().BeAfter(originalUpdatedAt);
            entity.TestProperty.Should().Be("new value");
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void MarkAsModified_WithInvalidModifiedBy_ShouldThrowDomainException(string invalidModifiedBy)
        {
            // Arrange
            var entity = new TestEntity("test", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                entity.UpdateTestProperty("new value", invalidModifiedBy)
            );
        }

        [Fact]
        public void MarkAsModified_WithLongModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var entity = new TestEntity("test", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var longModifiedBy = new string('A', 101);

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                entity.UpdateTestProperty("new value", longModifiedBy)
            );
        }

        [Fact]
        public void Constructor_WithSpacesInAuditFields_ShouldThrowDomainException()
        {
            // Arrange
            var createdByWithSpaces = "  admin  ";
            var lastModifiedByWithSpaces = "  modifier  ";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new TestEntity("test", _validCreatedAt, _validUpdatedAt, createdByWithSpaces, lastModifiedByWithSpaces)
            );
        }

        [Fact]
        public void MarkAsModified_WithSpacesInModifiedBy_ShouldThrowDomainException()
        {
            // Arrange
            var entity = new TestEntity("test", _validCreatedAt, _validUpdatedAt, _validCreatedBy, _validLastModifiedBy);
            var modifiedByWithSpaces = "  modifier  ";

            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                entity.UpdateTestProperty("new value", modifiedByWithSpaces)
            );
        }

        [Theory]
        [InlineData(" admin")]
        [InlineData("admin ")]
        [InlineData(" admin ")]
        [InlineData("  admin  ")]
        public void Constructor_WithSpacesAroundAuditFields_ShouldThrowDomainException(string auditFieldWithSpaces)
        {
            // Act & Assert
            Assert.Throws<DomainExceptionValidation>(() =>
                new TestEntity("test", _validCreatedAt, _validUpdatedAt, auditFieldWithSpaces, _validLastModifiedBy)
            );
        }
    }
}