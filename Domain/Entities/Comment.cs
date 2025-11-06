using Domain.Validation;

namespace Domain.Entities
{
    public class Comment : Entity
    {
        public int IdUser { get; private set; }
        public int IdRecipe { get; private set; }
        public string Content { get; private set; } = string.Empty;
        public int? ParentCommentId { get; private set; }
        public Comment? ParentComment { get; private set; }
        public User User { get; private set; }
        public Recipe Recipe { get; private set; }
        public IEnumerable<CommentLike> CommentLikes { get; private set; } = new List<CommentLike>();
        public ICollection<Comment> Replies { get; private set; } = new List<Comment>();

        private Comment() { }

        public Comment(string content, int idUser, int idRecipe, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(content, idUser, idRecipe);

            Content = content.Trim();
            IdUser = idUser;
            IdRecipe = idRecipe;
        }

        public Comment(int id, string content, int idUser, int idRecipe, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id do comentário inválido. Id deve ser um número inteiro e positivo.");
            Validate(content, idUser, idRecipe);

            Id = id;
            Content = content.Trim();
            IdUser = idUser;
            IdRecipe = idRecipe;
        }

        public Comment(string content, int idUser, int idRecipe, int? parentCommentId, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(content, idUser, idRecipe);
            
            if(parentCommentId != null)
            {
                ValidateParentCommentId((int)parentCommentId);
            }

            Content = content;
            IdUser = idUser;
            IdRecipe = idRecipe;
            ParentCommentId = parentCommentId;
        }

        public Comment(int id, string content, int idUser, int idRecipe, int parentCommentId, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id do comentário inválido. Id deve ser um número inteiro e positivo.");
            Validate(content, idUser, idRecipe);
            ValidateParentCommentId(parentCommentId);

            Id = id;
            Content = content;
            IdUser = idUser;
            IdRecipe = idRecipe;
            ParentCommentId = parentCommentId;
        }

        public void UpdateContent(string newContent, string modifiedBy)
        {
            ValidateContent(newContent);
            ValidateModifiedBy(modifiedBy);

            Content = newContent;
            MarkAsModified(modifiedBy);
        }

        public void AddReply(Comment reply, string modifiedBy)
        {
            ValidateModifiedBy(modifiedBy);
            ValidateDomain(reply == null, "A resposta não pode ser nula.");
            ValidateDomain(reply.IdRecipe != IdRecipe, "A resposta deve ser para a mesma receita.");

            Replies.Add(reply);
            MarkAsModified(modifiedBy);
        }

        public void RemoveReply(Comment reply, string modifiedBy)
        {
            ValidateModifiedBy(modifiedBy);
            ValidateDomain(reply == null, "A resposta não pode ser nula.");

            Replies.Remove(reply);
            MarkAsModified(modifiedBy);
        }

        public bool IsReply()
        {
            return ParentCommentId.HasValue;
        }

        public int GetRepliesCount()
        {
            return Replies?.Count ?? 0;
        }

        public int GetLikesCount()
        {
            return CommentLikes?.Count() ?? 0;
        }

        private static void Validate(string content, int idUser, int idRecipe)
        {
            ValidateContent(content);
            ValidateUserId(idUser);
            ValidateRecipeId(idRecipe);
        }

        private static void ValidateContent(string content)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(content), "O conteúdo do comentário é obrigatório.");
            ValidateDomain(content.Trim().Length < 1, "O comentário não pode estar vazio.");
            ValidateDomain(content.Trim().Length > 521, "O comentário não pode ultrapassar 521 caracteres.");
        }

        private static void ValidateUserId(int idUser)
        {
            ValidateDomain(idUser < 1, "Id do usuário inválido. Id deve ser um número inteiro e positivo.");
        }

        private static void ValidateRecipeId(int idRecipe)
        {
            ValidateDomain(idRecipe < 1, "Id da receita inválido. Id deve ser um número inteiro e positivo.");
        }

        private static void ValidateParentCommentId(int parentCommentId)
        {
            ValidateDomain(parentCommentId < 1, "Id do comentário pai inválido. Id deve ser um número inteiro e positivo.");
        }

        private static void ValidateModifiedBy(string modifiedBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(modifiedBy), "Para atualizar o comentário é necessário fornecer o nome do usuário que está modificando.");
            ValidateDomain(modifiedBy.Trim().Length < 3, "O nome de usuário para modificação deve ter pelo menos 3 caracteres.");
        }
    }
}