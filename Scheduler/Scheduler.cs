namespace SchedulerBuilder
{
    /// <summary>
    /// This class is used to create a course scheduler.
    /// </summary>
    public class Scheduler
    {
        private Dictionary<string, Course> courseNames;     // Mappings of course codes to course objects.
        private Dictionary<Course, List<Course>> courses;   // All courses and their prerequisites.
        private HashSet<Course> completed;                  // Courses that have been fulfilled.

        /// <summary>
        /// Constructs a Scheduler object.
        /// </summary>
        public Scheduler()
        {
            courseNames = new Dictionary<string, Course>();
            courses = new Dictionary<Course, List<Course>>();
            completed = new HashSet<Course>();
        }

        /// <summary>
        /// Constructs a scheduler object with a list of required 
        /// courses all students should take.
        /// </summary>
        /// <param name="courseList"></param>
        public Scheduler(List<Course> courseList)
        {
            courseNames = new Dictionary<string, Course>();
            courses = new Dictionary<Course, List<Course>>();
            completed = new HashSet<Course>();

            foreach (Course course in courseList)
            {
                AddCourse(course);
            }
        }

        /// <summary>
        /// Adds a course to the courses dictionary and 
        /// binds it to the course code.
        /// 
        /// Returns false if the course already exists.
        /// True otherwise.
        /// </summary>
        /// <param name="course"></param>
        public bool AddCourse(Course course)
        {
            if (courses.ContainsKey(course))
            {
                return false;
            }

            courses[course] = new List<Course>();
            courseNames[course.Code] = course;

            return true;
        }

        /// <summary>
        /// Returns the Course object using the course code as key.
        /// Throws an exception if the Course isn't found.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public Course GetCourse(string course)
        {
            if (!courseNames.ContainsKey(course))
            {
                throw new KeyNotFoundException("This course does not exist yet.");
            }

            return courseNames[course];
        }

        /// <summary>
        /// Adds a course to the completed set.
        /// 
        /// Returns false if the course already exists.
        /// True otherwise.
        /// </summary>
        /// <param name="course"></param>
        public bool AddCompleted(Course course)
        {
            if (completed.Contains(course))
            {
                return false;
            }

            completed.Add(course);

            return true;
        }

        /// <summary>
        /// Returns a list of all completed courses.
        /// </summary>
        /// <returns></returns>
        public List<Course> SeeCompleted()
        {
            return completed.ToList();
        }

        /// <summary>
        /// Adds both courses to the courses dictionary and creates 
        /// an edge between them to represent their relationship.
        /// 
        /// Returns true if the relationship is added or false if 
        /// it already exists.
        /// 
        /// Throws an exception if this relationship causes cycles 
        /// in the graph.
        /// </summary>
        /// <param name="prerequisite"></param>
        /// <param name="next"></param>
        /// <exception cref="ArgumentException"></exception>
        public bool AddPrerequisite(Course prerequisite, Course next)
        {
            AddCourse(prerequisite);
            AddCourse(next);

            // Check for self loops.
            if (prerequisite.Equals(next))
            {
                throw new ArgumentException("Cannot use a course as its own prerequisite.");
            }

            // Check for duplicates.
            if (courses[next].Contains(prerequisite))
            {
                return false;
            }

            courses[next].Add(prerequisite);
            if (hasCycles())
            {
                RemovePrerequisite(prerequisite, next);
                throw new ArgumentException("Cannot create a circular dependency.");
            }

            return true;
        }

        /// <summary>
        /// Removes the edge between the courses. Returns true if the relationship 
        /// is removed or false if it didn't exist in the first place.
        /// 
        /// Throws an exception if the course used as key can't be found.
        /// </summary>
        /// <param name="prerequisite"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public bool RemovePrerequisite(Course prerequisite, Course next)
        {
            if (!courses.ContainsKey(next))
            {
                throw new KeyNotFoundException("Course does not exist as a key.");
            }

            // Course is not a prerequisite of next.
            if (!courses[next].Contains(prerequisite))
            {
                return false;
            }

            courses[next].Remove(prerequisite);
            return true;
        }


        public string SeePrerequisites(Course course)
        {
            if (!courses.ContainsKey(course))
            {
                throw new KeyNotFoundException("This course is not in the prerequisites dictionary.");
            }

            string prereqs = "";

            for (int i = 0; i < courses[course].Count; i++)
            {
                prereqs += course.Code + " has the following prerequisite: " + courses[course][i].Code + "\n";
                //prereqs += prerequisites[course.Code][i].Code + " -> " + course.Code + "\n";
            }
            
            return prereqs;
        }


        public string SortCourses()
        {
            // To store nodes.
            Stack<Course> stack = new Stack<Course>();

            // Mark all the nodes as not visited.
            Dictionary<Course, bool> visited = new Dictionary<Course, bool>(courses.Count);

            // Call Topological Sort on all nodes, one by one.
            Dictionary<Course, List<Course>>.KeyCollection keyCollec = courses.Keys;
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
            foreach (Course course in courses[key])
            {
                if (!visited.ContainsKey(course))
                {
                    TopologicalSort(course, visited, stack);
                }
            }

            // Push current node to stack.
            stack.Push(key);
        }


        public bool hasCycles()
        {
            // To keep track of visited nodes.
            Dictionary<Course, bool> visited = new Dictionary<Course, bool>(courses.Count);
            Stack<Course> stack = new Stack<Course>(courses.Count);

            Dictionary<Course, List<Course>>.KeyCollection keyCollec = courses.Keys;
            foreach (Course key in keyCollec)
            {
                if (DFS(key, visited, stack))
                {
                    return true;
                }
            }

            return false;
        }


        private bool DFS(Course course, Dictionary<Course, bool> visited, Stack<Course> stack)
        {
            if (stack.Contains(course))
            {
                return true;
            }

            if (visited.ContainsKey(course) && visited[course])
            {
                return false;
            }

            visited[course] = true;
            stack.Push(course);

            foreach (Course prereq in courses[course])
            {
                if (DFS(prereq, visited, stack))
                {
                    return true;
                }
            }

            stack.Pop();

            return false;
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
        /// Returns true if two courses have the same 
        /// course code.
        /// i.e. CS 4150
        /// 
        /// False otherwise.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Course))
            {
                return false;
            }

            return ToString().Equals(obj.ToString());
        }

        /// <summary>
        /// Returns the hash code of a course based on 
        /// the course code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return code.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of the course.
        /// i.e. CS 4150
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return code;
        }
    }
}