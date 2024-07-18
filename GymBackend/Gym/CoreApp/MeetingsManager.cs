using DataAccess.CRUD;
using DTOs;
using System.Collections.Generic;

namespace CoreApp
{
    public class MeetingsManager
    {
        public void Create(Meetings meeting)
        {
            var mCrud = new MeetingsCrudFactory();
            // Validaciones adicionales
            mCrud.Create(meeting);
        }

        public void Update(Meetings meeting)
        {
            var mCrud = new MeetingsCrudFactory();
            mCrud.Update(meeting);
        }

        public void Delete(Meetings meeting)
        {
            var mCrud = new MeetingsCrudFactory();
            mCrud.Delete(meeting);
        }

        public List<Meetings> RetrieveAll()
        {
            var mCrud = new MeetingsCrudFactory();
            return mCrud.RetrieveAll<Meetings>();
        }

        public Meetings RetrieveById(int id)
        {
            var mCrud = new MeetingsCrudFactory();
            return mCrud.RetrieveById<Meetings>(id);
        }

        // Aquí irían las validaciones

        #region Validations
        // Aquí puedes agregar validaciones adicionales para Meetings, como asegurarte de que las fechas y horas sean válidas, etc.
        #endregion
    }
}