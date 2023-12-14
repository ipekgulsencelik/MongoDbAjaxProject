using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDbAjaxProject.DAL.Entities;
using MongoDbAjaxProject.DAL.Settings;
using Newtonsoft.Json;

namespace MongoDbAjaxProject.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IMongoCollection<Employee> _employeeCollection;

		public EmployeeController(IDatabaseSettings databaseSettings)
		{
			var client = new MongoClient(databaseSettings.ConnectionString);
			var database = client.GetDatabase(databaseSettings.DatabaseName);
			_employeeCollection = database.GetCollection<Employee>(databaseSettings.EmployeeCollectionName);
		}

		public IActionResult Index()
		{
			return View();
		}

        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeCollection.Find(x => true).ToListAsync();
            var jsonEmployee = JsonConvert.SerializeObject(values);
            return Json(jsonEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            await _employeeCollection.InsertOneAsync(employee);
            var values = JsonConvert.SerializeObject(employee);
            return Json(values);
        }
    }
}