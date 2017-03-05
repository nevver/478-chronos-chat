**Chronos Chat API**
====
 Currently hosted at https://chronoschat.co. <br />
 The server is currently graded `A+` by <a href="https://www.ssllabs.com/ssltest/analyze.html?d=chronoschat.co">Qualys SSL Labs</a>. <br>
 The client can be found https://github.com/dndo1/auth. <br>
 The improved v2 client can be found https://github.com/nevver/chronosclient. <br>
 The public key distribution API can befound https://github.com/nevver/chronospkd. <br>

====

**Register User**
----

  Returns registration status.

* **URL**

  /registration

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
  "status":
    "Success"
}`

* **Error Response:**

  * **Code:** 400 <br />
    **Content:** `{
  "error":
    "Invalid information"
}`

**Authorize User**
----

  Returns JWT status.

* **URL**

  /authenticate

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

  * **Code:** 400 <br />
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

  * **Code:** 401 <br />
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
  `{"recipient_email":"xy@z.com"}`

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

  * **Code:** 401 <br />
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
  `{"body": "string", "conversation_id":id, "body2": "string", "nc": "string", "nc2": "string", "tag": "string", "tag2": "string", "key": "string", "key2": "string"}`

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
  "status": "Message Sent"
}`

* **Error Response:**

  * **Code:** 400 <br />
    **Content:** `{
  "status": "error"}`
