# Client Communicate Portal 


## Description 

This client communication portal can be used in your business for a single unified point to communicate with clients through several third party APIs. 
Communications with clients can be done through Email, SMS, Whatsapp, Telegram ... etc


### How it works ?
This single-page application utlizes JQuery and AJAX functions to update a web page without reloading 
and to send data in the background.


### Step 0:
The $(document ).ready() method will run once the page DOM is ready to execute JavaScript code.
The first JS function call is getUserList();


![alt text](/DOM1.PNG)

### Step 1: What does getUserList(); do ?
The function getUserList request JSON data if user is created and on success it calls another function to draw table
in realtime.

![alt text](/DOM2.PNG)
