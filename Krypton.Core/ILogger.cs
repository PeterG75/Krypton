namespace Krypton.Core
{
    public interface ILogger
    {
        void Success(string message);
        void Warning(string message);
        void Error(string message);
        void Info(string message);
    }
}