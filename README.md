**Chronos Chat API**
====
 Currently hosted at https://chronoschat.co. <br />
 The server is currently graded A+ on <a href="https://www.ssllabs.com/ssltest/analyze.html?d=chronoschat.co">Qualys SSL Labs</a>.
 
====

**Register User**
----

  Returns registration status.

* **URL**

  /reg_user

* **Method:**

  `POST`
  
*  **URL Params**

   **Required:**

   None

* **Data Params**
  * **Body:**
   `{"email":"string", "password":"string", "password_comfirmation":"string"}`

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
  "status":
    "Success"
}`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{
  "error": 
    "Invalid information"
}`

**Authorize User**
----

  Returns JWT status.

* **URL**

  /auth_user

* **Method:**

  `POST`
  
*  **URL Params**

   **Required:**
 
   None

* **Data Params**
  * **Body:**
  `{"email":"string", "password":"string"}`

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
  "auth_token": "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjozHAiOjE0Nzc2MzQ1NjB9.0KhBGDEjt5DIS_qbRm8E",
  "user": {
    "id": x,
    "email": "x@x.am"
  }
}`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{
  "error":
    "Invalid Username/Password"
}`

**Home**
----

  Returns logged in status.

* **URL**

  /home

* **Method:**

  `GET`
  
*  **URL Params**

   **Required:**
 
   None

* **Data Params**
  * **Headers:** <br />
  Content-Type - application/json<br />
  Authorization - Valid JWT

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
  "logged_in": true
}`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{
  "error": 
    "Not Authenticated"
}`

**Conversations**
----

  Shows a list of active users the user can initiate a conversation with.

* **URL**

  conversations/index

* **Method:**

  `GET`
  
*  **URL Params**

   **Required:**

   None

* **Data Params**
  * **Headers:** <br />
  Content-Type - application/json<br />
  Authorization - Valid JWT

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `[
  "xy@z.com",
  "xy@z.com", 
  "...",
  "xy@z.com"
]`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{
  "error": "Not Authenticated"
}`

**Create Conversation**
----

  Creates a conversation between sender and receiver.

* **URL**

  conversations/create

* **Method:**

  `POST`
  
*  **URL Params**

   **Required:**

   None

* **Data Params**
  * **Headers:** <br />
  Content-Type - application/json<br />
  Authorization - Valid JWT
  * **Body:** 
  {"recipient_email":"xy@z.com"}

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
  "id": 4235
}`
 
* **Error Response:**

  * **Code:** 500 <br />
    **Content:** `NULL`

**Get Messages**
----

  Shows a list of unread messages.

* **URL**

  /messages/index

* **Method:**

  `GET`
  
*  **URL Params**

   **Required:** <br />

   ?conversation_id=(valid conversation ID)

* **Data Params**
  * **Headers:** <br />
  Content-Type - application/json<br />
  Authorization - Valid JWT

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `[ Messages ]`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{
  "error": "Not Authenticated"
}`


**Create Message**
----

  Creates a message for a valid conversation.

* **URL**

  messages/create

* **Method:**

  `POST`
  
*  **URL Params**

   **Required:**

   None

* **Data Params**
  * **Headers:** <br />
  Content-Type - application/json<br />
  Authorization - Valid JWT
  * **Body:** 
  {"body": "string", "conversation_id": "string"}

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
  "status": "Message Sent"
}`
 
* **Error Response:**

  * **Code:** 404 Not Found <br />
    **Content:** `{
  "status": "error"}`