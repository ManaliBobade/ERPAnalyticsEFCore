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

    public int TotalIn { get; set; }
    public int TotalOut { get; set; }

    public async Task OnGetAsync() {
        // Synchronous call for camera list (if you need to populate it for UI)
        Cameras = _cameraService.GetCameras();
    }

    public async Task<JsonResult> OnGetGetPeopleCountAsync([FromQuery] List<int> cameraIds,
        [FromQuery] long from,
        [FromQuery] long to) {

        Console.WriteLine($"Timestamps: {from} {to}");

        (int totalIn, int totalOut) = await _countDataService.GetCountTotalsFilteredAsync(cameraIds, from, to);
        TotalIn = totalIn;
        TotalOut = totalOut;
        return new JsonResult(new {
            totalIn,
            totalOut
        });
    }

}
