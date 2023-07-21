using System.ComponentModel.Design;

namespace SchedulerBuilder
{
    /// <summary>
    /// This class is used to create a course scheduler.
    /// </summary>
    public class Scheduler
    {
        private HashSet<Course> completed;
        private Dictionary<string, Course> allCourses;
        private Dictionary<Course, List<Course>> courses;

        /// <summary>
        /// Constructs a Scheduler object.
        /// </summary>
        public Scheduler()
        {
            completed = new HashSet<Course>();
            allCourses = new Dictionary<string, Course>();
            courses = new Dictionary<Course, List<Course>>();
        }

        /// <summary>
        /// Constructs a scheduler object with a list of required 
        /// courses all students should take.
        /// </summary>
        /// <param name="courseList"></param>
        public Scheduler(List<Course> courseList)
        {
            completed = new HashSet<Course>();
            allCourses = new Dictionary<string, Course>();
            courses = new Dictionary<Course, List<Course>>();

            for (int i = 0; i < courseList.Count; i++)
            {
                allCourses[courseList[i].Code] = courseList[i];
            }
        }

            ///// <summary>
            ///// Returns the Course object based on the argument as key.
            ///// Throws an exception if the Course isn't found.
            ///// </summary>
            ///// <param name="course"></param>
            ///// <returns></returns>
            ///// <exception cref="KeyNotFoundException"></exception>
            //public Course GetCompleted(Course course)
            //{
            //    if (!completed.Contains(course))
            //    {
            //        throw new KeyNotFoundException("This course is not in the completed dictionary.");
            //    }

            //    return completed;
            //}

            ///// <summary>
            ///// Returns the Course object based on the argument as key.
            ///// Throws an exception if the Course isn't found.
            ///// </summary>
            ///// <param name="course"></param>
            ///// <returns></returns>
            ///// <exception cref="KeyNotFoundException"></exception>
            //public Course GetCourse(string course)
            //{
            //    if (!allCourses.ContainsKey(course))
            //    {
            //        throw new KeyNotFoundException("This course is not in the courses dictionary.");
            //    }

            //    return allCourses[course];
            //}

        /// <summary>
        /// Adds a course to the completed dictionary.
        /// </summary>
        /// <param name="course"></param>
        public void AddCompleted(Course course)
        {
            if (!completed.Contains(course))
            {
                completed.Add(course);
            }
        }

        /// <summary>
        /// Adds a course to the courses dictionary.
        /// </summary>
        /// <param name="course"></param>
        public void AddCourse(Course course)
        {
            if (!allCourses.ContainsKey(course.Code))
            {
                allCourses[course.Code] = course;
            }
        }


        public void AddPrerequisite(Course prerequisite, Course next)
        {
            AddCourse(prerequisite);
            AddCourse(next);

            // Check for self loops.
            if (prerequisite.Equals(next))
            {
                throw new ArgumentException("Cannot use a course as its own prerequisite.");
            }

            // Check for duplicates.
            if (courses.ContainsKey(next) && !courses[next].Contains(prerequisite))
            {
                courses[next].Add(prerequisite);
                if (hasCycles())
                {
                    RemovePrerequisite(prerequisite, next);
                    throw new ArgumentException("Cannot create a circular dependency.");
                }
            }

            // Add the prerequisite course as a key.
            if (!courses.ContainsKey(prerequisite))
            {
                courses[prerequisite] = new List<Course>();
            }

            // Add key and value pair if it doesn't exist.
            if (!courses.ContainsKey(next))
            {
                courses[next] = new List<Course> {prerequisite};
            }
        }

        public void RemovePrerequisite(Course prerequisite, Course next)
        {
            // Check if next exists as a key.

            // Check if prerequisite exists as a value of next.

            // Remove it.
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