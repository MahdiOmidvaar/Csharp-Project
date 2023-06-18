using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Xml.Linq;
using System.Net.NetworkInformation;

namespace University_Dashboard
{
     class Program
    {
        static void Main(string[] args)
        {
            #region اکشن (Actions)
            Action PrintDateTimeAndUserStatistic = () =>
            {
                PersianCalendar pc = new PersianCalendar();
                Console.WriteLine($"{pc.GetYear(DateTime.Now)}/{pc.GetMonth(DateTime.Now)}/{pc.GetDayOfMonth(DateTime.Now)}    {pc.GetHour(DateTime.Now)}: {pc.GetMinute(DateTime.Now)} ");
                using (UniversityContext dbPrintDateTime = new UniversityContext("UniDbConStr"))
                {
                    Console.Clear();
                    Console.WriteLine($"\n\nDashboard Statistic\n\tEmployee: {dbPrintDateTime.Employees.Count()}\n\t     Master: {dbPrintDateTime.masters.Count()}\n\t     student: {dbPrintDateTime.Students.Count()}\n\t       Course: {dbPrintDateTime.Courses.Count()}");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------------------");
                }
            };

            Action <string> PrintWarningJustNumber = delegate (string Title)
            {
               PrintDateTimeAndUserStatistic();
                Console.WriteLine("warning!");
                Thread.Sleep(1000);
                Console.WriteLine($"insert only number for {Title}");
                Thread.Sleep(3000);
            };
            Action<string, int> PrintWarningNotFoundId = delegate (string TitleList, int id)
            {
                PrintDateTimeAndUserStatistic();
                Console.WriteLine("warning");
                Thread.Sleep(1000);
                Console.WriteLine($"id {id} not found in {TitleList}");
                Thread.Sleep(3000);
            };
            #endregion

            #region ساخت دیتابیس و رکورد های پیشفرض (Creating Database and Default Records)
            Console.WriteLine("database is loading...");
            using(UniversityContext db = new UniversityContext("UniDbConStr"))
            {
                db.Database.CreateIfNotExists();
                if (!db.Roles.Any())
                {
                    db.Roles.Add(new Role(0, "manager", "admin"));
                    db.Roles.Add(new Role(1, "operator", "employee"));
                    db.Roles.Add(new Role(2, "teacher", "master"));
                    db.Roles.Add(new Role(3, "daneshjo", "student"));

                }
                if (!db.Employees.Any())
                {
                    db.Employees.Add(new Employee("support",6000000,"mahdi","omidvar","09210017031","123456", db.Roles.Find(1)));
                    db.Employees.Add(new Employee("admin", 6000000, "anahita", "ahmadi", "09211131031", "123456", db.Roles.Find(1)));
                }
              
                if (!db.Courses.Any())
                {
                    db.Courses.Add(new Course("Python", 6, (new Master("it", 10000000, "mohsen", "pargoo", "09121026358","123465", db.Roles.Find(2)))));
                    db.Courses.Add(new Course("ASP.Net", 5, (new Master("it", 10000000, "hosein", "golbaba", "09126210010", "123456", db.Roles.Find(2)))));

                }
                if(!db.Students.Any())
                {
                    db.Students.Add(new Student("it", "fateme", "bashiri", "09125479630", "123456", db.Roles.Find(3)));
                    db.Students.Add(new Student("it","sonya","hasani","09120017031","123456", db.Roles.Find(3)));

                    Student student = new Student("it", "mona", "basiri", "09120054111", "123456", db.Roles.Find(3));
                    Master master = new Master("it", 30000000, "hosen", "baqeri", "09125521047", "123456", db.Roles.Find(3));
                    Course course = new Course("English", 2, master);

                    student.Courses.Add(course);
                    db.Students.Add(student);
                }
                db.SaveChanges();
                Console.WriteLine("\n\tRedirecting to log in form");
                Console.WriteLine("\tplease wait...");
                Thread.Sleep(3000);
            }
            #endregion

            #region فرم ورود (Entery Form)
            Login:
            Console.Clear();
            Console.WriteLine("\nLogin page");

            Console.Write("\n\tPhoneNumber: ");
            string username = Console.ReadLine();
            string PatternPhoneNumber = @"^09[0-9]{9}$";
            while(!Regex.IsMatch(username, PatternPhoneNumber))
            {
                Console.WriteLine("\n\tIncorrect PhoneNumber");
                Thread.Sleep(2000);
                PrintDateTimeAndUserStatistic();
                Console.WriteLine("\n\tLogin Page");
                Console.Write("\n\tPhoneNumber: ");
                username = Console.ReadLine();
            }
            Console.Write("\n\tEnter Your Password:  ");
            string password = Console.ReadLine();

            string adminName = "";
             using(UniversityContext dbLogin = new UniversityContext("UniDBConStr"))
            {
               var admin =  dbLogin.Employees.FirstOrDefault(t => t.PhoneNumber == username && t.Password == password);
                if (admin == null)
                {
                    Console.Clear();
                    Console.WriteLine("\n\tLogin Page");
                    Console.WriteLine("\n\tincorrect username or password");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto Login;
                }
                if (admin.IsActive == false)
                {
                    Console.Clear();
                    Console.WriteLine("\n\tYour access is blocked");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto Login;
                }
                else
                {
                    adminName = admin.Name + " " + admin.Family;
                    goto menu;
                }

            }

            #endregion

            #region گزینه های منو (Menu)

            menu:
            PrintDateTimeAndUserStatistic();
            Console.Write("\n\t1. View Emolyees List");
            Console.WriteLine("\t\t\t\t2. View Students List");
            Console.Write("\n\t3. View Masters List");
            Console.WriteLine("\t\t\t\t4. View Courses List");
             
            Console.Write("\n\t5. Add New Employee");
            Console.WriteLine("\t\t\t\t6. Add New Student");
            Console.Write("\n\t7. Add New Master");
            Console.WriteLine("\t\t\t\t8. Add New Course");

            Console.Write("\n\t9. Edit Employee Information");
            Console.WriteLine("\t\t\t10. Edit Student Information");
            Console.Write("\n\t11. Edit Master Information");
            Console.WriteLine("\t\t\t12. Edit Course Information");

            Console.Write("\n\t13. Remove Employee");
            Console.WriteLine("\t\t\t\t14. Remove Student");
            Console.Write("\n\t15. Remove Master");
            Console.WriteLine("\t\t\t\t16. Remove Course");
            Console.Write("\n\t17. Change Role Users");
            Console.WriteLine("\t\t\t\t18. Exit the Dashboard");

            Console.Write("\n Enter Your Request Number: ");
            int request;
            while (!int.TryParse(Console.ReadLine(),out request))
            {
                Console.Clear();
                PrintWarningJustNumber("Request number");
                Console.Clear();
                PrintDateTimeAndUserStatistic();
                goto menu;
            }
            #endregion

            switch (request)
            {
                #region نمایش لیست کارمندان (show the Employees list)

                case 1:

                    PrintDateTimeAndUserStatistic();
                    using (UniversityContext dbViewEmployeeList = new UniversityContext("UniDbConStr"))
                    {
                        Console.WriteLine("\nEmployee: \n");
                        List<Employee> viewEmployeeList = dbViewEmployeeList.Employees.ToList();
                        foreach (Employee employee in viewEmployeeList)
                        {
                            Console.WriteLine($"\tid: {employee.UserId}\tName: {employee.Name}\nFamily: {employee.Family}\t Department: {employee.Department}\t" +
                                $"PhoneNumber: {employee.PhoneNumber}\tSalary: {employee.Salary}\t\tactive: {employee.IsActive}");
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
                        }
                    }

                    Console.WriteLine("Press Any key to return to menu");
                    Console.ReadKey();
                    goto menu;
                #endregion

                #region نمایش لیست دانشجویان (show the students list)

                case 2:
                    PrintDateTimeAndUserStatistic();

                    using(UniversityContext dbViewStudentList= new UniversityContext("UniDBConStr"))
                    {
                        Console.WriteLine("\nStudent: \n");
                        List<Student> viewStudentList = dbViewStudentList.Students.ToList();
                        foreach  (Student student in viewStudentList)
                        {
                            Console.WriteLine($"id: {student.UserId}\tName: {student.Name}\tFamily: {student.Family}\n" +
                                $"PhoneNumber: {student.PhoneNumber}\tDegree: {student.Degree}\tActivity: {student.IsActive}");
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");

                            if (student.Courses.Any())
                            {
                                Console.WriteLine("\ncourse\n");
                                foreach (Course Course in student.Courses)
                                {
                                    Console.WriteLine($"\tid: {Course.CourseId}\tname: {Course.CourseName}\tunit: {Course.CourseUnit}\t\tactive: {Course.IsActive}");

                                }
                            }

                        }

                    }
                    Console.WriteLine("Press any key to return to menu");
                    Console.ReadKey();
                    goto menu;
                #endregion

                #region نمایش لیست استادان (show the master lists)

                case 3:
                    PrintDateTimeAndUserStatistic();
                    using (UniversityContext dbViewMasterList = new UniversityContext("UniDBConStr"))
                    {
                        Console.WriteLine("\nMaster: \n");
                        List<Master> ViewMasterList =dbViewMasterList.masters.ToList();
                        foreach (Master master in ViewMasterList)
                        {
                            Console.WriteLine($"id: {master.UserId}\tName: {master.Name}\tFamily: {master.Family}\t" +
                                $"Degree: {master.Degree}\tSalary: {master.Salary}\tPhoneNumber: {master.PhoneNumber}\tActivity: {master.IsActive}");

                          List<Course> CourseMaster =  dbViewMasterList.Courses.Where(t => t.Master.UserId==master.UserId).ToList();
                            for (int i = 0; i < CourseMaster.Count; i++)
                            {
                                if (i == 0)
                                {
                                    Console.WriteLine("\n\tCourse: "); //inja mige tooye dore aval course: ro chap kn dge nkn
                                }
                                Console.WriteLine($"\tid: {CourseMaster[i].CourseId}\tname: {CourseMaster[i].CourseName}\tUnit: {CourseMaster[i].CourseUnit}\t\tActivity: {CourseMaster[i].IsActive}");
                                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                            }
                        }

                    }
                    Console.WriteLine("Press any key to return to menu");
                    Console.ReadKey();
                    goto menu;
                #endregion

                #region نمایش لیست دروس (show the course list)

                case 4:
                    PrintDateTimeAndUserStatistic();
                    UniversityContext dbViewCourseList = new UniversityContext("UniDBConStr");
                    using (dbViewCourseList)
                    {
                       List<Course> ViewCourseList = dbViewCourseList.Courses.ToList();
                        foreach (Course course in ViewCourseList) 
                        {
                            Console.WriteLine($"Id: {course.CourseId}\tName: {course.CourseName}\tUnit: {course.CourseUnit}");
                            if (course.Master != null)
                            {
                                Console.WriteLine("\tMaster: ");
                                Console.WriteLine($"Id: {course.Master.UserId}\tName: {course.Master.Name}\tFamily: {course.Master.Family}\tActivity: {course.Master.IsActive}");
                                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");

                            }
                        }

                    }
                    Console.WriteLine("Press any key to return to menu");
                    Console.ReadKey();
                    goto menu;

                #endregion

                #region فرم ثبت نام کارمند جدید (registering a new employee)


                case 5:
                    PrintDateTimeAndUserStatistic();
                    UniversityContext dbAddNewEmployee = new UniversityContext("UniDBConStr");
                    using (dbAddNewEmployee)
                    {
                        Console.WriteLine("\nNew employee registration form");
                        Console.Write("\n\tDepartment Name: ");
                        string eAdepartment = Console.ReadLine();
                        Console.Write("\tMonthly salary : ");
                        float eASalary;
                        while (!float.TryParse(Console.ReadLine(), out eASalary))
                        {
                            PrintWarningJustNumber("Monthly salary");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            Console.WriteLine("\nNew employee registration form");
                            Console.Write($"\n\tDepartment Name: {eAdepartment}");
                            Console.Write("\n\tMonthly salary : ");
                        }
                        Console.Write("\tFirst Name     : ");
                        string eAfirstName = Console.ReadLine();
                        Console.Write("\tLast Name      : ");
                        string eAlastName = Console.ReadLine();
                        Console.Write("\tPhonenumber    : ");
                        string eAphonenumber = Console.ReadLine();
                        while (!Regex.IsMatch(eAphonenumber, PatternPhoneNumber))
                        {
                            Console.WriteLine("\n\tIncorrect phonenumber !");
                            Thread.Sleep(2000);
                            Console.Write("\tPhonenumber    : ");
                            eAphonenumber = Console.ReadLine();
                        }
                        Console.Write("\tPassword       : ");
                        string eApassword = Console.ReadLine();
                        dbAddNewEmployee.Employees.Add(new Employee($"{eAdepartment}", eASalary, $"{eAfirstName}", $"{eAlastName}", $"{eAphonenumber}", $"{eApassword}", dbAddNewEmployee.Roles.Find(1)));
                        PrintDateTimeAndUserStatistic();
                        Console.WriteLine($"\n\t{eAfirstName} {eAlastName} was registered\n");
                        dbAddNewEmployee.SaveChanges();
                        Console.Write("\nDo you want to return to menu?");
                        Console.ReadKey();
                    }

                    goto menu;
                #endregion

                #region فرم ثبت نام دانشجو جدید (registering a new student)

                case 6:
                    PrintDateTimeAndUserStatistic();
                    UniversityContext dbAddNewStudent = new UniversityContext("UniDBConStr");
                    using (dbAddNewStudent)
                    {
                        Console.WriteLine("\tStudent Registration Form");
                        Console.Write("\tEnter your degree      : ");
                        string eSdegree = Console.ReadLine();
                        Console.Write("\tEnter Your Name        : ");
                        string eSfirstname = Console.ReadLine();
                        Console.Write("\tEnter your Last Name   : ");
                        string eslastname = Console.ReadLine();
                        Console.Write("\tenter your PhoneNumber : ");
                        string eSphonenumber = Console.ReadLine();

                        while (!Regex.IsMatch(eSphonenumber,PatternPhoneNumber))
                        {
                            Console.WriteLine("\tincorrect Phonenumber");
                            Thread.Sleep(1000);
                            Console.Write("\tPhonenumber          : ");
                            eSphonenumber = Console.ReadLine();
                        }
                        Console.Write("\tenter your password      : ");
                        string Spassword = Console.ReadLine();

                        dbAddNewStudent.Students.Add(new Student($"{eSdegree}", $"{eSfirstname}", $"{eslastname}", $"{eSphonenumber}", $"{Spassword}",dbAddNewStudent.Roles.Find(3)));
                        PrintDateTimeAndUserStatistic();
                        Console.WriteLine($"\t{eSfirstname} {eslastname} registered successfuly");
                        dbAddNewStudent.SaveChanges();
                        Thread.Sleep(1000);
                        Console.WriteLine("press any key to go to menu");
                        Console.ReadKey();
                        

                    }
                    goto menu;
                #endregion

                #region فرم ثبت نام استاد جدید (registering a new master)

                case 7:
                    PrintDateTimeAndUserStatistic();
                    UniversityContext dbAddNewMaster = new UniversityContext("UniDBConStr");
                    using (dbAddNewMaster)
                    {
                        Console.WriteLine("Master Registration Form");
                        Console.Write("\tenter degree: ");
                        string mAdegree = Console.ReadLine();
                        Console.Write("\tenter salary: ");
                        float mAsalary;
                        while (!float.TryParse(Console.ReadLine(), out mAsalary ))
                        {
                            PrintWarningJustNumber("for salary");
                            Thread.Sleep(1000);
                            Console.Write("\tenter salary:");
                             
                        }
                        Console.Write("\tenter name       : ");
                        string mAname = Console.ReadLine();
                        Console.Write("\tenter family     : ");
                        string mAlastname = Console.ReadLine();
                        Console.Write("\tenter phonenumber: ");
                        string mAphonenumber = Console.ReadLine();
                        while (!Regex.IsMatch(mAphonenumber,PatternPhoneNumber))
                        {
                            Console.WriteLine("Incorrect phonenumber");
                            Thread.Sleep(1000);
                            Console.Write("\tphonenumber: ");
                            mAphonenumber = Console.ReadLine();

                        }
                        Console.Write("\tEnter password  : ");
                        string mApassword = Console.ReadLine();
                        dbAddNewMaster.masters.Add(new Master($"{mAdegree}",mAsalary,$"{mAname}",$"{mAlastname}",$"{mAphonenumber}",$"{mApassword}",dbAddNewMaster.Roles.Find(2)));
                        dbAddNewMaster.SaveChanges();
                        PrintDateTimeAndUserStatistic();
                        Console.WriteLine($"\t{mAname} {mAlastname} registered successfuly");
                        Console.WriteLine("press any key to go to menu");
                        Console.ReadKey();

                    }
                    goto menu;
                #endregion

                #region فرم ثبت نام درس جدید (registering a new course)

                case 8:
                    PrintDateTimeAndUserStatistic();
                    UniversityContext dbAddNewCourse = new UniversityContext("UniDBConStr");
                    using (dbAddNewCourse)
                    {
                        Console.WriteLine("New Course Registeration Course");
                        Console.Write("\tEnter Name: ");
                        string aCname = Console.ReadLine();
                        Console.Write("\tEnter unit: ");
                        int aCunit;
                        while (!int.TryParse(Console.ReadLine(), out aCunit ))
                        {
                            PrintWarningJustNumber("for unit");
                            Thread.Sleep(1000);
                            Console.Write("\tEnter unit: ");

                        }
                        AnswerAddNewCourse:
                        PrintDateTimeAndUserStatistic();
                        Console.WriteLine($"does {aCname} have a new master: ");
                        Console.WriteLine("\n\t1.Yes\n\t2.No");
                        Console.Write("your answer is: ");
                        int AddMasterToCourse;
                        while (!int.TryParse(Console.ReadLine(), out AddMasterToCourse))
                        {
                            PrintWarningJustNumber("select item of menu");
                            Console.Clear();
                            Console.WriteLine($"does {aCname} have a new master: ");
                            Console.WriteLine("\n\t1.Yes\n\t2.No");
                            Console.Write("your answer is: ");
                        }
                        switch (AddMasterToCourse)
                        {
                            case 1:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\nnew master registeration form for {aCname}");
                                Console.WriteLine("\n\n\tDegree Name: ");
                                string mAdegree = Console.ReadLine();
                                Console.Write("\tMonthly salary     :");
                                float mAsalary;
                                while (!float.TryParse(Console.ReadLine(),out mAsalary))
                                {
                                    PrintDateTimeAndUserStatistic();
                                    PrintWarningJustNumber("Monthly Salary");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                    Console.WriteLine($"\nNew master registration form for course: {aCname}");
                                    Console.WriteLine($"\n\n\tdegree name: {mAdegree}");
                                    Console.WriteLine("monthly salary: ");
                                }
                                Console.Write("enter firstname  : ");
                                string mAfirstname = Console.ReadLine();
                                Console.Write("enter last name  : ");
                                string mAlastname = Console.ReadLine();
                                Console.Write("enter phonenumber: ");
                                string mAphonenumber = Console.ReadLine();
                                while (!Regex.IsMatch(mAphonenumber,PatternPhoneNumber))
                                {
                                    Console.Write("Incorrect phonenumber");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                    Console.Write($"\n\n\tDegree name : {mAdegree}");
                                    Console.Write($"\n\tMonthly salary: {mAsalary}");
                                    Console.Write($"\n\tFirstname     : {mAfirstname}");
                                    Console.Write($"\n\tLastname      : {mAlastname}");
                                    Console.Write($"\n\tPhonenumber   : ");
                                    mAphonenumber= Console.ReadLine();

                                }
                                Console.Write("enter password:");
                                string mApassword= Console.ReadLine();
                                Master masterNew = new Master($"{mAdegree}", mAsalary, $"{mAfirstname}", $"{mAlastname}", $"{mAphonenumber}", $"{mApassword}", dbAddNewCourse.Roles.Find(2));
                                dbAddNewCourse.Courses.Add(new Course($"{aCname}", aCunit, masterNew));
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"{aCname} with master {mAfirstname}{mAlastname} registered successfuly");
                                goto finishAddNewCourse;

                            case 2:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"please select master for course: {aCname}\n");
                                List<Master> masterlist =dbAddNewCourse.masters.ToList();
                                foreach (Master master in masterlist)
                                {
                                    Console.WriteLine($"\tid: {master.UserId}\tname: {master.Name}\tfamily: {master.Family}\tactive: {master.IsActive}\n" +
                                                                                        $"\tphone:  {master.PhoneNumber}\tsalary: {master.Salary}\t\tdegree: {master.Degree}");
                                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                                }
                                Console.Write("enter master id: ");
                                int answerSelectNewCourse;
                                while (!int.TryParse(Console.ReadLine(),out answerSelectNewCourse))
                                {
                                    PrintWarningJustNumber("master id");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    foreach (Master master in masterlist)
                                    {
                                        Console.WriteLine($"\tid: {master.UserId}\tname: {master.Name}\tfamily: {master.Family}\tactive: {master.IsActive}\n" +
                                                                                      $"\tphone:  {master.PhoneNumber}\tsalary: {master.Salary}\t\tdegree: {master.Degree}");
                                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                                    }
                                    Console.Write("\nMaster id is: ");
                                }
                               var selectMaster  = dbAddNewCourse.masters.Find(answerSelectNewCourse);
                               if (selectMaster != null)
                                {
                                    dbAddNewCourse.Courses.Add(new Course($"{aCname}", aCunit, selectMaster));
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine($"\n\t {aCname} was registered\n");
                                }
                               else
                                {
                                    PrintDateTimeAndUserStatistic();
                                    PrintWarningNotFoundId("Master List", answerSelectNewCourse);
                                }
                                goto finishAddNewCourse;

                            default:
                                goto finishAddNewCourse;

                        }
                        finishAddNewCourse:
                        dbAddNewCourse.SaveChanges();

                        Console.WriteLine("press any key to go to menu");
                        Console.ReadKey();
                        goto menu;


                    }

