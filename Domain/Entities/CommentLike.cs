using Domain.Validation;

namespace Domain.Entities
{
    public class CommentLike : Entity
    {
        public int IdComment { get; private set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }
        public Comment Comment { get; private set; }

        private CommentLike() { }

        public CommentLike(int idComment, int idUser, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            Validate(idComment, idUser);

            IdComment = idComment;
            IdUser = idUser;
        }

        public CommentLike(int id, int idComment, int idUser, DateTime createdAt, DateTime updatedAt, string createdBy, string lastModifiedBy)
            : base(createdAt, updatedAt, createdBy, lastModifiedBy)
        {
            ValidateDomain(id < 1, "Id inválido. Id deve ser um número inteiro e positivo.");
            Validate(idComment, idUser);

            Id = id;
            IdComment = idComment;
            IdUser = idUser;
        }

        public void UpdateCommentLike(int newIdComment, int newIdUser, string modifiedBy)
        {
            Validate(newIdComment, newIdUser);
            ValidateModifiedBy(modifiedBy);

            IdComment = newIdComment;
            IdUser = newIdUser;
            MarkAsModified(modifiedBy);
        }

        private static void Validate(int idComment, int idUser)
        {
            ValidateIdComment(idComment);
            ValidateIdUser(idUser);
        }

        private static void ValidateIdComment(int idComment)
        {
            ValidateDomain(idComment < 1, "Id do comentário inválido. Id deve ser um número inteiro e positivo.");
        }

        private static void ValidateIdUser(int idUser)
        {
            ValidateDomain(idUser < 1, "Id do usuário inválido. Id deve ser um número inteiro e positivo.");
        }

        private static void ValidateModifiedBy(string modifiedBy)
        {
            ValidateDomain(string.IsNullOrWhiteSpace(modifiedBy), "Para atualizar o like do comentário é necessário fornecer o nome do usuário que está modificando.");
            ValidateDomain(modifiedBy.Trim().Length < 3, "O nome de usuário para modificação deve ter pelo menos 3 caracteres.");
        }

        // Override do Equals para comparar CommentLike pelos mesmos IdComment e IdUser
        public override bool Equals(object obj)
        {
            if (obj is CommentLike other)
            {
                return IdComment == other.IdComment && IdUser == other.IdUser;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdComment, IdUser);
        }
    }
}