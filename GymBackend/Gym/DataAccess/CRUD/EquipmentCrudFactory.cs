using DataAccess.DAOs;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class EquipmentCrudFactory : CrudFactory
    {
        public EquipmentCrudFactory() 
        { 
            _sqlDao = SqlDao.GetInstance();
        }
        public override void Create(BaseDTO baseDto)
        {
            //Conversion del DTO base a product
            var equipment = baseDto as Equipment;

            //Crear el instructivo para que el DAO Pueda realizar un create en la base de datos
            var sqlOperation = new SqlOperation();

            //Set del nombre del procedimiento
            sqlOperation.ProcedureName = "CRE_EQUIPMENT_PR";

            //Agregamos los parametros
            sqlOperation.AddStringParam("P_Name", equipment.Name);
            sqlOperation.AddStringParam("P_Location", equipment.Location);
            sqlOperation.AddStringParam("P_Description", equipment.Description);
            
            //Ir al DAO a ejecutor
            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDto)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO baseDto)
        {
            throw new NotImplementedException();
        }
    }
}
