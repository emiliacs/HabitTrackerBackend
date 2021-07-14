# Team Red Backend
*ASP.Net Core(3.1) API with React Native frontend

## What is this?
Application to help users to create habits, with positive reinforcement 

The user creates habit-entry, in which they can specify: 
- How many times habit has to be checked to complete 
- Write down the reward they get once the habit is complete 
- Habits can be completed as a group 
  - All users have their own habit to complete those counts to overall group success 
  - One big habit that all contribute towards 
- Among other variables 

For example, the user wants to get up early every day a month and if they succeed, they get to buy themself a chocolate bar. 


  
## Requirements
- ASP.Net Core(3.1) [Link](https://dotnet.microsoft.com/download/dotnet/3.1)
- PostgresSQL database (Not tested with other SQL datanbase options yet)

### Install
- Clone and make sure requirements are installed.
- REMEMBER to check appsettings.Development.json!
    - Minium
        - DatabaseLogin : EasyLog
        - JWTSettings (AverageLifespan: integers WITHOUT quotemarks)
- If no existing database you can run command on Package Manager Console (Visual Studio 2019) to create an empty database
```
update-database
```


## EndPoints
### User
| Method | URL | Description |
| ------ | ------ | ------ |
| POST | api/users/ | Create user |
| GET | api/users | Get all users |
| GET | api/users/me | Get user with token |
| GET | api/users/{userId:int} | Get user with id |
| GET | /api/users/search?name=value&email=value | get user with name and/or email |
| PATCH | api/users/verify?verificationcode=value| Verify user email |
| PATCH | api/users/{userId:int}  | Edit user profile |
| DELETE | api/users/{userId:int} | Delete user with id |
| POST | api/login | Login user|
| POST | api/logout | Logout user |


### Habit
| Method | URL | Description |
| ------ | ------ | ------ |
| GET | api/habits | Get all habits |
| GET | api/habits/{id:int} | Get habit with id |
| GET | api/habits/user{userId:int} | Get all users habits with id |
| POST | api/habits/ | Make new habit |
| Delete | api/habits/{id:int} | Delete habit with id |


