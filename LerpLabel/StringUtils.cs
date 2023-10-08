public static class StringUtils
{
    public static string FormattedNumber(this long number)
    {
        if (number >= 1000000000000000000)
        {
            return (number / 1000000000000000000.0f).ToString("F") + "Qi";
        }
        else if (number >= 1000000000000000)
        {
            return (number / 1000000000000000.0f).ToString("F") + "Qa";
        }
        else if (number >= 1000000000000)
        {
            return (number / 1000000000000.0f).ToString("F") + "T";
        }
        else if (number >= 1000000000)
        {
            return (number / 1000000000.0f).ToString("F") + "B";
        }
        else if (number >= 1000000)
        {
            return (number / 1000000.0f).ToString("F") + "M";
        } 
        else if (number >= 1000)
        {
            return (number / 1000.0f).ToString("F") + "K";
        }
        
        string fmt = number.ToString("#,#").Replace(',', ' ');
        return fmt == "" ? "0" : fmt;
        
        
    }
}