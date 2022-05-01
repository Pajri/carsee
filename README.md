# CarSee / Aplikasi Bandingin Mobil Api Documentation

## Decision

**Calculate Decision**
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
            "imageFileName": "string",
            "matchingLabel": "string | label of how match the value with criteria (Sangat Cocok, Cocok, Cukup Cocok, Kurang Cocok.",
            "nt": "float | total score (nilai total), total score of profile matching calculation. The ranking is sorted based on this value."
        }
    ]
    ```

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

**Get Decision**
----


* **URL**

  /api/decision

* **Method:**
  
  `GET`
  
*  **URL Params**

   **Required:**
 
   `id=[string|decision id]`

* **Data Params**

    N/A

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
            "imageFileName": "string",
            "matchingLabel": "string | Sangat Cocok, Cocok, Cukup Cocok, Kurang Cocok.",
            "nt": "float | total score (nilai total), total score of profile matching calculation. The ranking is sorted based on this value."
        }
    ]
    ```

=============================================




## Car

**Insert Image**
----


* **URL**

  /api/image

* **Method:**
  
  `POST`
  
*  **URL Params**

   N/A

* **Data Params**

    ```
    type : form data
    key : file
    ```

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** 
    ```
    string | filename generated by server
    ```
=============================================

**Insert Car**
----


* **URL**

  /api/car

* **Method:**
  
  `POST`
  
*  **URL Params**

   N/A

* **Data Params**

    ```json
    {
      "name": "string",
      "price": "int",
      "brand": "string",
      "productionYear": "int",
      "condition": "float",
      "description": "string",
      "mileage": "int",
      "imageFileName": "string | list of image separated with semicolon",
      "sellerName": "string",
      "sellerPhoneNumber": "string"
    }
    ```

* **Success Response:**

  * **Code:** 204 <br />
    **Content:** _no content_

=============================================

**Car Listing**
----


* **URL**

  /api/car

* **Method:**
  

  `GET`
  
*  **URL Params**

  **Optional:**
 
  ```
  page=[int|sart from 1]
  pageSize=[int|default=10]
  carName=[string]
  ```


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
                "condition": "float",
                "description": "string",
                "mileage": "int",
                "imageFileName": "string | the image file name is separated by semicolon"
            }  
        ],
        "searchParam": "string",
        "pageCount": "int",
        "currentPageIndex": "int"
    }
    ```

=============================================

## Feedback

**Insert Feedback**
----


* **URL**

  /api/feedback

* **Method:**
  
  `POST`
  
*  **URL Params**

   N/A

* **Data Params**

    ```json
    {
      "rating": "int",
      "comment": "string"
    }
    ```

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** 
    ```
    boolean | true/false describing success/error of the process
    ```

=============================================

**Get Feedback**
----


* **URL**

  /api/feedback

* **Method:**
  
  `GET`
  
*  **URL Params**

   N/A

* **Data Params**

   N/A

* **Success Response:**

  * **Code:** 200 <br />
    **Content:** 
    ```json
    [
      {
          "id": "string",
          "rating": "int",
          "comment": "string"
      }
    ]
    ```

=============================================
