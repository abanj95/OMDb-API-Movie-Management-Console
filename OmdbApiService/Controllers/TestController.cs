//using Microsoft.AspNetCore.Mvc;
//using OmdbApiService.Services;

//[Route("api/[controller]")]
//[ApiController]
//public class TestController : ControllerBase
//{
//    private readonly OmdbService _omdbService;

//    public TestController(OmdbService omdbService)
//    {
//        _omdbService = omdbService;
//    }

//    [HttpGet]
//    public IActionResult Get()
//    {
//        // Test returning data from the OmdbService
//        var data = _omdbService.GetData();
//        return Ok(new { Message = "API is working!", Data = data });
//    }
//}
