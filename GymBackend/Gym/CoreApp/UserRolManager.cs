﻿using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class UserRolManager
{
    public void Create(UserRole userRole)
    {
        var urCrud = new UserRoleFactory();
        urCrud.Create(userRole);
    }

    public void Update(UserRole userRole)
    {
        //No necesario por ahora preguntar
    }

    public void Delete(UserRole userRole)
    {
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

    #endregion
}