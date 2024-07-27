using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;

namespace CoreApp
{
    public class MeasuresManager
    {
        public void Create(Measures measure)
        {
            var mCrud = new MeasureCrud();
            // Validaciones adicionales
            mCrud.Create(measure);
        }

        public void Update(Measures measure)
        {
            var mCrud = new MeasureCrud();
            mCrud.Update(measure);
        }

        public void Delete(Measures measure)
        {
            var mCrud = new MeasureCrud();
            mCrud.Delete(measure);
        }

        public List<Measures> RetrieveAll()
        {
            var mCrud = new MeasureCrud();
            return mCrud.RetrieveAll<Measures>();
        }

        public Measures RetrieveById(int id)
        {
            var mCrud = new MeasureCrud();
            return mCrud.RetrieveById<Measures>(id);
        }

        // Aquí irían las validaciones

        #region Validations

        #endregion
    }
}