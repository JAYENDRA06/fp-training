namespace Models
{
    class Trainee : Employee, IEmployee
    {
        public float Bonus { get; set; }
        public string? Projectname { get; set; }

        public void GetTraineeDetails(string empid, string empname, DateTime doj, string projectname, float salary)
        {
            AcceptDetails(empid, empname, salary, doj);
            Projectname = projectname;
            Salary += CalculateSalary();
        }

        public void ShowTraineeDetails()
        {
            Console.WriteLine($"Empid = {Empid}, Empname = {Empname}, Salary = {Salary}, Doj = {Doj}, Bonus = {Bonus}, ProjectName = {Projectname}");
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Empid = {Empid}, Empname = {Empname}, Salary = {Salary}, Doj = {Doj}, Bonus = {Bonus}, ProjectName = {Projectname}");
        }

        public float CalculateSalary()
        {
            if (Projectname == "Banking") Bonus = Salary / 20;
            else if (Projectname == "Insurance") Bonus = Salary / 10;

            return Bonus;
        }

        public void AcceptDetails(string empid, string empname, float salary, DateTime doj)
        {
            Empid = empid;
            Empname = empname;
            Doj = doj;
            Salary = salary;
        }
    }
}