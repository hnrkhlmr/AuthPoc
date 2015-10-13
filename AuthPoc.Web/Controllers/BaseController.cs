using AuthPoc.ServiceAccess;
using System.Web.Mvc;

namespace AuthPoc.Web.Controllers
{
    public class BaseController : Controller
    {
        private WebClientsFactory _factory;
        public WebClientsFactory Factory { get { return _factory ?? (_factory = new WebClientsFactory()); } }
	}
}