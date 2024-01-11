namespace Models
{
    class Permanent:Employee, IEmployee
    {
        public float Basicpay { get; set; }
        public float HRA { get; set; }
        public float DA { get; set; }
        public float PF { get; set; }

        public void AcceptDetails(string empid, string empname, float salary, DateTime doj)
        {
            Empid = empid;
            Empname = empname;
            Doj = doj;
            Salary = salary;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Empid = {Empid}, Empname = {Empname}, Salary = {Salary}, Doj = {Doj}, Basicpay = {Basicpay}, HRA = {HRA}, DA = {DA}, PF = {PF}");
        }

        public void GetDetails(string empid, string empname, DateTime doj, float basicpay, float hra, float da, float pf)
        {
            AcceptDetails(empid, empname, 0, doj);
            Basicpay = basicpay;
            HRA = hra;
            DA = da;
            PF = pf;
            Salary = CalculateSalary();
        }

        public void ShowDetails()
        {
            Console.WriteLine($"Empid = {Empid}, Empname = {Empname}, Salary = {Salary}, Doj = {Doj}, Basicpay = {Basicpay}, HRA = {HRA}, DA = {DA}, PF = {PF}");
        }

        public float CalculateSalary()
        {
            return Basicpay + HRA + DA - PF;
        }
    }
}