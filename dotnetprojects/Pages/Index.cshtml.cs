using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

public class IndexModel(CameraService cameraService, CountDataService countDataService, ScheduleService scheduleService) : PageModel
{
    private readonly CameraService _cameraService = cameraService;
    private readonly CountDataService _countDataService = countDataService;
    private readonly ScheduleService _scheduleService = scheduleService;

    // Properties to hold data for the view
    public required List<Camera> Cameras { get; set; }

    public int TotalIn { get; set; }
    public int TotalOut { get; set; }

    public async Task OnGetAsync() {
        // Synchronous call for camera list (if you need to populate it for UI)
        Cameras = _cameraService.GetCameras();
    }

    public async Task<JsonResult> OnGetGetSchedulesAsync([FromQuery] int cameraId) {
        // Ideally, make your GetSchedules method async if DB supports it
        var schedules = _scheduleService.GetSchedules(cameraId);
        return new JsonResult(schedules);
    }

    public async Task<JsonResult> OnGetGetScheduleByIDAsync([FromQuery] int scheduleID) {
        // Ideally, make your GetSchedules method async if DB supports it
        var schedule = _scheduleService.GetScheduleByID(scheduleID);
        return new JsonResult(schedule);
    }

    public async Task<JsonResult> OnGetGetPeopleCountAsync([FromQuery] List<int> cameraIds,
        [FromQuery] long from,
        [FromQuery] long to) {

        (int totalIn, int totalOut) = await _countDataService.GetCountTotalsFilteredAsync(cameraIds, from, to);
        TotalIn = totalIn;
        TotalOut = totalOut;
        return new JsonResult(new {
            totalIn,
            totalOut
        });
    }
    public async Task<IActionResult> OnPostAddSchedue([FromBody] Schedule schedule)
    {
        try{
            await _scheduleService.AddScheduleAsync(schedule);
            // Example return: returning CameraID as "count"
            return new JsonResult(new { count = schedule.ScheduleName });
        }
        catch (Exception ex) {
            // Log the exception if needed
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

}
