# HumanRegisterWebApi
TASK: 

User must be able to register.

After registration, a User is created with the default role 'User'.

The user must be able to generate information about himself, in which ALL fields are mandatory (Personal information), the user must not be able to generate information about more
not a single person.

There must be different endpoints to update EACH of the fields, for example: Name, social security code, phone number, city (cannot update to an empty field or
whitespace)

When registering a person, it must be mandatory to upload a profile photo, its size must be reduced to 200x200 (if the photo is too small, it will be stretched to
200x200).

It must be possible to get all the information about the uploaded person according to his ID (the photo is returned as a byte array).

The user must not be able to update information other than his own, for the sake of convenience, let's say that with each request "from the frontend" the User's ID will come.

There must also be an 'Admin' role, which will be determined through the database and it will have an endpoint through which it can delete the user by ID (deleting the user deletes and
human info)

Offline should only be able to register and log in

Authentication and Authorization is done with Json Web Tokens.

Sql server database is used.

Entity Framework is used.

![image](https://github.com/karka1234/HumanRegisterWebApi/assets/5184302/b5094178-02d1-49b4-a3e5-e366c6e9fd8a)
