public static class StringUtils
{
    public static string FormattedNumber(this long number)
    {
        string fmt = number.ToString();//number.ToString("#,#").Replace(',', ' ');
        return fmt == "" ? "0" : fmt;
    }
}