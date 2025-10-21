namespace IP.API.Gemed.Domain.Utils;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class EnumValue(object value) : Attribute
{
    public object Value { get; set; } = value;

    public override string ToString()
    {
        return Value?.ToString() ?? string.Empty;
    }
}