using DataAccess.CRUD;
using DTOs;
using System.Collections.Generic;

namespace CoreApp
{
    public class MembershipManager
    {
        public void Create(Membership membership)
        {
            var meCrud = new MembershipCrudFactory();
            meCrud.Create(membership);
        }

        public void Update(Membership membership)
        {
            var meCrud = new MembershipCrudFactory();
            meCrud.Update(membership);
        }

        public void Delete(Membership membership)
        {
            var meCrud = new MembershipCrudFactory();
            meCrud.Delete(membership);
        }

        public List<Membership> RetrieveAll()
        {
            var meCrud = new MembershipCrudFactory();
            return meCrud.RetrieveAll<Membership>();
        }

        public Membership RetrieveById(int id)
        {
            var meCrud = new MembershipCrudFactory();
            return meCrud.RetrieveById<Membership>(id);
        }

        // Aquí irían las validaciones

        #region Validations

        #endregion
    }
}
