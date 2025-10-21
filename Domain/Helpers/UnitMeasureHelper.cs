// UnitMeasureHelper.cs
using Domain.Enums;
using IP.API.Gemed.Domain.Utils;

namespace Domain.Helpers
{
    public static class UnitMeasureHelper
    {
        public static string GetDescription(UnidadeMedidaEnum unit)
        {
            var field = unit.GetType().GetField(unit.ToString());
            var attribute = field?.GetCustomAttributes(typeof(EnumDescription), false)
                           .FirstOrDefault() as EnumDescription;
            return attribute?.Description ?? unit.ToString();
        }

        public static string GetValue(UnidadeMedidaEnum unit)
        {
            var field = unit.GetType().GetField(unit.ToString());
            var attribute = field?.GetCustomAttributes(typeof(EnumValue), false)
                           .FirstOrDefault() as EnumValue;
            return attribute?.Value?.ToString() ?? unit.ToString();
        }

        public static string GetCategory(UnidadeMedidaEnum unit)
        {
            return unit switch
            {
                // Unidades de peso
                UnidadeMedidaEnum.Grama or UnidadeMedidaEnum.Quilograma or
                UnidadeMedidaEnum.Miligrama or UnidadeMedidaEnum.Ounce or
                UnidadeMedidaEnum.Pound => "Peso",

                // Unidades de volume
                UnidadeMedidaEnum.Mililitro or UnidadeMedidaEnum.Litro or
                UnidadeMedidaEnum.ColherCha or UnidadeMedidaEnum.ColherSopa or
                UnidadeMedidaEnum.ColherCafe or UnidadeMedidaEnum.Xicara or
                UnidadeMedidaEnum.XicaraCha or UnidadeMedidaEnum.Cup or
                UnidadeMedidaEnum.Tablespoon or UnidadeMedidaEnum.Teaspoon => "Volume",

                // Unidades de quantidade
                UnidadeMedidaEnum.Unidade or UnidadeMedidaEnum.Dente or
                UnidadeMedidaEnum.Folha or UnidadeMedidaEnum.Ramo or
                UnidadeMedidaEnum.Pacote or UnidadeMedidaEnum.Lata or
                UnidadeMedidaEnum.Fatia => "Quantidade",

                // Medidas caseiras/subjetivas
                UnidadeMedidaEnum.Pitada or UnidadeMedidaEnum.PontaFaca or
                UnidadeMedidaEnum.Gotas or UnidadeMedidaEnum.AGosto or
                UnidadeMedidaEnum.QuantoBaste => "Caseira",

                UnidadeMedidaEnum.NaoInformada => "Não Informada",
                _ => "Outros"
            };
        }

        public static List<UnidadeMedidaEnum> GetUnitsByCategory(string category)
        {
            return Enum.GetValues(typeof(UnidadeMedidaEnum))
                      .Cast<UnidadeMedidaEnum>()
                      .Where(unit => GetCategory(unit) == category)
                      .ToList();
        }

        public static bool IsQuantifiable(UnidadeMedidaEnum unit)
        {
            var nonQuantifiable = new[] {
                UnidadeMedidaEnum.AGosto,
                UnidadeMedidaEnum.QuantoBaste,
                UnidadeMedidaEnum.NaoInformada
            };

            return !nonQuantifiable.Contains(unit);
        }

        public static bool RequiresDecimalQuantity(UnidadeMedidaEnum unit)
        {
            var decimalUnits = new[] {
                UnidadeMedidaEnum.Grama, UnidadeMedidaEnum.Quilograma, UnidadeMedidaEnum.Miligrama,
                UnidadeMedidaEnum.Mililitro, UnidadeMedidaEnum.Litro,
                UnidadeMedidaEnum.ColherCha, UnidadeMedidaEnum.ColherSopa, UnidadeMedidaEnum.ColherCafe,
                UnidadeMedidaEnum.Xicara, UnidadeMedidaEnum.XicaraCha,
                UnidadeMedidaEnum.Cup, UnidadeMedidaEnum.Tablespoon, UnidadeMedidaEnum.Teaspoon,
                UnidadeMedidaEnum.Ounce, UnidadeMedidaEnum.Pound
            };

            return decimalUnits.Contains(unit);
        }

        public static Dictionary<string, List<UnidadeMedidaEnum>> GetCategorizedUnits()
        {
            return new Dictionary<string, List<UnidadeMedidaEnum>>
            {
                ["Peso"] = GetUnitsByCategory("Peso"),
                ["Volume"] = GetUnitsByCategory("Volume"),
                ["Quantidade"] = GetUnitsByCategory("Quantidade"),
                ["Caseira"] = GetUnitsByCategory("Caseira")
            };
        }
    }
}