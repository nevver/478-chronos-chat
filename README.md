**Chronos Chat API**
====
 Currently hosted at https://chronoschat.co. <br />
 The server is currently graded A+ on <a href="https://www.ssllabs.com/">Qualys SSL Labs</a>.

**API Endpoints**
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
 
   `{"email":"[string]", "password":"[string]", "password_comfirmation":"[string]"}`

* **Data Params**

  None

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
  "status": [
    "Success"
  ]
}`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{
  "errors": [
    "Invalid information"
  ]
}`

**Authorize User**
----

  Returns json token authorization status.

* **URL**

  /auth_user

* **Method:**

  `POST`
  
*  **URL Params**

   **Required:**
 
   `{"email":"[string]", "password":"[string]"}`

* **Data Params**

  None

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
  "auth_token": "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjozLCJleHAiOjE0Nzc2MzQ1NjB9.0KhBGDEjt5axW1uCy7A-kEc5QhhhSwO2LDIS_qbRm8E",
  "user": {
    "id": x,
    "email": "x@x.am"
  }
}`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{
  "errors": [
    "Invalid Username/Password"
  ]
}`

**Home**
----

  Returns json logged in status.

* **URL**

  /home

* **Method:**

  `GET`
  
*  **URL Params**

   **Required:**
 
   None

* **Data Params**

  Content - json
  Authorization - Valid JWT Token

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** `{
  "logged_in": true
}`
 
* **Error Response:**

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{
  "errors": [
    "Not Authenticated"
  ]
}`
