using System.ComponentModel.DataAnnotations.Schema;

namespace ListDetailsTest.Model
{
    public class Employee
    {
        public int id { get; set; }
        public string? firstName { get; set; }
        public string? secondName { get; set; }
        public string? title { get; set; }
        public DateOnly birth { get; set; }
        public string? position { get; set; }

        public int companyId { get; set; }
        public Company? company { get; set; }
    }
}
