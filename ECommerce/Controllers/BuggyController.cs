using ECommerce.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class BuggyController : BaseApiController
    {
        public StoreContext Context { get; }
        public BuggyController(StoreContext context)
        {
            Context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var prod = this.Context.Products.Find(900);
            if (prod == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return this.Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBatRequest()
        {
            return this.BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBatRequest(int id)
        {
            return this.Ok();
        }

        [HttpGet("servererrror")]
        public ActionResult GetServerError()
        {
            var prod = this.Context.Products.Find(900);
            prod.Name = "Sam";
            return this.Ok();
        }

    }
}
