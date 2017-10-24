using System.Web.Http;
using NLog;

namespace Sample.Controller
{
    public class BaseController : ApiController
    {
        protected static ILogger Logger { get; } = LogManager.GetCurrentClassLogger();
    }
}
