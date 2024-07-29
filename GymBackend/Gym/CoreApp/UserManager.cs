using DataAccess.CRUD;
using DTOs;
using System.Collections.Generic;

namespace CoreApp;

public class UserManager
{
    public void Create(User user)
    {
        var uCrud = new UserCrudFactory();

        if (!IsValidName(user))
        {
            throw new Exception("Error: Nombre no válido.");
        }

        if (!IsValidLastName(user))
        {
            throw new Exception("Error: Apellido no válido.");
        }

        if (!IsValidNumber(user))
        {
            throw new Exception("Error: Número de teléfono debe tener 8 dígitos.");
        }

        if (!IsValidEmail(user))
        {
            throw new Exception("Error: Correo electrónico no válido.");
        }

        if (!IsValidGender(user))
        {
            throw new Exception("Error: Género no válido.");
        }

        if (!IsAtLeastOneRoleSelected(user))
        {
            throw new Exception("Error: Debe seleccionar al menos un rol.");
        }

        uCrud.Create(user);
        var pM = new PasswordManager();
        var sM = new ScheduleManager();
        // Hace Retrieve del user creado a través del Email
        var userJustCreated = uCrud.RetrieveByEmail(user.Email);

        // Se genera el nuevo password con la info recibida
        Password newUser = new Password();
        var password = GeneratePassword(8);
        newUser.PasswordContent = ComputeMD5Hash(password);

        newUser.UserId = userJustCreated.Id;
        pM.Create(newUser);

        // Checkea si DaysOfWeek contiene algo (Entrenador)
        if (user.DaysOfWeek != "" && user.DaysOfWeek != null)
        {
            Schedule newSchedule = new Schedule();
            newSchedule.DaysOfWeek = user.DaysOfWeek;

            // Manejar los valores nulos de TimeOfEntry y TimeOfExit
            newSchedule.TimeOfEntry = user.TimeOfEntry ?? default(TimeOnly);
            newSchedule.TimeOfExit = user.TimeOfExit ?? default(TimeOnly);
            
            newSchedule.EmployeeId = userJustCreated.Id;
            sM.Create(newSchedule);
        }
        // Itera a través de los roles, y se genera una instancia de userRole para cada uno de ellos.
        foreach (Rol rol in user.ListaRole)
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
        var urCrud = new UserRoleFactory();

        uCrud.Update(user);
        urCrud.Delete(user);

        // Itera a través de los roles, y se genera una instancia de userRole para cada uno de ellos.
        foreach (Rol rol in user.ListaRole)
        {
            var newUserRole = new UserRole();
            newUserRole.RoleId = rol.Id;
            newUserRole.UserId = user.Id;
            urCrud.Create(newUserRole);
        }
    }

        public void Delete(User user)
        {
            var uCrud = new UserCrudFactory();
            uCrud.Delete(user);
        }

    public List<User> RetrieveAll()
    {
        var uCrud = new UserCrudFactory();
        var urCrud = new UserRoleFactory();

        var users = uCrud.RetrieveAll<User>();

        foreach (var user in users)
        {
            user.ListaRole = urCrud.RetrieveByUserId(user.Id);
        }

        return users;
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

    public static string GeneratePassword(int length)
    {
        if (length < 8)
        {
            throw new ArgumentException("La longitud de la contraseña debe ser al menos 8 caracteres.");
        }

        var letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var digits = "0123456789";
        var symbols = "!@#$%&*?";

        var random = new Random();
        var password = new StringBuilder(length);

        // Ensure at least one letter, digit, and symbol
        password.Append(letters[random.Next(letters.Length)]);
        password.Append(digits[random.Next(digits.Length)]);
        password.Append(symbols[random.Next(symbols.Length)]);

        // Fill the rest of the password length with random characters from all sets
        var allCharacters = letters + digits + symbols;
        for (int i = 3; i < length; i++)
        {
            password.Append(allCharacters[random.Next(allCharacters.Length)]);
        }

        // Shuffle the result to ensure randomness
        var array = password.ToString().ToCharArray();
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            char temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }

        return new string(array);
    }

    public List<User> RetrieveByUserRoleWithSchedule(int userId)
    {
        var uCrud = new UserCrudFactory();
        return uCrud.RetrieveByRoleWithSchedule(userId);
    }

    public User RetrieveUserByCredentials(string email, string password)
    {
        var uCrud = new UserCrudFactory();
        var hashedPassword = ComputeMD5Hash(password);
        var user = uCrud.RetrieveUserByCredentials(email, hashedPassword);
        user.ListaRole = GetUserRoles(user.Id);
        return user;
    }

    public List<Rol> GetUserRoles(int userId)
    {
        var rCrud = new RoleCrudFactory();
        var roles = rCrud.RetrieveAllRolesByUserId(userId);
        return roles;
    }


    // Aquí irían las validaciones

        #region Validations

        #endregion

    }
}
