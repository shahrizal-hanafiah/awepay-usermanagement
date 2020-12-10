# awepay-usermanagement

# USER API Requirements
Create a REST endpoint that allows creation of new User and returns the user id when created.

## The User resource will have following fields:-
### Full Name (Required)
### Email (required)
### Phone (optional)
### Age (optional)

- Create a REST endpoint that allows us to update user information
- Create a REST endpoint that allows us to delete a user
- Create a REST endpoint that allows us to search all users by email and phone and provide sorting order by field

The results should be sorted by the given field.

# Answer:

## 1. To create user:
API: {{url}}/account/register
Method: GET
Payload: 
{
    "FullName": "string",
    "Email": "string",
    "Phone": "string",
    "DateOfBirth": "2020-10-12T00:00:00.000"
}

## 2. Login 
API: {{url}}/account/login
Method: POST
Payload: 
{
    "Email":"string",
    "Password":"P@$$w0rd"
}

## 3. Update user 
API: {{url}}/users
Method: PUT
Header: Key = Authorization,Value = {{token}}
Payload: 
{
    "Id":int,
    "FullName":"string",
    "Email":"string",
    "Phone":"string",
    "DateOfBirth":"2020-10-12T00:00:00.000"
}

## 4. Delete user
API: {{url}}/users/{{id}}
Header: Key = Authorization,Value = {{token}}
Method: Delete

## 5. Search all users, filter and sorted
API: {{url}}/users/filterSort
Header: Key = Authorization,Value = {{token}}
Method: GET
Payload: 
{
    "SearchByEmail":"aisyah",
    "SearchByPhone":"",
    "SortBy":"phone"//other option for sorting = fullname,email,phone,dateofbirth,created
}
