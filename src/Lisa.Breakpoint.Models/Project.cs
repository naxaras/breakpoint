namespace Lisa.Breakpoint.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Project()
        {
            Id = -1;
            Name = null;
        }

        public Project(string name)
        {
            Id = 0;
            Name = name;
        }

        public static bool IsInitialized(Project project)
        {
            if (project == null)
            {
                return false;
            }
            return string.IsNullOrEmpty(project.Name) ? false : true;
        }
    }
}