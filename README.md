# CarSee / Aplikasi Bandingin Mobil Api Documentation

**Decision**
----

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
                "price": "int",
                "brand": "string",
                "productionYear": "int",
                "condition": "float",
                "description": "string",
                "mileage": "int"
            }
        ],
        "criteria": {
            "mileage": "int",
            "condition": "float",
            "yearMade": "int",
            "brand": "string",
            "price": "int"
        }
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
            "price": "int",
            "brand": "string",
            "productionYear": "int",
            "condition": "float",
            "description": "string",
            "mileage": "int",
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
    
* **Sample Request:**
  ```json
  {
    "carList": [
      {
        "name": "Yaris",
        "price": 140000000,
        "brand": "Toyota",
        "productionYear": 2011,
        "condition": 0.9,
        "description": "Yaris Test",
        "mileage": 100000
      },
      {
        "name": "Ertiga",
        "price": 120000000,
        "brand": "Suzuki",
        "productionYear": 2011,
        "condition": 0.8,
        "description": "Ertiga Test",
        "mileage": 80000
      },
      {
        "name": "Mobilio",
        "price": 140000000,
        "brand": "Honda",
        "productionYear": 2015,
        "condition": 0.9,
        "description": "Ertiga Test",
        "mileage": 80000
      },
      {
        "name": "Wuling Confero",
        "price": 120000000,
        "brand": "Wuling",
        "productionYear": 2015,
        "condition": 0.95,
        "description": "Wuling Test",
        "mileage": 50000
      },
      {
        "name": "Innova",
        "price": 190000000,
        "brand": "Toyota",
        "productionYear": 2011,
        "condition": 0.85,
        "description": "Innova Test",
        "mileage": 100000
      }
    ],
    "criteria": {
      "mileage": 80000,
      "condition": 0.8,
      "yearMade": 2011,
      "brand": "Toyota",
      "price": 150000000
    }
  }
  ```

=============================================

**Decision**
----


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
                "price": "int",
                "brand": "string",
                "productionYear": "int",
                "condition": float,
                "description": "string",
                "mileage": "int",
                "imageFileName": "string"
            }  
        ],
        "searchParam": "string",
        "pageCount": "int",
        "currentPageIndex": "int"
    }
    ```
 
* **Error Response:**

  ***&lt;To Be Defined&gt;***

  * **Code:** 401 UNAUTHORIZED <br />
    **Content:** `{ error : "Log in" }`

  OR

  * **Code:** 422 UNPROCESSABLE ENTRY <br />
    **Content:** `{ error : "Email Invalid" }`
