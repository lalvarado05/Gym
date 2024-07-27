using DataAccess.CRUD;
using DTOs;
using System.Security.Cryptography;
using System.Text;

namespace CoreApp;

public class UserManager
{
    public void Create(User user)
    {
        var uCrud = new UserCrudFactory();
        uCrud.Create(user);

        var pM = new PasswordManager();
        var sM = new ScheduleManager();
        // Hace Retrieve del user creado a traves del Email
        var userJustCreated = uCrud.RetrieveByEmail(user.Email);

        // Se genera el nuevo password con la info recibida
        Password newUser = new Password();
        newUser.PasswordContent = ComputeMD5Hash(user.Password);
        newUser.UserId = userJustCreated.Id;
        pM.Create(newUser);

        // Checkea si DaysOfWeek contiene algo (Entrenador)
        if (user.DaysOfWeek != "")
        {
            Schedule newSchedule = new Schedule();
            newSchedule.DaysOfWeek = user.DaysOfWeek;
            newSchedule.TimeOfEntry = user.TimeOfEntry;
            newSchedule.TimeOfExit = user.TimeOfExit;
            newSchedule.EmployeeId = userJustCreated.Id;
            sM.Create(newSchedule);
        }
        // Itera a traves de los roles, y se genera una instancia de userRole para cada uno de ellos.
        foreach(Rol rol in user.ListaRole)
        {
            var urCrud = new UserRoleFactory();
            var newUserRole = new UserRole();
            newUserRole.RoleId = rol.Id;
            newUserRole.UserId = userJustCreated.Id;
            urCrud.Create(newUserRole);
        }


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
        var rolManager = new RolManager();
        var uCrud = new UserCrudFactory();
        var userFound = uCrud.RetrieveById<User>(id);
        var listaRolesUsuario = rolManager.RetrieveAllRolesByUserId(userFound.Id);
        userFound.ListaRole = listaRolesUsuario;
        return userFound;
    }

    public string ComputeMD5Hash(string password)
    {
        // Create an instance of the MD5CryptoServiceProvider
        using (MD5 md5 = MD5.Create())
        {
            // Compute the hash from the input string
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convert the byte array to a hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }

    public User RetrieveByEmail(string email)
    {
        var uCrud = new UserCrudFactory();
        return uCrud.RetrieveByEmail(email);
    }
    public List<User> RetrieveByUserRole(int userId)
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveByRole(userId);
        }

        public List<User> RetrieveByUserRoleWithSchedule(int userId)
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveByRoleWithSchedule(userId);
        }

    // Aquí irían las validaciones

    #region Validations

    #endregion
}