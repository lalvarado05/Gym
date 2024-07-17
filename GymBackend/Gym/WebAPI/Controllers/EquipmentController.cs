using DTOs;
using CoreApp;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        //CREATE --> POST
        //REATRIVE --> GET
        //UPDATE --> PUT
        //DELETE --> DELETE
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Equipment equipment)
        {
            try
            {
                var em = new EquipmentManager();
                em.Create(equipment);
                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
