namespace SchedulerBuilder
{
    /// <summary>
    /// This class is used to create a course scheduler.
    /// </summary>
    public class Scheduler
    {
        private Dictionary<string, Course> completed;
        private Dictionary<string, Course> courses;
        private Dictionary<Course, List<Course>> prerequisites;

        /// <summary>
        /// Constructs a Scheduler object.
        /// </summary>
        public Scheduler()
        {
            completed = new Dictionary<string, Course>();
            courses = new Dictionary<string, Course>();
            prerequisites = new Dictionary<Course, List<Course>>();
        }

        /// <summary>
        /// Constructs a scheduler object with a list of required 
        /// courses all students should take.
        /// </summary>
        /// <param name="courseList"></param>
        public Scheduler(List<Course> courseList)
        {
            completed = new Dictionary<string, Course>();
            courses = new Dictionary<string, Course>();
            prerequisites = new Dictionary<Course, List<Course>>();

            for (int i = 0; i < courseList.Count; i++)
            {
                courses[courseList[i].Code] = courseList[i];
            }
        }

        /// <summary>
        /// Returns the Course object based on the argument as key.
        /// Throws an exception if the Course isn't found.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Course GetCompleted(string course)
        {
            if (!completed.ContainsKey(course))
            {
                throw new KeyNotFoundException("This course is not in the completed dictionary.");
            }

            return completed[course];
        }

        /// <summary>
        /// Returns the Course object based on the argument as key.
        /// Throws an exception if the Course isn't found.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Course GetCourse(string course)
        {
            if (!courses.ContainsKey(course))
            {
                throw new KeyNotFoundException("This course is not in the courses dictionary.");
            }

            return courses[course];
        }

        /// <summary>
        /// Adds a course to the completed dictionary.
        /// </summary>
        /// <param name="course"></param>
        public void AddCompleted(Course course)
        {
            if (!completed.ContainsKey(course.Code))
            {
                completed[course.Code] = course;
            }
        }

        /// <summary>
        /// Adds a course to the courses dictionary.
        /// </summary>
        /// <param name="course"></param>
        public void AddCourse(Course course)
        {
            if (!courses.ContainsKey(course.Code))
            {
                courses[course.Code] = course;
            }
        }


        public void AddPrerequisite(Course prerequisite, Course next)
        {
            AddCourse(prerequisite);
            AddCourse(next);

            // Check for duplicates.
            if (prerequisites.ContainsKey(next) && !prerequisites[next].Contains(prerequisite))
            {
                prerequisites[next].Add(prerequisite);
            }

            // Add the prerequisite course as a key.
            if (!prerequisites.ContainsKey(prerequisite))
            {
                prerequisites[prerequisite] = new List<Course>();
            }

            // Add key and value pair if it doesn't exist.
            if (!prerequisites.ContainsKey(next))
            {
                prerequisites[next] = new List<Course> { prerequisite };
            }
        }


        public string SeePrerequisites(Course course)
        {
            if (!prerequisites.ContainsKey(course))
            {
                throw new KeyNotFoundException("This course is not in the prerequisites dictionary.");
            }

            string prereqs = "";

            for (int i = 0; i < prerequisites[course].Count; i++)
            {
                prereqs += course.Code + " has the following prerequisite: " + prerequisites[course][i].Code + "\n";
                //prereqs += prerequisites[course.Code][i].Code + " -> " + course.Code + "\n";
            }
            
            return prereqs;
        }


        public string SortCourses()
        {
            // To store nodes.
            Stack<Course> stack = new Stack<Course>();

            // Mark all the vertices as not visited.
            Dictionary<Course, bool> visited = new Dictionary<Course, bool>(prerequisites.Count);

            // Call Topological Sort on all vertices, one by one.
            Dictionary<Course, List<Course>>.KeyCollection keyCollec = prerequisites.Keys;
            foreach (Course key in keyCollec)
            {
                if (!visited.ContainsKey(key))
                {
                    TopologicalSort(key, visited, stack);
                }
            }

            // Reverse the order of the stack.
            Stack<Course> temp = new Stack<Course>();
            foreach (Course course in stack)
            {
                temp.Push(course);
            }
            stack = temp;

            string test = "";
            int counter = 0;
            foreach (Course course in stack)
            {
                if (counter < stack.Count - 1)
                {
                    test += course.Code + " -> ";
                    counter++;
                }
                else
                {
                    test += course.Code;
                }
            }

            return test;
        }


        private void TopologicalSort(Course key, Dictionary<Course, bool> visited, Stack<Course> stack)
        {
            // Mark current node as visited.
            visited[key] = true;

            // Visit the nodes (aka courses) connected to this one.
            foreach (Course course in prerequisites[key])
            {
                if (!visited.ContainsKey(course))
                {
                    TopologicalSort(course, visited, stack);
                }
            }

            // Push current node to stack.
            stack.Push(key);
        }
    }

    /// <summary>
    /// This class is used to store information about courses.
    /// </summary>
    public class Course
    {
        private string code;
        private string name;
        private string description;

        /// <summary>
        /// Constructs a Course object.
        /// </summary>
        /// <param name="departmentCode"></param>
        /// <param name="courseCode"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public Course(string departmentCode, int courseCode, string name, string description)
        {
            this.code = departmentCode + " " + courseCode.ToString();
            this.name = name;
            this.description = description;
        }

        /// <summary>
        /// Returns the code of the course.
        /// i.e. CS 4150
        /// </summary>
        public string Code
        {
            get { return code; }
        }

        /// <summary>
        /// Returns the name of the course.
        /// i.e. Algorithms
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Returns the description of the course.
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// Returns the string representation of the course.
        /// i.e. CS 4150 - Algorithms
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return code + " - " + name;
        }
    }
}