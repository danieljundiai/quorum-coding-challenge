1. Discuss your strategy and decisions implementing the application. Please, considertime complexity, effort cost, technologies used and any other variable that you understand important on your development process.
I chose to use .net Core on the backend and react on the front end as they are two technologies that I have a lot of experience with, and they perfectly met the project's requirements. They are quick, simple to implement, and have many professionals available on the market.

2. How would you change your solution to account for future columns that might be requested, such as “Bill Voted On Date” or “Co-Sponsors”?
You would just have to create the columns in the data source (in this case, in the CSV file) and add them to the model in the quorum-data project. This way, the column would be available for use in all projects that reference quorum-data.

3. How would you change your solution if instead of receiving CSVs of data, you were given a list of legislators or bills that you should generate a CSV for?
If this data update were frequent, I would create functionality to process this data and include it in the existing CSV. I have even created the update, insert and delete methods for new data in quorum-data. It wasn't the most efficient way (I rewrote the entire CSV) but given the time available to implement the project, it met the requirement.

4. How long did you spend working on the assignment?
I spent 3 hours on this project, including this documentation and quorum-data unit tests.
