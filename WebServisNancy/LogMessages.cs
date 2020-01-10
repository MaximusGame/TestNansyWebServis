using Abstractions;
using Serilog;

namespace WebServisNancy
{
    class LogMessages : ILoggerMessages
    {
        readonly ILogger _log;

        public LogMessages(ILogger log)
        {
            _log = log;
        }

        public void GetRequest(string id)
        {
            _log.Information("Receive GET Request id = " + id );
        }

        public void PostRequest(string data)
        {
            _log.Information("Receive POST Request data = " + data );
        }
    }
}
