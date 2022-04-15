# CarSee / Aplikasi Bandingin Mobil Api Documentation

**Decision**
----
  <_Additional information about your API call. Try to use verbs that match both request type (fetching vs modifying) and plurality (one vs multiple)._>

* **URL**

  /api/decision

* **Method:**
  

  `POST`
  
*  **URL Params**

   N/A

* **Data Params**

    ```json
    {
        "carList": [
            {
                "name": "uuid|not available yet",
                "name": "string",
                "price": int,
                "brand": "string",
                "productionYear": int,
                "condition": float,
                "description": "string",
                "mileage": int
            }
        ],
        "criteria": {
            "mileage": int,
            "condition": float,
            "yearMade": int,
            "brand": "string",
            "price": int
        }
    ```

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** 
    ```json
    [
        {
            "id": "uuid|this is still not returning id. need improvement on backend side",
            "name": "string",
            "price": int,
            "brand": "string",
            "productionYear": int,
            "condition": float,
            "description": "string",
            "mileage": int,
            "imageFileName": "string"
        }
    ]
    ```
 
* **Error Response:**

  ***&lt;To Be Defined&gt;***

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{ error : "Log in" }`

  OR

  * **Code:** 422 UNPROCESSABLE ENTRY <br />
    **Content:** `{ error : "Email Invalid" }`

=============================================

**Decision**
----
  <_Additional information about your API call. Try to use verbs that match both request type (fetching vs modifying) and plurality (one vs multiple)._>

* **URL**

  /api/car

* **Method:**
  

  `GET`
  
*  **URL Params**

   **Optional:**
 
   `page=[int|sart from 1]`

   `pageSize=[int|default=10]`
   
   `carName=[string]`


* **Success Response:**

  * **Code:** 200 <br />
    **Content:** 
    ```json
    {
        "carList": [
            {
                "id": "uuid",
                "name": "string",
                "price": int,
                "brand": "string",
                "productionYear": int,
                "condition": float,
                "description": "string",
                "mileage": int,
                "imageFileName": "string"
            }  
        ],
        "searchParam": "string",
        "pageCount": int,
        "currentPageIndex": int
    }
    ```
 
* **Error Response:**

  ***&lt;To Be Defined&gt;***

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{ error : "Log in" }`

  OR

  * **Code:** 422 UNPROCESSABLE ENTRY <br />
    **Content:** `{ error : "Email Invalid" }`

