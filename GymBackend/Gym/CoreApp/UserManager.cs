using DataAccess.CRUD;
using DTOs;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreApp;

public class UserManager
{
    public void Create(User user)
    {
        var uCrud = new UserCrudFactory();

        if (IsValidName(user))
        {
            if (IsValidLastName(user))
            {
                if (IsValidNumber(user))
                {
                    if (IsValidEmail(user))
                    {
                        if (IsValidGender(user))
                        {
                            if (IsAtLeastOneRoleSelected(user))
                            {
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
                                if (user.DaysOfWeek != "")
                                {
                                    Schedule newSchedule = new Schedule();
                                    newSchedule.DaysOfWeek = user.DaysOfWeek;
                                    newSchedule.TimeOfEntry = user.TimeOfEntry;
                                    newSchedule.TimeOfExit = user.TimeOfExit;
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
                            else
                            {
                                Console.WriteLine("Error: Debe seleccionar al menos un rol.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: Género no válido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Correo electrónico no válido.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Número de teléfono debe tener 8 dígitos.");
                }
            }
            else
            {
                Console.WriteLine("Error: Apellido no válido.");
            }
        }
        else
        {
            Console.WriteLine("Error: Nombre no válido.");
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

    // Aquí irían las validaciones

    #region Validations

    public bool IsValidName(User user)
    {
        return !string.IsNullOrWhiteSpace(user.Name);
    }

    public bool IsValidLastName(User user)
    {
        return !string.IsNullOrWhiteSpace(user.LastName);
    }

    public bool IsValidNumber(User user)
    {
        return user.Phone.Length == 8 && Regex.IsMatch(user.Phone, @"^\d{8}$");
    }

    public bool IsValidEmail(User user)
    {
        return Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public bool IsValidGender(User user)
    {
        var validGenders = new HashSet<string> { "M", "F", "O" };
        return validGenders.Contains(user.Gender);
    }

    public bool IsAtLeastOneRoleSelected(User user)
    {
        return user.ListaRole != null && user.ListaRole.Count > 0;
    }

    public bool IsValidUser(User user)
    {
        return IsValidName(user) &&
               IsValidLastName(user) &&
               IsValidNumber(user) &&
               IsValidEmail(user) &&
               IsValidGender(user) &&
               IsAtLeastOneRoleSelected(user);
    }

    #endregion
}
