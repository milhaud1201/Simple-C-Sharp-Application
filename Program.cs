// Program.cs
using System;
using MongoDB.Driver;

using com.hr; // Needed for Employee

namespace listlinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "mongodb://localhost";
			var client = new MongoClient(connectionString);
			var db = client.GetDatabase("hr");
			var collection = db.GetCollection<Employee>("emp");
			
            var employee = new Employee{First="Samuel", Last="Clayton", Salary=100000};
            collection.InsertOne(employee);  
            var filter = Builders<Employee>.Filter.Eq("Last", "Clayton") &
                         Builders<Employee>.Filter.Eq("First", "Samuel");

            var update = Builders<Employee>.Update.Set("Salary", 155000);
            var opts = new UpdateOptions(){IsUpsert = true};
            // collection.DeleteMany(filter);

            collection.UpdateOne(filter, update, opts);

            filter = Builders<Employee>.Filter.Eq("Last", "Miller") &
                         Builders<Employee>.Filter.Eq("First", "Heather");

            update = Builders<Employee>.Update.Set("Salary", 100000);
            collection.UpdataOne(filter, update);

            Employee.printCollection(collection);

            // db.DropCollection("emp");

        }
    }
}