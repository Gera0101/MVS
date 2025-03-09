using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVS.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using Npgsql;

namespace MVS.Controllers;

public class HomeController : Controller
{
    
    private string connectionString = "Server=localhost,1433;Database=master;User Id=SA;Password=CodeWithArjun123;TrustServerCertificate=True;";
    public HomeController()
    {
        
    }
    public IActionResult Home() => View();
    public IActionResult Create() => View();
    public IActionResult CreateForm(Person person)
    {
        string forId = "SELECT COUNT(*) as id FROM person;";
        int idForNext = 1;
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var id = connection.Query(forId);
            foreach ( var i in id) {
                idForNext += i.id;
                break;
            }
            var insert = $"INSERT INTO person(id, saxeli, gvari, misamarti, telefoni, tarigi, asaki) VALUES({idForNext}, '{person.saxeli}', '{person.gvari}', '{person.misamarti}', '{person.telefoni}', GETDATE(), {person.asaki});";
            connection.Execute(insert);
        }
        return RedirectToAction("Home");
    }
}

