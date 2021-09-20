AccountBalance

Steps on how to run the project
1. Run update-database on package manager console.
2. Once the app is running, you may use Postman to run https://localhost:5001/api/getbalanceandpayments. 
3. Go to Authentication tab and enter "testuser" as username and "SOME_ADMIN_PLAIN_PASSWORD" as the password.

Notes:
-I used MediatR for CQRS pattern. I also implemented Vertical slices architecture. So basically all code for a single feature is inside a single file. With this architecture, it's easier to trace the code and lesser coupling.
