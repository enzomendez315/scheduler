# Course Scheduler
The goal of this program is to help students keep track of the classes they have taken, as well as the ones they still need to take in order to graduate. This program takes into account prerequisites and circular dependencies. This way students will have a clear view of how many semesters they have before graduation, and they will be able to plan their classes better.

## How to use it
The program contains a class called `Scheduler` that can be instantiated by calling the one of the constructors. The default constructor takes no arguments and creates a new scheduler with no courses, while the other constructor takes in a list of courses that the student should take.

There is a nested class called `Course` that contains key information about a particular course, such as the name (Computer Systems), the code (CS 4400), and the description (This course focuses on how a computer's processor works). The constructor for the `Course` class takes in four arguments: departmentCode, courseCode, name, description. The deparment code refers to the first part of the previously described course code, such as CS for computer science. The course code refers to the integer value that follows the department code, such as 4400.

# (WORK IN PROGRESS)
