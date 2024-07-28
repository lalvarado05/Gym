using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class MeetingsManager
{
    public void Create(Meetings meeting)
    {
        var mCrud = new MeetingsCrudFactory();
        // Validaciones adicionales

        if (WorksDayOfWeek(meeting))
        {
            if (IsInWorkHours(meeting))
            {
                if (NoGroupClassInterrupt(meeting))
                {
                    if (NoMeasureAppointmentInterrupt(meeting))
                    {
                        if (NoPersonalTrainingInterrupt(meeting))
                            mCrud.Create(meeting);
                        else
                            throw new Exception(
                                "Lo sentimos, el entrenador ya tiene programada una cita dentro de esa horas");
                    }
                    else
                    {
                        throw new Exception(
                            "Lo sentimos, el entrenador ya tiene programada una cita dentro de esa horas");
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
            throw new Exception("Lo sentimos, el entrenador no trabaja el dia: " + meeting.ProgrammedDate.DayOfWeek);
        }
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

    public List<Meetings> RetrieveAllWithName()
    {
        var mCrud = new MeetingsCrudFactory();
        return mCrud.RetrieveAllWithName();
    }

    // Aquí irían las validaciones

    #region Validations

    public bool WorksDayOfWeek(Meetings meeting)
    {
        // Esta funcion verifica que la cita para entrenamiento personal sea en un dia de la semana donde el entrenador si trabaja
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(meeting.EmployeeId);
        var DaySchedule = schedule.DaysOfWeek;
        var DayWeekProg = meeting.ProgrammedDate.DayOfWeek;
        for (var i = 0; i < DaySchedule.Length; i++)
            if (DaySchedule[i] == 'L')
            {
                if (DayWeekProg == DayOfWeek.Monday) return true;
            }
            else if (DaySchedule[i] == 'K')
            {
                if (DayWeekProg == DayOfWeek.Tuesday) return true;
            }
            else if (DaySchedule[i] == 'M')
            {
                if (DayWeekProg == DayOfWeek.Wednesday) return true;
            }
            else if (DaySchedule[i] == 'J')
            {
                if (DayWeekProg == DayOfWeek.Thursday) return true;
            }
            else if (DaySchedule[i] == 'V')
            {
                if (DayWeekProg == DayOfWeek.Friday) return true;
            }
            else if (DaySchedule[i] == 'S')
            {
                if (DayWeekProg == DayOfWeek.Saturday) return true;
            }
            else if (DaySchedule[i] == 'D')
            {
                if (DayWeekProg == DayOfWeek.Sunday) return true;
            }

        return false;
    }

    public bool IsInWorkHours(Meetings meeting)
    {
        //Esta funcion verifica que la cita de medicion sea dentro de las horas laborales del entrenador
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(meeting.EmployeeId);
        var start = schedule.TimeOfEntry;
        var end = schedule.TimeOfExit;
        var startProg = meeting.TimeOfEntry;
        var endProg = meeting.TimeOfExit;
        if (startProg >= start && endProg <= end) return true;
        return false;
    }

    public bool NoGroupClassInterrupt(Meetings meeting)
    {
        var gcCrud = new GroupClassCrudFactory();
        var lstAllGC = gcCrud.RetrieveByUserId(meeting.EmployeeId);
        List<GroupClass> lstSameDate = [];
        foreach (var groupClass in lstAllGC)
            if (groupClass.ClassDate.Date == meeting.ProgrammedDate.Date)
                lstSameDate.Add(groupClass);
        foreach (var groupClass in lstSameDate)
            if (meeting.TimeOfEntry >= groupClass.StartTime && meeting.TimeOfEntry < groupClass.EndTime)
                return false;
            else if (meeting.TimeOfExit > groupClass.StartTime && meeting.TimeOfExit <= groupClass.EndTime)
                return false;
        return true;
    }

    public bool NoMeasureAppointmentInterrupt(Meetings meeting)
    {
        var mCrud = new MeetingsCrudFactory();
        var lstAllMeetings = mCrud.RetrieveByUserId(meeting.EmployeeId);
        List<Meetings> lstSameDate = [];
        foreach (var meetingTrainer in lstAllMeetings)
            if (meetingTrainer.ProgrammedDate.Date == meeting.ProgrammedDate.Date)
                lstSameDate.Add(meetingTrainer);
        foreach (var meetingTrainer in lstSameDate)
            if (meeting.TimeOfEntry >= meetingTrainer.TimeOfEntry && meeting.TimeOfEntry < meetingTrainer.TimeOfExit)
                return false;
            else if (meeting.TimeOfExit > meetingTrainer.TimeOfEntry &&
                     meeting.TimeOfExit <= meetingTrainer.TimeOfExit) return false;
        return true;
    }

    public bool NoPersonalTrainingInterrupt(Meetings meeting)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        var lstAllPT = ptCrud.RetrieveByEmployeeId(meeting.EmployeeId);
        List<PersonalTraining> lstSameDate = [];
        foreach (var pt in lstAllPT)
            if (pt.ProgrammedDate.Date == meeting.ProgrammedDate.Date)
                lstSameDate.Add(pt);
        foreach (var pt in lstSameDate)
            if (meeting.TimeOfEntry >= pt.TimeOfEntry && meeting.TimeOfEntry < pt.TimeOfExit)
                return false;
            else if (meeting.TimeOfExit > pt.TimeOfEntry && meeting.TimeOfExit <= pt.TimeOfExit) return false;
        return true;
    }

    #endregion
}