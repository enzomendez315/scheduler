namespace Scheduler
{
    public class Scheduler
    {
        private Dictionary<string, Course> completed;
        private Dictionary<string, Course> courses;

        public Scheduler()
        {
            completed = new Dictionary<string, Course>();
            courses = new Dictionary<string, Course>();
        }

        public Scheduler(List<string> courseList)
        {
            completed = new Dictionary<string, Course>();
            courses = new Dictionary<string, Course>();
        }

        public void AddCompleted(Course course)
        {
            if (!completed.ContainsKey(course.Code))
            {
                completed[course.Code] = course;
            }
        }

        public void AddCourse(Course course)
        {
            if (!courses.ContainsKey(course.Code))
            {
                courses[course.Code] = course;
            }
        }
    }

    public class Course
    {
        private string code;
        private string name;
        private string description;

        public Course(int code, string name, string description)
        {
            this.code = "CS " + code.ToString();
            this.name = name;
            this.description = description;
        }

        public string Code
        {
            get { return code; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }
    }
}