
using Pharmatime_Backend.Repositories;

public class ScheduleService
{

    public List<object> ScheduleDataDoctor(ScheduleDto model)
    {
        if (ScheduleRepository.DataScheduleDoctor(model) != null)
        {
            var Schedule = ScheduleRepository.DataScheduleDoctor(model);
            return Schedule;
        }
        else
        {
           return null;
        }
    }
    
    public List<object> ScheduleDataPatient(ScheduleDto model)
    {
        if (ScheduleRepository.DataSchedulePatient(model) != null)
        {
            var Schedule = ScheduleRepository.DataSchedulePatient(model);
            return Schedule;
        }
        else
        {
           return null;
        }
    }


    
}

