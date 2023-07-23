using Microsoft.EntityFrameworkCore;

namespace ListDetailsTest.Model
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Company> companies { get; set; } = null!;
        public DbSet<Employee> employees { get; set; } = null!;
        public DbSet<Note> notes { get; set; } = null!;
        public DbSet<Order> orders { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().HasOne(e => e.company).WithMany(c => c.employees).HasForeignKey(e => e.companyId);
            builder.Entity<Order>().HasOne(o => o.company).WithMany(c => c.orders).HasForeignKey(o => o.companyId);
            builder.Entity<Note>().HasOne(n => n.company).WithMany(c => c.notes).HasForeignKey(n => n.companyId);

            builder.Entity<Note>().HasKey(n => n.invoiceNumber);
            builder.Entity<Order>().HasKey(o => new { o.date, o.storeCity });

            Company company1 = new() { id=1, name = "Super Company", addres = "Some address", city = "Sydney", state = "New South Wales", phone = "+1-123-456-78-90" };
            Company company2 = new() { id=2, name = "General Armory", addres = "IDK, 10", city = "Tomsk", state = "Tomskaya Oblast'", phone = "+7-999-999-99-99" };
            Company company3 = new() { id=3, name = "Lorem Ipsum", addres = "Dolor sit amet", city = "Consectetur", state = "adipiscing elit", phone = "+1-111-111-11-11" };

            List<Employee> employees = new List<Employee> {
                new Employee { id=4, companyId = company1.id, firstName = "Helen", secondName = "Carter", birth = new DateOnly(1988,12,5), title="Mrs.", position = "CEO" },
                new Employee { id=5, companyId = company1.id, firstName="Feder", secondName="Pushinch", birth = new DateOnly(2000,5,17), title="Mr.", position = "Developer"},
                new Employee { id=6, companyId = company1.id, firstName="Alexey", secondName="Lumpov", birth= new DateOnly(2003,06,03), title="Mr.", position = "Developer"},
                new Employee { id=7, companyId = company1.id, firstName="Pavel", secondName="Durov", birth= new DateOnly(1987, 11, 11), title="Mr.", position = "Marketing"},

                new Employee { id=8, companyId = company2.id, firstName = "Wendy", secondName = "Reed", birth= new DateOnly(1977,5,21),title = "Ms.", position="CEO"},
                new Employee { id=9, companyId = company2.id, firstName="Alicia", secondName = "Lee", birth=new DateOnly(1999,3,27), title="Mrs.", position = "Developer"},
                new Employee { id=10, companyId = company2.id, firstName="Peggy", secondName="Wong", birth = new DateOnly(2000,10,10), title="Mrs.", position = "Marketing"},
                new Employee { id=11, companyId = company2.id, firstName="Bradley", secondName="White", birth=new DateOnly(1954, 5,12), title="Mr.", position="Security"},

                new Employee { id=12, companyId = company3.id, firstName="Michael", secondName="McDaniel", birth=new DateOnly(1997,4,7), title="Mr.", position="CEO"},
                new Employee { id=13, companyId = company3.id, firstName="Kent", secondName="Hughes", birth=new DateOnly(2001,12,31), title="Mr.", position="Developer"},
                new Employee { id=14, companyId = company3.id, firstName="Tyler", secondName="Durden", birth=new DateOnly(1978,2,15), title = "Mr.", position="Fighter" },
                new Employee { id=15, companyId = company3.id, firstName="Peter", secondName="Rose", birth= new DateOnly(1999,12,31), title="Mr.", position = "Marketing"}
            };

            List<Note> notes = new List<Note>
            {
                new Note{invoiceNumber = 123456, employee =  employees[1].firstName +" " + employees[1].secondName, companyId = company1.id },
                new Note{invoiceNumber = 225841,  employee =  employees[1].firstName +" " + employees[1].secondName, companyId = company1.id },
                new Note{invoiceNumber = 698874,  employee =  employees[3].firstName +" " + employees[3].secondName, companyId = company1.id },

                new Note{invoiceNumber = 865478,  employee =  employees[5].firstName +" " + employees[5].secondName, companyId = company2.id },
                new Note{invoiceNumber = 687445,  employee =  employees[6].firstName +" " + employees[6].secondName, companyId = company2.id },
                new Note{invoiceNumber = 141741,  employee =  employees[7].firstName +" " + employees[7].secondName, companyId = company2.id },

                new Note{invoiceNumber = 321893,  employee =  employees[9].firstName +" " + employees[9].secondName, companyId = company3.id },
                new Note{invoiceNumber = 166843,  employee =  employees[10].firstName +" " + employees[10].secondName, companyId = company3.id },
                new Note{invoiceNumber = 549863,  employee =  employees[11].firstName +" " + employees[11].secondName, companyId = company3.id },
            };

            List<Order> orders = new List<Order>
            {
                new Order{date = new DateOnly(2023,2,12), storeCity ="Sydney", companyId = company1.id},
                new Order{date = new DateOnly(2022,12,12), storeCity = "Melbourne", companyId = company1.id},
                new Order{date = new DateOnly(2022,11,12), storeCity = "Melbourne", companyId = company1.id},
                new Order{date = new DateOnly(2022,12,21), storeCity ="Sydney", companyId = company1.id},
                new Order{date = new DateOnly(2023,4,2), storeCity ="Sydney", companyId = company1.id},

                new Order{date = new DateOnly(2022,3,6), storeCity = "Tomsk", companyId=company2.id},
                new Order{date = new DateOnly(2012,12,31), storeCity = "Novosibirsk", companyId=company2.id},
                new Order{date = new DateOnly(2023,5,4), storeCity = "Kemerovo", companyId=company2.id},

                new Order{date = new DateOnly(2022,1,14), storeCity ="Los Angeles", companyId=company3.id},
                new Order{date = new DateOnly(2023,4,29), storeCity = "Goodsprings", companyId=company3.id},
                new Order{date = new DateOnly(2023,7,23), storeCity = "Nashville", companyId=company3.id}
            };

            builder.Entity<Company>().HasData(company1,company2,company3);
            builder.Entity<Employee>().HasData(employees);
            builder.Entity<Note>().HasData(notes);
            builder.Entity<Order>().HasData(orders);
        } 
    }
}
