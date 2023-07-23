using System.ComponentModel.DataAnnotations.Schema;

namespace ListDetailsTest.Model
{
    public class Company
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? addres { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? phone { get; set; }

        public List<Employee> employees { get; set; } = new();
        public List<Note> notes { get; set; } = new();
        public List<Order> orders { get; set; } = new();
    }
}
