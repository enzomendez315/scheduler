namespace SchedulerBuilder
{
    public class Scheduler
    {
        private Dictionary<string, Course> completed;
        private Dictionary<string, Course> courses;
        private Dictionary<string, List<Course>> prerequisites;

        public Scheduler()
        {
            completed = new Dictionary<string, Course>();
            courses = new Dictionary<string, Course>();
            prerequisites = new Dictionary<string, List<Course>>();
        }

        public Scheduler(List<string> courseList)
        {
            completed = new Dictionary<string, Course>();
            courses = new Dictionary<string, Course>();
            prerequisites = new Dictionary<string, List<Course>>();
        }

        public Course GetCompleted(string course)
        {
            if (!completed.ContainsKey(course))
            {
                throw new KeyNotFoundException(course);
            }

            return completed[course];
        }

        public Course GetCourse(string course)
        {
            if (!courses.ContainsKey(course))
            {
                throw new KeyNotFoundException(course);
            }

            return courses[course];
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

        public Course(string departmentCode, int courseCode, string name, string description)
        {
            this.code = departmentCode + " " + courseCode.ToString();
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

        public override string ToString()
        {
            return code + " - " + name;
        }
    }
}