using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class UserCrudFactory : CrudFactory
{
    public UserCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    public override void Update(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    public override void Delete(BaseDTO baseDto)
    {
        throw new NotImplementedException();
    }

    public override T Retrieve<T>()
    {
        throw new NotImplementedException();
    }

    public override T RetrieveById<T>(int id)
    {
        // Crear instructivo para que el dao pueda realizar un create a la base de datos
        var sqlOperation = new SqlOperation();

        sqlOperation.ProcedureName = "RET_USER_BYID_PR";
        sqlOperation.AddIntParam("P_ID", id);
        var listaResultados = _sqlDao.ExecuteQueryProcedure(sqlOperation);

        if (listaResultados.Count > 0)
        {
            var row = listaResultados[0];
            var readUser = (T)Convert.ChangeType(BuildUser(row), typeof(T));
            return readUser;
        }

        return default;
    }

    public override List<T> RetrieveAll<T>()
    {
        throw new NotImplementedException();
    }

    private User BuildUser(Dictionary<string, object> row)
    {
        var userToReturn = new User
        {
            Id = (int)row["id"],
            Name = (string)row["name"],
            Phone = (string)row["phone"],
            LastName = (string)row["last_name"],
            Email = (string)row["email"],
            LastLogin = (DateTime)row["last_login"],
            Status = (string)row["status"],
            Gender = (string)row["gender"],
            BirthDate = (DateTime)row["birthdate"],
            Created = (DateTime)row["created"]
        };
        return userToReturn;
    }
}