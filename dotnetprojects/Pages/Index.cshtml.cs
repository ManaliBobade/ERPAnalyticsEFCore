using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class IndexModel : PageModel
{
    private readonly CameraService _cameraService;

    // Properties to hold data for the view
    public List<Camera> Cameras { get; set; }


    public IndexModel(CameraService cameraService) {
        _cameraService = cameraService;
    }

    public void OnGet() {
        // Fetch the camera data (from a service or DB)
        Cameras = _cameraService.GetCameras();
    }

}
