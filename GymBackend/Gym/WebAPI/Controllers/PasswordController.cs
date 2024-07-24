using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        // CREATE --> POST
        // RETRIEVE --> GET
        // UPDATE --> PUT
        // DELETE --> DELETE

        #region POSTS

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Password password)
        {
            try
            {
                var pm = new PasswordManager();
                pm.Create(password);
                return Ok(password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region PUT

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Password password)
        {
            try
            {
                var pm = new PasswordManager();
                pm.Update(password);
                return Ok(password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region DELETE

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Password password)
        {
            try
            {
                var pm = new PasswordManager();
                pm.Delete(password);
                return Ok(password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion

        #region GETS

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var pm = new PasswordManager();
                return Ok(pm.RetrieveAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int id)
        {
            try
            {
                var pm = new PasswordManager();
                return Ok(pm.RetrieveById(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        #endregion
    }
}
