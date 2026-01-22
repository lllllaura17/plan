using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/activities")]
public class ActivitiesController : ControllerBase
{
    private readonly ActivityEvaluationService _service;

    public ActivitiesController(ActivityEvaluationService service)
    {
        _service = service;
    }

    // GET: api/activities
    [HttpGet]
    public IActionResult GetActivities()
    {
        return Ok(_service.GetActivities());
    }

    // POST: api/activities/evaluate
    [HttpPost("evaluate")]
    public IActionResult Evaluate([FromBody] WeatherData weather)
    {
        var result = _service.GetActivities()
            .Select(a => _service.Evaluate(a, weather));

        return Ok(result);
    }

    // POST: api/activities
    [HttpPost]
    public IActionResult AddActivity([FromBody] Activity activity)
    {
        var success = _service.AddActivity(activity);
        if (!success)
            return BadRequest(new { message = "Activity already exists" });

        return Ok(new { message = "Activity added successfully" });
    }

    // PUT: api/activities/{name}
    [HttpPut("{name}")]
    public IActionResult UpdateActivity(string name, [FromBody] Activity updatedActivity)
    {
        var success = _service.UpdateActivity(name, updatedActivity);
        if (!success)
            return NotFound(new { message = "Activity not found" });

        return Ok(new { message = "Activity updated successfully" });
    }

    // DELETE: api/activities/{name}
    [HttpDelete("{name}")]
    public IActionResult DeleteActivity(string name)
    {
        var success = _service.DeleteActivity(name);
        if (!success)
            return NotFound(new { message = "Activity not found" });

        return Ok(new { message = "Activity deleted successfully" });
    }

}
