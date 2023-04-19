using Shopping.Data.Entities;

namespace Shopping.Data
{
    public class SeedDB
    {
        private readonly DataContext _context;
        public SeedDB(DataContext context) { 
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountryAsync();
            await CheckCategoriesAsync();
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.categories.Any())
            {
                _context.categories.Add(new Category { Nombre = "Tecnología" });
                _context.categories.Add(new Category { Nombre = "Ropa" });
                _context.categories.Add(new Category { Nombre = "Gamer" });
                _context.categories.Add(new Category { Nombre = "Belleza" });
                _context.categories.Add(new Category { Nombre = "Nutrición" });

                await _context.SaveChangesAsync();
            }

        }

        private async Task CheckCountryAsync()
        {
            if (!_context.countries.Any())
            {
                _context.countries.Add(new Country {
                    Nombre = "Mexico",
                    states = new List<State>()
                    {
                        new State()
                        {
                            Nombre = "Tabasco",
                            cities = new List<City>()
                            {
                                new City(){Nombre = "Macuspana"},
                                new City(){Nombre = "Villahermosa"},
                                new City(){Nombre = "Cunduacan"},
                                new City(){Nombre = "Balancan",}
                            }
                        },
                        new State()
                        {
                            Nombre = "Campeche",
                            cities = new List<City>()
                            {
                                new City(){Nombre = "Campeche"},
                                new City(){Nombre = "CD Del Carmen"},
                                new City(){Nombre = "Candelaria"},
                            }
                        },
                    }
                });
                _context.countries.Add(new Country
                {
                    Nombre = "Estados Unidos",
                    states = new List<State>()
                    {
                        new State()
                        {
                            Nombre = "Florida",
                            cities = new List<City>() {
                                new City() { Nombre = "Orlando" },
                                new City() { Nombre = "Miami" },
                                new City() { Nombre = "Tampa" },
                                new City() { Nombre = "Fort Lauderdale" },
                                new City() { Nombre = "Key West" },
                            }
                        },
                        new State()
                        {
                            Nombre = "Texas",
                            cities = new List<City>() {
                                new City() { Nombre = "Houston" },
                                new City() { Nombre = "San Antonio" },
                                new City() { Nombre = "Dallas" },
                                new City() { Nombre = "Austin" },
                                new City() { Nombre = "El Paso" },
                            }
                        },
                    }
                });

                await _context.SaveChangesAsync();
            }

        }
    }
}
