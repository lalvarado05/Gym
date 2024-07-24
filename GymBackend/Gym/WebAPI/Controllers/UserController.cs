using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    #region POSTS

    [HttpPost]
    [Route("Create")]
    public ActionResult Create(User user)
    {
        try
        {
            var um = new UserManager();
            um.Create(user);
            return Ok(user);
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
    public ActionResult Update(User user)
    {
        try
        {
            var um = new UserManager();
            um.Update(user);
            return Ok(user);
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
    public ActionResult Delete(User user)
    {
        try
        {
            var um = new UserManager();
            um.Delete(user);
            return Ok(user);
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
            var um = new UserManager();
            return Ok(um.RetrieveAll());
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveById")]
    public ActionResult RetrieveById(int id)
    {
        try
        {
            var um = new UserManager();
            return Ok(um.RetrieveById(id));
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByRole")]
    public ActionResult RetrieveByRole(int id)
    {
        try
        {
            var um = new UserManager();
            return Ok(um.RetrieveByUserRole(id));
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("RetrieveByRoleWithSchedule")]
    public ActionResult RetrieveByRoleWithSchedule(int id)
    {
        try
        {
            var um = new UserManager();
            return Ok(um.RetrieveByUserRoleWithSchedule(id));
        }
        catch (Exception ex)
        {
            // 500 es internal server error
            return StatusCode(500, ex.Message);
        }
    }
    #endregion
}
