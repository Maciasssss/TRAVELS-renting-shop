using System;
using System.Diagnostics;
using System.Linq;
using TRAVELS.Models;
namespace TRAVELS.Data
{
    public static class DbInitializer
    {
        public static void Initialize(TravelDBcontext context)
        {
            context.Database.EnsureCreated();

            if (context.Travels.Any())
            {
                return;
            }

            var guides = new Guide[]
            {
                new Guide { Name = "John Doe" },
                new Guide { Name = "Jane Smith" },
                new Guide { Name = "Michael Johnson" },
                new Guide { Name = "Emily Brown" },
                new Guide { Name = "David Wilson" }
            };
            foreach (var guide in guides)
            {
                context.Guides.Add(guide);
            }
            context.SaveChanges();

            var travels = new Travel[]
            {
                new Travel { Destination = "Paris", DepartureDate = DateTime.Parse("2024-07-15"), ReturnDate = DateTime.Parse("2024-07-20"), Price = 500, Description = "Explore the beautiful city of Paris with our expert guide" },
                new Travel { Destination = "Rome", DepartureDate = DateTime.Parse("2024-08-10"), ReturnDate = DateTime.Parse("2024-08-17"), Price = 600, Description = "Discover the ancient ruins and art treasures of Rome" },
                new Travel { Destination = "Tokyo", DepartureDate = DateTime.Parse("2024-09-05"), ReturnDate = DateTime.Parse("2024-09-12"), Price = 700, Description = "Experience the vibrant culture and technology of Tokyo" },
                new Travel { Destination = "New York", DepartureDate = DateTime.Parse("2024-10-20"), ReturnDate = DateTime.Parse("2024-10-25"), Price = 550, Description = "Explore the bustling streets and iconic landmarks of New York City" },
                new Travel { Destination = "Sydney", DepartureDate = DateTime.Parse("2024-11-15"), ReturnDate = DateTime.Parse("2024-11-22"), Price = 800, Description = "Enjoy the stunning beaches and unique wildlife of Sydney" },
                new Travel { Destination = "Hungary", DepartureDate = DateTime.Parse("2025-11-15"), ReturnDate = DateTime.Parse("2026-11-22"), Price = 800, Description = "Enjoy the stunning beaches and unique wildlife of Sydney" }
            };
            foreach (var travel in travels)
            {
                var random = new Random();
                var guide = guides[random.Next(guides.Length)];
                travel.GuideId = guide.GuideId;

                context.Travels.Add(travel);
            }
            var Users = new User[]
            {
                new User{UserName = "Martin",  UserPhone=555666777},
                new User{UserName = "John", UserPhone =123123132},
                new User{UserName = "Bond",  UserPhone =321312321},
                new User{UserName = "SpiderMan",  UserPhone =888123132}

            };

            foreach (var travel in travels)
            {
                var random = new Random();
                var ruser = Users[random.Next(Users.Length)];
                travel.User.Add(ruser);
                context.Travels.Add(travel);
            }
            context.SaveChanges();
            Console.WriteLine("123");
        }
    }
}

