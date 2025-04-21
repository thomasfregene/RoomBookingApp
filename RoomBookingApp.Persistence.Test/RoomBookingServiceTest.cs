using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Domain;
using RoomBookingApp.Persistence.Repositories;

namespace RoomBookingApp.Persistence.Tests
{
    public class RoomBookingServiceTest
    {
        [Fact]
        public void Should_Return_Available_Rooms()
        {
            //Arrange
            var date = new DateTime(2025, 04, 25);

            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>().UseInMemoryDatabase("AvailableRoomTest")
                .Options;

            using var context = new RoomBookingAppDbContext(dbOptions);
            context.Add(new Room() { Id = 1, Name = "Room 1" });
            context.Add(new Room() { Id = 2, Name = "Room 2" });
            context.Add(new Room() { Id = 3, Name = "Room 3" });

            context.Add(new RoomBooking { RoomId = 1, Date = date });
            context.Add(new RoomBooking { RoomId = 2, Date = date.AddDays(-1) });

            context.SaveChanges();

            var roomBookingService = new RoomBookingService(context);

            //Act
            var availableRooms = roomBookingService.GetAvailableRooms(date);

            //Assert
            Assert.Equal(2, availableRooms.Count());

            Assert.Contains(availableRooms, q=>q.Id == 2);
            Assert.Contains(availableRooms, q=>q.Id == 3);
            Assert.DoesNotContain(availableRooms, q=>q.Id == 1);
        }

        [Fact]
        public void Should_Save_Room_Booking()
        {
            var dbOptions = new DbContextOptionsBuilder<RoomBookingAppDbContext>().UseInMemoryDatabase("ShouldSaveTest")
                .Options;

            var roomBookig = new RoomBooking { RoomId = 1, Date = new DateTime(2025, 04, 21) };
            using var context = new RoomBookingAppDbContext(dbOptions);
            var roomBookingService = new RoomBookingService(context);

            roomBookingService.Save(roomBookig);

            var bookings = context.RoomBookings.ToList();
            var booking = Assert.Single(bookings);

            Assert.Equal(roomBookig.Date, booking.Date);
            Assert.Equal(roomBookig.RoomId, booking.RoomId);
        }
    }
}
