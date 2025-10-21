namespace Domain.Entities
{
    public sealed class Vote : Entity
    {
        public int UserId { get; private set; }
        public int RecipeId { get; private set; }
        public bool IsUpvote { get; private set; }

        public User? User { get; private set; }
        public Recipe? Recipe { get; private set; }

        private Vote() { }

        public Vote(int userId, int recipeId, bool isUpvote)
        {
            UserId = userId;
            RecipeId = recipeId;
            IsUpvote = isUpvote;
        }
        public Vote(int userId, int recipeId, bool isUpvote, string createdBy)
        {
            UserId = userId;
            RecipeId = recipeId;
            IsUpvote = isUpvote;
            CreatedBy = createdBy;
        }

        public void ChangeVote(bool isUpvote, string modifiedBy)
        {
            IsUpvote = isUpvote;
            MarkAsModified(modifiedBy);
        }


    }
}