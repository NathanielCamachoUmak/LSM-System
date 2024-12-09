using System.Windows;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace LSM_prototype
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Database=BenjaminDB;Trusted_Connection=True;"; 
            string reseedQuery = "DBCC CHECKIDENT ('Orders', RESEED, 10000)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(reseedQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Table reseeded successfully.");
            }
        }
    }



}
