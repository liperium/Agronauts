public static class StringUtils
{
    public static string FormattedNumber(this long number)
    {
        return number.ToString("#,#").Replace(',', ' ');
    }
}