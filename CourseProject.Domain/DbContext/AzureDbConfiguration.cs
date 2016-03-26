using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace CourseProject.Domain.DbContext
{
    public class AzureDbConfiguration : DbConfiguration
    {
        public AzureDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}