namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            if (count % 10 == 1 && count != 11 && count % 100 != 11)
                return "рубль";
            else if (count % 10 >= 2 && count % 10 <= 4 && (count < 10 || count > 20) &&
                (count % 100 < 11 || count % 100 > 20))
                return "рубля";
            else
                return "рублей";
        }
    }
}