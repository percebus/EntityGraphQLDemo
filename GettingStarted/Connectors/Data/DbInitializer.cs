namespace JCystems.GettingStarted.Connectors.Data
{
    using System.Linq;
    using JCystems.GettingStarted.Connectors.Contexts;
    using JCystems.GettingStarted.Connectors.Entities;

    // SRC: https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-9.0
    public static class DbInitializer
    {
        public static void Initialize(DemoContext context)
        {
            context.Database.EnsureCreated();

            if (context.People.Any())
            {
                return; // DB has been seeded
            }

            // Directors
            var georgeLucas = new Person { Id = 1, FirstName = "George", LastName = "Lucas", Dob = new DateTime(1944, 5, 14) };
            var irvinKershner = new Person { Id = 2, FirstName = "Irvin", LastName = "Kershner", Dob = new DateTime(1923, 4, 29), Died = new DateTime(2010, 11, 27) };
            var richardMarquand = new Person { Id = 3, FirstName = "Richard", LastName = "Marquand", Dob = new DateTime(1937, 9, 22), Died = new DateTime(1987, 9, 4) };
            var jjAbrams = new Person { Id = 4, FirstName = "J.J.", LastName = "Abrams", Dob = new DateTime(1966, 6, 27) };
            var rianJohnson = new Person { Id = 5, FirstName = "Rian", LastName = "Johnson", Dob = new DateTime(1973, 12, 17) };

            // Writers
            var lawrenceKasdan = new Person { Id = 6, FirstName = "Lawrence", LastName = "Kasdan", Dob = new DateTime(1949, 1, 14) };
            var leighBrackett = new Person { Id = 7, FirstName = "Leigh", LastName = "Brackett", Dob = new DateTime(1915, 12, 7), Died = new DateTime(1978, 3, 18) };
            var chrisWeitz = new Person { Id = 8, FirstName = "Chris", LastName = "Weitz", Dob = new DateTime(1969, 11, 30) };
            var tonyGilroy = new Person { Id = 9, FirstName = "Tony", LastName = "Gilroy", Dob = new DateTime(1956, 9, 11) };
            var jonathanKasdan = new Person { Id = 10, FirstName = "Jonathan", LastName = "Kasdan", Dob = new DateTime(1979, 9, 30) };
            var derekConnolly = new Person { Id = 11, FirstName = "Derek", LastName = "Connolly", Dob = new DateTime(1976, 1, 1) };
            var colinTrevorrow = new Person { Id = 12, FirstName = "Colin", LastName = "Trevorrow", Dob = new DateTime(1976, 9, 13) };

            // Actors
            var markHamill = new Person { Id = 13, FirstName = "Mark", LastName = "Hamill", Dob = new DateTime(1951, 9, 25) };
            var harrisonFord = new Person { Id = 14, FirstName = "Harrison", LastName = "Ford", Dob = new DateTime(1942, 7, 13) };
            var carrieFisher = new Person { Id = 15, FirstName = "Carrie", LastName = "Fisher", Dob = new DateTime(1956, 10, 21), Died = new DateTime(2016, 12, 27) };
            var peterCushing = new Person { Id = 16, FirstName = "Peter", LastName = "Cushing", Dob = new DateTime(1913, 5, 26), Died = new DateTime(1994, 8, 11) };
            var alecGuinness = new Person { Id = 17, FirstName = "Alec", LastName = "Guinness", Dob = new DateTime(1914, 4, 2), Died = new DateTime(2000, 8, 5) };
            var frankOz = new Person { Id = 18, FirstName = "Frank", LastName = "Oz", Dob = new DateTime(1944, 5, 25) };
            var billyDeeWilliams = new Person { Id = 19, FirstName = "Billy Dee", LastName = "Williams", Dob = new DateTime(1937, 4, 6) };
            var ianMcDiarmid = new Person { Id = 20, FirstName = "Ian", LastName = "McDiarmid", Dob = new DateTime(1944, 8, 11) };
            var liamNeeson = new Person { Id = 21, FirstName = "Liam", LastName = "Neeson", Dob = new DateTime(1952, 6, 7) };
            var ewanMcGregor = new Person { Id = 22, FirstName = "Ewan", LastName = "McGregor", Dob = new DateTime(1971, 3, 31) };
            var nataliePortman = new Person { Id = 23, FirstName = "Natalie", LastName = "Portman", Dob = new DateTime(1981, 6, 9) };
            var haydenChristensen = new Person { Id = 24, FirstName = "Hayden", LastName = "Christensen", Dob = new DateTime(1981, 4, 19) };
            var samuelLJackson = new Person { Id = 25, FirstName = "Samuel L.", LastName = "Jackson", Dob = new DateTime(1948, 12, 21) };
            var daisyRidley = new Person { Id = 26, FirstName = "Daisy", LastName = "Ridley", Dob = new DateTime(1992, 4, 10) };
            var johnBoyega = new Person { Id = 27, FirstName = "John", LastName = "Boyega", Dob = new DateTime(1992, 3, 17) };
            var adamDriver = new Person { Id = 28, FirstName = "Adam", LastName = "Driver", Dob = new DateTime(1983, 11, 19) };
            var oscarIsaac = new Person { Id = 29, FirstName = "Oscar", LastName = "Isaac", Dob = new DateTime(1979, 3, 9) };
            var felicityJones = new Person { Id = 30, FirstName = "Felicity", LastName = "Jones", Dob = new DateTime(1983, 10, 17) };
            var aldenEhrenreich = new Person { Id = 31, FirstName = "Alden", LastName = "Ehrenreich", Dob = new DateTime(1989, 11, 22) };
            var donaldGlover = new Person { Id = 32, FirstName = "Donald", LastName = "Glover", Dob = new DateTime(1983, 9, 25) };

            var people = new Person[]
            {
                georgeLucas, irvinKershner, richardMarquand, jjAbrams, rianJohnson,
                lawrenceKasdan, leighBrackett, chrisWeitz, tonyGilroy, jonathanKasdan, derekConnolly, colinTrevorrow,
                markHamill, harrisonFord, carrieFisher, peterCushing, alecGuinness, frankOz, billyDeeWilliams,
                ianMcDiarmid, liamNeeson, ewanMcGregor, nataliePortman, haydenChristensen, samuelLJackson,
                daisyRidley, johnBoyega, adamDriver, oscarIsaac, felicityJones, aldenEhrenreich, donaldGlover
            };

            foreach (Person person in people)
            {
                context.People.Add(person);
            }

            context.SaveChanges();

            // Movies
            var movies = new Movie[]
            {
                // Original Trilogy
                new Movie
                {
                    Id = 1,
                    Name = "Star Wars: Episode IV - A New Hope",
                    Genre = Genre.Scifi,
                    Released = new DateTime(1977, 5, 25),
                    Director = georgeLucas,
                    Rating = 8.6
                },
                new Movie
                {
                    Id = 2,
                    Name = "Star Wars: Episode V - The Empire Strikes Back",
                    Genre = Genre.Scifi,
                    Released = new DateTime(1980, 5, 21),
                    Director = irvinKershner,
                    Rating = 8.7
                },
                new Movie
                {
                    Id = 3,
                    Name = "Star Wars: Episode VI - Return of the Jedi",
                    Genre = Genre.Scifi,
                    Released = new DateTime(1983, 5, 25),
                    Director = richardMarquand,
                    Rating = 8.3
                },
                // Prequel Trilogy
                new Movie
                {
                    Id = 4,
                    Name = "Star Wars: Episode I - The Phantom Menace",
                    Genre = Genre.Scifi,
                    Released = new DateTime(1999, 5, 19),
                    Director = georgeLucas,
                    Rating = 6.5
                },
                new Movie
                {
                    Id = 5,
                    Name = "Star Wars: Episode II - Attack of the Clones",
                    Genre = Genre.Scifi,
                    Released = new DateTime(2002, 5, 16),
                    Director = georgeLucas,
                    Rating = 6.6
                },
                new Movie
                {
                    Id = 6,
                    Name = "Star Wars: Episode III - Revenge of the Sith",
                    Genre = Genre.Scifi,
                    Released = new DateTime(2005, 5, 19),
                    Director = georgeLucas,
                    Rating = 7.6
                },
                // Sequel Trilogy
                new Movie
                {
                    Id = 7,
                    Name = "Star Wars: Episode VII - The Force Awakens",
                    Genre = Genre.Scifi,
                    Released = new DateTime(2015, 12, 18),
                    Director = jjAbrams,
                    Rating = 7.8
                },
                new Movie
                {
                    Id = 8,
                    Name = "Star Wars: Episode VIII - The Last Jedi",
                    Genre = Genre.Scifi,
                    Released = new DateTime(2017, 12, 15),
                    Director = rianJohnson,
                    Rating = 6.9
                },
                new Movie
                {
                    Id = 9,
                    Name = "Star Wars: Episode IX - The Rise of Skywalker",
                    Genre = Genre.Scifi,
                    Released = new DateTime(2019, 12, 20),
                    Director = jjAbrams,
                    Rating = 6.5
                },
            };

            foreach (Movie movie in movies)
            {
                context.Movies.Add(movie);
            }

            context.SaveChanges();

            // Writers
            var writers = new Writer[]
            {
                // Episode IV - A New Hope
                new Writer { PersonId = 1, MovieId = 1 }, // George Lucas
                // Episode V - The Empire Strikes Back
                new Writer { PersonId = 7, MovieId = 2 }, // Leigh Brackett
                new Writer { PersonId = 6, MovieId = 2 }, // Lawrence Kasdan
                new Writer { PersonId = 1, MovieId = 2 }, // George Lucas (story)
                // Episode VI - Return of the Jedi
                new Writer { PersonId = 6, MovieId = 3 }, // Lawrence Kasdan
                new Writer { PersonId = 1, MovieId = 3 }, // George Lucas
                // Episode I - The Phantom Menace
                new Writer { PersonId = 1, MovieId = 4 }, // George Lucas
                // Episode II - Attack of the Clones
                new Writer { PersonId = 1, MovieId = 5 }, // George Lucas
                new Writer { PersonId = 6, MovieId = 5 }, // Lawrence Kasdan (uncredited)
                // Episode III - Revenge of the Sith
                new Writer { PersonId = 1, MovieId = 6 }, // George Lucas
                // Episode VII - The Force Awakens
                new Writer { PersonId = 4, MovieId = 7 }, // J.J. Abrams
                new Writer { PersonId = 6, MovieId = 7 }, // Lawrence Kasdan
                // Episode VIII - The Last Jedi
                new Writer { PersonId = 5, MovieId = 8 }, // Rian Johnson
                // Episode IX - The Rise of Skywalker
                new Writer { PersonId = 4, MovieId = 9 }, // J.J. Abrams
                new Writer { PersonId = 8, MovieId = 9 }, // Chris Terrio
            };

            foreach (Writer writer in writers)
            {
                context.Writers.Add(writer);
            }

            context.SaveChanges();

            // Actors
            var actors = new Actor[]
            {
                // Episode IV - A New Hope
                new Actor { PersonId = 13, MovieId = 1 }, // Mark Hamill - Luke
                new Actor { PersonId = 14, MovieId = 1 }, // Harrison Ford - Han
                new Actor { PersonId = 15, MovieId = 1 }, // Carrie Fisher - Leia
                new Actor { PersonId = 16, MovieId = 1 }, // Peter Cushing - Tarkin
                new Actor { PersonId = 17, MovieId = 1 }, // Alec Guinness - Obi-Wan
                // Episode V - The Empire Strikes Back
                new Actor { PersonId = 13, MovieId = 2 }, // Mark Hamill
                new Actor { PersonId = 14, MovieId = 2 }, // Harrison Ford
                new Actor { PersonId = 15, MovieId = 2 }, // Carrie Fisher
                new Actor { PersonId = 18, MovieId = 2 }, // Frank Oz - Yoda
                new Actor { PersonId = 19, MovieId = 2 }, // Billy Dee Williams - Lando
                // Episode VI - Return of the Jedi
                new Actor { PersonId = 13, MovieId = 3 }, // Mark Hamill
                new Actor { PersonId = 14, MovieId = 3 }, // Harrison Ford
                new Actor { PersonId = 15, MovieId = 3 }, // Carrie Fisher
                new Actor { PersonId = 19, MovieId = 3 }, // Billy Dee Williams
                new Actor { PersonId = 20, MovieId = 3 }, // Ian McDiarmid - Emperor
                // Episode I - The Phantom Menace
                new Actor { PersonId = 21, MovieId = 4 }, // Liam Neeson - Qui-Gon
                new Actor { PersonId = 22, MovieId = 4 }, // Ewan McGregor - Obi-Wan
                new Actor { PersonId = 23, MovieId = 4 }, // Natalie Portman - Padmé
                new Actor { PersonId = 20, MovieId = 4 }, // Ian McDiarmid - Palpatine
                new Actor { PersonId = 18, MovieId = 4 }, // Frank Oz - Yoda
                // Episode II - Attack of the Clones
                new Actor { PersonId = 22, MovieId = 5 }, // Ewan McGregor
                new Actor { PersonId = 23, MovieId = 5 }, // Natalie Portman
                new Actor { PersonId = 24, MovieId = 5 }, // Hayden Christensen - Anakin
                new Actor { PersonId = 18, MovieId = 5 }, // Frank Oz - Yoda
                new Actor { PersonId = 25, MovieId = 5 }, // Samuel L. Jackson - Mace Windu
                // Episode III - Revenge of the Sith
                new Actor { PersonId = 22, MovieId = 6 }, // Ewan McGregor
                new Actor { PersonId = 23, MovieId = 6 }, // Natalie Portman
                new Actor { PersonId = 24, MovieId = 6 }, // Hayden Christensen
                new Actor { PersonId = 20, MovieId = 6 }, // Ian McDiarmid
                new Actor { PersonId = 25, MovieId = 6 }, // Samuel L. Jackson
                // Episode VII - The Force Awakens
                new Actor { PersonId = 26, MovieId = 7 }, // Daisy Ridley - Rey
                new Actor { PersonId = 27, MovieId = 7 }, // John Boyega - Finn
                new Actor { PersonId = 28, MovieId = 7 }, // Adam Driver - Kylo Ren
                new Actor { PersonId = 29, MovieId = 7 }, // Oscar Isaac - Poe
                new Actor { PersonId = 14, MovieId = 7 }, // Harrison Ford
                new Actor { PersonId = 15, MovieId = 7 }, // Carrie Fisher
                new Actor { PersonId = 13, MovieId = 7 }, // Mark Hamill
                // Episode VIII - The Last Jedi
                new Actor { PersonId = 26, MovieId = 8 }, // Daisy Ridley
                new Actor { PersonId = 27, MovieId = 8 }, // John Boyega
                new Actor { PersonId = 28, MovieId = 8 }, // Adam Driver
                new Actor { PersonId = 29, MovieId = 8 }, // Oscar Isaac
                new Actor { PersonId = 13, MovieId = 8 }, // Mark Hamill
                new Actor { PersonId = 15, MovieId = 8 }, // Carrie Fisher
                // Episode IX - The Rise of Skywalker
                new Actor { PersonId = 26, MovieId = 9 }, // Daisy Ridley
                new Actor { PersonId = 27, MovieId = 9 }, // John Boyega
                new Actor { PersonId = 28, MovieId = 9 }, // Adam Driver
                new Actor { PersonId = 29, MovieId = 9 }, // Oscar Isaac
                new Actor { PersonId = 20, MovieId = 9 }, // Ian McDiarmid
                new Actor { PersonId = 19, MovieId = 9 }, // Billy Dee Williams
            };

            foreach (Actor actor in actors)
            {
                context.Actors.Add(actor);
            }

            context.SaveChanges();
        }
    }
}
