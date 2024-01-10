namespace Models
{
    class Employee
    {
        public string? Empid { get; set; }
        public string? Empname { get; set; }
        public float Salary { get; set; }
        public DateTime Doj { get; set; }

        public void AcceptDetails(string empid, string empname, float salary, DateTime doj)
        {
            Empid = empid;
            Empname = empname;
            Salary = salary;
            Doj = doj;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Empid = {Empid}, Empname = {Empname}, Salary = {Salary}, Doj = {Doj}");
        }

        public virtual float CalculateSalary ()
        {
            return Salary;
        }
    }
}