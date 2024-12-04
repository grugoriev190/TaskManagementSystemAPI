# Task management API

The Task Management API allows you to create, edit, delete, and retrieve tasks. Support filtering by status, due date and priority, as well as sorting results.

## Authorization

An authentication token (Bearer Token) is required to access the API. Pass it in the header of each request:

## Endpoints for authorization
### 1. Registration of user
- **Method**: `POST`
- **URL**: `/register`
- **Description**: Creates a new user.
- **Request body**:
  ```json
  {
    "userName":"UserName1",
    "email": "someemail@gmail.com",
    "password": "SomePassword22_" 
  }
  ```

- **Responds:** <br>
  `Success (200 OK):`
  ```json
  {
    "message": "User registred."
  }
  ```
  `Error (400 Bad Request):`
  ```json
  {
    "message": "Username already in use."
  }
  ```
  `Error (400 Bad Request):`
  ```json
  {
    "message": "Password must be at least 8 characters long, contain an uppercase letter, a number, and a special character."
  }
  ```
### 2. Login of user
- **Method**: `POST`
- **URL**: `/login`
- **Description**: Login to account.
- **Request body**:
  ```json
  {
    "identifier":"UserName1",
    "password": "SomePassword22_" 
  }
  ```

- **Responds:** <br>
  `Success (200 OK):`
  ```json
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyTmFtZSI6IlVzZXJOYW1lMSIsImVtYWlsIjoic29tZWVtYWlsQGdtYWlsLmNvbSIsImlkIjoiYTJlMzM2MDUtOTk1OC00MmZhLTgwZGEtNTllM2I5MjI5NjUyIiwiZXhwIjoxNzMzMzI1NjgzfQ.REerW2MWBV_MbeA_tWmOu_f0WjYBACm_Op5VqPVUNeg"
  }
  ```
  `Error (401 Unauthorized):`
  ```json
  {
    "message": "Unauthorized"
  }
  ```
 ### 3. Deleting of user
- **Method**: `DELETE`
- **URL**: `/auth`
- **Description**: Deletes an account.
- **Example URL:**
  ```
  DELETE /auth/97D261E2-EF13-4FCB-9752-2CEAA7D4867F
  ```
- **Responds:** <br>
  `Success (200 OK):`
  ```json
  {
    "message": "Account deleted successfully."
  }
  ```
  `Error (401 Unauthorized):`
  ```json
  {
    "message": "Unauthorized"
  }
  ```
  `Error (401 Unauthorized):`
  ```json
  {
    "message": "You are not allowed to delete this account."
  }
  ```
   `Error (404 Not Found):`
  ```json
  {
    "message": "User not found."
  }
  ```
### 4. Updating of user
- **Method**: `PUT`
- **URL**: `/auth`
- **Description**: Updates an account.
- **Example URL:**
  ```
  DELETE /auth/97D261E2-EF13-4FCB-9752-2CEAA7D4867F
  ```
- **Responds:** <br>
  `Success (200 OK):`
  ```json
  {
    "message": "Account updated successfully."
  }
  ```
  `Error (401 Unauthorized):`
  ```json
  {
    "message": "Unauthorized"
  }
  ```
  `Error (401 Unauthorized):`
  ```json
  {
    "message": "You are not allowed to update this account."
  }
  ```
   `Error (404 Not Found):`
  ```json
  {
    "message": "User not found."
  }
  ```
  
## Endpoints for tasks

### 1. Creating a task
- **Method**: `POST`
- **URL**: `/tasks`
- **Description**: Creates a new task.
- **Request body**:
  ```json
  {
    "title": "Test Task",
    "description": "This is a test task",
    "dueDate": "2024-12-11T18:00:00Z",
    "priority": "High"
  }
  ```

- **Responds:** <br>
  `Success (200 OK):`
  ```json
  {
    "id": "d4f79b91-7a4f-4657-84c2-d5158d1d1f16",
    "title": "Test Task",
    "description": "This is a test task",
    "dueDate": "2024-12-11T18:00:00Z",
    "priority": "High"
  }
  ```
  `Error (400 Bad Request):`
  ```json
    {
      "message": "Invalid priority value. Allowed values: Low, Medium, High."
    }
  ```
  `Error (401 Unauthorized):`
  ```json
    
  ```
