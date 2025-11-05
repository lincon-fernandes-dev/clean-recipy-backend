using Domain.Entities;
using Domain.Validation;
using System;
using FluentAssertions;
using Xunit;
using Domain.Enums;

namespace Domain.Tests.Entities
{
    public class RecipeTests
    {
        private readonly string _validTitle = "Bolo de Chocolate Caseiro";
        private readonly string _validDescription = "Um delicioso bolo de chocolate feito com cacau em pó e cobertura cremosa. Perfeito para festas e lanches da tarde.";
        private readonly IEnumerable<Instruction> _validInstructions = [
            new Instruction(1,1,"teste")
            ];
        private readonly int _validUserId = 1;
        private readonly int _validPreparationTime = 50;
        private readonly int _validServings = 50;
        private readonly string _validDifficult = "Facil";
        private readonly string _validImageURL = "chef_john";
        private readonly string _validCreatedBy = "chef_john";


        [Fact(DisplayName = "Deve criar receita válida com Id especificado")]
        public void CreateRecipe_WithValidParametersAndId_ShouldSucceed()
        {
            // Arrange
            var id = 1;

            // Act
            var recipe = new Recipe(_validTitle, _validDescription, _validInstructions, _validUserId, _validImageURL, _validPreparationTime, _validServings, _validDifficult);

            // Assert
            recipe.Id.Should().Be(id);
            recipe.Title.Should().Be(_validTitle);
            recipe.Description.Should().Be(_validDescription);
            recipe.IdUser.Should().Be(_validUserId);
            recipe.CreatedBy.Should().Be(_validCreatedBy);
            recipe.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}