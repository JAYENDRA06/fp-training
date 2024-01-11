using Models;

Permanent e1 = new ();
e1.GetDetails("E001", "Jayendra", DateTime.Now, 1000000, 200, 300, 4000);
e1.DisplayDetails();


Trainee e2 = new();
e2.GetTraineeDetails("E002", "Abhinandita", DateTime.Now, "Banking", 20000);
e2.DisplayDetails();

Trainee e3 = new();
e3.GetTraineeDetails("E003", "Jay", DateTime.Now, "Insurance", 50000);
e3.DisplayDetails();