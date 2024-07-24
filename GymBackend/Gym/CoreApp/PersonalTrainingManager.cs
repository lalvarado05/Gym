using DataAccess.CRUD;
using DTOs;

namespace CoreApp
{
    public class PersonalTrainingManager
    {
        public void Create(PersonalTraining personalTraining)
        {
            var ptCrud = new PersonalTrainingCrudFactory();
            if (WorksDayOfWeek(personalTraining))
            {
                if (IsInWorkHours(personalTraining))
                {
                    if (NoGroupClassInterrupt(personalTraining))
                    {
                        if (NoMeasureAppointmentInterrupt(personalTraining))
                        {
                            if (NoPersonalTrainingInterrupt(personalTraining))
                            {
                                ptCrud.Create(personalTraining);
                            }
                            else
                            {
                                throw new Exception("Lo sentimos, el entrenador ya tiene programada una cita dentro de esa horas");
                            }
                        }
                        else
                        {
                            throw new Exception("Lo sentimos, el entrenador ya tiene programada una cita dentro de esa horas");
                        }
                    }
                    else
                    {
                        throw new Exception("Lo sentimos, el entrenador da una clase grupal a esta hora");
                    }
                }
                else
                {
                    throw new Exception("Lo sentimos, el entrenador no trabaja a estas horas");
                }
            }
            else
            {
                throw new Exception("Lo sentimos, el entrenador no trabaja el dia: " + personalTraining.ProgrammedDate.DayOfWeek);
            }
            
        }

        public void Update(PersonalTraining personalTraining)
        {
            var ptCrud = new PersonalTrainingCrudFactory();
            ptCrud.Update(personalTraining);
        }

        public void Delete(PersonalTraining personalTraining)
        {
            var ptCrud = new PersonalTrainingCrudFactory();
            ptCrud.Delete(personalTraining);
        }

        public List<PersonalTraining> RetrieveAll()
        {
            var ptCrud = new PersonalTrainingCrudFactory();
            return ptCrud.RetrieveAll<PersonalTraining>();
        }

        public PersonalTraining RetrieveById(int id)
        {
            var ptCrud = new PersonalTrainingCrudFactory();
            return ptCrud.RetrieveById<PersonalTraining>(id);
        }

        // Aquí irían las validaciones

        #region Validations
        public bool WorksDayOfWeek(PersonalTraining personalTraining)
        {
            // Esta funcion verifica que la cita para entrenamiento personal sea en un dia de la semana donde el entrenador si trabaja
            var sCrud = new ScheduleCrudFactory();
            var schedule = sCrud.RetrieveScheduleByUserID(personalTraining.EmployeeId);
            var DaySchedule = schedule.DaysOfWeek;
            var DayWeekProg = personalTraining.ProgrammedDate.DayOfWeek;
            for(int i = 0; i< DaySchedule.Length;i++)
            {
                if (DaySchedule[i] == 'L')
                {
                    if(DayWeekProg == DayOfWeek.Monday)
                    {
                        return true;
                    }
                }
                else if(DaySchedule[i] == 'K')
                {
                    if (DayWeekProg == DayOfWeek.Tuesday)
                    {
                        return true;
                    }
                }
                else if (DaySchedule[i] == 'M')
                {
                    if (DayWeekProg == DayOfWeek.Wednesday)
                    {
                        return true;
                    }
                }
                else if (DaySchedule[i] == 'J')
                {
                    if (DayWeekProg == DayOfWeek.Thursday)
                    {
                        return true;
                    }
                }
                else if (DaySchedule[i] == 'V')
                {
                    if (DayWeekProg == DayOfWeek.Friday)
                    {
                        return true;
                    }
                }
                else if (DaySchedule[i] == 'S')
                {
                    if (DayWeekProg == DayOfWeek.Saturday)
                    {
                        return true;
                    }
                }
                else if (DaySchedule[i] == 'D')
                {
                    if (DayWeekProg == DayOfWeek.Sunday)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool IsInWorkHours(PersonalTraining personalTraining)
        {
            //Esta funcion verifica que el entrenamiento personal sea dentro de las horas laborales del entrenador
            var sCrud = new ScheduleCrudFactory();
            var schedule = sCrud.RetrieveScheduleByUserID(personalTraining.EmployeeId);
            TimeOnly start = schedule.TimeOfEntry;
            TimeOnly end = schedule.TimeOfExit;
            TimeOnly startProg = personalTraining.TimeOfEntry;
            TimeOnly endProg = personalTraining.TimeOfExit;
            if(startProg>=start && endProg <= end)
            {
                return true;
            }
            return false;
        }

        public bool NoGroupClassInterrupt(PersonalTraining personalTraining) 
        { 
            var gcCrud = new GroupClassCrudFactory();
            List<GroupClass> lstAllGC = gcCrud.RetrieveByUserId(personalTraining.EmployeeId);
            List<GroupClass> lstSameDate = [];
            foreach(GroupClass groupClass in lstAllGC)
            {
                if (groupClass.ClassDate.Date == personalTraining.ProgrammedDate.Date)
                {
                    lstSameDate.Add(groupClass);
                }
            }
            foreach(GroupClass groupClass in lstSameDate)
            {
                if (personalTraining.TimeOfEntry >= groupClass.StartTime && personalTraining.TimeOfEntry < groupClass.EndTime)
                {
                    return false;
                }
                else if (personalTraining.TimeOfExit > groupClass.StartTime && personalTraining.TimeOfExit <= groupClass.EndTime)
                {
                    return false;
                }
            }
            return true;
        }

        public bool NoMeasureAppointmentInterrupt(PersonalTraining personalTraining)
        {
            var mCrud = new MeetingsCrudFactory();
            List<Meetings> lstAllMeetings = mCrud.RetrieveByUserId(personalTraining.EmployeeId);
            List<Meetings> lstSameDate = [];
            foreach (Meetings meeting in lstAllMeetings)
            {
                if (meeting.ProgrammedDate == personalTraining.ProgrammedDate.Date)
                {
                    lstSameDate.Add(meeting);
                }
            }
            foreach (Meetings meeting in lstSameDate)
            {
                if (personalTraining.TimeOfEntry >= meeting.TimeOfEntry && personalTraining.TimeOfEntry < meeting.TimeOfExit)
                {
                    return false;
                }
                else if (personalTraining.TimeOfExit > meeting.TimeOfEntry && personalTraining.TimeOfExit <= meeting.TimeOfExit)
                {
                    return false;
                }
            }
            return true;
        }

        public bool NoPersonalTrainingInterrupt(PersonalTraining personalTraining)
        {
            var ptCrud = new PersonalTrainingCrudFactory();
            List<PersonalTraining> lstAllPT = ptCrud.RetrieveByUserId(personalTraining.EmployeeId);
            List<PersonalTraining> lstSameDate = [];
            foreach (PersonalTraining pt in lstAllPT)
            {
                if (pt.ProgrammedDate.Date == personalTraining.ProgrammedDate.Date)
                {
                    lstSameDate.Add(pt);
                }
            }
            foreach (PersonalTraining pt in lstSameDate)
            {
                if (personalTraining.TimeOfEntry >= pt.TimeOfEntry && personalTraining.TimeOfEntry < pt.TimeOfExit)
                {
                    return false;
                }
                else if (personalTraining.TimeOfExit > pt.TimeOfEntry && personalTraining.TimeOfExit <= pt.TimeOfExit)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}