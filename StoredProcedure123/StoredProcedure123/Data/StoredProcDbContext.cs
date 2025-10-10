using Microsoft.EntityFrameworkCore;

namespace StoredProcedure123.Data
{
    public class StoredProcDbContext : DbContext
    {
        public StoredProcDbContext(DbContextOptions<StoredProcDbContext> options)
            : base(options) {}


    }
}
