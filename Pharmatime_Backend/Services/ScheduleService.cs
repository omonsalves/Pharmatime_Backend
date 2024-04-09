
using Pharmatime_Backend.Repositories;

public class ScheduleService
{

    public List<object> ScheduleData(ScheduleDto model)
    {
        if (ScheduleRepository.DataSchedule(model) != null)
        {
            var Schedule = ScheduleRepository.DataSchedule(model);
            return Schedule;
        }
        else
        {
           return null;
        }
    }

}

