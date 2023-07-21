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
            scheduler.AddCourse(new Course("CS", 3505, "Software Practice II", "This course teaches C++."));

            Assert.AreEqual("CS 3505 - Software Practice II", scheduler.GetCourse("CS 3505").ToString());
        }

        [TestMethod]
        public void Test_Argument_Constructor()
        {
            List<Course> list = new List<Course>
            {
                new Course("CS", 3505, "Software Practice II", "This course teaches C++."),
                new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA."),
                new Course("CS", 4150, "Algorithms", "This course teaches DSA in depth.")
            };
            Scheduler scheduler = new Scheduler(list);

            Assert.AreEqual("CS 3505 - Software Practice II", scheduler.GetCourse("CS 3505").ToString());
            Assert.AreEqual("CS 2420 - Intro to Data Structures and Algorithms", scheduler.GetCourse("CS 2420").ToString());
            Assert.AreEqual("CS 4150 - Algorithms", scheduler.GetCourse("CS 4150").ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Test_Exception_GetCompleted()
        {
            Scheduler scheduler = new Scheduler();
            scheduler.GetCompleted("CS 4150");
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
        public void Test_AddCourse()
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
    }
}