using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;

namespace RoomBookingApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomBookingController : ControllerBase
    {
        private readonly IRoomBookingRequestProcessor _roomBookingRequest;

        public RoomBookingController(IRoomBookingRequestProcessor roomBookingRequest)
        {
            _roomBookingRequest = roomBookingRequest;
        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(RoomBookingRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = _roomBookingRequest.BookRoom(request);
                if (result.Flag == BookingResultFlag.Success)
                {
                    return Ok(result);
                }

                ModelState.AddModelError(nameof(RoomBookingRequest.Date), "No Rooms Available For Given Date");
            }

            return BadRequest(ModelState);
        }
    }
}
