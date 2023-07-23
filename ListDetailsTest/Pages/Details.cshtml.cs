using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ListDetailsTest.Model;
using Microsoft.EntityFrameworkCore;

namespace ListDetailsTest.Pages
{
    public class DetailsModel : PageModel
    {

        public ApplicationContext context;

        public DetailsModel(ApplicationContext db)
        {
            context = db;
        }

        public int Id { get; private set; }
        public Company? company;
        public void OnGet(int id)
        {
            Id = id;
            company = context.companies.Include(c => c.orders).Include(c => c.notes).Include(c => c.employees).FirstOrDefault(c => c.id == Id);
        }

    }
}
