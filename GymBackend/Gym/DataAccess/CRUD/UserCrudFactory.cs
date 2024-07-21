using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class UserCrudFactory : CrudFactory
    {
        public UserCrudFactory()
        {
            _sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseDTO baseDto)
        {
            var user = baseDto as User;

            var sqlOperation = new SqlOperation
            {
                ProcedureName = "CRE_USER_PR"
            };

            sqlOperation.AddStringParam("P_Name", user.Name);
            sqlOperation.AddStringParam("P_LastName", user.LastName);
            sqlOperation.AddStringParam("P_Phone", user.Phone);
            sqlOperation.AddStringParam("P_Email", user.Email);
            sqlOperation.AddStringParam("P_Status", user.Status);
            sqlOperation.AddStringParam("P_Gender", user.Gender);
            sqlOperation.AddDateTimeParam("P_BirthDate", user.BirthDate);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }


        public override void Delete(BaseDTO baseDto)
        {
            var user = baseDto as User;

            var sqlOperation = new SqlOperation
            {
                ProcedureName = "DEL_USER_PR"
            };

            sqlOperation.AddIntParam("P_Id", user.Id);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstUsers = new List<T>();
            var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_USERS_PR" };
            var lstResults = _sqlDao.ExecuteQueryProcedure(sqlOperation);
            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var user = BuildUser(row);
                    lstUsers.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }
            return lstUsers;
        }

        public override void Update(BaseDTO baseDto)
        {
            var user = baseDto as User;

            var sqlOperation = new SqlOperation
            {
                ProcedureName = "UPD_USER_PR"
            };

            sqlOperation.AddIntParam("P_Id", user.Id);
            sqlOperation.AddStringParam("P_Name", user.Name);
            sqlOperation.AddStringParam("P_LastName", user.LastName);
            sqlOperation.AddStringParam("P_Phone", user.Phone);
            sqlOperation.AddStringParam("P_Email", user.Email);
            sqlOperation.AddDateTimeParam("P_LastLogin", user.LastLogin);
            sqlOperation.AddStringParam("P_Status", user.Status);
            sqlOperation.AddStringParam("P_Gender", user.Gender);
            sqlOperation.AddDateTimeParam("P_BirthDate", user.BirthDate);

            _sqlDao.ExecuteProcedure(sqlOperation);
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
}