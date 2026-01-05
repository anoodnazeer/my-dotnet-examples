create database JobPortal_final;
create database JobPortal_Project;


DECLARE @AdminId UNIQUEIDENTIFIER = NEWID();

 --Insert into SystemUsers
INSERT INTO SystemUsers (
    Id,UserName,FirstName,LastName,Phone,Email,Role)
VALUES ( @AdminId,'adminuser','VIJISHA','shikesh','0527162367','vijishikesh@gmail.com',1);

-- Insert into AuthUser (use same @AdminId)
INSERT INTO AuthUser ( Id,Password,ConnectionId,OnlineStatus,JobProviderId)
VALUES ( @AdminId,'Aitrich@123',NULL,0,NULL);


select * from AuthUser;
select * from Location;

INSERT INTO JobCategory (Id, Name, Description)
VALUES
(NEWID(), 'IT', 'IT Dept'),
(NEWID(), 'HR', 'HR Dept'),
(NEWID(), 'Finance', 'Finance Dept');
select * from JobCategory;


DECLARE @LocationId UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM Location);

INSERT INTO JobProviderCompany (Id, LegalName, Email, Address, Summary, Website, Location)
VALUES
(NEWID(), 'Dummy Company', 'dummy@company.com', '123 Street, Abu Dhabi', 'Testing Company', 'www.dummy.com', @LocationId);

select * from JobProviderCompany;
-- Get inserted company ID
DECLARE @CompanyId UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM JobProviderCompany);


INSERT INTO CompanyUser (Id, FirstName, LastName, Role, UserName, Email, Phone, Company)
VALUES
(NEWID(), 'Dummy', 'Admin', 1, 'dummyadmin', 'dummyadmin@test.com', '0501234567', @CompanyId),
(NEWID(), 'Test', 'User', 1, 'testuser', 'testuser@test.com', '0509876543', @CompanyId);

select * from CompanyUser;

DECLARE @LocationId UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM Location);
DECLARE @CategoryId UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM JobCategory);
DECLARE @CompanyId UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM JobProviderCompany WHERE LegalName = 'Dummy Company');
DECLARE @PostedBy UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM CompanyUser WHERE UserName = 'testuser');  -- merged into one


INSERT INTO JobPost
(
    Id, JobTitle, JobSummary, PostedDate,
    LocationId, IndustryId, CategoryId, CompanyId, PostedBy, Status
)
VALUES
-- IT Industry
(NEWID(), 'Soft Dev', 'Develop .NET apps', GETDATE(), 
 'DC0143EC-2BA9-4878-8F96-08666FF53F09', '294A00A4-4356-429E-B003-8AE29248E9AD', 
 @CategoryId, @CompanyId, @PostedBy, 'Pending'),

-- Health Industry
(NEWID(), 'Health Mg', 'Manage hospital ops', GETDATE(), 
 'E6610209-9C55-484D-95CB-3A37CF8EF378', '294A00A4-4356-429E-B003-8AE29248E9AD', 
 @CategoryId, @CompanyId, @PostedBy, 'Pending');

 select * from JobPost;
select * from Industry;
 select * from JobSeekers;
 select * from Skill;
 select * from JobSeekerProfiles;
 select * from JobSeekerProfileSkill;
 select * from WorkExperience;
 select * from SystemUsers;
select * from Location;
select * from Qualification;
select * from JobProviderCompany;
select * from SignUpRequests;
select * from CompanyUser;
select * from GroupMembers;
select * from JobCategory;
SELECT * FROM JobCategory WHERE Id = 'DC5ED686-21ED-4D7C-855B-75F207363609';
select * from JobPost;
SELECT Id, JobTitle FROM JobPost
WHERE Id = '8F641ECE-33A6-43B9-AD74-60E168B2A7E6';


select * from Interviews;


DECLARE @Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO JobCategory (Id, Name, Description)
VALUES (@Id, 'Software Development', 'All developer and programming-related jobs');



