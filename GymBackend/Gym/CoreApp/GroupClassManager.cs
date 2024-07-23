using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class GroupClassManager
{
    public void Create(GroupClass groupClass)
    {
        var gcCrud = new GroupClassCrudFactory();
        // Validaciones adicionales
        gcCrud.Create(groupClass);
    }

    public void Update(GroupClass groupClass)
    {
        var gcCrud = new GroupClassCrudFactory();
        gcCrud.Update(groupClass);
    }

    public void Delete(GroupClass groupClass)
    {
        var gcCrud = new GroupClassCrudFactory();
        gcCrud.Delete(groupClass);
    }

    public List<GroupClass> RetrieveAll()
    {
        var gcCrud = new GroupClassCrudFactory();
        return gcCrud.RetrieveAll<GroupClass>();
    }

    public GroupClass RetrieveById(int id)
    {
        var gcCrud = new GroupClassCrudFactory();
        return gcCrud.RetrieveById<GroupClass>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    // Aquí puedes agregar validaciones adicionales para GroupClass, como asegurarte de que las fechas y horas sean válidas, etc.

    #endregion
}