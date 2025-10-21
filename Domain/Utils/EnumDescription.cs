using System.ComponentModel;

namespace IP.API.Gemed.Domain.Utils;

public class EnumDescription(string description) : DescriptionAttribute(description)
{
    public override string ToString()
    {
        return Description;
    }
}