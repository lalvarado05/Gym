using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserManager
{
    public void Create(User user)
    {
        var uCrud = new UserCrudFactory();
        uCrud.Create(user);
    }

    public void Update(User user)
    {
        var uCrud = new UserCrudFactory();
        uCrud.Update(user);
    }

    public void Delete(User user)
    {
        var uCrud = new UserCrudFactory();
        uCrud.Delete(user);
    }

    public List<User> RetrieveAll()
    {
        var uCrud = new UserCrudFactory();
        return uCrud.RetrieveAll<User>();
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
  
   public List<User> RetrieveByUserRole(int userId)
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveByRole(userId);
        }

        public List<User> RetrieveByUserRoleWithSchedule(int userId)
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveByRoleWithSchedule(userId);
        }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}