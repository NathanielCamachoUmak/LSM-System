using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, (string password, string role)> userAccounts = new Dictionary<string, (string password, string role)>
        {
            { "employee1", ("password1", "Employee") },
            { "employee2", ("password2", "Employee") },
            { "employee3", ("password3", "Employee") },
            { "admin", ("adminpassword", "Admin") },
        };

        Console.WriteLine("Login");
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (userAccounts.TryGetValue(username, out var userInfo) && userInfo.password == password)
        {
            Console.WriteLine($"Welcome, {userInfo.role}!");
        }
        else
        {
            Console.WriteLine("Invalid Username or Password.");
        }
    } 
}
