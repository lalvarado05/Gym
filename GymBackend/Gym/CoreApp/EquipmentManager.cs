using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class EquipmentManager
    {
        public void Create(Equipment equipment)
        {
            var eCrud = new EquipmentCrudFactory();
            //Validaciones adicionales
            eCrud.Create(equipment);
        }
    }
}