                #endregion

                #region فرم ویرایش اطلاعات کارمندان (editting the employee form)

                case 9:
                    employeeListForEdit:
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine("\nPlease select employee for edit\n");
                    UniversityContext dbEditEmployee = new UniversityContext("UniDBConStr");
                    using (dbEditEmployee)
                    {
                        List<Employee> viewEmployeeList = dbEditEmployee.Employees.ToList();
                        foreach (Employee employee in viewEmployeeList)
                        {
                            Console.WriteLine($"\tid: {employee.UserId}\tname: {employee.Name}\tfamily: {employee.Family}\tdepartment: {employee.Department}\n" +
                                              $"\tphone:  {employee.PhoneNumber}\tsalary: {employee.Salary}\t\tactive: {employee.IsActive}");
                            Console.WriteLine("\t-------------------------------------------------------------------");
                        }
                        Console.Write("\nEmployee id is: ");
                        int answerSelectEditEmployee;
                        while (!int.TryParse(Console.ReadLine(), out answerSelectEditEmployee))
                        {
                            PrintWarningJustNumber("Employee id");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            foreach (Employee employee in viewEmployeeList)
                            {
                                Console.WriteLine($"\tid: {employee.UserId}\tname: {employee.Name}\tfamily: {employee.Family}\tdepartment: {employee.Department}\n" +
                                                  $"\tphone:  {employee.PhoneNumber}\tsalary: {employee.Salary}\t\tactive: {employee.IsActive}");
                                Console.WriteLine("\t-------------------------------------------------------------------");
                            }
                            Console.Write("\nEmployee id is: ");
                        }
                        PrintDateTimeAndUserStatistic();
                        var selectEmployeeForEdit = dbEditEmployee.Employees.Find(answerSelectEditEmployee);
                        if (selectEmployeeForEdit != null)
                        {
                            goto editEmployee;
                        }
                        else
                        {
                            PrintDateTimeAndUserStatistic();
                            PrintWarningNotFoundId("Employee List", answerSelectEditEmployee);
                            goto finishEditEmployee;
                        }
                        editEmployee:
                        Console.WriteLine($"\nChose property for edit employee: {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}");
                        Console.WriteLine("\n\t\t1. Department" +
                                          "\n\t\t2. Salary" +
                                          "\n\t\t3. Name" +
                                          "\n\t\t4. Family" +
                                          "\n\t\t5. Phonenumber" +
                                          "\n\t\t6. Password" +
                                          "\n\t\t7. Access");
                        Console.Write("\nYour answer number: ");
                        int answerEditEmployee;
                        while (!int.TryParse(Console.ReadLine(), out answerEditEmployee))
                        {
                            PrintWarningJustNumber("Select item of menu");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            Console.WriteLine($"\nChose property for edit employee: {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}");
                            Console.WriteLine("\n\t\t1. Department" +
                                              "\n\t\t2. Salary" +
                                              "\n\t\t3. Name" +
                                              "\n\t\t4. Family" +
                                              "\n\t\t5. Phonenumber" +
                                              "\n\t\t6. Password" +
                                              "\n\t\t7. Access");
                            Console.Write("\nYour answer number: ");
                        }
                        PrintDateTimeAndUserStatistic();

                        switch (answerEditEmployee)
                        {
                            case 1:
                                Console.WriteLine($"\n\tEdit form for employee: {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}");
                                Console.WriteLine($"\n\tDepartment: {selectEmployeeForEdit.Department}");
                                Console.Write("\n\tNew department: ");
                                string newDepartmentForEditEmployee = Console.ReadLine();
                                selectEmployeeForEdit.Department = newDepartmentForEditEmployee;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tThe department was changed to {newDepartmentForEditEmployee} for {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}\n");
                                goto finishEditEmployee;

                            case 2:
                                Console.WriteLine($"\n\tEdit form for employee: {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}");
                                Console.WriteLine($"\n\tSalary: {selectEmployeeForEdit.Salary}");
                                Console.Write("\n\tNew Salary: ");
                                float newSalaryForEditEmployee;
                                while (!float.TryParse(Console.ReadLine(), out newSalaryForEditEmployee))
                                {
                                    PrintWarningJustNumber("Monthly salary");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine($"\n\tEdit form for employee: {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}");
                                    Console.WriteLine($"\n\tSalary: {selectEmployeeForEdit.Salary}");
                                    Console.Write("\n\tNew Salary: ");
                                }
                                selectEmployeeForEdit.Salary = newSalaryForEditEmployee;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tThe Salary was changed to {newSalaryForEditEmployee} for {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}\n");
                                goto finishEditEmployee;

                            case 3:
                                Console.WriteLine($"\n\tEdit form for employee: {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}");
                                Console.WriteLine($"\n\tName: {selectEmployeeForEdit.Name}");
                                Console.Write("\n\tNew name: ");
                                string newNameForEditEmployee = Console.ReadLine();
                                selectEmployeeForEdit.Name = newNameForEditEmployee;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tThe Name was changed to {newNameForEditEmployee} for {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}\n");
                                goto finishEditEmployee;

                            case 4:
                                Console.WriteLine($"\n\tEdit form for employee: {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}");
                                Console.WriteLine($"\n\tFamily: {selectEmployeeForEdit.Family}");
                                Console.Write("\n\tNew family: ");
                                string newFamilyForEditEmployee = Console.ReadLine();
                                selectEmployeeForEdit.Family = newFamilyForEditEmployee;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tThe Family was changed to {newFamilyForEditEmployee} for {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}\n");
                                goto finishEditEmployee;

                            case 5:
                                Console.WriteLine($"\n\tEdit form for employee: {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}");
                                Console.WriteLine($"\n\tPhonenumber: {selectEmployeeForEdit.PhoneNumber}");
                                Console.Write("\n\tNew phonenumber: ");
                                string newPhonenumberForEditEmployee = Console.ReadLine();
                                while (!Regex.IsMatch(newPhonenumberForEditEmployee, PatternPhoneNumber))
                                {
                                    Console.WriteLine("\n\tIncorrect phonenumber !");
                                    Thread.Sleep(2000);
                                    Console.Write("\tPhonenumber    : ");
                                    newPhonenumberForEditEmployee = Console.ReadLine();
                                }
                                selectEmployeeForEdit.PhoneNumber = newPhonenumberForEditEmployee;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tThe phonenumber was changed to {newPhonenumberForEditEmployee} for {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}\n");
                                goto finishEditEmployee;

                            case 6:
                                Console.WriteLine($"\n\tEdit form for employee: {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}");
                                Console.WriteLine($"\n\tPassword: {selectEmployeeForEdit.Password}");
                                Console.Write("\n\tNew password: ");
                                string newPasswordForEditEmployee = Console.ReadLine();
                                selectEmployeeForEdit.Password = newPasswordForEditEmployee;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tThe password was changed to {newPasswordForEditEmployee} for {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}\n");
                                goto finishEditEmployee;

                            case 7:
                                if (selectEmployeeForEdit.IsActive == true)
                                {
                                    selectEmployeeForEdit.IsActive = false;
                                }
                                else
                                {
                                    selectEmployeeForEdit.IsActive = true;
                                }
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tThe access was changed to {selectEmployeeForEdit.IsActive} for {selectEmployeeForEdit.Name} {selectEmployeeForEdit.Family}\n");
                                goto finishEditEmployee;

                            default:
                                goto employeeListForEdit;
                        }
                        finishEditEmployee:
                        dbEditEmployee.SaveChanges();
                        Console.Write("\nDo you want to return to menu?");
                        Console.ReadKey();
                    }
                    goto menu;
                #endregion

