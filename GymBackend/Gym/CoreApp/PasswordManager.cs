using DataAccess.CRUD;
using DTOs;

namespace CoreApp
{
    public class PasswordManager
    {
        public void Create(Password password)
        {
            var pCrud = new PasswordCrudFactory();
            // Validaciones adicionales
            pCrud.Create(password);
        }

        public void Update(Password password)
        {
            var pCrud = new PasswordCrudFactory();
            pCrud.Update(password);
        }

        public void Delete(Password password)
        {
            var pCrud = new PasswordCrudFactory();
            pCrud.Delete(password);
        }

        public List<Password> RetrieveAll()
        {
            var pCrud = new PasswordCrudFactory();
            return pCrud.RetrieveAll<Password>();
        }

        public Password RetrieveById(int id)
        {
            var pCrud = new PasswordCrudFactory();
            return pCrud.RetrieveById<Password>(id);
        }

        internal bool isValidPassword(string password)
        {
            throw new NotImplementedException();
        }

        // Aquí irían las validaciones

        #region Validations

        #endregion
    }
}
