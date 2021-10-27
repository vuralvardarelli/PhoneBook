using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.Core
{
    public static class Constants
    {
        public const string RequestLoggingTemplate = "{Type} {Schema} {Host} {Path} {Method} {TimeUTC} {QueryString} {RequestBody}";

        public const string ResponseLoggingTemplate = "{Type} {Schema} {Host} {Path} {TimeUTC} {StatusCode} {ResponseBody}";

        public const string ErrorLoggingTemplate = "{Type} {Message} {StackTrace} {Service} {Method} {RequestId}";
    }
}
