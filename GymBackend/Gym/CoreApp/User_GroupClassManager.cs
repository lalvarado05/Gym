using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserGroupClassManager
{
    public void Create(UserGroupClass userGroupClass)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        var gcCrud = new GroupClassCrudFactory();
        // Validaciones adicionales
        GroupClass groupClass = gcCrud.RetrieveById<GroupClass>(userGroupClass.GroupClassId);

        if (userGroupClass.GroupClassId == 0)
        {
            throw new Exception("Por favor elija una clase de la tabla.");
        }
        if (groupClass.CurrentRegistered == groupClass.MaxCapacity)
        {
            throw new Exception("Lo sentimos, esta clase esta llena.");
        }

        //Logica para verificar si el usuario ya esta registrado en la clase
        List<UserGroupClass> alreadyRegistered = ugcCrud.RetrieveByGroupClassId(userGroupClass.GroupClassId);
        foreach (UserGroupClass check in alreadyRegistered) {
            if (check.ClientId == userGroupClass.ClientId)
            {
                throw new Exception("Lo sentimos, usted ya esta registrado a esta clase.");
            }
        }

        groupClass.CurrentRegistered += 1;
        gcCrud.Update(groupClass);
        ugcCrud.Create(userGroupClass);
    }

    public void Update(UserGroupClass userGroupClass)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        ugcCrud.Update(userGroupClass);
    }

    public void Delete(UserGroupClass userGroupClass)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        ugcCrud.Delete(userGroupClass);
    }

    public List<UserGroupClass> RetrieveAll()
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        return ugcCrud.RetrieveAll<UserGroupClass>();
    }

    public UserGroupClass RetrieveById(int id)
    {
        var ugcCrud = new UserGroupClassCrudFactory();
        return ugcCrud.RetrieveById<UserGroupClass>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}