# SnrDevExercise2

Some Information For Defect Resolution Exercise:

•	**The Bonus Allocation algorithm has been copied from a legacy system.  There have been reports that the money assigned to pay out for bonuses is not all being paid out.  Find the source of the problem and fix the algorithm.**
1) Employees were getting removed while adding recipients to the allocation list - so this is now fixed and commented the code accordingly
2) There is a possibility of divide by zero error which needs validation check if value for OneInXEmployees is entered zero in front-end. 
   I have not fixed this as assumption is made that this was not in the requirement.

•	**The Bonus Allocation submission is vulnerable to attacks from unauthorized locations, please fix that**
AntiForgeryToken token is now added to the allocate action method and made sure that the same is there in the UI as well from the place where this action method is being called.

•	**When errors occur on the web service, too many details are being displayed.  Ensure appropriate http status codes are returned with sensible error messages and that internal details are not returned to the caller.**
Unit tests are written to cover all the possible failed scenarios and made sure it returns some sensible error message.

**Additional Information for web API post method**:
1) TDD approach was followed which can be confirmed through git commit history - local git files will be included to view history but packages/bin folders will be deleted to keep the zip file small.
2) Unity is used for dependency injection
3) Fluent validation is used for model validation
4) Moq is used for mocking the data
5) Assumption for this test is made that unit tests will be written only for RemunerationController and not for the EmployeeController
6) xml file is written in app_data folder for web api project with the name of EmployeeBonus.xml_
7) Assumption is made the date field written in the EmployeeBonus.xml will be the system current date with time