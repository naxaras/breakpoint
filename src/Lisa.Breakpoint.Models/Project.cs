using Lisa.Breakpoint.Models.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lisa.Breakpoint.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<UserCollection> Members { get; set; }

        public Project()
        {
            Id = -1;
            Name = null;
            Members = null;
        }

        public Project(string name, IEnumerable<UserCollection> members)
        {
            Id = 0;
            Name = name;
            Members = members.ToList();
        }

        public static bool IsInitialized(Project project)
        {
            if (project == null || project.Members == null)
            {
                return false;
            }

            return string.IsNullOrEmpty(project.Name) ? false : true;
        }
    }
}