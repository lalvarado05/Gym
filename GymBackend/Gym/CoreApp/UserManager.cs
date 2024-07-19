using DataAccess.CRUD;
using DTOs;
using System.Collections.Generic;

namespace CoreApp
{
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
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveById<User>(id);
        }

        // Aquí irían las validaciones

        #region Validations

        #endregion
    }
}
