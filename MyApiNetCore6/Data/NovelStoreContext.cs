using Microsoft.EntityFrameworkCore;

namespace MyApiNetCore6.Data
{
    public class NovelStoreContext : DbContext
    {
        public NovelStoreContext(DbContextOptions<NovelStoreContext> options) : base(options) {

        }
        #region DbSet
        public DbSet<Novel> ? Novel { get; set; }
        #endregion
    }
}
