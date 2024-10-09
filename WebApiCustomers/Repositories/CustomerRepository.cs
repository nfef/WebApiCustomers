using WebApiCustomers.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApiCustomers.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    //Define your additional Signature methods here
    private readonly CustomerDemoDbContext _context;
    public CustomerRepository(CustomerDemoDbContext context) : base(context) 
    {
        _context = context;
    }

}
