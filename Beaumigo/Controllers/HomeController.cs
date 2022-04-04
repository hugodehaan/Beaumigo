using Beaumigo.Models;
using MySql.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Beaumigo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public List<Locatie> GetLocaties()
        {
            // stel in waar de database gevonden kan worden
            //string connectionString = "Server=172.16.160.21;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";
            string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Locatie> locaties = new List<Locatie>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from locaties", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Locatie l = new Locatie

                        {   
                        // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["Id"]),
                            straatnummer = reader["straatnummer"].ToString(),
                            postcode = reader["postcode"].ToString(),
                            telefoonnummer = reader["Telefoonnummer"].ToString(),
     
                         };

                    // voeg de naam toe aan de lijst met namen
                    locaties.Add(l);
                    }
                }
            }

            // return de lijst met namen
            return locaties;
        }


        [Route("eten")]
        public IActionResult Eten()
        {
            var Eten = GetEten();

            return View(Eten);
        }

        public List<Eten> GetEten()
        {
            // stel in waar de database gevonden kan worden
            //string connectionString = "Server=172.16.160.21;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";
            string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Eten> Eten = new List<Eten>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from locaties", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Eten e = new Eten

                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["Id"]),
                            gang = reader["gang"].ToString(),
                            ingredienten = reader["ingredienten"].ToString(),
                            allergieen = reader["allergieen"].ToString(),
                            naam = reader["naam"].ToString(),

                        };

                        // voeg de naam toe aan de lijst met namen
                        Eten.Add(e);
                    }
                }
            }

            // return de lijst met namen
            return Eten;
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            var contacten = GetContacten();

            return View();
        }

        public List<Contact> GetContacten()
        {
            // stel in waar de database gevonden kan worden
            //string connectionString = "Server=172.16.160.21;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";
            string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<Contact> contacten = new List<Contact>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from contacten", conn);

                // resultaat van de query lezen
                using (var reader = cmd.ExecuteReader())
                {
                    // elke keer een regel (of eigenlijk: database rij) lezen
                    while (reader.Read())
                    {
                        Contact c = new Contact

                        {
                            // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"
                            Id = Convert.ToInt32(reader["Id"]),
                            naam = reader["naam"].ToString(),
                            email = reader["email"].ToString(),
                            telefoon = reader["telefoon"].ToString(),

                        };

                        // voeg de naam toe aan de lijst met namen
                        contacten.Add(c);
                    }
                }
            }

            // return de lijst met namen
            return contacten;
        }

        [Route("locatie")]
        public IActionResult Locatie()
        {
            var locaties = GetLocaties();
           
            return View(locaties); 
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("404")]
        public IActionResult Foutpagina()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
