using Microsoft.EntityFrameworkCore;
using TodoListGQL.Models;

namespace TodoListGQL.Data
{
    public class ApiDbContext:DbContext
    {
        public virtual DbSet<ItemData> items {get; set;}
        public virtual DbSet<ItemList> lists {get; set;}
        public ApiDbContext(DbContextOptions<ApiDbContext> options):base(options){}
        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            model.Entity<ItemData>(entity =>
            {
                entity.HasOne(d => d.ItemList)
                    .WithMany(p => p.ItemDatas)
                    .HasForeignKey(d => d.ListId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_ItemData_ItemList");
            });
        }
    }
}