using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListDetailsTest.Model
{
    public class Note
    {
        public int invoiceNumber { get; set; }
        public string? employee { get; set; }
        public int companyId { get; set; }
        public Company? company { get; set; }
    }
}
