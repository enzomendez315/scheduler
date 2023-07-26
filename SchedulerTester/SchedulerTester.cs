using SchedulerBuilder;

namespace SchedulerTester
{
    [TestClass]
    public class SchedulerTester
    {
        [TestMethod]
        public void Test_Default_Constructor()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddCourse(cs3505);

            Assert.AreEqual(cs3505, scheduler.GetCourse("CS 3505"));
        }

        [TestMethod]
        public void Test_Argument_Constructor()
        {
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs4150 = new Course("CS", 4150, "Algorithms", "This course teaches DSA in depth.");

            List<Course> list = new List<Course> { cs3505, cs2420, cs4150 };
            Scheduler scheduler = new Scheduler(list);

            Assert.AreEqual(cs3505, scheduler.GetCourse("CS 3505"));
            Assert.AreEqual(cs2420, scheduler.GetCourse("CS 2420"));
            Assert.AreEqual(cs4150, scheduler.GetCourse("CS 4150"));
        }

        [TestMethod]
        public void Test_AddCourse_New()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");

            Assert.IsTrue(scheduler.AddCourse(cs3505));
        }

        [TestMethod]
        public void Test_AddCourse_Existing_Single()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddCourse(cs3505);

            Assert.IsFalse(scheduler.AddCourse(cs3505));
        }

        [TestMethod]
        public void Test_AddCourse_Existing_Multiple()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            Course cs3810 = new Course("CS", 3810, "Computer Organization", "This course teaches how computers' hardware works.");
            Course cs4150 = new Course("CS", 4150, "Algorithms", "This course teaches DSA in depth.");

            Assert.IsTrue(scheduler.AddCourse(cs3505));
            Assert.IsTrue(scheduler.AddCourse(cs4150));
            Assert.IsFalse(scheduler.AddCourse(cs3505));
            Assert.IsTrue(scheduler.AddCourse(cs3810));
            Assert.IsFalse(scheduler.AddCourse(cs3810));
        }

        [TestMethod]
        public void Test_AddCourse_Count()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddCourse(cs3505);

            Assert.AreEqual(1, scheduler.GetCourses().Count);
        }

        [TestMethod]
        public void Test_AddCourse_GetCourse()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddCourse(cs3505);

            Assert.AreEqual(cs3505, scheduler.GetCourse("CS 3505"));
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Test_Exception_GetCourse()
        {
            Scheduler scheduler = new Scheduler();
            scheduler.GetCourse("CS 4150");
        }

        [TestMethod]
        public void Test_AddCompleted()
        {

        }

        [TestMethod]
        public void Test_AddPrerequisite()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddPrerequisite(
                new Course("CS", 3500, "Software Practice I", "This course teaches C#."),
                cs3505);

            string test = scheduler.SeePrerequisites(cs3505);


            // Assert stuff.
        }

        [TestMethod]
        public void Test_AddPrerequisite_Duplicate_Keys()
        {

        }

        [TestMethod]
        public void Test_AddPrerequisite_Duplicate_Values()
        {

        }

        [TestMethod]
        public void Test_AddPrerequisite_Same_Key_Value()
        {

        }

        [TestMethod]
        public void Test_SortCourses()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2100 = new Course("CS", 2100, "Discrete Structures", "This course teaches discrete math.");
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            Course cs3810 = new Course("CS", 3810, "Computer Organization", "This course teaches how computers' hardware works.");
            Course cs4150 = new Course("CS", 4150, "Algorithms", "This course teaches DSA in depth.");
            Course cs4400 = new Course("CS", 4400, "Computer Systems", "This course teaches how computers work in depth.");
            scheduler.AddPrerequisite(cs2420, cs3500);
            scheduler.AddPrerequisite(cs3500, cs3505);
            scheduler.AddPrerequisite(cs2420, cs3810);
            scheduler.AddPrerequisite(cs2100, cs4150);
            scheduler.AddPrerequisite(cs3500, cs4150);
            scheduler.AddPrerequisite(cs3810, cs4400);
            scheduler.SortCourses();            
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Direct_Circular_Dependencies()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            scheduler.AddPrerequisite(cs2420, cs2420);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Indirect_Circular_Dependencies_Small()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            scheduler.AddPrerequisite(cs2420, cs3500);
            scheduler.AddPrerequisite(cs3500, cs2420);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Indirect_Circular_Dependencies_Large()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            Course cs3810 = new Course("CS", 3810, "Computer Organization", "This course teaches how computers' hardware works.");
            Course cs4150 = new Course("CS", 4150, "Algorithms", "This course teaches DSA in depth.");
            Course cs4400 = new Course("CS", 4400, "Computer Systems", "This course teaches how computers work in depth.");
            scheduler.AddPrerequisite(cs2420, cs3500);
            scheduler.AddPrerequisite(cs3500, cs3505);
            scheduler.AddPrerequisite(cs2420, cs3810);
            scheduler.AddPrerequisite(cs3500, cs4150);
            scheduler.AddPrerequisite(cs3810, cs4400);
            scheduler.AddPrerequisite(cs4400, cs2420);
        }

        [TestMethod]
        public void Test_Course_Equals_True()
        {
            Course course1 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course course2 = new Course("CS", 2420, "Introduction to DSA", "This course teaches basic algorithms.");

            Assert.IsTrue(course1.Equals(course2));
        }

        [TestMethod]
        public void Test_Course_Equals_False()
        {
            Course course1 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course course2 = new Course("CS", 2421, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");

            Assert.IsFalse(course1.Equals(course2));
        }

        [TestMethod]
        public void Test_Course_GetHashCode_Equal()
        {
            Course course1 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course course2 = new Course("CS", 2420, "Introduction to DSA", "This course teaches basic algorithms.");

            Assert.AreEqual(course1.GetHashCode(), course2.GetHashCode());
        }

        [TestMethod]
        public void Test_Course_GetHashCode_Unequal()
        {
            Course course1 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course course2 = new Course("CS", 2421, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");

            Assert.AreNotEqual(course1.GetHashCode(), course2.GetHashCode());
        }

        [TestMethod]
        public void Test_Course_ToString()
        {
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");

            Assert.AreEqual("CS 3500", cs3500.ToString());
        }
    }
}