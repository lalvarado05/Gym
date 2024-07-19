using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class EquipmentManager
{
    public void Create(Equipment equipment)
    {
        var eCrud = new EquipmentCrudFactory();
        //Validaciones adicionales
        eCrud.Create(equipment);
    }

    public void Update(Equipment equipment)
    {
        var eCrud = new EquipmentCrudFactory();
        eCrud.Update(equipment);
    }

    public void Delete(Equipment equipment)
    {
        var eCrud = new EquipmentCrudFactory();
        eCrud.Delete(equipment);
    }

    public List<Equipment> RetrieveAll()
    {
        var eCrud = new EquipmentCrudFactory();
        return eCrud.RetrieveAll<Equipment>();
    }

    public Equipment RetrieveById(int id)
    {
        var eCrud = new EquipmentCrudFactory();
        return eCrud.RetrieveById<Equipment>(id);
    }


    //Aqui irian las validaciones

    #region Validations

    #endregion
}