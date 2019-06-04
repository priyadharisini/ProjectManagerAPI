using System.Data.Entity;
using TM.Entities;

namespace TM.Data
{
    public class PMdbContext : DbContext
    {
        public PMdbContext() : base("name=TaskManagerEntities")
        {
            Database.SetInitializer<PMdbContext>(new CreateDatabaseIfNotExists<PMdbContext>());
        }

        public DbSet<ParentTask> ParentTasks { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectTask> Tasks { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
