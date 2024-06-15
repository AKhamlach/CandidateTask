This project uses Microsoft SqlServer
You also must install Microsoft.Data.SqlClient
To start the solution you must have a DB named CandidatesAssessement.

The following script is needed to create the table 

CREATE TABLE Candidates (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(20) NULL,
    Email NVARCHAR(100) NOT NULL,
    PreferedCallInterval NVARCHAR(100) NULL,
    LinkedInProfile NVARCHAR(255) NULL,
    GitHubProfile NVARCHAR(255) NULL,
    Comment NVARCHAR(1500) NULL
);


