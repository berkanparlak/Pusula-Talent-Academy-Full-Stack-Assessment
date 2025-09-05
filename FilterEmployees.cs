using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class FilterEmployeesSolution
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null)
        {
            return JsonSerializer.Serialize(new
            {
                Names = Array.Empty<string>(),
                TotalSalary = 0m,
                AverageSalary = 0m,
                MinSalary = 0m,
                MaxSalary = 0m,
                Count = 0
            });
        }

        var minHireInclusive = new DateTime(2017, 12, 31);

        var filtered = employees
            .Where(e =>
                e.Age >= 25 && e.Age <= 40 &&
                (string.Equals(e.Department, "IT", StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(e.Department, "Finance", StringComparison.OrdinalIgnoreCase)) &&
                e.Salary >= 5000m && e.Salary <= 9000m &&
                e.HireDate >= minHireInclusive
            )
            .ToList();

        var orderedNames = filtered
            .Select(e => e.Name ?? string.Empty)
            .OrderByDescending(n => n.Length)
            .ThenBy(n => n, StringComparer.Ordinal)
            .ToArray();

        var count = filtered.Count;
        var total = filtered.Sum(e => e.Salary);
        var avg = count > 0 ? Math.Round(total / count, 2) : 0m;
        var min = count > 0 ? filtered.Min(e => e.Salary) : 0m;
        var max = count > 0 ? filtered.Max(e => e.Salary) : 0m;

        return JsonSerializer.Serialize(new
        {
            Names = orderedNames,
            TotalSalary = total,
            AverageSalary = avg,
            MinSalary = min,
            MaxSalary = max,
            Count = count
        });
    }
}

