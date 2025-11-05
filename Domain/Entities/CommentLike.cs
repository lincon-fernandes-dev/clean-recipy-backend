using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CommentLike : Entity
    {
        public int IdComment { get; private set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }
        public Comment Comment { get; private set; }

        public CommentLike() { }
        public CommentLike(int idCommentLike, int idComment, int idUser)
        {
            ValidateDomain(idCommentLike < 0, "id do like commentario invalido, id deve ser um numero inteiro positivo");
            Validate(idComment, idUser);

            Id = idCommentLike;
            IdComment = idCommentLike;
            IdUser = idUser;

        }
        public CommentLike(int idComment, int idUser)
        {
            Validate(idComment, idUser);

            IdComment = idComment;
            IdUser = idUser;
        }

        public static void Validate(int idComment, int idUser)
        {
            ValidateDomain(idComment < 0, "id comentario invalido, id deve ser um numero inteiro positivo");
            ValidateDomain(idUser < 0, "id usuario invalido, id deve ser um numero inteiro positivo");

        }
    }
}
