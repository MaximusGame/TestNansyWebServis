

namespace Abstractions
{
    public class PersonModel
    {
        public string Id;
        public string PersonGuid;
        public string Name;
        public string BirthDay;
    }

    public class LogConteiner
    {
        public static ILoggerMessages mylog;
    }
}
