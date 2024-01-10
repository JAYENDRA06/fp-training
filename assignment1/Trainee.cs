namespace Models
{
    class Trainee : Employee
    {
        public float Bonus { get; set; }
        public string? Projectname { get; set; }

        public void GetTraineeDetails(string empid, string empname, DateTime doj, string projectname, float salary)
        {
            Empid = empid;
            Empname = empname;
            Doj = doj;
            Projectname = projectname;
            Salary = salary;
            Salary += CalculateSalary();
        }

        public void ShowTraineeDetails()
        {
            Console.WriteLine($"Empid = {Empid}, Empname = {Empname}, Salary = {Salary}, Doj = {Doj}, Bonus = {Bonus}, ProjectName = {Projectname}");
        }

        public override float CalculateSalary()
        {
            if (Projectname == "Banking") Bonus = Salary / 20;
            else if (Projectname == "Insurance") Bonus = Salary / 10;

            return Bonus;
        }
    }
}