# RewardsPlus
Rewards Hub for any org - SAAS multi tenant - API Project - Loyalty integrations - Employee monthly subscriptions

Keywords in Code

OFD - only for development 

AskExperts- ask with experts later

ToDoLater - ok it explains itself

GoodIdea - great ideas 


In progress

// Hang fire to dispatch items - delivery boyservice

// Signar noti on new purchase or order status change

//Employee monthly subscriptions from employerr

// complate ToDoLater stuffs in code

// AskExperts on Archictcture

Done:

// Gift tokens - cashier service

// Buy tokens- cashier service

// Buy Items service - butler service

// Message templates - with transaction history


Multi tenant Use Case: 

1, Need: Employee rewards hub 
2, Employees should be able to claim different kinds of rewards(eg: Amazon gift card) 
  based on available reward tokens
3, Reward tokens can be provided to users by leadership
  of the respective user
4, Tokens can be purchased by admins and allocated to the leaders based on the project 

ToDo:
1, Admin for a tenant to manage roles of users and leadership
2, Super admin for managing all tenents
3, Cart and points distribution
4, Team mode(equal distribution) etc..

Features :

Users (App)
Explore catalog
Buy or gift points
Use points to get items/ merchandise 

Company owner(Panel & App)
Add brand and products to purchase
Gifting with a personalized message
Gift points to users
Add users
View all user's balances for the tenant
View reports
     
 Super admin (Panel)
Default gifting message templates - for mail
View tenant details
Activate and deactivate tenants
All kinds of settings


DEV CONFIG 

Db connect in ssms - (localdb)\MSSQLLocalDB  - windows auth

Any change in Db use following commands
Project migrator.mssql - delete migrations folder for fresh and no cache migration

> add-migration -context ApplicationDbContext "initialTodo"
> update-database -context ApplicationDbContext 
add-migration -context TenantDbContext "initialTodo"
update-database -context TenantDbContext

dotnet ef migrations add initial --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application


Availabe DB Contexts
TenantDbContext

ApplicationDbContext

BaseDbContext

Hidden dbcontext - 
IdentityDbContext

