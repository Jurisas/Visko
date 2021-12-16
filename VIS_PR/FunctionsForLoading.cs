using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace VIS_PR
{
    public static class FunctionsForLoading
    {
        private static readonly List<Patients> PatientsList = new List<Patients>();
        private static readonly List<Departments> DepartmentsList = new List<Departments>();
        private static readonly List<Doctors> DoctorsList = new List<Doctors>();
        private static readonly List<Operations> OperationsList = new List<Operations>();
        private static User _activeUser;
        public static void StartMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Hospital IS");
                Console.WriteLine("For login click (a)");
                Console. ForegroundColor = ConsoleColor.White;
                var info = Console.ReadLine();
                switch (info)
                {
                    case "a":
                        Login();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong answer");
                        continue;
                }
                break;
            }
        }

        private static void Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            //Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("Hospital IS");
            Console.WriteLine("Write name");
            Console.ForegroundColor = ConsoleColor.White;
            var name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Write password");
            Console.ForegroundColor = ConsoleColor.White;
            var password = Console.ReadLine();
            _activeUser = UserMapper.Find(name, password);
            if (_activeUser.Id == -1)
            {
                while (true)
                {
                    Console.Clear(); 
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Wrong username or password. Try it again?"); 
                    Console.WriteLine("Type (y) for yes or (n) for no"); 
                    Console.ForegroundColor = ConsoleColor.White; 
                    var info = Console.ReadLine(); 
                    switch (info) 
                    { 
                        case "y": 
                            Login(); 
                            break;
                        case "n": 
                            StartMenu(); 
                            break;
                        default: 
                            Console.ForegroundColor = ConsoleColor.Red; 
                            Console.WriteLine("Wrong answer"); 
                            continue;
                    }
                }
            }
            else 
            {
                ChooseMenu();
            }
        }

        private static void ChooseMenu()
        {
            if (_activeUser.IsAdmin == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Logged in");
                MainMenuAdmin();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Logged in");
                MainMenu();
            }
        }

        private static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Hospital IS");
                Console.WriteLine("(a) Add patient");
                Console.WriteLine("(b) Search patient");
                Console.WriteLine("(c) Choose patient");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(d) Show my login info");
                Console.WriteLine("(e) Edit my password");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("(f) Logout");
                Console.ForegroundColor = ConsoleColor.White;
                var info = Console.ReadLine();
                switch (info)
                {
                    case "a":
                        AddingPatient();
                        break;
                    case "b":
                        SearchPatient();
                        break;
                    case "c":
                        ChoosePatient();
                        break;
                    case "d":
                        ChoosePatient();
                        break;
                    case "e":
                        EditPassword();
                        break;
                    case "f":
                        StartMenu();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong answer");
                        continue;
                }
                break;
            }
        }
        
        
        private static void MainMenuAdmin()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Hospital IS");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("(a) Add patient");
                Console.WriteLine("(b) Search patient");
                Console.WriteLine("(c) Choose patient");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("(d) Add doctor");
                Console.WriteLine("(e) Search doctor");
                Console.WriteLine("(f) Choose doctor");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("(g) Add department");
                Console.WriteLine("(h) Search department");
                Console.WriteLine("(i) Choose department");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("(j) Show my login info");
                Console.WriteLine("(k) Edit my password");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("(l) Logout");
                Console.ForegroundColor = ConsoleColor.White;
                var info = Console.ReadLine();
                switch (info)
                {
                    case "a":
                        AddingPatient();
                        break;
                    case "b":
                        SearchPatient();
                        break;
                    case "c":
                        ChoosePatient();
                        break;
                    //Doctors
                    case "d":
                        AddingDoctor();
                        break;
                    case "e":
                        SearchDoctor();
                        break;
                    case "f":
                        ChooseDoctor();
                        break;
                    //Departments
                    case "g":
                        AddingDepartment();
                        break;
                    case "h":
                        SearchDepartment();
                        break;
                    case "i":
                        ChooseDepartment();
                        break;
                    case "j":
                        ChooseDepartment();
                        break;
                    case "k":
                        EditPassword();
                        break;
                    //LogOut
                    case "l":
                        StartMenu();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong answer");
                        continue;
                }
                break;
            }
        }

        private static void EditPassword()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Write your new password");
            Console.ForegroundColor = ConsoleColor.White;
            var info = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Repeat your new password");
            Console.ForegroundColor = ConsoleColor.White;
            var info2 = Console.ReadLine();
            if (info == info2)
            {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Save new password?\nyes -> (y) no -> (n)");
                    Console.ForegroundColor = ConsoleColor.White;
                    info = Console.ReadLine();
                    switch (info)
                    {
                        case "y":
                            _activeUser.Password = info2;
                            UserMapper.Update(_activeUser);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                            break;
                        case "n":
                            ChooseMenu();
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong answer");
                }
            }
        }

        private static void SearchPatient()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hospital IS");
            Console.WriteLine("Type Patient name");
            Console.ForegroundColor = ConsoleColor.White;
            var info = Console.ReadLine();
            PatientMapper.FindByName(info);
            Console.ReadKey();
            ChooseMenu();
        }
        
        private static void SearchDoctor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hospital IS");
            Console.WriteLine("Type Doctor name");
            Console.ForegroundColor = ConsoleColor.White;
            var info = Console.ReadLine();
            DoctorMapper.FindByName(info);
            Console.ReadKey();
            ChooseMenu();
        }
        
        private static void SearchDepartment()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hospital IS");
            Console.WriteLine("Type Doctor name");
            Console.ForegroundColor = ConsoleColor.White;
            var info = Console.ReadLine();
            DepartmentMapper.FindByName(info);
            Console.ReadKey();
            ChooseMenu();
        }

        private static void ChoosePatient()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hospital IS");
            Console.WriteLine("Type ID of Patient");
            Console.ForegroundColor = ConsoleColor.White;
            var id = int.Parse(Console.ReadLine() ?? string.Empty);
            
            if (PatientMapper.CanFind(id))
            {
                bool exist = false;
                foreach (var d in PatientsList)
                {
                    if (d.PatientId == id)
                    {
                        exist = true;
                    }
                }

                if (!exist)
                {
                    PatientsList.Add(PatientMapper.Find(id));
                }

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Work with this patient?"); 
                    Console.WriteLine("yes->(y) no->(n)"); 
                    Console.ForegroundColor = ConsoleColor.White;
                    var info = Console.ReadLine(); 
                    switch (info) 
                    { 
                        case "y": 
                            EditPatient(id); 
                            break;
                        case "n": 
                            ChooseMenu(); 
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong Answer");
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This patient doesnt exist");
            ChooseMenu();
        }
        
        private static void ChooseDoctor()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hospital IS");
            Console.WriteLine("Type ID of Doctor");
            Console.ForegroundColor = ConsoleColor.White;
            var id = int.Parse(Console.ReadLine() ?? string.Empty);
            if (DoctorMapper.CanFind(id))
            {
                bool exist = false;
                foreach (var d in DoctorsList.Where(d => d.DoctorId == id))
                {
                    if (d.DoctorId == id)
                    {
                        exist = true;
                    }
                }

                if (!exist)
                {
                    DoctorsList.Add(DoctorMapper.Find(id));
                }

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Work with this doctor?"); 
                    Console.WriteLine("yes->(y) no->(n)"); 
                    Console.ForegroundColor = ConsoleColor.White;
                    var info = Console.ReadLine(); 
                    switch (info) 
                    { 
                        case "y": 
                            EditDoctor(id); 
                            break;
                        case "n": 
                            ChooseMenu(); 
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong Answer");
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This doctor doesnt exist");
            ChooseMenu();
        }
        
        private static void ChooseDepartment()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hospital IS");
            Console.WriteLine("Type ID of Department");
            Console.ForegroundColor = ConsoleColor.White;
            var id = int.Parse(Console.ReadLine() ?? string.Empty);
            if (DepartmentMapper.CanFind(id))
            {
                bool exist = false;
                foreach (var d in DepartmentsList.Where(d => d.DepartmentId == id))
                {
                    if (d.DepartmentId == id)
                    {
                        exist = true;
                    }
                }

                if (!exist)
                {
                    DepartmentsList.Add(DepartmentMapper.Find(id));
                }

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Work with this department?"); 
                    Console.WriteLine("yes->(y) no->(n)"); 
                    Console.ForegroundColor = ConsoleColor.White;
                    var info = Console.ReadLine(); 
                    switch (info) 
                    { 
                        case "y": 
                            EditDepartment(id); 
                            break;
                        case "n": 
                            ChooseMenu(); 
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong Answer");
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This department doesnt exist");
            ChooseMenu();
        }
    

        private static void EditPatient(int id)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("(a) Firstname");
                Console.WriteLine("(b) Lastname");
                Console.WriteLine("(c) Date Birth");
                Console.WriteLine("(d) Info");
                Console.WriteLine("(e) Doctor");
                Console.WriteLine("(f) To add operation");
                Console.WriteLine("(g) Save to file patient operations");
                Console.WriteLine("(h) Show patient operations");
                Console.WriteLine("(i) Go back");
                Console.ForegroundColor = ConsoleColor.White;
                var info = Console.ReadLine();
                switch (info)
                {
                    case "a":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        info = Console.ReadLine();
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            d.Firstname = info;
                            PatientMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }

                        break;
                    case "b":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        info = Console.ReadLine();
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            d.Lastname = info;
                            PatientMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }

                        break;
                    case "c":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        info = Console.ReadLine();
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            d.DateBirth = info;
                            PatientMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }

                        break;
                    case "d":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        info = Console.ReadLine();
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            d.Info = info;
                            PatientMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }

                        break;
                    case "e":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        var docId = Console.ReadLine();
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            if (docId != null) d.Doctor = DoctorMapper.Find(int.Parse(docId));
                            PatientMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }
                        break;
                    case "f":
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            AddOperation(d.PatientId);
                            ChooseMenu();
                        }

                        break;
                    case "g":
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            PrintOperations(d.PatientId);
                        }
                        break;
                    case "h":
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            Console.WriteLine(d.Firstname + " " + d.Lastname + ":");
                        }
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            OperationMapper.ShowOperationsByPatient(d);
                        }

                        Console.ReadKey();
                        ChooseMenu();
                        break;
                    case "i":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Going back...");
                        ChooseMenu();
                        break;
                        
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WrongAnswer");
            }
            // ReSharper disable once FunctionNeverReturns
        }
        
        private static void EditDoctor(int id)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("(a) Firstname");
                Console.WriteLine("(b) Lastname");
                Console.WriteLine("(c) Date Birth");
                Console.WriteLine("(d) Department");
                Console.WriteLine("(e) Go back");
                Console.ForegroundColor = ConsoleColor.White;
                var info = Console.ReadLine();
                switch (info)
                {
                    case "a":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        info = Console.ReadLine();
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            d.Firstname = info;
                            PatientMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }

                        break;
                    case "b":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        info = Console.ReadLine();
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            d.Lastname = info;
                            PatientMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }

                        break;
                    case "c":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        info = Console.ReadLine();
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            d.DateBirth = info;
                            PatientMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }
                        break;
                    case "d":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        var depId = Console.ReadLine();
                        foreach (var d in DoctorsList.Where(d => d.DoctorId == id))
                        {
                            if (depId != null) d.Department = DepartmentMapper.Find(int.Parse(depId));
                            DoctorMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }
                        break;
                    case "e":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Going back...");
                        ChooseMenu();
                        break;
                        
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WrongAnswer");
            }
            // ReSharper disable once FunctionNeverReturns
        }
        
        private static void EditDepartment(int id)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("(a) Change name");
                Console.WriteLine("(b) Go back");
                Console.ForegroundColor = ConsoleColor.White;
                var info = Console.ReadLine();
                switch (info)
                {
                    case "a":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Write");
                        Console.ForegroundColor = ConsoleColor.White;
                        info = Console.ReadLine();
                        foreach (var d in DepartmentsList.Where(d => d.DepartmentId == id))
                        {
                            d.Name = info;
                            DepartmentMapper.Update(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }

                        break;
                    case "b":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Going back...");
                        ChooseMenu();
                        break;

                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("WrongAnswer");
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void AddingPatient()
        {
            Console.Clear(); string fname; string lname; string info; string date; string doctor;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Patient firstname");
                Console.ForegroundColor = ConsoleColor.White;
                fname = Console.ReadLine();
                if (fname != null && Regex.IsMatch(fname, @"^[a-zA-Z]+$"))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong name");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Patient lastname");
                Console.ForegroundColor = ConsoleColor.White;
                lname = Console.ReadLine();
                if (lname != null && Regex.IsMatch(lname, @"^[a-zA-Z]+$"))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong name");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Patient information");
                Console.ForegroundColor = ConsoleColor.White;
                info = Console.ReadLine();
                if (info != null)
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong info");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Patient birth date. Format: YYYY-MM-DD");
                Console.ForegroundColor = ConsoleColor.White;
                date = Console.ReadLine();
                if (date != null)
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong info");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type doctor id");
                Console.ForegroundColor = ConsoleColor.White;
                doctor = Console.ReadLine();
                if (doctor != null &&  DoctorMapper.Find(int.Parse(doctor)) != null)
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong info");
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Do you want create new patient?");
                Console.WriteLine("(y)-> yes (n) -> no");
                Console.ForegroundColor = ConsoleColor.White;
                var letter = Console.ReadLine();
                switch (letter)
                {
                    case "y":
                        var id = PatientMapper.NextIncrement();
                        PatientsList.Add(new Patients
                        {
                            PatientId = id,
                            Firstname = fname, Lastname = lname, DateBirth = date, Info = info,
                            Doctor = DoctorMapper.Find(int.Parse(doctor))
                        });
                        foreach (var d in PatientsList.Where(d => d.PatientId == id))
                        {
                            PatientMapper.Insert(d);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }
                        break;
                    case "n":
                        ChooseMenu();
                        break;
                }
            }

            // ReSharper disable once FunctionNeverReturns
        }
        
        private static void AddingDoctor()
        {
            Console.Clear(); string fname; string lname; string date; string department;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Patient firstname");
                Console.ForegroundColor = ConsoleColor.White;
                fname = Console.ReadLine();
                if (fname != null && Regex.IsMatch(fname, @"^[a-zA-Z]+$"))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong name");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Patient lastname");
                Console.ForegroundColor = ConsoleColor.White;
                lname = Console.ReadLine();
                if (lname != null && Regex.IsMatch(lname, @"^[a-zA-Z]+$"))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong name");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Patient birth date. Format: YYYY-MM-DD");
                Console.ForegroundColor = ConsoleColor.White;
                date = Console.ReadLine();
                if (date != null)
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong info");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type department id");
                Console.ForegroundColor = ConsoleColor.White;
                department = Console.ReadLine();
                if (department != null &&  DepartmentMapper.Find(int.Parse(department)) != null)
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong info");
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Do you want create new doctor?");
                Console.WriteLine("(y)-> yes (n) -> no");
                Console.ForegroundColor = ConsoleColor.White;
                var letter = Console.ReadLine();
                switch (letter)
                {
                    case "y":
                        var id = DoctorMapper.NextIncrement();
                        DoctorsList.Add(new Doctors
                        {
                            DoctorId = id,
                            Firstname = fname, Lastname = lname, DateBirth = date,
                            Department = DepartmentMapper.Find(int.Parse(department))
                        });
                        foreach (var d in DoctorsList.Where(d => d.DoctorId == id))
                        {
                            DoctorMapper.Insert(d);
                            UserMapper.Insert(new User(){Username = d.Firstname.ToLower() + "."+ d.Lastname.ToLower(),Password = d.Firstname.ToLower() + "."+ d.Lastname.ToLower(),Doctor = d, IsAdmin = 0});
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }
                        break;
                    case "n":
                        ChooseMenu();
                        break;
                }
            }

            // ReSharper disable once FunctionNeverReturns
        }

        private static void AddingDepartment()
        {
            Console.Clear(); 
            string name;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Department name");
                Console.ForegroundColor = ConsoleColor.White;
                name = Console.ReadLine();
                if (name != null && Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong name");
            }
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Do you want create new department?");
                Console.WriteLine("(y)-> yes (n) -> no");
                Console.ForegroundColor = ConsoleColor.White;
                var letter = Console.ReadLine();
                switch (letter)
                {
                    case "y":
                        var id = DepartmentMapper.NextIncrement();
                        DepartmentsList.Add(new Departments
                        {
                            DepartmentId = id,
                            Name = name
                        });
                        foreach (var d in DepartmentsList.Where(d => d.DepartmentId == id))
                        {
                            DepartmentMapper.Insert(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }

                        break;
                    case "n":
                        ChooseMenu();
                        break;
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }


        private static void AddOperation(int id)
        {
            Console.Clear();
            string name; string info; string date; string doctor;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Name of operation");
                Console.ForegroundColor = ConsoleColor.White;
                name = Console.ReadLine();
                if (name != null && Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong name");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Operation information");
                Console.ForegroundColor = ConsoleColor.White;
                info = Console.ReadLine();
                if (info != null)
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong info");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type Operation date. Format: YYYY-MM-DD");
                Console.ForegroundColor = ConsoleColor.White;
                date = Console.ReadLine();
                if (date != null)
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong info");
            }
            
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Type doctor id");
                Console.ForegroundColor = ConsoleColor.White;
                doctor = Console.ReadLine();
                if (doctor != null &&  DoctorMapper.Find(int.Parse(doctor)) != null)
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong info");
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Do you want create new operation?\n(y)-> yes (n) -> no");
                Console.ForegroundColor = ConsoleColor.White;
                var letter = Console.ReadLine();
                switch (letter)
                {
                    case "y":
                        var idOperation = Operations.NextIncrement();
                        OperationsList.Add(new Operations
                        {
                            OperationId = idOperation,
                            Name = name, Date = date, Info = info,
                            Doctor = DoctorMapper.Find(int.Parse(doctor)), 
                            Patient = PatientMapper.Find(id)
                        });
                        foreach (var d in OperationsList.Where(d => d.OperationId == idOperation))
                        {
                            Operations.Insert(d);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Success");
                            ChooseMenu();
                        }
                        break;
                    case "n":
                        ChooseMenu();
                        break;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Wrong Answer");
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private static void PrintOperations(int id)
        {
            foreach (var p in PatientsList.Where(p => p.PatientId == id))
            {
                var operationList = new OperationList{OperationsList = OperationMapper.GetOperationsByPatient(p)};
                operationList.Serializer();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success");
                ChooseMenu();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error");
            ChooseMenu();
        }
    }
}