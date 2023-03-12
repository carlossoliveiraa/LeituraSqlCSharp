using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        ADO();
        //Dapper();
        //EntityCore();
    }

    #region EntityFrameworkCOre
    private static void EntityCore()
    {

        string connectionString = "Data Source=DESKTOP-RDKD4J2;Initial Catalog=ControleSimples;User ID=sa;Password=123;Encrypt=false;";

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        using (var dbContext = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>().UseSqlServer(connectionString).Options))
        {
            List<Tb_Teste> data = dbContext.Tb_Teste.ToList();

            foreach (var item in data)
            {
                Console.WriteLine("Id: {0}, Name: {1}", item.Id, item.Name);
            }
        }

        stopwatch.Stop();
        Console.WriteLine("Query executada em {0} milliseconds", stopwatch.ElapsedMilliseconds);
    }
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Tb_Teste> Tb_Teste { get; set; }
    }
    public class Tb_Teste
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    #endregion

    #region Dapper
    private static void Dapper()
    {
        string connectionString = "Data Source=DESKTOP-RDKD4J2;Initial Catalog=ControleSimples;User ID=sa;Password=123";
        string query = "SELECT * FROM Tb_Teste";

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            var results = connection.Query(query);

            foreach (var result in results)
            {
                int id = result.Id;
                string name = result.Name;
                int age = result.Age;

                Console.WriteLine($"Id: {id}, Name: {name}, Age: {age}");
            }
        }
        // Stop the stopwatch and print the elapsed time
        stopwatch.Stop();
        Console.WriteLine("Query executada em {0} milliseconds", stopwatch.ElapsedMilliseconds);
    }
    #endregion

    #region ADO
    private static void ADO()
    {
        string connectionString = "Data Source=DESKTOP-RDKD4J2;Initial Catalog=ControleSimples;User ID=sa;Password=123";
        string query = "SELECT * FROM Tb_Teste";

        // Set up a stopwatch to measure the time it takes to execute the query
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int age = reader.GetInt32(2);

                    Console.WriteLine($"Id: {id}, Name: {name}, Age: {age}");
                }
            }
        }
        // Stop the stopwatch and print the elapsed time
        stopwatch.Stop();
        Console.WriteLine("Query executada em {0} milliseconds", stopwatch.ElapsedMilliseconds);
    }
    #endregion
}
