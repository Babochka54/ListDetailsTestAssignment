using ListDetailsTest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ListDetailsTest.Pages
{
    public class IndexModel : PageModel
    {

        public ApplicationContext context;
        public List<Company> companies = new();

        public IndexModel(ApplicationContext db)
        {
            context = db;
        }

        public void OnGet()
        {
            companies = context.companies.ToList();
        }
    }
}
