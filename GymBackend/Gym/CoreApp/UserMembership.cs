using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;

namespace CoreApp;

public class UserMembershipManager
{
    public void Create(UserMembership userMembership)
    {
        ValidateUserMembership(userMembership);

        var umCrud = new UserMembershipCrud();
        umCrud.Create(userMembership);
    }

    public void Update(UserMembership userMembership)
    {
        var umCrud = new UserMembershipCrud();
        umCrud.Update(userMembership);
    }

    public void Delete(UserMembership userMembership)
    {
        var umCrud = new UserMembershipCrud();
        umCrud.Delete(userMembership);
    }

    public List<UserMembership> RetrieveAll()
    {
        var umCrud = new UserMembershipCrud();
        return umCrud.RetrieveAll<UserMembership>();
    }

    public UserMembership RetrieveById(int id)
    {
        var umCrud = new UserMembershipCrud();
        return umCrud.RetrieveById<UserMembership>(id);
    }

    public List<UserMembership> RetrieveByUserId(int userId)
    {
        var umCrud = new UserMembershipCrud();
        return umCrud.RetrieveByUserId(userId);
    }

    #region Validations
    private void ValidateUserMembership(UserMembership userMembership)
    {
        if (userMembership.UserId <= 0)
        {
            throw new Exception("Por favor selecione un cliente.");
        }

        if (userMembership.MembershipId <= 0)
        {
            throw new Exception("El ID de membresía no es válido.");
        }
    }
    #endregion
}