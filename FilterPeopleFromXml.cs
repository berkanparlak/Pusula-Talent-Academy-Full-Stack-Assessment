using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

public class FilterPeopleFromXmlSolution
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        if (string.IsNullOrWhiteSpace(xmlData))
        {
            return JsonSerializer.Serialize(new
            {
                Names = Array.Empty<string>(),
                TotalSalary = 0m,
                AverageSalary = 0m,
                MaxSalary = 0m,
                Count = 0
            });
        }

        var doc = XDocument.Parse(xmlData);

        var cutoff = new DateTime(2019, 1, 1);

        var people = doc.Descendants("Person")
            .Select(p => new
            {
                Name = (string?)p.Element("Name") ?? "",
                Age = (int?)p.Element("Age") ?? 0,
                Department = (string?)p.Element("Department") ?? "",
                Salary = decimal.TryParse((string?)p.Element("Salary"), out var s) ? s : 0m,
                HireDate = DateTime.TryParse((string?)p.Element("HireDate"), out var d) ? d : DateTime.MinValue
            })
            .Where(x =>
                x.Age > 30 &&
                string.Equals(x.Department, "IT", StringComparison.OrdinalIgnoreCase) &&
                x.Salary > 5000m &&
                x.HireDate < cutoff
            )
            .ToList();

        var names = people.Select(x => x.Name).OrderBy(n => n, StringComparer.Ordinal).ToArray();
        var count = people.Count;
        var total = people.Sum(x => x.Salary);
        var avg = count > 0 ? total / count : 0m;
        var max = count > 0 ? people.Max(x => x.Salary) : 0m;

        return JsonSerializer.Serialize(new
        {
            Names = names,
            TotalSalary = total,
            AverageSalary = avg,
            MaxSalary = max,
            Count = count
        });
    }
}

