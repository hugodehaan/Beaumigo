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
           // var locaties = GetLocaties(); 

            return View();
        }

        public List<string> GetLocaties()
        {
            // stel in waar de database gevonden kan worden
            //string connectionString = "Server=172.16.160.21;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";
            string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110368;Uid=110368;Pwd=inf2021sql;";

            // maak een lege lijst waar we de namen in gaan opslaan
            List<string> locaties = new List<string>();

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


                        // selecteer de kolommen die je wil lezen. In dit geval kiezen we de kolom "naam"                           
                        string straatnummer = reader["straatnummer"].ToString();

                        

                        // voeg de naam toe aan de lijst met namen
                        locaties.Add(straatnummer);
                    }
                }
            }

            // return de lijst met namen
            return locaties;
        }


        [Route("eten")]
        public IActionResult Eten()
        {
            return View();
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("locatie")]
        public IActionResult Locatie()
        {
            return View();
        }

        [Route("login")]
        public IActionResult Login()
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
