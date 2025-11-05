using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public IEnumerable<CommentLike> CommentLikes { get; private set; }
        public ICollection<Comment> Replies { get; private set; } = new List<Comment>();

        public Comment() { }
        public Comment(string content, int idUser, int idRecipe)
        {
            Validate(content, idUser, idRecipe);
            Content = content;
            IdUser = idUser;
            IdRecipe = idRecipe;
        }
        public Comment(int idComment, string content, int idUser, int idRecipe)
        {
            ValidateDomain(idComment < 1, "Id do comentario inválido, Id deve ser um numero inteiro e positivo");
            Validate(content, idUser, idRecipe);

            Id = idComment;
            Content = content;
            IdUser = idUser;
            IdRecipe = idRecipe;
        }

        public static void Validate(string content, int idUser, int idRecipe)
        {
            ValidateDomain(content.Length < 1, "Comentario nulo, verifique novamente o valor digitado");
            ValidateDomain(content.Length > 521, "Comentario muito longo, maximo de 521 caracteres");

            ValidateDomain(idUser < 1, "Id de usuario inválido, Id deve ser um numero inteiro e positivo");
            ValidateDomain(idRecipe < 1, "Id da receita inválido, Id deve ser um numero inteiro e positivo");

        }

    }
}
