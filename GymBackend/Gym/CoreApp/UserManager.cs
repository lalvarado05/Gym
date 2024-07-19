using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserManager
{
    public void Create(Schedule schedule)
    {
    }

    public void Update(Schedule schedule)
    {
    }

    public void Delete(Schedule schedule)
    {
    }

    public List<Schedule> RetrieveAll()
    {
        return null;
    }

    public User RetrieveById(int id)
    {
        var rolManager = new RolManager();

        var uCrud = new UserCrudFactory();
        var userFound = uCrud.RetrieveById<User>(id);
        var listaRolesUsuario = rolManager.RetrieveAllRolesByUserId(userFound.Id);
        userFound.ListaRole = listaRolesUsuario;
        return userFound;
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}