using log4net;

namespace WeightLifting.Library.Logger
{
    public class LoggerService : ILoggerService
    {
        private readonly ILog _logger;

        public LoggerService()
        {
            _logger = LogManager.GetLogger(typeof(LoggerService));
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }
    }
}
