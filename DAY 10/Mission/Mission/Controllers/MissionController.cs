using Microsoft.AspNetCore.Mvc;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;

namespace Mission.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionController(IMissionService missionService) : Controller
    {
        [HttpGet]
        [Route("MissionList")]
        public ResponseResult MissionList()
        {
            return new ResponseResult() { Data = missionService.GetMissionList(), Message = "", Result = ResponseStatus.Success };
        }

        [HttpPost]
        [Route("AddMission")]
        public ActionResult AddMission(AddMissionRequestModel model)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                var data = missionService.AddMission(model);
                result.Data = data;
                result.Message = "Success";
                result.Result = ResponseStatus.Success;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.Data = null;
                result.Message = ex.Message;
                result.Result = ResponseStatus.Error;
                return BadRequest(result);
            }
        }
    }
}
