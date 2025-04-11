using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class IndexModel(CameraService cameraService, CountDataService countDataService) : PageModel
{
    private readonly CameraService _cameraService = cameraService;
    private readonly CountDataService _countDataService = countDataService;

    // Properties to hold data for the view
    public required List<Camera> Cameras { get; set; }

    public List<string> CameraNames { get; set; } = new List<string>();
    public int TotalIn { get; set; }
    public int TotalOut { get; set; }

    public async Task OnGetAsync([FromQuery] List<string> cameraNames) {
        if (cameraNames != null && cameraNames.Any()) {
            CameraNames = cameraNames;
        } else {
            CameraNames = new List<string> {  }; // Default if no cameras are provided
        }

        // Synchronous call for camera list (if you need to populate it for UI)
        Cameras = _cameraService.GetCameras();

        // Async call for filtered count data
        var fromTime = new DateTime(2025, 4, 1, 0, 0, 0);
        var toTime = new DateTime(2025, 4, 10, 23, 59, 59);

        (int totalIn, int totalOut) = await _countDataService.GetCountTotalsFilteredAsync(cameraNames, fromTime, toTime);
        
        TotalIn = totalIn;
        TotalOut = totalOut;
    }

}
