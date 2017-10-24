using System.Web.Http;
using NLog;

namespace NewsletterService.Controller
{
    public class BaseController : ApiController
    {
        protected static ILogger Logger { get; } = LogManager.GetCurrentClassLogger();
    }
}
