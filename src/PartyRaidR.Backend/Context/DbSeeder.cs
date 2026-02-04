using NetTopologySuite.Geometries;
using PartyRaidR.Backend.Models;
using PartyRaidR.Shared.Enums;

namespace PartyRaidR.Backend.Context
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            Guid admin = Guid.CreateVersion7(),
                 user1   = Guid.CreateVersion7(),
                 user2   = Guid.CreateVersion7(),
                 user3   = Guid.CreateVersion7(),
                 city1   = Guid.CreateVersion7(),
                 city2   = Guid.CreateVersion7(),
                 city3   = Guid.CreateVersion7(),
                 city4   = Guid.CreateVersion7(),
                 place1  = Guid.CreateVersion7(),
                 place2  = Guid.CreateVersion7(),
                 place3  = Guid.CreateVersion7(),
                 place4  = Guid.CreateVersion7();

            if (!context.Users.Any())
            {
                List<User> users = new()
                {
                    new(admin, "Admin1", "admin1@mail.org", BCrypt.Net.BCrypt.HashPassword("Password123"), "", new DateTime(2026, 1, 15), new DateOnly(2000, 2, 13), UserRole.Admin, "0000-0000"),
                    new(user1, "User1", "user1@mail.org", BCrypt.Net.BCrypt.HashPassword("Password123"), "", new DateTime(2026, 2, 1), new DateOnly(2002, 8, 30), UserRole.User, "0000-0001"),
                    new(user2, "User2", "user2@mail.org", BCrypt.Net.BCrypt.HashPassword("Password123"), "", new DateTime(2025, 12, 27), new DateOnly(2007, 12, 15), UserRole.User, "0000-0002"),
                    new(user3, "User3", "user3@mail.org", BCrypt.Net.BCrypt.HashPassword("Password123"), "", new DateTime(2026, 1, 23), new DateOnly(2002, 8, 30), UserRole.User, "0000-0003")
                };
                await context.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }

            if (!context.Cities.Any())
            {
                List<City> cities = new()
                {
                    new(city1, "Szeged", "6700", "Csongrád-Csanád", "Hungary"),
                    new(city2, "Budapest", "1007", "Pest", "Hungary"),
                    new(city3, "Rábakecöl", "9344", "Győr-Moson-Sopron", "Hungary"),
                    new(city4, "Debrecen", "4000", "Hajdú-Bihar", "Hungary")
                };
                await context.AddRangeAsync(cities);
                await context.SaveChangesAsync();
            }

            if (!context.Places.Any())
            {
                List<Place> places = new()
                {
                    new(place1, "Hősök tere", "Hősok tere 1.", city2.ToString(), PlaceCategory.PublicSpace, new Point(0f, 0f), "Híres emlékmű Budapesten.", admin.ToString()),
                    new(place2, "Laci Kocsmája", "Arany János utca 12.", city3.ToString(), PlaceCategory.Club, new Point(0f, 0f), "A környék legjobb kocsmája.", user1.ToString()),
                    new(place3, "Olasz Kávézó", "Masa út 56.", city4.ToString(), PlaceCategory.Club, new Point(0f, 0f), "Nagyon finom kávé.", user2.ToString()),
                    new(place4, "Pick Aréna", "Felső Tisza-Part 35.", city1.ToString(), PlaceCategory.Club, new Point(0f, 0f), "A környék legjobb kocsmája.", user3.ToString())
                };
                await context.AddRangeAsync(places);
                await context.SaveChangesAsync();
            }

            if (!context.Events.Any())
            {
                List<Event> events = new()
                {
                    new Event(Guid.CreateVersion7(), "Ének Jézussal", "Keresztény összejövetel - Felekezetfüggetlen zenés est.", new DateTime(new DateOnly(2026, 4, 12), new TimeOnly(16, 0, 0)), new DateTime(new DateOnly(2026, 4, 12), new TimeOnly(18, 30, 0)), place1.ToString(), EventCategory.Concert, admin.ToString(), 0, 0, DateTime.Now),
                    new Event(Guid.CreateVersion7(), "Humor est", "Békési János önálló estje.", new DateTime(new DateOnly(2026, 2, 28), new TimeOnly(18, 45, 0)), new DateTime(new DateOnly(2026, 2, 28), new TimeOnly(20, 45, 0)), place4.ToString(), EventCategory.IndoorsActivity, user1.ToString(), 0, 0, DateTime.Now),
                    new Event(Guid.CreateVersion7(), "21. századi költészet - Gyűlés", "Beszélgessünk a 21. század költészetének nehézségeiről egy kávé mellett!", new DateTime(new DateOnly(2026, 2, 22), new TimeOnly(10, 30, 0)), new DateTime(new DateOnly(2026, 2, 22), new TimeOnly(12, 0, 0)), place3.ToString(), EventCategory.IndoorsActivity, user2.ToString(), 0, 0, DateTime.Now),
                    new Event(Guid.CreateVersion7(), "Karaoke est", "Mutasd meg énektudásod! Minden résztvevőt meghívunk egy italra.", new DateTime(new DateOnly(2026, 3, 8), new TimeOnly(18, 40, 0)), new DateTime(new DateOnly(2026, 3, 8), new TimeOnly(20, 45, 0)), place2.ToString(), EventCategory.IndoorsActivity, user2.ToString(), 0, 0, DateTime.Now),
                    new Event(Guid.CreateVersion7(), "Vakrandi est", "Szeretettel várjuk az ismerkedni kívánó szingliket!", new DateTime(new DateOnly(2026, 6, 29), new TimeOnly(17, 0, 0)), new DateTime(new DateOnly(2026, 6, 29), new TimeOnly(18, 30, 0)), place3.ToString(), EventCategory.IndoorsActivity, user1.ToString(), 0, 0, DateTime.Now),
                    new Event(Guid.CreateVersion7(), "Párok éjszakája", "Felejthetetlen est új feltörekvő DJ-kkel!", new DateTime(new DateOnly(2026, 8, 10), new TimeOnly(23, 0, 0)), new DateTime(new DateOnly(2026, 8, 10), new TimeOnly(5, 0, 0)), place4.ToString(), EventCategory.Party, user2.ToString(), 0, 0, DateTime.Now),
                    new Event(Guid.CreateVersion7(), "Megemlékezés a szabadságról", "Szabadtéri műsor az 56-os forradalom eseményeiről.", new DateTime(new DateOnly(2026, 10, 23), new TimeOnly(16, 0, 0)), new DateTime(new DateOnly(2026, 10, 23), new TimeOnly(17, 30, 0)), place1.ToString(), EventCategory.OutdoorsActivity, user3.ToString(), 0, 0, DateTime.Now)
                };
                await context.AddRangeAsync(events);
                await context.SaveChangesAsync();
            }
        }
    }
}
