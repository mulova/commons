namespace mulova.commons
{
    public interface LogAppender
    {
        void Write(ILog logger, LogLevel level, object message);

        void Cleanup();
    }
}
