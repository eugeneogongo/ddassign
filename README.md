# DD Finance Assessment

>This project Demonstrates API used for authentication Purpose using JWT tokens in ASP.Net C# API.<br>
>The Storing of the password Hashes is done using BCrypt more on Bcrypt  [Read Bcrypt](https://en.wikipedia.org/wiki/Bcrypt) <br>
>JWT tokens contains email as a claim for purpose of demonstration
>Used Embedded Database .mdf
### How to Run.

 1. open the DDWebservice.sln 
 2. Press Build 
 3. run the Project.
#### Routes
| Route |HTTP method | Input|
|--|--|--|
|api/account/register  | Post |Email,firstname,familyname,password|
|api/account/login|post|email,password


