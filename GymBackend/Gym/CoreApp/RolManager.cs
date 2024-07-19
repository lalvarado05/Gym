﻿using DataAccess.CRUD;
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

    public void Delete()
    {
    }

    public List<Schedule> RetrieveAll()
    {
        return null;
    }

    public Rol RetrieveById(int id)
    {
        var rCrud = new RoleCrudFactory();
        return rCrud.RetrieveById<Rol>(id);
    }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}