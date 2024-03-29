The API and Data Access Layer was developed using .net Core net8.0, and the front-end application using react 18.2. 
The following steps consider that you already have both versions installed.

STEPS using UBUNTU via Terminal
3 - Go to the quorum-api folder
4 - execute the command "dotnet build"
5 - execute de command "dotnet run". The api server will run on the https port 5001. Test accessing https://localhost:5001/swagger/index.html
6 - Go to the quorum-web folder
7 - Run the command "npm install" to download all the dependencies.
8 - Run the command "npm start" to run the app on the port 3000. 

STEPS to run the quorum-data.tests
1 - Go to the quorum-data\quorum-data.tests folder
2 - run "dotnet build".
3 - run "dotnet test".
