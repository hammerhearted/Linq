using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Linq12
{
    class Program
    {
        static void Main(string[] args)
        {           
            var strs = File.ReadAllLines("db.txt");
            var dataBase = strs
                .Select(s => s.Split())
                .Select(data => new Record
                {
                    ClientID = int.Parse(data[0]),
                    Year = int.Parse(data[1]),
                    Month = int.Parse(data[2]),
                    Duration = int.Parse(data[3])
                })
                .ToList();

            PrintClientsYearWithMaxDuration(dataBase);
            
            var numbersArray = new[] { 33, -189, 11, 22, 5, 17, 32, -100, 400, 396, 2, 33, 667 };
            Console.WriteLine(string.Join(", ", GetNumbersOrderedByReminderThenMin(numbersArray, 2)));

            Console.ReadKey();


        }

        static void PrintClientsYearWithMaxDuration(List<Record> db)
        {
            var durations = db
                .GroupBy(record => (record.ClientID, record.Year))
                .Select(records => (Duration: records.Sum(record => record.Duration), Year: records.Key.Year, ClientID: records.Key.ClientID))
                .GroupBy(cortege => cortege.ClientID)
                .Select(corteges => corteges.Max())
                .OrderBy(tuple => tuple.ClientID);
            foreach (var (duration, year, clientId) in durations)
                Console.WriteLine($"ID клиента: {clientId}  Год: {year}  Продолжительность занятий составила {duration} часов");
        }

        static int[] GetNumbersOrderedByReminderThenMin(int[] array, int number)
        {
            return array
                .Where(n => n > 0)
                .Distinct()
                .OrderBy(n => n % number)
                .ThenByDescending(n => n)
                .ToArray();
        }

    }
}
