using System.Collections.Generic;

namespace CourseProject.Domain.Entities
{
    public class Medal
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public string  Description { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
