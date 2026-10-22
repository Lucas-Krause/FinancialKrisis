using FinancialKrisis.Application.Enums;

namespace FinancialKrisis.Application.Extensions;

public static class GrammaticalGenderExtensions
{
    extension(GrammaticalGender pGender)
    {
        public string ToPtArticle()
        {
            return pGender switch
            {
                GrammaticalGender.Masculine => "o",
                GrammaticalGender.Feminine => "a",
                _ => throw new ArgumentOutOfRangeException(nameof(pGender), pGender, null)
            };
        }

        public string ToPtIndefiniteArticle()
        {
            return pGender switch
            {
                GrammaticalGender.Masculine => "um",
                GrammaticalGender.Feminine => "uma",
                _ => throw new ArgumentOutOfRangeException(nameof(pGender), pGender, null)
            };
        }
    }
}
