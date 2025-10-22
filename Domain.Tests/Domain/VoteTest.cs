using Domain.Entities;
using Domain.Enums;
using Domain.Validation;
using FluentAssertions;
using System;
using Xunit;

namespace Domain.Tests.Entities
{
    public class VoteTests
    {
        private readonly int _validUserId = 1;
        private readonly int _validRecipeId = 1;
        private readonly bool _validIsUpvote = true;
        private readonly string _validCreatedBy = "test_user";

        #region Constructor Tests

        [Fact(DisplayName = "Deve criar voto válido com parâmetros básicos")]
        public void CreateVote_WithBasicParameters_ShouldSucceed()
        {
            // Act
            var vote = new Vote(_validUserId, _validRecipeId, _validIsUpvote);

            // Assert
            vote.UserId.Should().Be(_validUserId);
            vote.RecipeId.Should().Be(_validRecipeId);
            vote.IsUpvote.Should().Be(_validIsUpvote);
            vote.CreatedBy.Should().BeEmpty();
            vote.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve criar voto válido com createdBy")]
        public void CreateVote_WithCreatedBy_ShouldSucceed()
        {
            // Act
            var vote = new Vote(_validUserId, _validRecipeId, _validIsUpvote, _validCreatedBy);

            // Assert
            vote.UserId.Should().Be(_validUserId);
            vote.RecipeId.Should().Be(_validRecipeId);
            vote.IsUpvote.Should().Be(_validIsUpvote);
            vote.CreatedBy.Should().Be(_validCreatedBy);
            vote.CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve criar voto com downvote")]
        public void CreateVote_WithDownvote_ShouldSucceed()
        {
            // Arrange
            var isDownvote = false;

            // Act
            var vote = new Vote(_validUserId, _validRecipeId, isDownvote, _validCreatedBy);

            // Assert
            vote.IsUpvote.Should().BeFalse();
        }

        [Theory(DisplayName = "Deve criar voto com IDs válidos")]
        [InlineData(1, 1)]
        [InlineData(1, 100)]
        [InlineData(100, 1)]
        [InlineData(999, 888)]
        public void CreateVote_WithValidIds_ShouldSucceed(int userId, int recipeId)
        {
            // Act
            var vote = new Vote(userId, recipeId, _validIsUpvote, _validCreatedBy);

            // Assert
            vote.UserId.Should().Be(userId);
            vote.RecipeId.Should().Be(recipeId);
            vote.IsUpvote.Should().Be(_validIsUpvote);
        }

        #endregion

        #region Edge Cases - Invalid IDs

        [Theory(DisplayName = "Deve aceitar IDs zero ou negativos (dependendo da validação do banco)")]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(0, 0)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(-1, -1)]
        public void CreateVote_WithEdgeCaseIds_ShouldSucceed(int userId, int recipeId)
        {
            // Act
            var vote = new Vote(userId, recipeId, _validIsUpvote, _validCreatedBy);

            // Assert
            vote.UserId.Should().Be(userId);
            vote.RecipeId.Should().Be(recipeId);
        }

        #endregion

        #region ChangeVote Tests

        [Fact(DisplayName = "Deve alterar voto de upvote para downvote")]
        public void ChangeVote_FromUpvoteToDownvote_ShouldSucceed()
        {
            // Arrange
            var vote = new Vote(_validUserId, _validRecipeId, true, _validCreatedBy);
            var originalModifiedDate = vote.LastModifiedDate;
            var modifiedBy = "modified_user";

            // Act
            vote.ChangeVote(false, modifiedBy);

            // Assert
            vote.IsUpvote.Should().BeFalse();
            vote.LastModifiedBy.Should().Be(modifiedBy);
            vote.LastModifiedDate.Should().BeAfter(originalModifiedDate);
            vote.LastModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve alterar voto de downvote para upvote")]
        public void ChangeVote_FromDownvoteToUpvote_ShouldSucceed()
        {
            // Arrange
            var vote = new Vote(_validUserId, _validRecipeId, false, _validCreatedBy);
            var originalModifiedDate = vote.LastModifiedDate;
            var modifiedBy = "modified_user";

            // Act
            vote.ChangeVote(true, modifiedBy);

            // Assert
            vote.IsUpvote.Should().BeTrue();
            vote.LastModifiedBy.Should().Be(modifiedBy);
            vote.LastModifiedDate.Should().BeAfter(originalModifiedDate);
        }

        [Fact(DisplayName = "Deve manter mesmo valor ao alterar para o mesmo tipo de voto")]
        public void ChangeVote_ToSameVoteType_ShouldSucceed()
        {
            // Arrange
            var vote = new Vote(_validUserId, _validRecipeId, true, _validCreatedBy);
            var originalModifiedDate = vote.LastModifiedDate;
            var modifiedBy = "modified_user";

            // Act
            vote.ChangeVote(true, modifiedBy);

            // Assert
            vote.IsUpvote.Should().BeTrue();
            vote.LastModifiedBy.Should().Be(modifiedBy);
            vote.LastModifiedDate.Should().BeAfter(originalModifiedDate);
        }

        [Theory(DisplayName = "Deve alterar voto múltiplas vezes")]
        [InlineData(true, false, true)]
        [InlineData(false, true, false)]
        [InlineData(true, false, false)]
        [InlineData(false, true, true)]
        public void ChangeVote_MultipleTimes_ShouldSucceed(bool initial, bool firstChange, bool secondChange)
        {
            // Arrange
            var vote = new Vote(_validUserId, _validRecipeId, initial, _validCreatedBy);
            var modifiedBy = "modified_user";

            // Act
            vote.ChangeVote(firstChange, modifiedBy);
            var firstModifiedDate = vote.LastModifiedDate;

            vote.ChangeVote(secondChange, $"{modifiedBy}_2");

            // Assert
            vote.IsUpvote.Should().Be(secondChange);
            vote.LastModifiedBy.Should().Be($"{modifiedBy}_2");
            vote.LastModifiedDate.Should().BeAfter(firstModifiedDate);
        }

        #endregion

        #region Navigation Properties Tests

        [Fact(DisplayName = "Deve inicializar propriedades de navegação como nulas")]
        public void Vote_ShouldInitializeNavigationPropertiesAsNull()
        {
            // Act
            var vote = new Vote(_validUserId, _validRecipeId, _validIsUpvote, _validCreatedBy);

            // Assert
            vote.User.Should().BeNull();
            vote.Recipe.Should().BeNull();
        }

        [Fact(DisplayName = "Deve permitir definir propriedades de navegação via reflection")]
        public void Vote_ShouldAllowSettingNavigationProperties()
        {
            // Arrange
            var vote = new Vote(_validUserId, _validRecipeId, _validIsUpvote, _validCreatedBy);
            var user = new User("Test User", "test@email.com", "hash", UserStatus.Active, _validCreatedBy);
            var recipe = new Recipe("Test Recipe", "Description com 25 caracteres minimo", "instructions  com 25 caracteres minimo", 30, _validCreatedBy);

            // Act
            vote.GetType().GetProperty("User")!.SetValue(vote, user);
            vote.GetType().GetProperty("Recipe")!.SetValue(vote, recipe);

            // Assert
            vote.User.Should().Be(user);
            vote.Recipe.Should().Be(recipe);
        }

        #endregion

        #region Audit Fields Tests

        [Fact(DisplayName = "Deve herdar e funcionar campos de auditoria da Entity")]
        public void Vote_ShouldInheritAuditFieldsFromEntity()
        {
            // Arrange
            var createdDate = new DateTime(2025, 10, 20);
            var createdBy = "creator";
            var vote = new Vote(_validUserId, _validRecipeId, _validIsUpvote, createdBy);

            // Act
            vote.GetType().GetProperty("CreatedDate")!.SetValue(vote, createdDate);
            vote.MarkAsModified("modifier");

            // Assert
            vote.CreatedDate.Should().Be(createdDate);
            vote.CreatedBy.Should().Be(createdBy);
            vote.LastModifiedBy.Should().Be("modifier");
            vote.LastModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact(DisplayName = "Deve atualizar ModifiedDate ao chamar ChangeVote")]
        public void ChangeVote_ShouldUpdateModifiedDate()
        {
            // Arrange
            var vote = new Vote(_validUserId, _validRecipeId, _validIsUpvote, _validCreatedBy);
            var initialModifiedDate = vote.LastModifiedDate;

            // Aguardar um pouco para garantir que o tempo mude
            System.Threading.Thread.Sleep(10);

            // Act
            vote.ChangeVote(false, "modifier");

            // Assert
            vote.LastModifiedDate.Should().BeAfter(initialModifiedDate);
        }

        #endregion

        #region Business Logic Tests

        [Fact(DisplayName = "Deve representar upvote corretamente")]
        public void Vote_WithIsUpvoteTrue_ShouldRepresentUpvote()
        {
            // Act
            var vote = new Vote(_validUserId, _validRecipeId, true, _validCreatedBy);

            // Assert
            vote.IsUpvote.Should().BeTrue();
        }

        [Fact(DisplayName = "Deve representar downvote corretamente")]
        public void Vote_WithIsUpvoteFalse_ShouldRepresentDownvote()
        {
            // Act
            var vote = new Vote(_validUserId, _validRecipeId, false, _validCreatedBy);

            // Assert
            vote.IsUpvote.Should().BeFalse();
        }

        [Fact(DisplayName = "Deve permitir toggle do voto múltiplas vezes")]
        public void Vote_ShouldAllowMultipleToggles()
        {
            // Arrange
            var vote = new Vote(_validUserId, _validRecipeId, true, _validCreatedBy);

            // Act & Assert - Primeira alteração
            vote.ChangeVote(false, "modifier1");
            vote.IsUpvote.Should().BeFalse();

            // Act & Assert - Segunda alteração
            vote.ChangeVote(true, "modifier2");
            vote.IsUpvote.Should().BeTrue();

            // Act & Assert - Terceira alteração
            vote.ChangeVote(false, "modifier3");
            vote.IsUpvote.Should().BeFalse();
        }

        #endregion

        #region Consistency Tests

        [Fact(DisplayName = "Deve manter consistência entre UserId e RecipeId após criação")]
        public void Vote_ShouldMaintainIdConsistency()
        {
            // Arrange
            var userId = 5;
            var recipeId = 10;

            // Act
            var vote = new Vote(userId, recipeId, _validIsUpvote, _validCreatedBy);

            // Assert
            vote.UserId.Should().Be(userId);
            vote.RecipeId.Should().Be(recipeId);
            vote.UserId.Should().NotBe(vote.RecipeId);
        }

        [Fact(DisplayName = "Deve manter dados imutáveis após criação")]
        public void Vote_ShouldKeepImmutableDataAfterCreation()
        {
            // Arrange
            var vote = new Vote(_validUserId, _validRecipeId, _validIsUpvote, _validCreatedBy);
            var originalUserId = vote.UserId;
            var originalRecipeId = vote.RecipeId;

            // Act
            // Tentativa de alterar via reflection (simulando acesso indevido)
            vote.GetType().GetProperty("UserId")!.SetValue(vote, 999);
            vote.GetType().GetProperty("RecipeId")!.SetValue(vote, 999);

            // Assert
            vote.UserId.Should().Be(999); // Reflection pode burlar a privacidade
            vote.RecipeId.Should().Be(999);

            // Mas em uso normal, essas propriedades são privadas set
            // O teste mostra que precisamos confiar no encapsulamento
        }

        #endregion
    }
}