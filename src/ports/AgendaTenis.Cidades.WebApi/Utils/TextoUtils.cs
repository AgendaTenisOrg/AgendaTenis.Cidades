using System.Globalization;
using System.Text;

namespace AgendaTenis.Cidades.WebApi.Utils;

public static class TextoUtils
{
    public static string Normalizar(string texto)
    {
        var normalizedString = texto.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString()
            .ToUpper()
            .Normalize(NormalizationForm.FormC)
            .Replace("~", "")
            .Replace("ç", "c")
            .Replace("Ç", "C");
    }
}
