using InspectionAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace InspectionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyBaseController : ControllerBase
    {
        protected readonly DataContext _context;

        public MyBaseController(DataContext context)
        {
            _context = context;
        }
    }
}