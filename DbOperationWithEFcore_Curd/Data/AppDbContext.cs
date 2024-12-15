using Microsoft.EntityFrameworkCore;

namespace DbOperationWithEFcore_Curd.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }
    }
}
