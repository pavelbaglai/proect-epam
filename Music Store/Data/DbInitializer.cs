using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Music_Store.Models;
using System;
using System.IO;

namespace Music_Store.Data
{
    public static partial class DbInitializer
    {
        private const string BASE_PATH = "~/res/images/";
        private const string ALBUMS_PATH = BASE_PATH + "albums/";
        private const string ARTISTS_PATH = BASE_PATH + "artists/";
        private const string SONGS_PATH = BASE_PATH + "songs/";
        private const string USERS_PATH = BASE_PATH + "users/";

        private const string AUDIO_SONG_BASE_PATH = "wwwroot/res/songs/";

        public static void Initialize(ApplicationDbContext context, 
            IWebHostEnvironment environment, 
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var newImagePath = Path.Combine(environment.WebRootPath, "res", "newImages");

            // delete previous res
            if (Directory.Exists(newImagePath))
            {
                Directory.Delete(newImagePath, true);
            }

            // craete folders for new res
            var dir = Directory.CreateDirectory(newImagePath);
            dir.CreateSubdirectory("albums");
            dir.CreateSubdirectory("artists");
            dir.CreateSubdirectory("songs");
            dir.CreateSubdirectory("users");

            string employeeRole = "Employee";
            string customerRole = "Customer";

            string password = "Password1@";

            //Initialize Role data
            var r1 = roleManager.CreateAsync(new IdentityRole(employeeRole)).Result;
            var r2 = roleManager.CreateAsync(new IdentityRole(customerRole)).Result;
            //Initialize User1/Customer1 data
            var user1 = new User
            {
                UserName = "bill@gmail.com",
                Email = "bill@gmail.com",
                Firstname = "Bill",
                Lastname = "Tester",
                Nickname = "Bill"
            };
            var customer1 = new Customer
            {
                Gender = "Male",
                Street = "299 Doon Valley Dr",
                City = "Kitchener",
                Province = "ON",
                CountryCode = "CA",
                PostalCode = "N2G 4M4",
                DateOfBirth = new DateTime(2020, 2, 4)
            };
            user1.Customer = customer1;

            var result1 = userManager.CreateAsync(user1).Result;
            if (result1.Succeeded)
            {
                r2 = userManager.AddPasswordAsync(user1, password).Result;
                r2 = userManager.AddToRoleAsync(user1, customerRole).Result;
            }

            //Initialize User2/Customer2 data
            var user2 = new User
            {
                UserName = "ben@gmail.com",
                Email = "ben@gmail.com",
                Firstname = "Ben",
                Lastname = "Tester",
                Nickname = "Ben"
            };
            var customer2 = new Customer
            {
                Gender = "Male",
                Street = "299 Doon Valley Dr",
                City = "Kitchener",
                Province = "ON",
                CountryCode = "CA",
                PostalCode = "N2G 4M4",
                DateOfBirth = new DateTime(2020, 2, 4)
            };
            user2.Customer = customer2;

            var result2 = userManager.CreateAsync(user2).Result;
            if (result2.Succeeded)
            {
                r2 = userManager.AddPasswordAsync(user2, password).Result;
                r2 = userManager.AddToRoleAsync(user2, customerRole).Result;
            }

            var users = new User[]
            {
                new User
                {
                    UserName = "user1@google.com",
                    Email = "user1@google.com",
                    Firstname = "Liam",
                    Lastname = "Smith",
                    Nickname = "Roo",
                    ImagePath = USERS_PATH + "user1@google.com.jpg"
                },
                new User
                {
                    UserName = "user2@google.com",
                    Email = "user2@google.com",
                    Firstname = "Olivia",
                    Lastname = "Johnson",
                    Nickname = "Olivia",
                    ImagePath = USERS_PATH + "user2@google.com.jpg"
                },
                new User
                {
                    UserName = "user3@google.com",
                    Email = "user3@google.com",
                    Firstname = "Noah",
                    Lastname = "Williams",
                    Nickname = "Nono"
                },
                new User
                {
                    UserName = "user4@google.com",
                    Email = "user4@google.com",
                    Firstname = "Emma",
                    Lastname = "Brown",
                    Nickname = "Ben"
                },
                new User
                {
                    UserName = "user5@google.com",
                    Email = "user5@google.com",
                    Firstname = "Oliver",
                    Lastname = "Jones",
                    Nickname = "Olive"
                },
                new User
                {
                    UserName = "user6@google.com",
                    Email = "user6@google.com",
                    Firstname = "Ava",
                    Lastname = "Garcia",
                    Nickname = "Hppy_cl0ver"
                },
                new User
                {
                    UserName = "user7@google.com",
                    Email = "user7@google.com",
                    Firstname = "William",
                    Lastname = "Miller",
                    Nickname = "Will"
                },
                new User
                {
                    UserName = "user8@google.com",
                    Email = "user8@google.com",
                    Firstname = "Sophia",
                    Lastname = "Davis",
                    Nickname = "Kita"
                },
                new User
                {
                    UserName = "user9@google.com",
                    Email = "user9@google.com",
                    Firstname = "Elijah",
                    Lastname = "Rodriguez",
                    Nickname = "Olick"
                },
                new User
                {
                    UserName = "user10@google.com",
                    Email = "user10@google.com",
                    Firstname = "Isabella",
                    Lastname = "Martinez",
                    Nickname = "Onsa"
                },
            };
            var customers = new Customer[]
            {
                new Customer
                {
                    Gender = "Male",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(2020, 2, 4)
                },
                new Customer
                {
                    Gender = "Female",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(1998, 10, 2)
                },
                new Customer
                {
                    Gender = "Male",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(1983, 1, 4)
                },
                new Customer
                {
                    Gender = "Female",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(2000, 2, 1)
                },
                new Customer
                {
                    Gender = "Male",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(1976, 6, 18)
                },
                new Customer
                {
                    Gender = "Female",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(1999, 9, 29)
                },
                new Customer
                {
                    Gender = "Male",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(1989, 1, 4)
                },
                new Customer
                {
                    Gender = "Female",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(1981, 12, 31)
                },
                new Customer
                {
                    Gender = "Male",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(2002, 9, 22)
                },
                new Customer
                {
                    Gender = "Female",
                    Street = "299 Doon Valley Dr",
                    City = "Kitchener",
                    Province = "ON",
                    CountryCode = "CA",
                    PostalCode = "N2G 4M4",
                    DateOfBirth = new DateTime(1983, 10, 15)
                },

            };
            for (int i = 0; i < 10; i++)
            {
                users[i].Customer = customers[i];
                userManager.CreateAsync(users[i]).Wait();
                userManager.AddPasswordAsync(users[i], password).Wait();
                userManager.AddToRoleAsync(users[i], customerRole).Wait();
            }

            //Initialize User3/Employee1 data
            var user3 = new User
            {
                UserName = "mzhu9815@conestogac.on.ca",
                Email = "mzhu9815@conestogac.on.ca",
                Firstname = "Mingji",
                Lastname = "Zhu",
                Nickname = "Mingji"
            };
            var employee1 = new Employee {};
            user3.Employee = employee1;

            var result3 = userManager.CreateAsync(user3).Result;
            if (result3.Succeeded)
            {
                r1 = userManager.AddPasswordAsync(user3, password).Result;
                r1 = userManager.AddToRoleAsync(user3, employeeRole).Result;
            }

            //Initialize User4/Employee2 data
            var user4 = new User
            {
                UserName = "jayden@gmail.com",
                Email = "jayden@gmail.com",
                Firstname = "Jayden",
                Lastname = "Tester",
                Nickname = "Jayden"
            };
            var employee2 = new Employee { };
            user4.Employee = employee2;

            var result4 = userManager.CreateAsync(user4).Result;
            if (result4.Succeeded)
            {
                r1 = userManager.AddPasswordAsync(user4, password).Result;
                r1 = userManager.AddToRoleAsync(user4, employeeRole).Result;
            }

            //Initialize User5/Employee3 data
            var user5 = new User
            {
                UserName = "yshah@conestogac.on.ca",
                Email = "yshah@conestogac.on.ca",
                Firstname = "Yash",
                Lastname = "Shah",
                Nickname = "Yash"
            };
            var employee3 = new Employee { };
            user5.Employee = employee3;

            var result5 = userManager.CreateAsync(user5).Result;
            if (result5.Succeeded)
            {
                r1 = userManager.AddPasswordAsync(user5, password).Result;
                r1 = userManager.AddToRoleAsync(user5, employeeRole).Result;
            }

            //Initialize User5/Employee4 data
            var user6 = new User
            {
                UserName = "test@test.com",
                Email = "test@test.com",
                Firstname = "Test",
                Lastname = "Test",
                Nickname = "Tester"
            };
            var employee4 = new Employee { };
            user6.Employee = employee4;

            var result6 = userManager.CreateAsync(user6).Result;
            if (result6.Succeeded)
            {
                r1 = userManager.AddPasswordAsync(user6, password).Result;
                r1 = userManager.AddToRoleAsync(user6, employeeRole).Result;
            }

            // Initialize GenreType data
            var genres = new Genre[]
            {
                new Genre{Name="Hip Hop"},
                new Genre{Name="Trap"},
                new Genre{Name="Rap"},
                new Genre{Name="R&B"},
                new Genre{Name="Pop"},
                new Genre{Name="K-pop"},
                new Genre{Name="EDM"},
                new Genre{Name="Country"},
                new Genre{Name="Rock"},
                new Genre{Name="Blue-eyed Soul"},
                new Genre{Name="TropicalHouse"},
                new Genre{Name="Dance"}
            };
            context.AddRange(genres);
            context.SaveChanges();

            // Initialize Artist data
            var artists = new Artist[]
            {
                new Artist{Stagename="Cardi B", Fullname="Belcalis Marlenis Almánzar", DebutYear=2015, ImagePath=$"{ARTISTS_PATH}{EArtist.CardiB}.png" },
                new Artist{Stagename="Drake", Fullname="Aubrey Drake Graham", DebutYear=2006, ImagePath=$"{ARTISTS_PATH}{EArtist.Drake}.png"},
                new Artist{Stagename="The Weeknd", Fullname="Abel Makkonen Tesfaye", DebutYear=2010, ImagePath=$"{ARTISTS_PATH}{EArtist.TheWeeknd}.png"},
                new Artist{Stagename="BTS", Fullname="Bangtan Sonyeondan", DebutYear=2013, ImagePath=$"{ARTISTS_PATH}{EArtist.BTS}.png"},
                new Artist{Stagename="DaBaby", Fullname="Jonathan Lyndale Kirk", DebutYear=2015, ImagePath=$"{ARTISTS_PATH}{EArtist.DaBaby}.png"},
                new Artist{Stagename="Gabby Barrett", Fullname="Gabby Barrett", DebutYear=2020, ImagePath=$"{ARTISTS_PATH}{EArtist.GabbyBarrett}.png"},
                new Artist{Stagename="Harry Styles", Fullname="Harry Edward Styles", DebutYear=2010, ImagePath=$"{ARTISTS_PATH}{EArtist.HarryStyles}.png"},
                new Artist{Stagename="Lewis Capaldi", Fullname="Lewis Marc Capaldi", DebutYear=2019, ImagePath=$"{ARTISTS_PATH}{EArtist.LewisCapaldi}.png"},
                new Artist{Stagename="Fleetwood Mac", Fullname="Fleetwood Mac", DebutYear=1967, ImagePath=$"{ARTISTS_PATH}{EArtist.FleetwoodMac}.png"},
                new Artist{Stagename="Pop Smoke", Fullname="Bashar Barakah Jackson", DebutYear=2019, ImagePath=$"{ARTISTS_PATH}{EArtist.PopSmoke}.png"},
                new Artist{Stagename="Justin Bieber", Fullname="Justin Drew Bieber", DebutYear=2009, ImagePath=$"{ARTISTS_PATH}{EArtist.JustinBieber}.png"},
                new Artist{Stagename="Jason Aldean", Fullname="Jason Aldine Williams", DebutYear=2005, ImagePath=$"{ARTISTS_PATH}{EArtist.JasonAldean}.png"},
                new Artist{Stagename="Lee Brice", Fullname=" Kenneth Mobley Brice Jr", DebutYear=2007, ImagePath=$"{ARTISTS_PATH}{EArtist.LeeBrice}.png"},
                new Artist{Stagename="Post Malone", Fullname="Austin Richard Post", DebutYear=2015, ImagePath=$"{ARTISTS_PATH}{EArtist.PostMalone}.png"},
                new Artist{Stagename="Shawn Mendes", Fullname="Shawn Peter Raul Mendes", DebutYear=2014, ImagePath=$"{ARTISTS_PATH}{EArtist.ShawnMendes}.png"},
                new Artist{Stagename="Travis Scott", Fullname="Jacques Berman Webster II", DebutYear=2009, ImagePath=$"{ARTISTS_PATH}{EArtist.TravisScott}.png"},
                new Artist{Stagename="AJR", Fullname="Adam Metzger, Jack, and Ryan", DebutYear=2005, ImagePath=$"{ARTISTS_PATH}{EArtist.AJR}.png"},
                new Artist{Stagename="Dua Lipa", Fullname="Dua Lipa", DebutYear=2015, ImagePath=$"{ARTISTS_PATH}{EArtist.DuaLipa}.png"},
                new Artist{Stagename="Surf Mesa", Fullname="Powell Aguirre", DebutYear=2019, ImagePath=$"{ARTISTS_PATH}{EArtist.SurfMesa}.png"},
                new Artist{Stagename="SAINt JHN", Fullname="Carlos St. John Phillips", DebutYear=2016, ImagePath=$"{ARTISTS_PATH}{EArtist.SAINtJHN}.png"},
                new Artist{Stagename="Ava Max", Fullname="Amanda Ava Koci", DebutYear=2018, ImagePath=$"{ARTISTS_PATH}{EArtist.AvaMax}.png"},
            };
            context.AddRange(artists);
            context.SaveChanges();

            // Initialize Publisher data
            var publishers = new Publisher[]
            {
                new Publisher{Name="Atlantic Records", EstablishedYear=1947},
                new Publisher{Name="Republic Records", EstablishedYear=1995},
                new Publisher{Name="XO", EstablishedYear=2012},
                new Publisher{Name="Big Hit Entertainment", EstablishedYear=2005},
                new Publisher{Name="Columbia Records", EstablishedYear=1889},
                new Publisher{Name="Interscope Records", EstablishedYear=1990},
                new Publisher{Name="Warner Records", EstablishedYear=1958},
                new Publisher{Name="Internet Money Records", EstablishedYear=2016},
                new Publisher{Name="Vertigo Records", EstablishedYear=1969},
                new Publisher{Name="Def Jam Recordings", EstablishedYear=1984},
                new Publisher{Name="BBR Music Group", EstablishedYear=1999},
                new Publisher{Name="Curb Records", EstablishedYear=1964},
                new Publisher{Name="RCA Nashville", EstablishedYear=1957},
                new Publisher{Name="Island Records", EstablishedYear=1959},
                new Publisher{Name="Epic Records", EstablishedYear=1953},
                new Publisher{Name="AJR Productions", EstablishedYear=2005},
                new Publisher{Name="Astralwerks", EstablishedYear=1993},
                new Publisher{Name="Big Loud", EstablishedYear=2011},
                new Publisher{Name="Hitco Publishing Group", EstablishedYear=1996},
            };
            context.AddRange(publishers);
            context.SaveChanges();

            // Initialize Album data
            var albums = new Album[]
            {
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.XO].ID,
                    ArtistID=artists[(int)EArtist.TheWeeknd].ID,
                    Name="After Hours",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.AfterHours}.png",
                    PublishDate=new DateTime(2020, 3, 20),
                    Price=12.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.InterscopeRecords].ID,
                    ArtistID=artists[(int)EArtist.DaBaby].ID,
                    Name="Blame It on Baby",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.BlameItonBaby}.png",
                    PublishDate=new DateTime(2020, 4, 17),
                    Price=11.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.WarnerRecords].ID,
                    ArtistID=artists[(int)EArtist.GabbyBarrett].ID,
                    Name="Goldmine",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.Goldmine}.png",
                    PublishDate=new DateTime(2020, 6, 19),
                    Price=11.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.ColumbiaRecords].ID,
                    ArtistID=artists[(int)EArtist.HarryStyles].ID,
                    Name="Fine Line",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.FineLine}.png",
                    PublishDate=new DateTime(2019, 12, 13),
                    Price=10.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.VertigoRecords].ID,
                    ArtistID=artists[(int)EArtist.LewisCapaldi].ID,
                    Name="Divinely Uninspired to a Hellish Extent",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.DivinelyUninspiredToAHellishExtent}.png",
                    PublishDate=new DateTime(2019, 5, 17),
                    Price=10.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.WarnerRecords].ID,
                    ArtistID=artists[(int)EArtist.FleetwoodMac].ID,
                    Name="Rumours",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.Rumours}.png",
                    PublishDate=new DateTime(1977, 2, 4),
                    Price=9.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.RepublicRecords].ID,
                    ArtistID=artists[(int)EArtist.PopSmoke].ID,
                    Name="Shoot for the Stars, Aim for the Moon",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.ShootForTheStarsAimForTheMoon}.png",
                    PublishDate=new DateTime(2020, 7, 3),
                    Price=12.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.BBRMusicGroup].ID,
                    ArtistID=artists[(int)EArtist.JasonAldean].ID,
                    Name="9",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.Nine}.png",
                    PublishDate=new DateTime(2019, 11, 22),
                    Price=10.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.RepublicRecords].ID,
                    ArtistID=artists[(int)EArtist.PostMalone].ID,
                    Name="Hollywood's Bleeding",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.HollywoodsBleeding}.png",
                    PublishDate=new DateTime(2019, 9, 6),
                    Price=10.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.WarnerRecords].ID,
                    ArtistID=artists[(int)EArtist.DuaLipa].ID,
                    Name="Future Nostalgia",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.FutureNostalgia}.png",
                    PublishDate=new DateTime(2020, 3, 27),
                    Price=11.99f,
                },
                new Album
                {
                    PublisherID=publishers[(int)EPublisher.AtlanticRecords].ID,
                    ArtistID=artists[(int)EArtist.AvaMax].ID,
                    Name="Heaven & Hell",
                    ImagePath=$"{ALBUMS_PATH}{EAlbum.HeavenAndHell}.png",
                    PublishDate=new DateTime(2020, 9, 18),
                    Price=12.99f,
                },
            };
            context.AddRange(albums);
            context.SaveChanges();

            // Initialize Song data
            var songs = new Song[]
            {
                new Song
                {
                    ArtistID=artists[(int)EArtist.CardiB].ID,
                    PublisherID=publishers[(int)EPublisher.AtlanticRecords].ID,
                    Name="WAP",
                    ImagePath=$"{SONGS_PATH}{ESong.WAP}.png",
                    ReleaseDate=new DateTime(2020, 8, 7),
                    RuntimeInSec=187,
                    Price=4.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.WAP}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.Drake].ID,
                    PublisherID=publishers[(int)EPublisher.RepublicRecords].ID,
                    Name="Laugh Now Cry Later",
                    ImagePath=$"{SONGS_PATH}{ESong.LaughNowCryLater}.png",
                    ReleaseDate=new DateTime(2020, 8, 14),
                    RuntimeInSec=261,
                    Price=4.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.LaughNowCryLater}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.TheWeeknd].ID,
                    AlbumID=albums[(int)EAlbum.AfterHours].ID,
                    PublisherID=publishers[(int)EPublisher.XO].ID,
                    Name="Blinding Lights",
                    ImagePath=$"{SONGS_PATH}{ESong.BlindingLights}.png",
                    ReleaseDate=new DateTime(2019, 11, 29),
                    RuntimeInSec=202,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.BlindingLights}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.BTS].ID,
                    PublisherID=publishers[(int)EPublisher.BigHitEntertainment].ID,
                    Name="Dynamite",
                    ImagePath=$"{SONGS_PATH}{ESong.Dynamite}.png",
                    ReleaseDate=new DateTime(2020, 8, 21),
                    RuntimeInSec=199,
                    Price=4.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.Dynamite}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.DaBaby].ID,
                    AlbumID=albums[(int)EAlbum.BlameItonBaby].ID,
                    PublisherID=publishers[(int)EPublisher.InterscopeRecords].ID,
                    Name="Rockstar",
                    ImagePath=$"{SONGS_PATH}{ESong.Rockstar}.png",
                    ReleaseDate=new DateTime(2020, 4, 17),
                    RuntimeInSec=181,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.Rockstar}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.GabbyBarrett].ID,
                    AlbumID=albums[(int)EAlbum.Goldmine].ID,
                    PublisherID=publishers[(int)EPublisher.WarnerRecords].ID,
                    Name="I Hope",
                    ImagePath=$"{SONGS_PATH}{ESong.IHope}.png",
                    ReleaseDate=new DateTime(2019, 7, 29),
                    RuntimeInSec=209,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.IHope}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.HarryStyles].ID,
                    AlbumID=albums[(int)EAlbum.FineLine].ID,
                    PublisherID=publishers[(int)EPublisher.ColumbiaRecords].ID,
                    Name="Watermelon Sugar",
                    ImagePath=$"{SONGS_PATH}{ESong.WatermelonSugar}.png",
                    ReleaseDate=new DateTime(2020, 5, 15),
                    RuntimeInSec=173,
                    Price=4.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.WatermelonSugar}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.LewisCapaldi].ID,
                    AlbumID=albums[(int)EAlbum.DivinelyUninspiredToAHellishExtent].ID,
                    PublisherID=publishers[(int)EPublisher.VertigoRecords].ID,
                    Name="Before You Go",
                    ImagePath=$"{SONGS_PATH}{ESong.BeforeYouGo}.png",
                    ReleaseDate=new DateTime(2019, 11, 19),
                    RuntimeInSec=216,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.BeforeYouGo}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.FleetwoodMac].ID,
                    AlbumID=albums[(int)EAlbum.Rumours].ID,
                    PublisherID=publishers[(int)EPublisher.WarnerRecords].ID,
                    Name="Dreams",
                    ImagePath=$"{SONGS_PATH}{ESong.Dreams}.png",
                    ReleaseDate=new DateTime(1977, 3, 24),
                    RuntimeInSec=254,
                    Price=2.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.Dreams}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.JustinBieber].ID,
                    PublisherID=publishers[(int)EPublisher.DefJamRecordings].ID,
                    Name="Holy",
                    ImagePath=$"{SONGS_PATH}{ESong.Holy}.png",
                    ReleaseDate=new DateTime(2020, 9, 18),
                    RuntimeInSec=212,
                    Price=4.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.Holy}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.JasonAldean].ID,
                    AlbumID=albums[(int)EAlbum.Nine].ID,
                    PublisherID=publishers[(int)EPublisher.BBRMusicGroup].ID,
                    Name="Got What I Got",
                    ImagePath=$"{SONGS_PATH}{ESong.GotWhatIGot}.png",
                    ReleaseDate=new DateTime(2020, 4, 6),
                    RuntimeInSec=178,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.GotWhatIGot}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.LeeBrice].ID,
                    PublisherID=publishers[(int)EPublisher.CurbRecords].ID,
                    Name="One of Them Girls",
                    ImagePath=$"{SONGS_PATH}{ESong.OneOfThemGirls}.png",
                    ReleaseDate=new DateTime(2020, 4, 10),
                    RuntimeInSec=187,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.OneOfThemGirls}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.JasonAldean].ID,
                    PublisherID=publishers[(int)EPublisher.AtlanticRecords].ID,
                    Name="Whats Poppin",
                    ImagePath=$"{SONGS_PATH}{ESong.WhatsPoppin}.png",
                    ReleaseDate=new DateTime(2020, 1, 21),
                    RuntimeInSec=139,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.WhatsPoppin}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.PostMalone].ID,
                    AlbumID=albums[(int)EAlbum.HollywoodsBleeding].ID,
                    PublisherID=publishers[(int)EPublisher.RepublicRecords].ID,
                    Name="Circles",
                    ImagePath=$"{SONGS_PATH}{ESong.Circles}.png",
                    ReleaseDate=new DateTime(2019, 9, 3),
                    RuntimeInSec=214,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.Circles}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.ShawnMendes].ID,
                    PublisherID=publishers[(int)EPublisher.IslandRecords].ID,
                    Name="Wonder",
                    ImagePath=$"{SONGS_PATH}{ESong.Wonder}.png",
                    ReleaseDate=new DateTime(2020, 10, 2),
                    RuntimeInSec=173,
                    Price=4.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.Wonder}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.TravisScott].ID,
                    PublisherID=publishers[(int)EPublisher.EpicRecords].ID,
                    Name="Franchise",
                    ImagePath=$"{SONGS_PATH}{ESong.Franchise}.png",
                    ReleaseDate=new DateTime(2020, 9, 25),
                    RuntimeInSec=202,
                    Price=4.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.Franchise}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.HarryStyles].ID,
                    AlbumID=albums[(int)EAlbum.FineLine].ID,
                    PublisherID=publishers[(int)EPublisher.ColumbiaRecords].ID,
                    Name="Adore You",
                    ImagePath=$"{SONGS_PATH}{ESong.AdoreYou}.png",
                    ReleaseDate=new DateTime(2019, 12, 6),
                    RuntimeInSec=207,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.AdoreYou}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.AJR].ID,
                    PublisherID=publishers[(int)EPublisher.AJRProductions].ID,
                    Name="Bang!",
                    ImagePath=$"{SONGS_PATH}{ESong.Bang}.png",
                    ReleaseDate=new DateTime(2020, 2, 12),
                    RuntimeInSec=179,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.Bang}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.DuaLipa].ID,
                    AlbumID=albums[(int)EAlbum.FutureNostalgia].ID,
                    PublisherID=publishers[(int)EPublisher.WarnerRecords].ID,
                    Name="Break My Heart",
                    ImagePath=$"{SONGS_PATH}{ESong.BreakMyHeart}.png",
                    ReleaseDate=new DateTime(2020, 3, 25),
                    RuntimeInSec=221,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.BreakMyHeart}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.SurfMesa].ID,
                    PublisherID=publishers[(int)EPublisher.Astralwerks].ID,
                    Name="ILY (I Love You Baby)",
                    ImagePath=$"{SONGS_PATH}{ESong.ILY}.png",
                    ReleaseDate=new DateTime(2019, 11, 26),
                    RuntimeInSec=176,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.ILY}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.SAINtJHN].ID,
                    PublisherID=publishers[(int)EPublisher.HitcoPublishingGroup].ID,
                    Name="Roses",
                    ImagePath=$"{SONGS_PATH}{ESong.Roses}.png",
                    ReleaseDate=new DateTime(2019, 9, 18),
                    RuntimeInSec=176,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.Roses}.mp3",
                },
                new Song
                {
                    ArtistID=artists[(int)EArtist.AvaMax].ID,
                    AlbumID=albums[(int)EAlbum.HeavenAndHell].ID,
                    PublisherID=publishers[(int)EPublisher.AtlanticRecords].ID,
                    Name="Kings & Queens",
                    ImagePath=$"{SONGS_PATH}{ESong.KingsAndQueens}.png",
                    ReleaseDate=new DateTime(2020, 3, 12),
                    RuntimeInSec=162,
                    Price=3.99f,
                    DataUrl=$"{AUDIO_SONG_BASE_PATH}{ESong.KingsAndQueens}.mp3",
                },
            };
            context.AddRange(songs);
            context.SaveChanges();

            //Initialize SongGenre data
            var songGenres = new SongGenre[]
            {
                new SongGenre
                {
                    SongID=songs[(int)ESong.WAP].ID,
                    GenreID=genres[(int)EGenre.HipHop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.WAP].ID,
                    GenreID=genres[(int)EGenre.Rap].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.LaughNowCryLater].ID,
                    GenreID=genres[(int)EGenre.HipHop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.BlindingLights].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Dynamite].ID,
                    GenreID=genres[(int)EGenre.KPop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Rockstar].ID,
                    GenreID=genres[(int)EGenre.HipHop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.IHope].ID,
                    GenreID=genres[(int)EGenre.Country].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.IHope].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.WatermelonSugar].ID,
                    GenreID=genres[(int)EGenre.Rock].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.WatermelonSugar].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.BeforeYouGo].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Dreams].ID,
                    GenreID=genres[(int)EGenre.Rock].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Holy].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.GotWhatIGot].ID,
                    GenreID=genres[(int)EGenre.Country].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.OneOfThemGirls].ID,
                    GenreID=genres[(int)EGenre.Country].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.WhatsPoppin].ID,
                    GenreID=genres[(int)EGenre.HipHop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.WhatsPoppin].ID,
                    GenreID=genres[(int)EGenre.Trap].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Circles].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Circles].ID,
                    GenreID=genres[(int)EGenre.Rock].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Wonder].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Wonder].ID,
                    GenreID=genres[(int)EGenre.Rock].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Franchise].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Franchise].ID,
                    GenreID=genres[(int)EGenre.Rock].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.AdoreYou].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.AdoreYou].ID,
                    GenreID=genres[(int)EGenre.Rock].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Bang].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Bang].ID,
                    GenreID=genres[(int)EGenre.Rock].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.BreakMyHeart].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.ILY].ID,
                    GenreID=genres[(int)EGenre.TropicalHouse].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.Roses].ID,
                    GenreID=genres[(int)EGenre.Dance].ID
                },
                new SongGenre
                {
                    SongID=songs[(int)ESong.KingsAndQueens].ID,
                    GenreID=genres[(int)EGenre.Pop].ID
                },
            };
            context.AddRange(songGenres);
            context.SaveChanges();

            //Initialize Review data
            var reviews = new Review[]
            {
                new Review
                {
                    Rating = 1,
                    Content = "I'm not lying when I say that Ben Shapiro did this song better",
                    Song = songs[(int)ESong.WAP],
                    Customer = customers[0],
                    WrittenDate = new DateTime(2020, 11, 1, 10, 1, 1)
                },
                new Review
                {
                    Rating = 5,
                    Content = "Wap just made every song, a christan song.",
                    Song = songs[(int)ESong.WAP],
                    Customer = customers[1],
                    WrittenDate = new DateTime(2020, 11, 1, 10, 30, 5)
                },
                new Review
                {
                    Rating = 1,
                    Content = "This song just begins and goes absolutely nowhere.",
                    Song = songs[(int)ESong.WAP],
                    Customer = customers[2],
                    WrittenDate = new DateTime(2020, 11, 1, 11, 4, 9)
                },
                new Review
                {
                    Rating = 2,
                    Content = "People are praising this song as a sex-positive anthem or feminist anthem, but quite frankly, it isn't. This is just generic modern rap music with generic rap lyrics about sex.",
                    Song = songs[(int)ESong.WAP],
                    Customer = customers[3],
                    WrittenDate = new DateTime(2020, 11, 1, 11, 7, 10)
                },
                new Review
                {
                    Rating = 1,
                    Content = "Obviously, like everyone, I've never liked Cardi B, and this isn't an exception.",
                    Song = songs[(int)ESong.WAP],
                    Customer = customers[4],
                    WrittenDate = new DateTime(2020, 11, 1, 12, 10, 11)
                },
                new Review
                {
                    Rating = 5,
                    Content = "There has never been a song in 2020 that demands context more than WAP.",
                    Song = songs[(int)ESong.WAP],
                    Customer = customers[5],
                    WrittenDate = new DateTime(2020, 11, 1, 13, 30, 45)
                },
                new Review
                {
                    Rating = 3,
                    Content = "Reactions to music are endlessly fascinating to me. Metal songs about killing all humans are okay.",
                    Song = songs[(int)ESong.WAP],
                    Customer = customers[6],
                    WrittenDate = new DateTime(2020, 11, 1, 15, 59, 33)
                },
                // Song: Roses
                new Review
                {
                    Rating = 4,
                    Content = "Undeniable banger of a track",
                    Song = songs[(int)ESong.Roses],
                    Customer = customers[1],
                    WrittenDate = new DateTime(2020, 7, 2, 21, 59, 33)
                },
                new Review
                {
                    Rating = 5,
                    Content = "This song hits different in quarantine.",
                    Song = songs[(int)ESong.Roses],
                    Customer = customers[2],
                    WrittenDate = new DateTime(2020, 10, 28, 11, 59, 33)
                },
                new Review
                {
                    Rating = 4,
                    Content = "This hits different when driving to the store for toilet paper.",
                    Song = songs[(int)ESong.Roses],
                    Customer = customers[3],
                    WrittenDate = new DateTime(2020, 7, 2, 22, 12, 57)
                },
                new Review
                {
                    Rating = 2,
                    Content = "It's good, but if I never heard this song again, I wouldn't be disappointed at all.",
                    Song = songs[(int)ESong.LaughNowCryLater],
                    Customer = customers[4],
                    WrittenDate = new DateTime(2020, 5, 1, 9, 59, 3)
                },
                new Review
                {
                    Rating = 4,
                    Content = "if you don’t like this song you hate fun",
                    Song = songs[(int)ESong.LaughNowCryLater],
                    Customer = customers[5],
                    WrittenDate = new DateTime(2020, 6, 23, 10, 9, 53)
                },
                new Review
                {
                    Rating = 4,
                    Content = "This song would have been pretty meh but the Durk feature made this entire song",
                    Song = songs[(int)ESong.LaughNowCryLater],
                    Customer = customers[6],
                    WrittenDate = new DateTime(2020, 8, 11, 21, 59, 3)
                },
                new Review
                {
                    Rating = 3,
                    Content = "This is an alright song.",
                    Song = songs[(int)ESong.BlindingLights],
                    Customer = customers[3],
                    WrittenDate = new DateTime(2020, 2, 1, 19, 9, 43)
                },
                new Review
                {
                    Rating = 4,
                    Content = "The song doesn't quite know how to end properly, but don't let that put you off.",
                    Song = songs[(int)ESong.BlindingLights],
                    Customer = customers[1],
                    WrittenDate = new DateTime(2020, 4, 2, 9, 33, 8)
                },
                new Review
                {
                    Rating = 4,
                    Content = "I really like this track and I hope he stays on this track rather than go down the usual sound he does.",
                    Song = songs[(int)ESong.BlindingLights],
                    Customer = customers[6],
                    WrittenDate = new DateTime(2020, 8, 21, 22, 45, 4)
                },
                new Review
                {
                    Rating = 5,
                    Content = "this is my song of 2020.",
                    Song = songs[(int)ESong.BlindingLights],
                    Customer = customers[0],
                    WrittenDate = new DateTime(2020, 10, 12, 9, 8, 28)
                },
                new Review
                {
                    Rating = 4,
                    Content = "To me, this is just a nice funky song that likely deserves a 3-3.5, but I'm giving it a 4 because I really enjoy it more than I probably should",
                    Song = songs[(int)ESong.Dynamite],
                    Customer = customers[6],
                    WrittenDate = new DateTime(2020, 9, 28, 9, 28, 58)
                },
                new Review
                {
                    Rating = 3,
                    Content = "It’s also a disco-inspired dance-pop track so it’s very much on-trend for 2020",
                    Song = songs[(int)ESong.Dynamite],
                    Customer = customers[0],
                    WrittenDate = new DateTime(2020, 10, 2, 19, 46, 8)
                },
                new Review
                {
                    Rating = 4,
                    Content = "Amazing funky track from arguably the biggest band in the world right now.",
                    Song = songs[(int)ESong.Dynamite],
                    Customer = customers[1],
                    WrittenDate = new DateTime(2020, 10, 15, 1, 6, 2)
                },
                new Review
                {
                    Rating = 3,
                    Content = "This song feels like a team of songwriters wrote it in 2012.",
                    Song = songs[(int)ESong.KingsAndQueens],
                    Customer = customers[4],
                    WrittenDate = new DateTime(2020, 11, 15, 2, 6, 44)
                },
                new Review
                {
                    Rating = 4,
                    Content = "There is a part of me that wants to listen to this album on my own in the dark, at 3.30am after a big night on the town amongst a big crowd.",
                    Album = albums[(int)EAlbum.AfterHours],
                    Customer = customers[0],
                    WrittenDate = new DateTime(2020, 10, 15, 21, 26, 44)
                },
                new Review
                {
                    Rating = 4,
                    Content = "A great 80s synth-pop R&B album records come out this year and must to be awarded like grammy something else.",
                    Album = albums[(int)EAlbum.AfterHours],
                    Customer = customers[1],
                    WrittenDate = new DateTime(2020, 10, 17, 19, 36, 5)
                },
                new Review
                {
                    Rating = 5,
                    Content = "Album of The Year.",
                    Album = albums[(int)EAlbum.AfterHours],
                    Customer = customers[2],
                    WrittenDate = new DateTime(2020, 10, 22, 12, 26, 2)
                },
                new Review
                {
                    Rating = 3,
                    Content = "Sad boy lyrics+80's stylized instrumentals=massive success",
                    Album = albums[(int)EAlbum.AfterHours],
                    Customer = customers[3],
                    WrittenDate = new DateTime(2020, 10, 30, 23, 6, 44)
                },
                new Review
                {
                    Rating = 4,
                    Content = "Amazing pop record, probably the best of 2020 not including After Hours",
                    Album = albums[(int)EAlbum.FutureNostalgia],
                    Customer = customers[6],
                    WrittenDate = new DateTime(2020, 10, 2, 1, 9, 4)
                },
                new Review
                {
                    Rating = 4,
                    Content = "if you need a break from albums that challenge your line of thinking, this serves it well",
                    Album = albums[(int)EAlbum.FutureNostalgia],
                    Customer = customers[5],
                    WrittenDate = new DateTime(2020, 5, 20, 13, 6, 9)
                },
                new Review
                {
                    Rating = 3,
                    Content = "he lyrical content and vocals on this album were so one note that if it weren't for the production I could swear I was listening to one song on repeat.",
                    Album = albums[(int)EAlbum.FutureNostalgia],
                    Customer = customers[3],
                    WrittenDate = new DateTime(2020, 2, 1, 17, 2, 44)
                },
                new Review
                {
                    Rating = 4,
                    Content = "this was really enjoyable, looking forward to seeing what you bring next ",
                    Album = albums[(int)EAlbum.FutureNostalgia],
                    Customer = customers[1],
                    WrittenDate = new DateTime(2020, 10, 10, 15, 2, 33)
                },
                new Review
                {
                    Rating = 5,
                    Content = "BEAUTIFUL VOICE AND PERSON!",
                    Album = albums[(int)EAlbum.Goldmine],
                    Customer = customers[4],
                    WrittenDate = new DateTime(2020, 10, 2, 3, 2, 45)
                },
                new Review
                {
                    Rating = 4,
                    Content = "Holy fack",
                    Album = albums[(int)EAlbum.Goldmine],
                    Customer = customers[5],
                    WrittenDate = new DateTime(2020, 9, 14, 10, 12, 23)
                },
                new Review
                {
                    Rating = 4,
                    Content = "I love this song it was on my recommended for a few weeks so I tried dis one AND I LOVVVEE ITTTTT",
                    Album = albums[(int)EAlbum.Goldmine],
                    Customer = customers[1],
                    WrittenDate = new DateTime(2020, 11, 10, 12, 5, 28)
                },
                new Review
                {
                    Rating = 4,
                    Content = "Beautiful voice , sounds like Carrie Underwood has some competition .",
                    Album = albums[(int)EAlbum.Goldmine],
                    Customer = customers[0],
                    WrittenDate = new DateTime(2020, 11, 20, 15, 2, 33)
                },
                new Review
                {
                    Rating = 4,
                    Content = "He’s tryna tell us about the pedophiles that belong to Hollywood",
                    Album = albums[(int)EAlbum.HollywoodsBleeding],
                    Customer = customers[6],
                    WrittenDate = new DateTime(2020, 7, 20, 23, 2, 33)
                },
                new Review
                {
                    Rating = 5,
                    Content = "I can’t believe there was a time I didn’t know this song",
                    Album = albums[(int)EAlbum.Goldmine],
                    Customer = customers[2],
                    WrittenDate = new DateTime(2020, 11, 9, 21, 2, 5)
                },
                new Review
                {
                    Rating = 4,
                    Content = "A song you put on repeat and don't. ever get tired of hearing",
                    Album = albums[(int)EAlbum.Goldmine],
                    Customer = customers[3],
                    WrittenDate = new DateTime(2020, 11, 2, 14, 2, 8)
                },
            };
            context.AddRange(reviews);
            context.SaveChanges();
        }
    }
}
