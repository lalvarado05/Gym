using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class RolManager
{
    public void Create(Rol rol)
    {
        var rCrud = new RoleCrudFactory();
        // Validaciones adicionales
        rCrud.Create(rol);
    }

    public void Update(Rol rol)
    {
        var rCrud = new RoleCrudFactory();
        rCrud.Update(rol);
    }

    public void Delete(int idUsuario, int idRol)
    {
        var rCrud = new RoleCrudFactory();
        rCrud.DeleteById(idUsuario, idRol);
    }

    public List<Rol> RetrieveAll()
    {
        var uRol = new RoleCrudFactory();
        return uRol.RetrieveAll<Rol>();
    }

    public Rol RetrieveById(int id)
    {
        var rCrud = new RoleCrudFactory();
        return rCrud.RetrieveById<Rol>(id);
    }

    public List<Rol> RetrieveAllRolesByUserId(int userId)
    {
        var rCrud = new RoleCrudFactory();
        return rCrud.RetrieveAllRolesByUserId(userId);
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}