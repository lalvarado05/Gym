using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserRolManager
{
    public void Create(UserRole userRole)
    {
        if (AlreadyHave(userRole)) throw new Exception("El usuario ya tiene asigando el rol que se ingreso");
        if (RetrieveById(userRole.UserId) == null)
        {
            throw new Exception("El Usuario ingresado no existe");
        }
        var urCrud = new UserRoleFactory();
        urCrud.Create(userRole);
    }

    public void Update(UserRole userRole)
    {
        //No necesario por ahora preguntar
    }

    public void Delete(UserRole userRole)
    {
        var urCrud = new UserRoleFactory();
        urCrud.Delete(userRole);
    }

    public List<UserRole> RetrieveAll()
    {
        var urCrud = new UserRoleFactory();
        return urCrud.RetrieveAll<UserRole>();
    }

    public UserRole RetrieveById(int id)
    {
        var urCrud = new UserRoleFactory();
        return urCrud.RetrieveById<UserRole>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    public bool AlreadyHave(UserRole userRole)
    {
        var rCrud = new RoleCrudFactory();
        if (rCrud.RetrieveAllRolesByUserId(userRole.UserId) == null)
            return false;
        foreach (var item in rCrud.RetrieveAllRolesByUserId(userRole.UserId))
            if (userRole.RoleId == item.Id)
                return true;
        return false;
    }

    #endregion
}