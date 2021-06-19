public static class IntEx
{
    public static int Clamp(this int v, int min, int max)
    {
        if (v <= min)
        {
            return min;
        }
        if (v >= max)
        {
            return max;
        }
        return v;
    }

    public static bool IsInRange(this int val, int min, int max)
    {
        return val >= min && val <= max;
    }
}
