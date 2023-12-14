using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbAjaxProject.DAL.Entities
{
	public class Employee
	{
		[BsonId]
		[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
		public string EmployeeID { get; set; }
		public string EmployeeName { get; set; }
		public string EmployeeSurname { get; set; }
		public decimal EmployeeSalary { get; set; }
	}
}