### 2. Receiving all tasks
- **Method**: `GET`
- **URL**: `/tasks`
- **Description**: Receives an information about all tasks.
- **Query parameters**:
  ```
  status (optional): Filters tasks by status. Example: status=Pending
  dueDate (optional): Filters by due date. Example: dueDate=2024-12-11
  priority (optional): Filters tasks by priority. Example: priority=High
  sortBy (optional): Parameter for sorting (by default it sorts by execution date). Example: sortBy=Priority
  sortOrder (optional): Sort order. Example: sortOrder=asc or sortOrder=desc
  ```
- **Example URL:**
  ```
  GET /tasks?status=Pending&dueDate=2024-12-11&priority=High&sortBy=DueDate&sortOrder=asc
  ```
- **Responds:** <br>
  `Success (200 OK):`
  ```json
  {
    "id": "d4f79b91-7a4f-4657-84c2-d5158d1d1f16",
    "title": "Test Task",
    "description": "This is a test task",
    "dueDate": "2024-12-11T18:00:00Z",
    "status": 0,
    "priority": 0,
    "createdAt": "2024-12-02T11:33:01.1736865",
    "updatedAt": "2024-12-02T11:33:01.1736866",
    "userId": "97d261e2-ef13-4fcb-9752-2ceaa7d4867f",
    "user": "UserName"
  }
  ```
  `Error (401 Unauthorized):`
  ```json
    
  ```
### 3. Receiving task by ID
- **Method**: `GET`
- **URL**: `/tasks/{taskId}`
- **Description**: Receives an information about the task by ID.
- **Example URL:**
  ```
  GET /tasks/d4f79b91-7a4f-4657-84c2-d5158d1d1f16
  ```
- **Responds:** <br>
  `Success (200 OK):`
  ```json
  {
    "id": "d4f79b91-7a4f-4657-84c2-d5158d1d1f16",
    "title": "Test Task",
    "description": "This is a test task",
    "dueDate": "2024-12-11T18:00:00Z",
    "status": 0,
    "priority": 0,
    "createdAt": "2024-12-02T11:33:01.1736865",
    "updatedAt": "2024-12-02T11:33:01.1736866",
    "userId": "97d261e2-ef13-4fcb-9752-2ceaa7d4867f",
    "user": "UserName"
  }
  ```
  `Error (404 Not Found):`
  ```json
  {
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
    "title": "Not Found",
    "status": 404,
    "traceId": "00-e65b364bf8eacf208b6f28acd1e18095-73352c152ba38b30-00"
  }
  ```
 
  `Error (401 Unauthorized):`
  ```json
  {
    "message": "Unauthorized to update this task."
  }
  ```
### 4. Editing task
- **Method**: `PUT`
- **URL**: `/tasks/{taskId}`
- **Description**: Edits an information about the task by ID.
- **Example URL:**
  ```
  PUT /tasks/d4f79b91-7a4f-4657-84c2-d5158d1d1f16
  ```
- **Request body**:
  ```json
  {
    "title": "Test Task",
    "description": "This is a test task",
    "dueDate": "2024-12-11T18:00:00Z",
    "priority": "High"
  }
  ```
  
- **Responds:** <br>
  `Success (200 OK):`
  ```json
  
  ```
  `Error (400 Bad Request):`
  ```json
   {
      "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "errors": {
          "taskId": [
              "The value 'c2538427-c4d8-46bf-bf7b-588f30ecea4' is not valid."
          ]
      },
      "traceId": "00-8b646ea050a0f887c334bafe63e5f73a-1cb6523ba09ffee8-00"
  }
  ```
  `Error (401 Unauthorized):`
  ```json
  {
    "message": "Unauthorized to update this task."
  }
  ```
### 5. Deleting task
- **Method**: `DELETE`
- **URL**: `/tasks/{taskId}`
- **Description**: Deletes the task by ID.
- **Example URL:**
  ```
  DELETE /tasks/d4f79b91-7a4f-4657-84c2-d5158d1d1f16
  ```
  
- **Responds:** <br>
  `Success (200 OK):`
  ```json
  {
    "message": "Task deleted successfully."
  }
  ```
  `Error (404 Not Found):`
  ```json
   {
    "message": "Task not found."
  }
  ```
  `Error (401 Unauthorized):`
  ```json
  {
    "message": "Unauthorized to delete this task."
  }
  ```

