using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class DiscountManager
{
    public void Create(Discount discount)
    {
        var dCrud = new DiscountCrudFactory();
        dCrud.Create(discount);
    }

    public void Update(Discount discount)
    {
        var dCrud = new DiscountCrudFactory();
        dCrud.Update(discount);
    }

    public void Delete(Discount discount)
    {
        var dCrud = new DiscountCrudFactory();
        dCrud.Delete(discount);
    }

    public List<Discount> RetrieveAll()
    {
        var dCrud = new DiscountCrudFactory();
        return dCrud.RetrieveAll<Discount>();
    }

    public Discount RetrieveById(int id)
    {
        var dCrud = new DiscountCrudFactory();
        return dCrud.RetrieveById<Discount>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}