                #region قرم ویرایش اطلاعات دانشحویان (editting the students information)

                case 10:
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine("\nStudent Edit Form\n");
                    UniversityContext dbStudentEditForm = new UniversityContext("UniDbConStr");
                    using (dbStudentEditForm)
                    {
                         List<Student> StudentList = dbStudentEditForm.Students.ToList();
                        foreach (Student student in StudentList)
                        {
                            Console.WriteLine($"id: {student.UserId}\tName: {student.Name}\tFamily: {student.Family}\tDegree: {student.Degree}\tPhoneNumber: {student.PhoneNumber}\tActivity: {student.IsActive}");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------");

                        }
                        Console.Write("\tEnter the Student Id for Editting: ");
                        int IdForEditting;
                        while (!int.TryParse(Console.ReadLine(), out IdForEditting))
                        {
                            PrintWarningJustNumber("student id");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            Console.WriteLine("\nplease select student for edit\n");
                            foreach (Student student in StudentList)
                            {
                                Console.WriteLine($"id: {student.UserId}\tName: {student.Name}\tFamily: {student.Family}\tDegree: {student.Degree}\tPhoneNumber: {student.PhoneNumber}\tActivity: {student.IsActive}");
                                Console.WriteLine("------------------------------------------------------------------------------------------------------------");

                            }
                            Console.Write("\tEnter the Student Id for Editting: ");
                        }
                       Student selectStudentForEdit = dbStudentEditForm.Students.Find(IdForEditting);
                        if (selectStudentForEdit != null)
                        {
                            goto editStudent;
                        }
                        else
                        {
                            PrintDateTimeAndUserStatistic();
                            PrintWarningNotFoundId("Student List", IdForEditting);
                            goto finishEditstudent;
                        }
                        editStudent:
                        Console.WriteLine($"\n\tedit the property of student {selectStudentForEdit.Name} {selectStudentForEdit.Family}\n");
                        Console.WriteLine("\n\t\t1. Degree" +
                                         "\n\t\t2. Code" +
                                         "\n\t\t3. Name" +
                                         "\n\t\t4. Family" +
                                         "\n\t\t5. Phonenumber" +
                                         "\n\t\t6. Password" +
                                         "\n\t\t7. Access" +
                                         "\n\t\t8. Add course");
                        Console.Write("\tselect the item you want to edit: ");
                        int itemselectedforedit;
                        while (!int.TryParse(Console.ReadLine(), out itemselectedforedit))
                        {
                            PrintWarningJustNumber("\tselect number");
                            Console.Clear();
                            Console.WriteLine("\n\t\t1. Degree" +
                                        "\n\t\t2. Code" +
                                        "\n\t\t3. Name" +
                                        "\n\t\t4. Family" +
                                        "\n\t\t5. Phonenumber" +
                                        "\n\t\t6. Password" +
                                        "\n\t\t7. Access" +
                                        "\n\t\t8. Add course");
                            Console.WriteLine("\tselect the item you want to edit: ");

                        }
                        PrintDateTimeAndUserStatistic();

                        switch (itemselectedforedit)
                        {
                            case 1:
                                Console.WriteLine("\n\tDegree editting form");
                                Console.WriteLine($"\n\t{selectStudentForEdit.Degree} Degree");
                                Console.Write("\n\tNew degree: ");
                                string newEditedDegree = Console.ReadLine();
                                selectStudentForEdit.Degree= newEditedDegree;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"the degree changed to {newEditedDegree} for {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                goto finishEditstudent;
                            case 2:
                                Console.WriteLine($"\n\tcode editting form of {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                Console.WriteLine($"\n\tStudent current code: {selectStudentForEdit.StudentCode}");
                                Console.Write("\n\tenter new code: ");
                                int NewEdeitedCode;
                                while (!int.TryParse(Console.ReadLine(), out NewEdeitedCode))
                                {
                                    PrintWarningJustNumber("for code");
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine($"\n\t{selectStudentForEdit.Name} {selectStudentForEdit.Family} current code: {selectStudentForEdit.StudentCode}");
                                    Console.Write("\tenter new code: ");

                                }
                                selectStudentForEdit.StudentCode = NewEdeitedCode;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"the code has changed to {NewEdeitedCode} for {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                goto finishEditstudent;
                            case 3:
                                Console.WriteLine($"Name editting form of {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                Console.WriteLine($"student current name: {selectStudentForEdit.Name}");
                                Console.Write("enter the new name: ");
                                string NewEditedStudentName = Console.ReadLine();
                                selectStudentForEdit.Name = NewEditedStudentName;
                                Console.WriteLine($"\tName has changed to {NewEditedStudentName} for {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                Thread.Sleep(2000);
                                goto finishEditstudent;
                            case 4:
                                Console.WriteLine($"\n\tLastname editting form of {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                Console.WriteLine($"\n\tcurrent student Lastname: {selectStudentForEdit.Family}");
                                Console.Write("\n\t enter the new Lastname: ");
                                string newEditedLastname= Console.ReadLine();
                                selectStudentForEdit.Family=newEditedLastname;
                                Console.WriteLine($"\n\tlastname has changed to {newEditedLastname}");
                                goto finishEditstudent;
                            case 5:
                                Console.WriteLine($"\n\tphoneNumber Editting form of {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                Console.WriteLine($"\n\tcurrent phonenumber is: {selectStudentForEdit.PhoneNumber}");
                                Console.Write("\n\tenter the new phonenumber: ");
                                string newEditedPhoneNumber = Console.ReadLine();
                                while (!Regex.IsMatch(newEditedPhoneNumber,PatternPhoneNumber))
                                {
                                    Console.WriteLine("incorrect phonenumber");
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine($"\n\tcurrent phonenumber is: {selectStudentForEdit.PhoneNumber}");
                                    Console.Write("\n\tenter the new phonenumber: ");
                                    newEditedPhoneNumber = Console.ReadLine();

                                }
                                selectStudentForEdit.PhoneNumber= newEditedPhoneNumber;
                                Console.WriteLine($"phonenumber has changed to {newEditedPhoneNumber}");
                                goto finishEditstudent;
                            case 6:
                                Console.WriteLine($"\n\tpassword editting form of {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                Console.WriteLine($"\n\tcurrent password is: {selectStudentForEdit.Password}");
                                Console.Write($"\n\tenter the new password: ");
                                string newEditedPassword = Console.ReadLine();
                                selectStudentForEdit.Password = newEditedPassword;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tpassword has changed to {newEditedPassword} for {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                goto finishEditstudent;
                            case 7:
                                Console.WriteLine($"activity statue of {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                Console.WriteLine($"\n\tcurrent activity statue: {selectStudentForEdit.IsActive}");
                                if (selectStudentForEdit.IsActive == true)
                                {
                                    selectStudentForEdit.IsActive = false;
                                }
                                else
                                {
                                    selectStudentForEdit.IsActive = true;
                                }
                                Console.WriteLine("\n\t activity statue has changed ");
                                goto finishEditstudent;
                            case 8:
                                PrintDateTimeAndUserStatistic();
                                AddCourseForStudent:
                                Console.WriteLine($"\n\tadd course to the {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                Console.WriteLine($"\n\tAre you adding a new Course?");
                                Console.WriteLine("\n\t\t1. Select old course" +
                                                  "\n\t\t2. Create new course");
                                Console.Write("\nYour answer number: ");
                                int CourseOperation;
                                while (!int.TryParse(Console.ReadLine(), out CourseOperation))
                                {
                                    PrintWarningJustNumber("Select item of menu");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine($"\n\tEdit form for student: {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                    Console.WriteLine($"\n\tAre you adding a new Course?");
                                    Console.WriteLine("\n\t\t1. Select old course" +
                                                      "\n\t\t2. Create new course");
                                    Console.Write("\nYour answer number: ");
                                }
                                PrintDateTimeAndUserStatistic();
                                switch (CourseOperation)
                                {
                                    case 1:
                                        Console.WriteLine($"\nPlease select a course for add to Student :  {selectStudentForEdit.Name} {selectStudentForEdit.Family}\n");
                                        List<Course> CourseList = dbStudentEditForm.Courses.ToList();
                                        foreach (Course course in CourseList)
                                        {
                                            Console.WriteLine($"\n\tid: {course.CourseId}\tName: {course.CourseName}\tCourse unit: {course.CourseUnit}\tRegister Date: {course.RegisterDate}");
                                            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
                                        }
                                        Console.WriteLine("\n\tselect the course id: ");
                                        int courseIdSelected;
                                        while (!int.TryParse(Console.ReadLine(), out courseIdSelected))
                                        {
                                            PrintWarningJustNumber("course id");
                                            Console.Clear();
                                            PrintDateTimeAndUserStatistic();
                                            foreach (Course course in CourseList)
                                            {
                                                Console.WriteLine($"\n\tid: {course.CourseId}\tName: {course.CourseName}\tCourse unit: {course.CourseUnit}\tRegister Date: {course.RegisterDate}");
                                                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
                                            }
                                            Console.WriteLine("\n\tselect the course id: ");
                                        }
                                        Course selectExsitedCourse  =  dbStudentEditForm.Courses.Find(courseIdSelected);
                                        selectStudentForEdit.Courses.Add(selectExsitedCourse);
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine($"\n\t{selectExsitedCourse.CourseName} course added to student: {selectStudentForEdit.Name} {selectStudentForEdit.Family}\n");
                                        goto finishEditstudent;
                                    case 2:
                                        Console.WriteLine($"\n\tCreate new course form for {selectStudentForEdit.Name} {selectStudentForEdit.Family}");
                                        Console.WriteLine("\n\tenter new course name: ");
                                        string newCourseName = Console.ReadLine();
                                        Console.WriteLine("\n\tenter the course unit: ");
                                        int newCourseUnit;
                                        while (!int.TryParse(Console.ReadLine(), out newCourseUnit))
                                        {
                                            PrintWarningJustNumber(" course unit");
                                            Thread.Sleep(1000);
                                            PrintDateTimeAndUserStatistic();
                                            Console.WriteLine("\n\tenter the course unit: ");
                                        }

                                        addNewCourseForStudent:

                                        Console.WriteLine("\n\tDoes this course have a master?");
                                        Console.WriteLine("\n\t\t1. Yes" +
                                                          "\n\t\t2. No");
                                        Console.Write("\n\tEnter the item: ");
                                        int chooseMaster;
                                        while (!int.TryParse(Console.ReadLine(), out chooseMaster))
                                        {
                                            PrintWarningJustNumber("select item");
                                            Thread.Sleep(1000);
                                            Console.WriteLine("\n\tDoes this course have a master?");
                                            Console.WriteLine("\n\t\t1. Yes" +
                                                              "\n\t\t2. No");
                                            Console.Write("\n\tEnter the item: ");
                                        }
                                        chooseMaster:
                                        switch (chooseMaster)
                                        {
                                            case 1:
                                                PrintDateTimeAndUserStatistic();
                                                Console.WriteLine("\n\tMaster List");
                                                List<Master> masterList = dbStudentEditForm.masters.ToList();
                                                foreach (Master master in masterList)
                                                {
                                                    Console.WriteLine($"id: {master.UserId}\tName: {master.Name}\tFamily: {master.Family}\tPhonenumber: {master.PhoneNumber}\tSalary: {master.Salary}");
                                                    Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                                                }
                                                Console.WriteLine("select the master: ");
                                                int selectedMasterForCourse;
                                                while (!int.TryParse(Console.ReadLine(), out selectedMasterForCourse))
                                                {
                                                    PrintWarningJustNumber("master id");
                                                    Console.Clear();
                                                    foreach (Master master in masterList)
                                                    {
                                                        Console.WriteLine($"id: {master.UserId}\tName: {master.Name}\tFamily: {master.Family}\tPhonenumber: {master.PhoneNumber}\tSalary: {master.Salary}");
                                                        Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                                                    }
                                                    Console.WriteLine("select the master: ");
                                                }
                                                 Master courseMasterSelected = dbStudentEditForm.masters.Find(selectedMasterForCourse);
                                                if (selectedMasterForCourse != null)
                                                {
                                                    goto addNewCourseForStudent;
                                                }
                                                else
                                                {
                                                    PrintDateTimeAndUserStatistic();
                                                    PrintWarningNotFoundId("Master List", selectedMasterForCourse);
                                                    PrintDateTimeAndUserStatistic();
                                                    goto addNewCourseForStudent;
                                                }
                                                Course newCourseByMaster = new Course($"{newCourseName}", newCourseUnit, courseMasterSelected);
                                                dbStudentEditForm.Courses.Add(newCourseByMaster); 
                                                selectStudentForEdit.Courses = new List<Course> { newCourseByMaster };
                                                PrintDateTimeAndUserStatistic();
                                                Console.WriteLine($"\n\t{newCourseByMaster.CourseName} course added to student: {selectStudentForEdit.Name} {selectStudentForEdit.Family}\n");
                                                goto finishEditstudent;
                                            case 2:
                                                Console.WriteLine($"\nNew master registration form for course: {newCourseName}");
                                                Console.Write("\n\n\tDegree     : ");
                                                string mAdegree = Console.ReadLine();
                                                Console.Write("\tMonthly salary : ");
                                                float mASalary;
                                                while (!float.TryParse(Console.ReadLine(), out mASalary))
                                                {
                                                    PrintWarningJustNumber("Monthly salary");
                                                    Console.Clear();
                                                    PrintDateTimeAndUserStatistic();
                                                    Console.WriteLine($"\nNew master registration form for course: {newCourseName}");
                                                    Console.WriteLine($"\n\n\tDegree Name    : {mAdegree}");
                                                    Console.Write("\tMonthly salary : ");
                                                }
                                                Console.Write("\tFirst Name     : ");
                                                string mAfirstName = Console.ReadLine();
                                                Console.Write("\tLast Name      : ");
                                                string mAlastName = Console.ReadLine();
                                                Console.Write("\tPhonenumber    : ");
                                                string mAphonenumber = Console.ReadLine();
                                                while (!Regex.IsMatch(mAphonenumber, PatternPhoneNumber))
                                                {
                                                    Console.WriteLine("\n\tIncorrect phonenumber !");
                                                    Thread.Sleep(2000);
                                                    Console.Clear();
                                                    PrintDateTimeAndUserStatistic();
                                                    Console.WriteLine($"\nNew master registration form for course: {newCourseName}");
                                                    Console.Write($"\n\n\tDegree Name    : {mAdegree}");
                                                    Console.Write($"\n\tMonthly salary : {mASalary}");
                                                    Console.Write($"\n\tFirst Name     : {mAfirstName}");
                                                    Console.Write($"\n\tLast Name      : {mAlastName}");
                                                    Console.Write("\n\tPhonenumber    : ");
                                                    mAphonenumber = Console.ReadLine();
                                                }
                                                Console.Write("\tPassword       : ");
                                                string mApassword = Console.ReadLine();
                                                Course newCourse = new Course($"{newCourseName}", newCourseUnit, new Master($"{mAdegree}", mASalary, $"{mAfirstName}", $"{mAlastName}", $"{mAphonenumber}", $"{mApassword}", dbStudentEditForm.Roles.Find(2)));
                                                dbStudentEditForm.Courses.Add(newCourse);
                                                selectStudentForEdit.Courses = new List<Course> { newCourse };
                                                PrintDateTimeAndUserStatistic();
                                                Console.WriteLine($"\n\t{newCourse.CourseName} course added to student: {selectStudentForEdit.Name} {selectStudentForEdit.Family}\n");
                                                goto finishEditstudent;
                                           
                                                default:
                                                    goto addNewCourseForStudent;


                                        }
                                    default:
                                        goto AddCourseForStudent;

                                }
                            default:
                                goto editStudent;


                        }
                    finishEditstudent:
                    dbStudentEditForm.SaveChanges();
                    Console.Write("\nDo you want to return to menu?");
                    Console.ReadKey();
                    goto menu;

                    }

                #endregion

                #region فرم ویرایش اطلاعات استادان (editting the masters information)

                case 11:
                    MasterEditForm:
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine("\nMaster editting form");
                    UniversityContext dbEditMaster = new UniversityContext("UniDbConStr");
                    using (dbEditMaster)
                    {
                        List<Master> viewMasterList =dbEditMaster.masters.ToList();
                        foreach (Master master in viewMasterList)
                        {
                            Console.WriteLine($"id: {master.UserId}\tName: {master.Name}\tLast name: {master.Family}\tPhonenumber: {master.PhoneNumber}\tPassword: {master.Password}\tActivity: {master.IsActive}");
                            Console.WriteLine("____________________________________________________________________________________________________________");
                        }
                        Console.Write("\n\tEnter the master id for edit: ");
                        int masteridforedit;
                        while (!int.TryParse(Console.ReadLine(), out masteridforedit)) 
                        {
                            PrintWarningJustNumber("Master id");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            Console.WriteLine("\nMaster editting form");
                            foreach (Master master in viewMasterList)
                            {
                                Console.WriteLine($"id: {master.UserId}\tName: {master.Name}\tLast name: {master.Family}\tPhonenumber: {master.PhoneNumber}\tPassword: {master.Password}\tActivity: {master.IsActive}");
                                Console.WriteLine("____________________________________________________________________________________________________________");
                            }
                            Console.WriteLine("\n\tEnter the master id for edit: ");
                        }
                        PrintDateTimeAndUserStatistic();
                        var selectMasterForEdit = dbEditMaster.masters.Find(masteridforedit);
                        if (selectMasterForEdit != null)
                        {
                            goto selectMenuForEditStudent;
                        }
                        else
                        {
                            PrintDateTimeAndUserStatistic();
                            PrintWarningNotFoundId("master list", masteridforedit);
                            goto MasterEditForm;
                        }
                        selectMenuForEditStudent:
                        Console.WriteLine($"\nchoose property for edit master: {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                        Console.WriteLine("\n\t\t1. Degree" +
                                          "\n\t\t2. Salary" +
                                          "\n\t\t3. Name" +
                                          "\n\t\t4. Family" +
                                          "\n\t\t5. Phonenumber" +
                                          "\n\t\t6. Password" +
                                          "\n\t\t7. Access");
                        Console.WriteLine("\nyour answer number: ");
                        int itemSelectForEdit;
                        while (!int.TryParse(Console.ReadLine(), out itemSelectForEdit))
                        {
                            PrintWarningJustNumber("select item");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            Console.WriteLine($"\nchoose property for edit master: {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                            Console.WriteLine("\n\t\t1. Degree" +
                                              "\n\t\t2. Salary" +
                                              "\n\t\t3. Name" +
                                              "\n\t\t4. Family" +
                                              "\n\t\t5. Phonenumber" +
                                              "\n\t\t6. Password" +
                                              "\n\t\t7. Access");
                            Console.WriteLine("\nYour answer number: ");

                        }
                        PrintDateTimeAndUserStatistic();

                        switch(itemSelectForEdit)
                        {
                            case 1:
                                Console.WriteLine($"\n\tMaster degree editting form for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                Console.WriteLine($"\n\tCurrent master degree: {selectMasterForEdit.Degree}");
                                Console.Write("\n\tEnter the new degree  : ");
                                string newMasterDegree = Console.ReadLine();
                                selectMasterForEdit.Degree= newMasterDegree;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tDegree has changed to {newMasterDegree} for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                goto finishEditMaster;

                            case 2:
                                Console.WriteLine($"\n\tMaster salary editting form for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                Console.WriteLine($"\n\tCurrent salary: {selectMasterForEdit.Salary}");
                                Console.Write("\n\tEnter the new salary: ");
                                float newSalaryforMaster;
                                while (!float.TryParse(Console.ReadLine(), out newSalaryforMaster))
                                {
                                    PrintWarningJustNumber("master salary");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine("\n\tMaster salary form edit for ");
                                    Console.WriteLine($"\n\tCurrent salary: {selectMasterForEdit.Salary}");
                                    Console.Write("\n\tEnter the new salary: ");
                                }
                                selectMasterForEdit.Salary=newSalaryforMaster;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tSalary has changed to {newSalaryforMaster} for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                goto finishEditMaster;
                            case 3:
                                Console.WriteLine($"\nMaster name editting form for {selectMasterForEdit.Name} {selectMasterForEdit.Family} \n");
                                Console.WriteLine($"\n\tCurrent master name: {selectMasterForEdit.Name}");
                                Console.Write("\n\tEnter new master name: ");
                                string newMasterName = Console.ReadLine();
                                selectMasterForEdit.Name = newMasterName;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\t Master name has changed to {newMasterName} for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                goto finishEditMaster;
                            case 4:
                                Console.WriteLine($"\nMaster last name editting form for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                Console.WriteLine($"\n\tCurrent master last name: {selectMasterForEdit.Family}");
                                Console.Write($"\n\tNew master last name: ");
                                string newMasterFamily= Console.ReadLine(); 
                                selectMasterForEdit.Family=newMasterFamily;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tmaster family has changed to {newMasterFamily} for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                goto finishEditMaster;
                            case 5:
                                Console.WriteLine($"\n\tMaster phone number editting form for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                Console.WriteLine($"\n\tCurrent phone number: {selectMasterForEdit.PhoneNumber} ");
                                Console.Write("\n\tEnter the new phone number: ");
                                string newMasterPhonenumber=Console.ReadLine();
                                while (!Regex.IsMatch(newMasterPhonenumber,PatternPhoneNumber))
                                {
                                    Console.WriteLine("\n\tIncorrect phone number");
                                    Thread.Sleep(1000);
                                    PrintDateTimeAndUserStatistic();
                                    Console.Write("\n\tEnter the new phone number: ");
                                    newMasterPhonenumber=Console.ReadLine();
                                }
                                selectMasterForEdit.PhoneNumber=newMasterPhonenumber;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tPhone number has changed to {newMasterPhonenumber} for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                goto finishEditMaster;
                            case 6:
                                Console.WriteLine($"\n\tPassword editting form for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                Console.WriteLine($"\n\tCurrent master password: {selectMasterForEdit.Password}");
                                Console.Write("\n\tEnter the new password: ");
                                string newMasterPassword = Console.ReadLine();
                                selectMasterForEdit.Password = newMasterPassword;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tPassword has changed to {newMasterPassword} for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                goto finishEditMaster;
                            case 7:
                                Console.WriteLine("\n\tMaster activity statue");
                                if (selectMasterForEdit.IsActive == true)
                                {
                                    selectMasterForEdit.IsActive = false;
                                }
                                else
                                {
                                    selectMasterForEdit.IsActive = true;
                                }
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"The access has changed to {selectMasterForEdit.IsActive} for {selectMasterForEdit.Name} {selectMasterForEdit.Family}");
                                goto finishEditMaster;
                            default:
                                goto MasterEditForm;

                        }
                  finishEditMaster:
                    dbEditMaster.SaveChanges();
                    Console.Write("\nPress any key to return to menu");
                    Console.ReadKey();

                    }
                    
                    goto menu;
                #endregion

                #region فرم ویرایش اطلاعات دروس (editting the course information)
                
                case 12:
                    CourseEditForm:
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine("\nCourse editting form\n");
                    UniversityContext dbCourseEdit = new UniversityContext("UniDbConStr");
                    using (dbCourseEdit)
                    {
                        List<Course> viewCourseList =dbCourseEdit.Courses.ToList();
                        foreach (Course course in viewCourseList)
                        {
                            Console.WriteLine($"id: {course.CourseId}\tName: {course.CourseName}\tUnit: {course.CourseUnit}\tRegister Date: {course.RegisterDate}");
                            if (course.Master.Courses != null)
                            {
                                Console.WriteLine("\tMaster");
                                Console.WriteLine($"id: {course.Master.UserId}\tName: {course.Master.Name}\tFamily: {course.Master.Family}\t Activity: {course.Master.IsActive}");
                            }
                            Console.WriteLine("-------------------------------------------------------------------------------------------------");

                        }
                        Console.Write("Enter Course id: ");
                        int CourseIdForEdit;
                        while (!int.TryParse(Console.ReadLine(), out CourseIdForEdit)) 
                        {
                            PrintWarningJustNumber("course id");
                            Console.Clear();
                            Thread.Sleep(2000);
                            PrintDateTimeAndUserStatistic();
                            foreach (Course course in viewCourseList)
                            {
                                Console.WriteLine($"id: {course.CourseId}\tName: {course.CourseName}\tUnit: {course.CourseUnit}\tRegister Date: {course.RegisterDate}");
                                if (course.Master.Courses != null)
                                {
                                    Console.WriteLine("\tMaster");
                                    Console.WriteLine($"id: {course.Master.UserId}\tName: {course.Master.Name}\tFamily: {course.Master.Family}\t Activity: {course.Master.IsActive}");
                                }
                                Console.WriteLine("-------------------------------------------------------------------------------------------------");

                            }
                            Console.Write("Enter Course id: ");
                        }
                        PrintDateTimeAndUserStatistic();
                        var selectCourseIdForEdit =dbCourseEdit.Courses.Find(CourseIdForEdit);
                        if (selectCourseIdForEdit != null)
                        {
                            goto selectItemForEditCourse;
                        }
                        else
                        {
                            PrintDateTimeAndUserStatistic();
                            PrintWarningNotFoundId("course list", CourseIdForEdit);
                            goto CourseEditForm;
                        }
                       selectItemForEditCourse:
                        Console.WriteLine($"\nChoose property for edit course: {selectCourseIdForEdit.CourseName}");
                        Console.WriteLine("\n\t\t1. Name" +
                                          "\n\t\t2. Unit" +
                                          "\n\t\t3. Master");
                        Console.Write("\nYour answer number: ");
                        int editCourseOperation;
                        while (!int.TryParse(Console.ReadLine(),out editCourseOperation))
                        {
                            PrintWarningJustNumber("select item of menu");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            Console.WriteLine($"\nChose property for edit course: {selectCourseIdForEdit.CourseName}");
                            Console.WriteLine("\n\t\t1. Name" +
                                              "\n\t\t2. Unit" +
                                              "\n\t\t3. Master");
                            Console.Write("\nYour answer number: ");

                        }
                        PrintDateTimeAndUserStatistic();

                        switch (editCourseOperation)
                        {
                            case 1:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine("\nCourse Name editting form\n");
                                Console.WriteLine($"\n\tCurrent course name: {selectCourseIdForEdit.CourseName}");
                                Console.Write($"\n\tEnter the new course name: ");
                                string newEditedCourseName= Console.ReadLine();
                                selectCourseIdForEdit.CourseName= newEditedCourseName;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\t course name has changed to {newEditedCourseName}\n");
                                goto finishEditCourse;
                            case 2:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine("\nCourse unit editting form\n");
                                Console.WriteLine($"\n\tCurrent course unit: {selectCourseIdForEdit.CourseUnit}");
                                Console.Write($"\n\tEnter the new course unit: ");
                                int newEditedCourseUnit;
                                while (!int.TryParse(Console.ReadLine(), out newEditedCourseUnit))
                                {
                                    PrintWarningJustNumber("course unit");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine($"\n\tCurrent course unit: {selectCourseIdForEdit.CourseUnit}");
                                    Console.Write($"\n\tEnter the new course unit: ");

                                }
                                selectCourseIdForEdit.CourseUnit= newEditedCourseUnit;
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\n\tCourse unit has changed to {newEditedCourseUnit}");
                                goto finishEditCourse;

                              case 3:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine("\nMaster course editting form\n");
                                Console.WriteLine($"\nCurrent master course: {selectCourseIdForEdit.Master.Name}");
                                Console.WriteLine($"\n\tDoes {selectCourseIdForEdit.CourseName} have a new master? ");
                                Console.Write("\n\t\t\t1.Yes\n\t\t\t2.No");
                                Console.Write("\n\tAnswer: ");
                                int masterStatue;
                                while (!int.TryParse(Console.ReadLine(), out masterStatue))
                                {
                                    PrintWarningJustNumber("master statue");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine($"\nCurrent master course: {selectCourseIdForEdit.Master.Name}");
                                    Console.WriteLine($"\n\tDoes {selectCourseIdForEdit.CourseName} have a new master? ");
                                    Console.Write("\n\t\t\t1.Yes\n\t\t\t2.No");
                                    Console.Write("\n\tAnswer: ");

                                }
                                switch (masterStatue)
                                {
                                   case 1: 
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine("\nNew master registration form");
                                        Console.Write("\n\n\tDegree Name    : ");
                                        string cMaDegree = Console.ReadLine();
                                        Console.Write("\tMonthly salary     : ");
                                        float cMaSalary;
                                        while (!float.TryParse(Console.ReadLine(), out cMaSalary))
                                        {
                                            PrintDateTimeAndUserStatistic();
                                            PrintWarningJustNumber("Monthly salary");
                                            PrintDateTimeAndUserStatistic();
                                            Console.WriteLine($"\nNew master registration form for course: {selectCourseIdForEdit.CourseName}");
                                            Console.WriteLine($"\n\n\tDegree Name    : {cMaDegree}");
                                            Console.Write("\tMonthly salary : ");
                                        }

                                        Console.Write("\tFirst name    : ");
                                        string cMaFirstname = Console.ReadLine();
                                        Console.Write("\tLast name     : ");
                                        string cMaLastname = Console.ReadLine() ;
                                        Console.Write("\tPhonenumber   : ");
                                        string cMaPhonenumber = Console.ReadLine();
                                        while (!Regex.IsMatch(cMaPhonenumber,PatternPhoneNumber))
                                        {
                                            Console.WriteLine("Incorrect Phone number");
                                            Thread.Sleep(2000);
                                            PrintDateTimeAndUserStatistic();
                                            Console.Write("\tPhonenumber   : ");
                                            cMaPhonenumber = Console.ReadLine();
                                        }
                                        Console.Write("\tPassword   : ");
                                        string cMaPassword = Console.ReadLine();

                                        Master newMaster = new Master($"{cMaDegree}", cMaSalary, $"{cMaFirstname}", $"{cMaLastname}", $"{cMaPhonenumber}", $"{cMaPassword}", dbCourseEdit.Roles.Find(2));
                                        dbCourseEdit.masters.Add(newMaster);
                                        selectCourseIdForEdit.Master= newMaster;
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine($"\n\tMaster of course {selectCourseIdForEdit.CourseName} change to {cMaFirstname} {cMaLastname}\n");
                                        goto finishEditCourse;
                                    case 2:
                                        selectMasterforEditedCourse:
                                        Console.WriteLine($"\nPlease select master for course: {selectCourseIdForEdit.CourseName}\n");
                                        List<Master> MasterList = dbCourseEdit.masters.ToList();
                                        foreach (Master master in MasterList)
                                        {
                                            Console.WriteLine($"\tId: {master.UserId}\tName: {master.Name}\tFamily: {master.Family}\tActivity: {master.IsActive}\n" +
                                                              $"\tPhone:  {master.PhoneNumber}\tSalary: {master.Salary}\t\tDegree: {master.Degree}");
                                            Console.WriteLine("\t----------------------------------------------------------------");
                                        }
                                        Console.Write("\n\tEnter master id: ");
                                        int MasterSelection;
                                        while (!int.TryParse(Console.ReadLine(), out MasterSelection))
                                        {
                                            PrintWarningJustNumber("Master Id");
                                            Console.Clear();
                                            PrintDateTimeAndUserStatistic();
                                            foreach (Master master in MasterList)
                                            {
                                                Console.WriteLine($"\tId: {master.UserId}\tName: {master.Name}\tFamily: {master.Family}\tActivity: {master.IsActive}\n" +
                                                                  $"\tPhone:  {master.PhoneNumber}\tSalary: {master.Salary}\t\tDegree: {master.Degree}");
                                                Console.WriteLine("\t----------------------------------------------------------------");
                                            }
                                            Console.Write("\n\tEnter master id: ");
                                        }
                                          var selectedmasterForEditedCourse = dbCourseEdit.masters.Find(MasterSelection);
                                        if (selectedmasterForEditedCourse != null)
                                        {
                                            goto editCourse;
                                        }
                                        else
                                        {
                                            PrintDateTimeAndUserStatistic();
                                            PrintWarningNotFoundId("master id", MasterSelection);
                                            goto selectMasterforEditedCourse;
                                        }
                                        editCourse:
                                        selectCourseIdForEdit.Master = selectedmasterForEditedCourse;
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine($"\n\tMaster of course {selectCourseIdForEdit.CourseName} was changed to {selectedmasterForEditedCourse.Name} {selectedmasterForEditedCourse.Family}\n");
                                        goto finishEditCourse;

                                    default:
                                        goto CourseEditForm;

                                }


                        }
                        finishEditCourse:
                        dbCourseEdit.SaveChanges();
                        Console.Write("\nPress any key to return to menu");
                        Console.ReadKey();
                        goto menu;

                    }
                #endregion

                #region فرم حذف کارمند (removing employee)

                case 13:
                    removeEmployee:
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine("\n\tEmployee removal form ");
                    UniversityContext dbEmployeeRemove = new UniversityContext("UniDbConStr");
                    using (dbEmployeeRemove)
                    {
                        List<Employee> ViewEmployee =dbEmployeeRemove.Employees.ToList();
                        foreach (Employee employee in ViewEmployee)
                        {
                            Console.WriteLine($"Id: {employee.UserId}\tName: {employee.Name}\t Family: {employee.Family}\tPhonenumber: {employee.PhoneNumber}\tRegister date: {employee.RegisterDate}");
                            Console.WriteLine("----------------------------------------------------------------------------------------------");
                        }
                        Console.Write("\n\tEnter employee id: ");
                        int employeeIdforRemove;
                        while (!int.TryParse(Console.ReadLine(), out employeeIdforRemove))
                        {
                            PrintWarningJustNumber("Employee Id");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            foreach (Employee employee in ViewEmployee)
                            {
                                Console.WriteLine($"Id: {employee.UserId}\tName: {employee.Name}\t Family: {employee.Family}\tPhonenumber: {employee.PhoneNumber}\tRegister date: {employee.RegisterDate}");
                                Console.WriteLine("----------------------------------------------------------------------------------------------");
                            }
                            Console.Write("\n\tEnter employee id: ");
                        }
                        var selectEmployeeForRemove = dbEmployeeRemove.Employees.Find(employeeIdforRemove);
                        if (selectEmployeeForRemove!=null)
                        {
                            goto removeEmployeeById;
                        }
                        else
                        {
                            PrintDateTimeAndUserStatistic();
                            PrintWarningNotFoundId("Employee List", employeeIdforRemove);
                            goto removeEmployee;
                        }
                    removeEmployeeById:
                        dbEmployeeRemove.Employees.Remove(selectEmployeeForRemove);
                        PrintDateTimeAndUserStatistic();
                        Console.WriteLine($"\n\t{selectEmployeeForRemove.Name} {selectEmployeeForRemove.Family} was removed");
                        dbEmployeeRemove.SaveChanges();
                        Console.WriteLine("\nPress any key to return to menu");
                        Console.ReadKey();

                        
                    }                     
                    goto menu;

                #endregion

                #region فرم حذف دانشجو (removing students)


                case 14:
                    StudentRemove:
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine("\n\tStudent removal form\n");
                    UniversityContext dbStudentRemove = new UniversityContext("UniDBConStr");
                    using (dbStudentRemove)
                    {
                        List<Student> selectStudentForRemove =dbStudentRemove.Students.ToList();
                        foreach (Student student in selectStudentForRemove)
                        {
                            Console.WriteLine($"\tid: {student.UserId}\tcode: {student.StudentCode}\tname: {student.Name}\tfamily: {student.Family}\n" +
                                                                         $"\tphone:  {student.PhoneNumber}\tdegree: {student.Degree}\tactive: {student.IsActive}");
                            Console.WriteLine("\t----------------------------------------------------------------");
                        }
                        Console.Write("\n\tEnter Student Id for remove: ");
                        int RemoveStudent;
                        while (!int.TryParse(Console.ReadLine(), out RemoveStudent))
                        {
                            PrintWarningJustNumber("Student id");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            foreach (Student student in selectStudentForRemove)
                            {
                                Console.WriteLine($"\tid: {student.UserId}\tcode: {student.StudentCode}\tname: {student.Name}\tfamily: {student.Family}\n" +
                                                                             $"\tphone:  {student.PhoneNumber }\tdegree: {student.Degree}\tactive: {student.IsActive}");
                                Console.WriteLine("\t----------------------------------------------------------------");
                            }
                            Console.Write("\n\tEnter Student Id for remove: ");
                        }
                        var removeStudent =dbStudentRemove.Students.Find(RemoveStudent);
                        if (removeStudent!=null)
                        {
                            goto RemoveStudentById;

                        }
                        else
                        {
                            PrintDateTimeAndUserStatistic();
                            PrintWarningNotFoundId("Student List", RemoveStudent);
                            goto StudentRemove;
                        }
                        RemoveStudentById:
                        dbStudentRemove.Students.Remove(removeStudent);
                        PrintDateTimeAndUserStatistic();
                        Console.WriteLine($"\n\t{removeStudent.Name} {removeStudent.Family} was removed");
                        dbStudentRemove.SaveChanges();
                        Console.WriteLine("\n\tPress any key to return to menu");
                        Console.ReadKey();


                    }
                    goto menu;
                #endregion

                #region فرم حذف استاد (removing master)

                case 15:
                    RemoveMaster:
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine("\n\tMaster removal form\n\n");
                    UniversityContext dbRemoveMaster = new UniversityContext("UniDBConStr");
                    using (dbRemoveMaster)
                    {
                        List<Master> viewMasterList =dbRemoveMaster.masters.ToList();
                        foreach (Master master in viewMasterList)
                        {
                            Console.WriteLine($"\tId: {master.UserId}\tName: {master.Name}\tFamiy: {master.Family}\tActivity: {master.IsActive}");
                            Console.WriteLine("-----------------------------------------------------------------------------------");
                        }
                        Console.Write("\n\tEnter master id for remove: ");
                        int RemoveMasterId;
                        while (!int.TryParse(Console.ReadLine(), out RemoveMasterId))
                        {
                            PrintWarningJustNumber("Remove Master");
                            Console.Clear();
                            PrintDateTimeAndUserStatistic();
                            foreach (Master master in viewMasterList)
                            {
                                Console.WriteLine($"Id: {master.UserId}\tName: {master.Name}\tFamiy: {master.Family}\tActivity: {master.IsActive}");
                                Console.WriteLine("-----------------------------------------------------------------------------------");
                            }
                            Console.Write("Enter master id for remove: ");
                        }
                          var RemovingItem = dbRemoveMaster.masters.Find(RemoveMasterId);
                        if (RemovingItem!=null)
                        {
                            goto RemoveMasterById;
                        }
                        else
                        {
                            PrintDateTimeAndUserStatistic();
                            PrintWarningNotFoundId("master list", RemoveMasterId);
                        }
                        RemoveMasterById:
                        PrintDateTimeAndUserStatistic();
                        var CourseMasterRemove =dbRemoveMaster.Courses.FirstOrDefault(t=>t.Master.UserId == RemovingItem.UserId);
                        if (!dbRemoveMaster.Courses.Any()) 
                        {
                            dbRemoveMaster.Courses.Remove(CourseMasterRemove);

                        }
                        dbRemoveMaster.masters.Remove(RemovingItem);
                        PrintDateTimeAndUserStatistic();
                        Console.WriteLine($"\n\t{RemovingItem.Name} {RemovingItem.Family} was removed\n");
                        dbRemoveMaster.SaveChanges();
                        Console.WriteLine("\nPress any key to return to menu");
                        Console.ReadKey();


                    }
                    goto menu;

                #endregion

                #region فرم حذف درس (removing course)

                case 16:
                    RemoveCourse:
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine("\n\tCourse Removal Form\n");
                    UniversityContext dbRemoveCourse = new UniversityContext("UniDBConStr");
                    using (dbRemoveCourse)
                    {
                       List<Course> viewCourseList = dbRemoveCourse.Courses.ToList();
                        foreach (Course course in viewCourseList)
                        {
                            Console.WriteLine($"\tId: {course.CourseId}\tName: {course.CourseName}\t Course Unit: {course.CourseId}\tRegister Date: {course.RegisterDate}");
                            Console.WriteLine("----------------------------------------------------------------------------------------");
                        }
                        Console.Write("\n\tEnter Course Id for remove: ");
                        int RemoveCourse;
                        while (!int.TryParse(Console.ReadLine(),out RemoveCourse))
                        {
                            PrintWarningJustNumber("Remove Course");
                            Console.Clear();
                            foreach (Course course in viewCourseList)
                            {
                                Console.WriteLine($"\tId: {course.CourseId}\tName: {course.CourseName}\t Course Unit: {course.CourseId}\tRegister Date: {course.RegisterDate}");
                                Console.WriteLine("----------------------------------------------------------------------------------------");
                            }
                            Console.Write("\n\tEnter Course Id for remove: ");

                        }
                        var RemovingItem = dbRemoveCourse.Courses.Find(RemoveCourse);
                        if (RemovingItem != null)
                        {
                            goto removeCourseById;
                        }
                        else
                        {
                            PrintDateTimeAndUserStatistic() ;
                            PrintWarningNotFoundId("remove course", RemoveCourse);
                        }
                        removeCourseById:
                        dbRemoveCourse.Courses.Remove(RemovingItem);
                        PrintDateTimeAndUserStatistic();
                        Console.WriteLine($"\n\t{RemovingItem.CourseName} was removed");
                        dbRemoveCourse.SaveChanges();
                        Console.WriteLine("\n\tPress any key to return to menu");
                        Console.ReadKey();

                    }


                    goto menu;
                #endregion

                #region تغییر نقش کاربری (changing role)

                case 17:
                    changeRole:
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine("\n\tChanging User Role");
                    Console.WriteLine("\n\tPlease select object for change role");
                    Console.WriteLine("\t\t1. Employee" +
                                    "\n\t\t2. Master" +
                                    "\n\t\t3. Student");
                    Console.Write("\n\tEnter the item: ");
                    int ItemForChange;
                    while (true)
                    {
                        PrintWarningJustNumber("select item of menu");
                        Console.Clear();
                        PrintDateTimeAndUserStatistic();
                        Console.WriteLine("\n\tPlease select object for change role");
                        Console.WriteLine("\t\t1. Employee" +
                                        "\n\t\t2. Master" +
                                        "\n\t\t3. Student");
                        Console.Write("\n\tEnter the item: ");
                    }
                    UniversityContext dbChangeRole = new UniversityContext("UniDBConStr");
                    using (dbChangeRole)
                    {
                        switch (ItemForChange)
                        {
                            #region فرم تغییر نقش کارمندان
                            case 1:
                                changeEmployee:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine("\nPlease select employee for change role\n");
                                List<Employee> veiwEmployeeList = dbChangeRole.Employees.ToList();
                                foreach (Employee employee in veiwEmployeeList)
                                {
                                    Console.WriteLine($"\n\tId: {employee.UserId}\tName: {employee.Name}\tFamily: {employee.Family}\tDepartment: {employee.Department}\tPhoneNumber: {employee.PhoneNumber}\tActivity: {employee.IsActive}");
                                    Console.WriteLine("----------------------------------------------------------------------------------");
                                }
                                Console.Write("\n\tEnter the employee id: ");
                                int EmployeeForEdit;
                                while (!int.TryParse(Console.ReadLine(), out EmployeeForEdit))
                                {
                                    PrintWarningJustNumber("change employee role");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    foreach (Employee employee in veiwEmployeeList)
                                    {
                                        Console.WriteLine($"\n\tId: {employee.UserId}\tName: {employee.Name}\tFamily: {employee.Family}\tDepartment: {employee.Department}\tPhoneNumber: {employee.PhoneNumber}\tActivity: {employee.IsActive}");
                                        Console.WriteLine("----------------------------------------------------------------------------------");
                                    }
                                    Console.Write("\n\tEnter the employee id: ");
                                }
                                Employee selectedEmployeeForChangeRole =dbChangeRole.Employees.Find(EmployeeForEdit);
                                if (selectedEmployeeForChangeRole!=null)
                                {
                                    goto selectObjectForChangeEmployeeRole;
                                }
                                else
                                {
                                    PrintWarningNotFoundId("employee list", EmployeeForEdit);
                                }
                                selectObjectForChangeEmployeeRole:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine("\n\tSelect the object you want to change");
                                Console.WriteLine("\t\t1. Master" +
                                                "\n\t\t2. Student");
                                Console.WriteLine("\nEnter your item: ");
                                int changingObject;
                                while (!int.TryParse(Console.ReadLine(), out changingObject))
                                {
                                    PrintWarningJustNumber("change object");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine("\n\tSelect the object you want to change");
                                    Console.WriteLine("\t\t1. Master" +
                                                    "\n\t\t2. Student");
                                    Console.WriteLine("\nEnter your item: ");

                                }
                                switch (changingObject)
                                {
                                    case 1:
                                        dbChangeRole.masters.Add(new Master("", selectedEmployeeForChangeRole.Salary, selectedEmployeeForChangeRole.Name, selectedEmployeeForChangeRole.Family, selectedEmployeeForChangeRole.PhoneNumber, selectedEmployeeForChangeRole.Password, dbChangeRole.Roles.Find(2)));
                                        dbChangeRole.Employees.Remove(selectedEmployeeForChangeRole);
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine($"\n\tRole of {selectedEmployeeForChangeRole.Name} {selectedEmployeeForChangeRole.Family} was change to master\n");
                                        goto finishChangeRole;
                                    case 2:
                                        dbChangeRole.Students.Add(new Student("", selectedEmployeeForChangeRole.Name, selectedEmployeeForChangeRole.Family, selectedEmployeeForChangeRole.PhoneNumber, selectedEmployeeForChangeRole.Password, dbChangeRole.Roles.Find(3)));
                                        dbChangeRole.Employees.Remove(selectedEmployeeForChangeRole);
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine($"\n\tRole of {selectedEmployeeForChangeRole.Name} {selectedEmployeeForChangeRole.Family} was change to student\n");
                                        goto finishChangeRole;

                                    default:
                                        goto selectObjectForChangeEmployeeRole;
                                }
                            #endregion

                            #region فرم تغییر نقش اساتید

                            case 2:
                                changeMaster:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine("\nPlease select master for change role\n");
                                List<Master> masterList = dbChangeRole.masters.ToList();
                                foreach (Master master in masterList)
                                {
                                    Console.WriteLine($"\tid: {master.UserId}\tname: {master.Name}\tfamily: {master.Family}\tactive: {master.IsActive}\n" +
                                                      $"\tphone:  {master.PhoneNumber}\tsalary: {master.Salary}\t\tdegree: {master.Degree}");
                                    var courseMaster = dbChangeRole.Courses.Where(t => t.Master.UserId == master.UserId).ToList();
                                    Console.WriteLine("\t----------------------------------------------------------------");
                                }
                                Console.Write("\nMaster id is: ");
                                int answerSelectChangeMaster;
                                while (!int.TryParse(Console.ReadLine(), out answerSelectChangeMaster))
                                {
                                    PrintWarningJustNumber("Master id");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    foreach (Master master in masterList)
                                    {
                                        Console.WriteLine($"\tid: {master.UserId}\tname: {master.Name}\tfamily: {master.Family}\tactive: {master.IsActive}\n" +
                                                          $"\tphone:  {master.PhoneNumber}\tsalary: {master.Salary}\t\tdegree: {master.Degree}");
                                        var courseMaster = dbChangeRole.Courses.Where(t => t.Master.UserId == master.UserId).ToList();
                                        Console.WriteLine("\t----------------------------------------------------------------");
                                    }
                                    Console.Write("\nMaster id is: ");
                                }
                                Master selectMasterForChangeRole = dbChangeRole.masters.Find(answerSelectChangeMaster);
                                if (selectMasterForChangeRole != null)
                                {
                                    goto selectObjectForChangeMasterRole;
                                }
                                else
                                {
                                    PrintDateTimeAndUserStatistic();
                                    PrintWarningNotFoundId("Master List", answerSelectChangeMaster);
                                    goto changeMaster;
                                }
                                selectObjectForChangeMasterRole:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\nPlease select object for master: {selectMasterForChangeRole.Name} {selectMasterForChangeRole.Family}\n");
                                Console.WriteLine("\t\t1. Employee" +
                                                "\n\t\t2. Student");
                                Console.Write("\nYour answer number: ");
                                int newObjectForChangeMaster;
                                while (!int.TryParse(Console.ReadLine(), out newObjectForChangeMaster))
                                {
                                    PrintWarningJustNumber("Select item of menu");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine($"\nPlease select object for master: {selectMasterForChangeRole.Name} {selectMasterForChangeRole.Family}\n");
                                    Console.WriteLine("\t\t1. Employee" +
                                                    "\n\t\t2. Student");
                                    Console.Write("\nYour answer number: ");
                                }
                                switch (newObjectForChangeMaster)
                                {
                                    case 1:
                                        dbChangeRole.Employees.Add(new Employee("", selectMasterForChangeRole.Salary, selectMasterForChangeRole.Name, selectMasterForChangeRole.Family, selectMasterForChangeRole.PhoneNumber, selectMasterForChangeRole.Password, dbChangeRole.Roles.Find(1)));
                                        dbChangeRole.masters.Remove(selectMasterForChangeRole);
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine($"\n\tRole of {selectMasterForChangeRole.Name} {selectMasterForChangeRole.Family} was change to master\n");
                                        goto finishChangeRole;

                                    case 2:
                                        dbChangeRole.Students.Add(new Student(selectMasterForChangeRole.Degree, selectMasterForChangeRole.Name, selectMasterForChangeRole.Family, selectMasterForChangeRole.PhoneNumber, selectMasterForChangeRole.Password, dbChangeRole.Roles.Find(3)));
                                        dbChangeRole.masters.Remove(selectMasterForChangeRole);
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine($"\n\tRole of {selectMasterForChangeRole.Name} {selectMasterForChangeRole.Family} was change to student\n");


                                        goto finishChangeRole;
                                    default:
                                        goto selectObjectForChangeMasterRole;
                                }
                            #endregion

                            #region فرم تغییر نقش دانشجویان
                            case 3:
                                changeStudent:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine("\nPlease select student for change role\n");
                                List<Student> studentList = dbChangeRole.Students.ToList();
                                foreach (Student student in studentList)
                                {
                                    Console.WriteLine($"\tid: {student.UserId}\tcode: {student.StudentCode}\tname: {student.Name}\tfamily: {student.Family}\n" +
                                                      $"\tphone:  {student.PhoneNumber}\tdegree: {student.Degree}\tactive: {student.IsActive}");
                                    Console.WriteLine("\t----------------------------------------------------------------");
                                }
                                Console.Write("\nstudent id is: ");
                                int ChangeStudentRole;
                                while (!int.TryParse(Console.ReadLine(), out ChangeStudentRole))
                                {
                                    PrintWarningJustNumber("student id");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    foreach (Student student in studentList)
                                    {
                                        Console.WriteLine($"\tid: {student.UserId}\tcode: {student.StudentCode}\tname: {student.Name}\tfamily: {student.Family}\n" +
                                                          $"\tphone:  {student.PhoneNumber}\tdegree: {student.Degree}\tactive: {student.IsActive}");
                                        Console.WriteLine("\t----------------------------------------------------------------");
                                    }
                                    Console.Write("\nstudent id is: ");
                                }
                                Student selectStudentForChangeRole = dbChangeRole.Students.Find(ChangeStudentRole);
                                if (selectStudentForChangeRole != null)
                                {
                                    goto selectObjectForChangeStudentRole;
                                }
                                else
                                {
                                    PrintDateTimeAndUserStatistic();
                                    PrintWarningNotFoundId("Student List", ChangeStudentRole);
                                    goto changeStudent;
                                }
                                selectObjectForChangeStudentRole:
                                PrintDateTimeAndUserStatistic();
                                Console.WriteLine($"\nPlease select object for student: {selectStudentForChangeRole.Name} {selectStudentForChangeRole.Family}\n");
                                Console.WriteLine("\t\t1. Employee" +
                                                "\n\t\t2. Master");
                                Console.Write("\nInter your item: ");
                                int newObjectForChangeStudent;
                                while (!int.TryParse(Console.ReadLine(), out newObjectForChangeStudent))
                                {
                                    PrintWarningJustNumber("Select item of menu");
                                    Console.Clear();
                                    PrintDateTimeAndUserStatistic();
                                    Console.WriteLine($"\nPlease select object for student: {selectStudentForChangeRole.Name} {selectStudentForChangeRole.Family}\n");
                                    Console.WriteLine("\t\t1. Employee" +
                                                    "\n\t\t2. Master");
                                    Console.Write("\nEnter your item: ");
                                }
                                switch (newObjectForChangeStudent)
                                {
                                    case 1:
                                        dbChangeRole.Employees.Add(new Employee("", 0, selectStudentForChangeRole.Name, selectStudentForChangeRole.Family, selectStudentForChangeRole.PhoneNumber, selectStudentForChangeRole.Password, dbChangeRole.Roles.Find(1)));
                                        dbChangeRole.Students.Remove(selectStudentForChangeRole);
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine($"\n\tRole of {selectStudentForChangeRole.Name} {selectStudentForChangeRole.Family} was change to employee\n");
                                        goto finishChangeRole;

                                    case 2:
                                        dbChangeRole.masters.Add(new Master("", 0, selectStudentForChangeRole.Name, selectStudentForChangeRole.Family, selectStudentForChangeRole.PhoneNumber, selectStudentForChangeRole.Password, dbChangeRole.Roles.Find(2)));
                                        dbChangeRole.Students.Remove(selectStudentForChangeRole);
                                        PrintDateTimeAndUserStatistic();
                                        Console.WriteLine($"\n\tRole of {selectStudentForChangeRole.Name} {selectStudentForChangeRole.Family} was change to master\n");
                                        goto finishChangeRole;

                                    default:
                                        goto selectObjectForChangeStudentRole;
                                }
                                #endregion
                        }
                        finishChangeRole:
                        dbChangeRole.SaveChanges();
                        Console.Write("\npress any key to return to menu");
                        Console.ReadKey();
                        goto menu;
                    }

                    #endregion

                #region خروج از داشبورد (exit to menu)

                    Console.Clear();
                    PrintDateTimeAndUserStatistic();
                    Console.WriteLine($"\n\tHave a good time {adminName}");
                    Thread.Sleep(4000);
                    Console.WriteLine($"\tBye");
                    Thread.Sleep(2000);
                    goto Login;
                #endregion

                default:
                    goto menu;




            }

        }
     }
}
