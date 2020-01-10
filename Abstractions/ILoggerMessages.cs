using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions
{
   public interface ILoggerMessages
   {
        void GetRequest(string id);
        void PostRequest(string data);
   }
}
