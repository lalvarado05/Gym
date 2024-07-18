using DataAccess.DAOs;
using DTOs;

namespace DataAccess.CRUD;

public class RoleCrudFactory : CrudFactory
{
    public RoleCrudFactory()
    {
        _sqlDao = SqlDao.GetInstance();
    }

    public override void Create(BaseDTO baseDto)
    {
        //Conversion del DTO base a product
        var rol = baseDto as Rol;

        //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
        var sqlOperation = new SqlOperation();

        //Set del nombre del procedimiento
        sqlOperation.ProcedureName = "CRE_ROL_PR";

        //Agregamos los parametros
        sqlOperation.AddStringParam("P_Name", rol.Name);
        sqlOperation.AddStringParam("P_Status", rol.Status);

        //Ir al DAO a ejecutor
        _sqlDao.ExecuteProcedure(sqlOperation);
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
        var sqlOperation = new SqlOperation { ProcedureName = "RET_ROL_BY_ID_PR" };
        sqlOperation.AddIntParam("P_ID", id);

        //Ejecutamos contra el DAO
        var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
        if (lstResults.Count > 0)
        {
            var row = lstResults[0];
            var retRol = (T)Convert.ChangeType(BuildRol(row), typeof(T));
            return retRol;
        }

        return default;
    }

    public override List<T> RetrieveAll<T>()
    {
        throw new NotImplementedException();
    }

    private Rol BuildRol(Dictionary<string, object> row)
    {
        var rolToReturn = new Rol
        {
            Id = (int)row["id"],
            Name = (string)row["name"],
            Status = (string)row["status"]
        };
        return rolToReturn;
    }
}