using Domain.Constants;

namespace Common
{
    public static class CultureHelper
    {
        public static string GetCulture(string cultureName)
        {
            switch (cultureName)
            {
                case CultureTwoLetterISONames.English:
                    return CultureTwoLetterISONames.English;

                case CultureTwoLetterISONames.Serbian:
                    return CultureTwoLetterISONames.Serbian;

                default:
                    return DefaultConstants.Language;
            }
        }
    }
}
