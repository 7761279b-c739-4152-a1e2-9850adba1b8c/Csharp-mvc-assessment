using Microsoft.AspNetCore.Mvc;
using C__MVC_Assessment.Data;

namespace C__MVC_Assessment.Controllers
{
    public abstract class BaseController : Controller
    {
        private bool _disposed = false;

        protected Context Context { get; private set; }

        public BaseController(Context context)
        {
            Context = context;

        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
