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

            Assert.AreEqual(1, scheduler.GetAllCourses().Count);
        }

        [TestMethod]
        public void Test_AddCourse_Count_Duplicates()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddCourse(cs2420);
            scheduler.AddCourse(cs3505);
            scheduler.AddCourse(cs3500);
            scheduler.AddCourse(cs3505);
            scheduler.AddCourse(cs3505);

            Assert.AreEqual(3, scheduler.GetAllCourses().Count);
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
        public void Test_AddCourse_Indirectly_AddPrerequisite()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddPrerequisite(cs2420, cs3500);
            scheduler.AddPrerequisite(cs3500, cs3505);

            Assert.AreEqual(3, scheduler.GetAllCourses().Count);
            Assert.AreEqual(cs2420, scheduler.GetCourse("CS 2420"));
            Assert.AreEqual(cs3500, scheduler.GetCourse("CS 3500"));
            Assert.AreEqual(cs3505, scheduler.GetCourse("CS 3505"));
        }

        [TestMethod]
        public void Test_AddCourse_Indirectly_AddCompleted()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddCompleted(cs2420);
            scheduler.AddCompleted(cs3500);
            scheduler.AddCompleted(cs3505);

            Assert.AreEqual(3, scheduler.GetAllCourses().Count);
            Assert.AreEqual(cs2420, scheduler.GetCourse("CS 2420"));
            Assert.AreEqual(cs3500, scheduler.GetCourse("CS 3500"));
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
        public void Test_GetAllCourses_Empty()
        {
            Scheduler scheduler = new Scheduler();

            Assert.AreEqual(0, scheduler.GetAllCourses().Count);
        }

        [TestMethod]
        public void Test_GetAllCourses_Multiple()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3810 = new Course("CS", 3810, "Computer Organization", "This course teaches how computers' hardware works.");
            Course cs4400 = new Course("CS", 4400, "Computer Systems", "This course teaches how computers work in depth.");
            scheduler.AddCourse(cs2420);
            scheduler.AddCourse(cs3810);
            scheduler.AddCourse(cs4400);

            Assert.AreEqual(3, scheduler.GetAllCourses().Count);
            Assert.IsTrue(scheduler.GetAllCourses().Contains(cs2420));
            Assert.IsTrue(scheduler.GetAllCourses().Contains(cs3810));
            Assert.IsTrue(scheduler.GetAllCourses().Contains(cs4400));
        }

        [TestMethod]
        public void Test_AddCompleted_New()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");

            Assert.IsTrue(scheduler.AddCompleted(cs3505));
        }

        [TestMethod]
        public void Test_AddCompleted_Existing_Single()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddCompleted(cs3505);

            Assert.IsFalse(scheduler.AddCompleted(cs3505));
        }

        [TestMethod]
        public void Test_AddCompleted_Existing_Multiple()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            Course cs3810 = new Course("CS", 3810, "Computer Organization", "This course teaches how computers' hardware works.");
            Course cs4150 = new Course("CS", 4150, "Algorithms", "This course teaches DSA in depth.");

            Assert.IsTrue(scheduler.AddCompleted(cs3505));
            Assert.IsTrue(scheduler.AddCompleted(cs4150));
            Assert.IsFalse(scheduler.AddCompleted(cs3505));
            Assert.IsTrue(scheduler.AddCompleted(cs3810));
            Assert.IsFalse(scheduler.AddCompleted(cs3810));
        }

        [TestMethod]
        public void Test_AddCompleted_Count()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddCompleted(cs3505);

            Assert.AreEqual(1, scheduler.GetAllCourses().Count);
        }

        [TestMethod]
        public void Test_GetCompleted_Empty()
        {
            Scheduler scheduler = new Scheduler();

            Assert.AreEqual(0, scheduler.GetCompleted().Count);
        }

        [TestMethod]
        public void Test_GetCompleted_Multiple()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3810 = new Course("CS", 3810, "Computer Organization", "This course teaches how computers' hardware works.");
            Course cs4400 = new Course("CS", 4400, "Computer Systems", "This course teaches how computers work in depth.");
            scheduler.AddCompleted(cs2420);
            scheduler.AddCompleted(cs3810);
            scheduler.AddCompleted(cs4400);

            Assert.AreEqual(3, scheduler.GetCompleted().Count);
            Assert.IsTrue(scheduler.GetCompleted().Contains(cs2420));
            Assert.IsTrue(scheduler.GetCompleted().Contains(cs3810));
            Assert.IsTrue(scheduler.GetCompleted().Contains(cs4400));
        }

        [TestMethod]
        public void Test_AddPrerequisite_New()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");

            Assert.IsTrue(scheduler.AddPrerequisite(cs3500, cs3505));
        }

        [TestMethod]
        public void Test_AddPrerequisite_List()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddPrerequisite(cs3500, cs3505);

            Assert.AreEqual(1, scheduler.GetPrerequisites(cs3505).Count);
            Assert.IsTrue(scheduler.GetPrerequisites(cs3505).Contains(cs3500));
        }

        [TestMethod]
        public void Test_AddPrerequisite_Duplicate_Keys()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3810 = new Course("CS", 3810, "Computer Organization", "This course teaches how computers' hardware works.");
            scheduler.AddPrerequisite(cs2420, cs3500);
            scheduler.AddPrerequisite(cs2420, cs3810);

            Assert.AreEqual(3, scheduler.GetAllCourses().Count);
        }

        [TestMethod]
        public void Test_AddPrerequisite_Duplicate_Values()
        {
            Scheduler scheduler = new Scheduler();
            Course cs2420 = new Course("CS", 2420, "Intro to Data Structures and Algorithms", "This course is an intro to DSA.");
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            scheduler.AddPrerequisite(cs2420, cs3500);

            Assert.IsFalse(scheduler.AddPrerequisite(cs2420, cs3500));
            Assert.AreEqual(2, scheduler.GetAllCourses().Count);
            Assert.AreEqual(1, scheduler.GetPrerequisites(cs3500).Count);
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
        public void Test_RemovePrerequisite_Single()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.AddPrerequisite(cs3500, cs3505);

            Assert.IsTrue(scheduler.RemovePrerequisite(cs3500, cs3505));
            Assert.AreEqual(0, scheduler.GetPrerequisites(cs3505).Count);
            Assert.AreEqual(2, scheduler.GetAllCourses().Count);
        }

        [TestMethod]
        public void Test_RemovePrerequisite_Multiple()
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

            Assert.AreEqual(7, scheduler.GetAllCourses().Count);
            Assert.AreEqual(2, scheduler.GetPrerequisites(cs4150).Count);

            scheduler.RemovePrerequisite(cs3500, cs4150);
            Assert.AreEqual(1, scheduler.GetPrerequisites(cs4150).Count);
            Assert.IsFalse(scheduler.GetPrerequisites(cs4150).Contains(cs3500));

            scheduler.RemovePrerequisite(cs2100, cs4150);
            Assert.AreEqual(0, scheduler.GetPrerequisites(cs4150).Count);
            Assert.AreEqual(7, scheduler.GetAllCourses().Count);
        }

        [TestMethod]
        public void Test_RemovePrerequisite_No_Prerequisite()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Test_RemovePrerequisite_Invalid_Key()
        {
            Scheduler scheduler = new Scheduler();
            Course cs3500 = new Course("CS", 3500, "Software Practice I", "This course teaches C#.");
            Course cs3505 = new Course("CS", 3505, "Software Practice II", "This course teaches C++.");
            scheduler.RemovePrerequisite(cs3500, cs3505);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void Test_GetPrerequisites_Invalid_Key()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Test_GetPrerequisites_Empty()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Test_GetPrerequisites_Multiple()
        {
            throw new NotImplementedException();
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

            throw new NotImplementedException();
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