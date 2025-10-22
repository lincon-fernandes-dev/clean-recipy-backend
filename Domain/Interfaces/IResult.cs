using IP.API.Gemed.Domain.Utils;

namespace Domain.Interfaces;

public interface IResult
{
    bool IsSuccess { get; }
    string Error { get; }
    string Suggestion { get; }
    string Detail { get; }
    TipoMensagemEnum TipoMensagem { get; }
    string StatusCode { get; }
}