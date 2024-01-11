namespace Models
{
    interface IEmployee
    {
        void AcceptDetails(string empid, string empname, float salary, DateTime doj);

        void DisplayDetails();

        float CalculateSalary ();
    }
}