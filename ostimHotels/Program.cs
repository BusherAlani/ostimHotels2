using System;
using System.Collections.Generic;
using System.Threading;

namespace OstimHotelReservation
{
    class Room
    {
        public int RoomNumber;
        public string Type;
        public double Price;
        public bool IsAvailable;

        public Room(int roomNumber, string type, double price)
        {
            RoomNumber = roomNumber;
            Type = type;
            Price = price;
            IsAvailable = true;
        }
    }

    class Customer
    {
        public string Name;
        public string Email;
    }

    class Reservation
    {
        public Customer Customer;
        public Room Room;
        public string CheckInDate;
        public string CheckOutDate;
    }

    class Program
    {
        static List<Room> rooms = new List<Room>();
        static List<Reservation> reservations = new List<Reservation>();

        static void Main(string[] args)
        {
            rooms.Add(new Room(1, "Single Room", 100));
            rooms.Add(new Room(2, "Double Room", 150));
            rooms.Add(new Room(3, "Suite", 250));

            Console.WriteLine(" Welcome to Ostim Hotels - Ankara!");
            Console.WriteLine("We’re happy to help you reserve your stay.\n");

            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Show available rooms");
                Console.WriteLine("2. Book a room");
                Console.WriteLine("3. Cancel a reservation");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    ShowRooms();
                }
                else if (choice == "2")
                {
                    BookRoom();
                }
                else if (choice == "3")
                {
                    CancelReservation();
                }
                else if (choice == "4")
                {
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
        }

        static void ShowRooms()
        {
            foreach (Room room in rooms)
            {
                if (room.IsAvailable)
                {
                    Console.WriteLine("Room " + room.RoomNumber + " - " + room.Type + " - $" + room.Price);
                }
            }
        }

        static void BookRoom()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter room number: ");
            int roomNum = int.Parse(Console.ReadLine());

            Room selectedRoom = rooms.Find(r => r.RoomNumber == roomNum && r.IsAvailable);

            if (selectedRoom != null)
            {
                Console.Write("Enter check-in month (1-12): ");
                int inMonth = int.Parse(Console.ReadLine());
                Console.Write("Enter check-in day (1-31): ");
                int inDay = int.Parse(Console.ReadLine());

                Console.Write("How many days will you stay? ");
                int stayDays = int.Parse(Console.ReadLine());

                string checkIn = $"{inMonth}/{inDay}";
                string checkOut = $"after {stayDays} day(s)";

                Console.WriteLine("Booking room...");
                Thread.Sleep(1000);

                Customer customer = new Customer() { Name = name, Email = email };
                Reservation res = new Reservation()
                {
                    Customer = customer,
                    Room = selectedRoom,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut
                };

                reservations.Add(res);
                selectedRoom.IsAvailable = false;

                Console.WriteLine("Thanks for booking with Ostim Hotels!");
                Console.WriteLine("Reservation for: " + name);
                Console.WriteLine("Room: " + selectedRoom.RoomNumber + " - " + selectedRoom.Type);
                Console.WriteLine("Check-in: " + checkIn);
                Console.WriteLine("Stay duration: " + stayDays + " day(s)");
            }
            else
            {
                Console.WriteLine("Room not available.");
            }
        }

        static void CancelReservation()
        {
            Console.Write("Enter room number to cancel: ");
            int roomNum = int.Parse(Console.ReadLine());

            Reservation res = reservations.Find(r => r.Room.RoomNumber == roomNum);
            if (res != null)
            {
                res.Room.IsAvailable = true;
                reservations.Remove(res);
                Console.WriteLine("Reservation cancelled.");
            }
            else
            {
                Console.WriteLine("Reservation not found.");
            }
        }
    }
}
