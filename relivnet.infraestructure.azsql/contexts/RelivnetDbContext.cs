using relivnet.infraestructure.azsql.seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using relivnet.domain.entities;
using relivnet.domain.models;
using relivnet.infraestructure.azsql.configs;

namespace relivnet.infraestructure.azsql.contexts
{
    public class RelivnetDbContext : DbContext
    {
        private readonly Func<UserInfoModel> _userInfoFactory;
        private UserInfoModel UserInfo => _userInfoFactory();


        public RelivnetDbContext(DbContextOptions<RelivnetDbContext> options) : base(options)
        {
        }

        public RelivnetDbContext(DbContextOptions<RelivnetDbContext> options,
                                Func<UserInfoModel> userInfoFactory) : base(options)
        {
            this._userInfoFactory = userInfoFactory;
        }
        
        public override int SaveChanges()
        {
            this.SetFieldsAudit();
            return base.SaveChanges();
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetFieldsAudit();
            return base.SaveChangesAsync(cancellationToken);
        }


        private void SetFieldsAudit()
        {
            foreach (EntityEntry<BaseEntity> baseEntity in ChangeTracker.Entries<BaseEntity>())
            {
                switch (baseEntity.State)
                {
                    case EntityState.Added:
                        baseEntity.Entity.UserCreatedAt = "N/A";
                        baseEntity.Entity.CreateDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        baseEntity.Entity.UpdateDate = DateTime.Now;
                        baseEntity.Entity.UserUpdatedAt = this.UserInfo.UserName;
                        break;
                }
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new StateConfig());
            
            modelBuilder.SeedUser();
            modelBuilder.SeedRole();
            modelBuilder.SeedUserRole();
        }

        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<RoleEntity> RoleEntity { get; set; }
        public DbSet<UserRoleEntity> UserRoleEntity { get; set; }
        public DbSet<ProductEntity> ProductEntity { get; set; }
        public DbSet<CategoryEntity> CategotyEntity { get; set; }
        public DbSet<StateEntity> StateEntity { get; set; }
        
    }
}
