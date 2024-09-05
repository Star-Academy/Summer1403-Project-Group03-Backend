using AnalysisData.Graph.Model.Category;
using AnalysisData.Graph.Model.Edge;
using AnalysisData.Graph.Model.File;
using AnalysisData.Graph.Model.Node;
using AnalysisData.User.Model;
using Microsoft.EntityFrameworkCore;

namespace AnalysisData.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User.Model.User> Users { get; set; }
    public DbSet<AttributeEdge> AttributeEdges { get; set; }
    public DbSet<AttributeNode> AttributeNodes { get; set; }
    public DbSet<EntityEdge> EntityEdges { get; set; }
    public DbSet<EntityNode> EntityNodes { get; set; }
    public DbSet<ValueEdge> ValueEdges { get; set; }
    public DbSet<ValueNode> ValueNodes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<FileEntity> FileUploadedDb { get; set; }
    public DbSet<UserFile> UserFiles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ResetPasswordToken> Tokens { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, RoleName = "admin", RolePolicy = "gold" },
            new Role { Id = 2, RoleName = "Data-Analyst", RolePolicy = "bronze" },
            new Role { Id = 3, RoleName = "Data-Manager", RolePolicy = "silver" }
        );
    
        modelBuilder.Entity<User.Model.User>().HasData(
            
            new User.Model.User
            {
                Id = Guid.NewGuid(),
                Username = "admin",
                Password = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", 
                PhoneNumber = "09131111111",
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@gmail.com",
                RoleId = 1
            }
        );
    }

}