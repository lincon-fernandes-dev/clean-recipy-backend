namespace IP.API.Gemed.Domain.Utils;

public enum TipoMensagemEnum
{
    [EnumDescription("Erro")]
    [EnumValue("1")]
    Erro,
    [EnumDescription("Aviso")]
    [EnumValue("2")]
    Aviso,
    [EnumDescription("Informação")]
    [EnumValue("3")]
    Informacao,
    [EnumDescription("Sucesso")]
    [EnumValue("4")]
    Sucesso
}