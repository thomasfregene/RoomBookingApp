using RoomBookingApp.Core.Models;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor
    {
        public RoomBookingRequestProcessor()
        {
        }

        public RoomBookingResult BookRoom(RoomBookingRequest bookinRequest)
        {
            return new RoomBookingResult
            {
                FullName = bookinRequest.FullName,
                Date = bookinRequest.Date,
                Email = bookinRequest.Email
            };
        }
    }
}