public class ScheduleService
{
    private readonly ApplicationDBContext _dbContext;

    // Inject ApplicationDBContext via constructor
    public ScheduleService(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Method to get all cameras from the database
   public List<Schedule> GetSchedules(int cameraID)
    {
        return _dbContext.Schedules
            .Where(s => s.CameraID == cameraID)
            .ToList();
    }
}