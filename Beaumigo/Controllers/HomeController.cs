
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
using Microsoft.AspNetCore.Http;

namespace Beaumigo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // stel in waar de database gevonden kan worden
        //string connectionString = "Server=172.16.160.21;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";
        string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["user"] = HttpContext.Session.GetString("User");

            return View();
        }

        public List<Locatie> GetLocaties()
        {
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

        [Route("gerecht/{id}")]
        public IActionResult Gerecht(string id)
        {
            var model = GetGerecht(id);
            return View(model);
        }

        private Gerecht GetGerecht(string id)
        {
            List<Gerecht> festival = new List<Gerecht>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from eten where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Gerecht g = new Gerecht
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Naam = reader["naam"].ToString(),
                            Gang = reader["gang"].ToString(),
                            Ingredienten = reader["ingredienten"].ToString(),
                            Allergieen = reader["allergieen"].ToString(),
                            Prijs = reader["prijs"].ToString(),
                            Foto = reader["foto"].ToString(),


                        };
                        festival.Add(g);
                    }
                }
            }

            return festival[0];
        }

        public List<Eten> GetEten()
        {


            // maak een lege lijst waar we de namen in gaan opslaan
            List<Eten> Eten = new List<Eten>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from eten", conn);

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
                            Gang = reader["gang"].ToString(),
                            Ingredienten = reader["ingredienten"].ToString(),
                            Allergieen = reader["allergieen"].ToString(),
                            Naam = reader["naam"].ToString(),
                            Prijs = reader["prijs"].ToString(),
                            Foto = reader["foto"].ToString(),


                        };

                        // voeg de naam toe aan de lijst met namen
                        Eten.Add(e);
                    }
                }
            }

            // return de lijst met namen
            return Eten;
        }



        public List<Contact> GetContacten()
        {
            // maak een lege lijst waar we de namen in gaan opslaan
            List<Contact> contacten = new List<Contact>();

            // verbinding maken met de database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                // verbinding openen
                conn.Open();

                // SQL query die we willen uitvoeren
                MySqlCommand cmd = new MySqlCommand("select * from contact", conn);

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


        [Route("contact")]
        public IActionResult Contact()
        {
            //var contacten = GetContacten();
            return View();
        }


        [HttpPost]
        [Route("contact")]
        public IActionResult contact(Person person)
        {
            if (ModelState.IsValid)
            {
                SavePerson(person);
                return Redirect("/succes");
            }


            return View(person);
        }

        [Route("locatie")]
        public IActionResult Locatie()
        {
            var locaties = GetLocaties();

            return View(locaties);
        }

        [Route("straatnummer/{id}")]
        public IActionResult Plaats(string id)
        {
            var model = GetPlaats(id);
            return View(model);
        }

        private Plaats GetPlaats(string id)
        {
            List<Plaats> locatie = new List<Plaats>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from locaties where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Plaats p = new Plaats
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            straatnummer = reader["straatnummer"].ToString(),
                            postcode = reader["postcode"].ToString(),
                            telefoonnummer = reader["telefoonnummer"].ToString(),


                        };
                        locatie.Add(p);
                    }
                }
            }

            return locatie[0];
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login(string username, string password)
        {
            if (password == "geheim")
            {
                HttpContext.Session.SetString("User", username);
                return Redirect("/");
            }

            return View();
        }

        [Route("succes")]
        public IActionResult Succes()
        {
            return View();
        }



        private void SavePerson(Person person)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(voornaam, achternaam, email, telefoon, adres, bericht) VALUES(?voornaam, ?achternaam, ?email, ?telefoon, ?adres, ?bericht)", conn);

                cmd.Parameters.Add("?voornaam", MySqlDbType.Text).Value = person.FirstName;
                cmd.Parameters.Add("?achternaam", MySqlDbType.Text).Value = person.LastName;
                cmd.Parameters.Add("?email", MySqlDbType.Text).Value = person.Email;
                cmd.Parameters.Add("?telefoon", MySqlDbType.Text).Value = person.Phone;
                cmd.Parameters.Add("?adres", MySqlDbType.Text).Value = person.Address;
                cmd.Parameters.Add("?bericht", MySqlDbType.Text).Value = person.Message;
                cmd.ExecuteNonQuery();
            }
        }



        //  [HttpPost]
        //   public IActionResult Login(string voornaam, string achternaam)
        //  {
        //     ViewData["voornaam"] = voornaam;
        //     ViewData["achternaam"] = achternaam;
        //
        //      return View();
        // }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}