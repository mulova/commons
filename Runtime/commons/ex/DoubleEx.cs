using System;

public static class DoubleEx
{
    public const double Epsilon = 0.000001;
    public static bool ApproximatelyEquals(this double d1, double d2)
    {
        double d = d1 - d2;
        return d <= Epsilon && d >= -Epsilon;
    }

    /// <summary>
    /// Round the specified d and digit.
    /// </summary>
    /// <returns>The round.</returns>
    /// <param name="d">D.</param>
    /// <param name="digit">소수점 자리수</param>
    public static double Round(double d, int digit)
    {
        double pow = Math.Pow(10, digit);
        return Math.Round(d * pow) / pow;
    }

    public static double Clamp(this double v, double min, double max)
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

    public static bool IsInRange(this double val, double min, double max)
    {
        return val >= min && val <= max;
    }

    public static double Interpolate(this double val, double min, double max)
    {
        double diff = max - min;
        double inter = diff * val;
        return min + inter;
    }
}
