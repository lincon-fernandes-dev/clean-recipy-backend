
using IP.API.Gemed.Domain.Utils;

namespace Domain.Enums
{
    public enum UnidadeMedidaEnum
    {
        [EnumDescription("")]
        [EnumValue("")]
        NaoInformada,

        // Unidades de peso
        [EnumDescription("Grama")]
        [EnumValue("g")]
        Grama,

        [EnumDescription("Quilograma")]
        [EnumValue("kg")]
        Quilograma,

        [EnumDescription("Miligrama")]
        [EnumValue("mg")]
        Miligrama,

        // Unidades de volume
        [EnumDescription("Mililitro")]
        [EnumValue("ml")]
        Mililitro,

        [EnumDescription("Litro")]
        [EnumValue("l")]
        Litro,

        // Colheres e xícaras
        [EnumDescription("Colher de chá")]
        [EnumValue("c.chá")]
        ColherCha,

        [EnumDescription("Colher de sopa")]
        [EnumValue("c.sopa")]
        ColherSopa,

        [EnumDescription("Colher de café")]
        [EnumValue("c.café")]
        ColherCafe,

        [EnumDescription("Xícara")]
        [EnumValue("xíc.")]
        Xicara,

        [EnumDescription("Xícara de chá")]
        [EnumValue("xíc.chá")]
        XicaraCha,

        // Unidades de quantidade
        [EnumDescription("Unidade")]
        [EnumValue("un")]
        Unidade,

        [EnumDescription("Dente")]
        [EnumValue("dte")]
        Dente,

        [EnumDescription("Folha")]
        [EnumValue("fl")]
        Folha,

        [EnumDescription("Ramo")]
        [EnumValue("ramo")]
        Ramo,

        [EnumDescription("Pacote")]
        [EnumValue("pct")]
        Pacote,

        [EnumDescription("Lata")]
        [EnumValue("lata")]
        Lata,

        [EnumDescription("Fatia")]
        [EnumValue("fatia")]
        Fatia,

        // Medidas caseiras
        [EnumDescription("Pitada")]
        [EnumValue("pitada")]
        Pitada,

        [EnumDescription("Ponta da faca")]
        [EnumValue("p.faca")]
        PontaFaca,

        [EnumDescription("Gotas")]
        [EnumValue("gotas")]
        Gotas,

        [EnumDescription("A gosto")]
        [EnumValue("a gosto")]
        AGosto,

        [EnumDescription("Quanto baste")]
        [EnumValue("q.b.")]
        QuantoBaste,

        // Unidades internacionais
        [EnumDescription("Xícara americana")]
        [EnumValue("cup")]
        Cup,

        [EnumDescription("Colher de sopa americana")]
        [EnumValue("tbsp")]
        Tablespoon,

        [EnumDescription("Colher de chá americana")]
        [EnumValue("tsp")]
        Teaspoon,

        [EnumDescription("Onça")]
        [EnumValue("oz")]
        Ounce,

        [EnumDescription("Libra")]
        [EnumValue("lb")]
        Pound
    }
}