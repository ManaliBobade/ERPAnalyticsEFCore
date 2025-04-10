public class CameraService
{
    private readonly ApplicationDBContext _dbContext;

    // Inject ApplicationDBContext via constructor
    public CameraService(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Method to get all cameras from the database
    public List<Camera> GetCameras()
    {
        return _dbContext.Cameras.ToList();
    }
}