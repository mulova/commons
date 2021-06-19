using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public static class TimeSpanEx
{
    public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
    {
        return Task.Delay(timeSpan).GetAwaiter();
    }
}
