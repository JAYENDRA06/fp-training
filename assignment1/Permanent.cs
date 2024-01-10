namespace Models
{
    class Permanent:Employee
    {
        public float Basicpay { get; set; }
        public float HRA { get; set; }
        public float DA { get; set; }
        public float PF { get; set; }

        public void GetDetails(string empid, string empname, DateTime doj, float basicpay, float hra, float da, float pf)
        {
            Empid = empid;
            Empname = empname;
            Doj = doj;
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

        public override float CalculateSalary()
        {
            return Basicpay + HRA + DA - PF;
        }
    }
}