using DataAccess.CRUD;
using DTOs;

namespace CoreApp;

public class MeetingsManager
{
    public void Create(Meetings meeting)
    {
        var mCrud = new MeetingsCrudFactory();

        ValidateMeeting(meeting);

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
    private void ValidateMeeting(Meetings meeting)
    {
        if (meeting.ClientId <= 0)
        {
            throw new Exception("Por favor seleciona un cliente.");
        }

        if (meeting.EmployeeId <= 0)
        {
            throw new Exception("Por favor seleciona un entrenador.");
        }

        // Validacion de que TimeOfEntry y TimeOfExit sean válidos y que TimeOfEntry sea menor que TimeOfExit
        if (meeting.TimeOfEntry == default)
        {
            throw new Exception("Por favor ingresa un hora en entrada.");
        }

        if (meeting.TimeOfExit == default)
        {
            throw new Exception("Por favor ingresa un hora en entrada.");
        }

        if (meeting.TimeOfEntry >= meeting.TimeOfExit)
        {
            throw new Exception("La hora de salida no puede ser antes que la hora de entrada.");
        }

        // Validacion de que ProgrammedDate no sea nulo o vacío y que sea una fecha en el futuro
        if (meeting.ProgrammedDate == default)
        {
            throw new Exception("Por favor elige una fecha");
        }

        if (meeting.ProgrammedDate.Date <= DateTime.Now.Date)
        {
            throw new Exception("La cita debe ser una fecha en el futuro.");
        }

        // Validaciones de logica para que no choquen las citas
        if (!WorksDayOfWeek(meeting))
        {
            throw new Exception("Lo sentimos, el entrenador no trabaja este día de la semana.");
        }

        if (!IsInWorkHours(meeting))
        {
            throw new Exception("Lo sentimos, el entrenador no trabaja a estas horas");
        }

        if (!NoGroupClassInterrupt(meeting))
        {
            throw new Exception("Lo sentimos, el entrenador da una clase grupal a esta hora");
        }

        if (!NoMeasureAppointmentInterrupt(meeting))
        {
            throw new Exception("Lo sentimos, el entrenador ya tiene programada una cita dentro de esas horas");
        }

        if (!NoPersonalTrainingInterrupt(meeting))
        {
            throw new Exception("Lo sentimos, el entrenador ya tiene programada una cita dentro de esas horas");
        }
    }


    public bool WorksDayOfWeek(Meetings meeting)
    {
        // Esta funcion verifica que la cita para entrenamiento personal sea en un dia de la semana donde el entrenador si trabaja
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(meeting.EmployeeId);
        var DaySchedule = schedule.DaysOfWeek;
        var DayWeekProg = meeting.ProgrammedDate.DayOfWeek;

        var daysMap = new Dictionary<char, DayOfWeek>
        {
        { 'L', DayOfWeek.Monday },
        { 'K', DayOfWeek.Tuesday },
        { 'M', DayOfWeek.Wednesday },
        { 'J', DayOfWeek.Thursday },
        { 'V', DayOfWeek.Friday },
        { 'S', DayOfWeek.Saturday },
        { 'D', DayOfWeek.Sunday }
        };

        foreach (var day in DaySchedule)
        {
            if (daysMap.TryGetValue(day, out var mappedDay) && mappedDay == DayWeekProg)
            {
                return true;
            }
        }

        return false;
    }
    public bool IsInWorkHours(Meetings meeting)
    {
        //Esta funcion verifica que la cita de medicion sea dentro de las horas laborales del entrenador
        var sCrud = new ScheduleCrudFactory();
        var schedule = sCrud.RetrieveScheduleByUserID(meeting.EmployeeId);
        TimeOnly start = schedule.TimeOfEntry;
        TimeOnly end = schedule.TimeOfExit;
        TimeOnly startProg = meeting.TimeOfEntry;
        TimeOnly endProg = meeting.TimeOfExit;

        return startProg >= start && endProg <= end;
    }

    public bool NoGroupClassInterrupt(Meetings meeting)
    {
        var gcCrud = new GroupClassCrudFactory();
        var lstAllGC = gcCrud.RetrieveByUserId(meeting.EmployeeId);

        return !lstAllGC.Any(gc =>
            gc.ClassDate.Date == meeting.ProgrammedDate.Date &&
            (meeting.TimeOfEntry < gc.EndTime && meeting.TimeOfExit > gc.StartTime)
        );
    }

    public bool NoMeasureAppointmentInterrupt(Meetings meeting)
    {
        var mCrud = new MeetingsCrudFactory();
        var lstAllMeetings = mCrud.RetrieveByUserId(meeting.EmployeeId);

        return !lstAllMeetings.Any(m =>
            m.ProgrammedDate.Date == meeting.ProgrammedDate.Date &&
            (meeting.TimeOfEntry < m.TimeOfExit && meeting.TimeOfExit > m.TimeOfEntry)
        );
    }

    public bool NoPersonalTrainingInterrupt(Meetings meeting)
    {
        var ptCrud = new PersonalTrainingCrudFactory();
        var lstAllPT = ptCrud.RetrieveByEmployeeId(meeting.EmployeeId);

        return !lstAllPT.Any(pt =>
            pt.ProgrammedDate.Date == meeting.ProgrammedDate.Date &&
            (meeting.TimeOfEntry < pt.TimeOfExit && meeting.TimeOfExit > pt.TimeOfEntry)
        );
    }
    #endregion

    public List<Meetings> RetrieveAllWithName()
    {
        var mCrud = new MeetingsCrudFactory();
        return mCrud.RetrieveAllWithName();
    }

    public void CancelMeeting(Meetings meeting)
    {
        if (meeting.Id == 0)
        {
            throw new ArgumentException("Por favor elige una cita.");
        }

        var mCrud = new MeetingsCrudFactory();
        mCrud.CancelMeeting(meeting);
    }
}