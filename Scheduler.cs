namespace Scheduler
{
    public class Scheduler
    {
        private Dictionary<string, bool> completed;
        private Dictionary<string, bool> courses;

        public Scheduler()
        {
            completed = new Dictionary<string, bool>();
            courses = new Dictionary<string, bool>();
        }

        public Scheduler(List<string> courses)
        {
            completed = new Dictionary<string, bool>();
            this.courses = new Dictionary<string, bool>();
        }
    }

    public class Course
    {
        private string classification;
        private string name;
        private string description;

        public Course(int number, string name, string description)
        {
            classification = "CS " + number.ToString();
            this.name = name;
            this.description = description;
        }
    }
}