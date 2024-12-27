# SecureCam Project

SecureCam is a comprehensive surveillance application designed to enhance security through real-time monitoring, alert notifications, and problem reporting. The application provides functionalities for both **users** and **administrators** to ensure seamless and efficient operation.

---

## Table of Contents
1. [Features Overview](#features-overview)
   - [User Side](#user-side)
   - [Admin Side](#admin-side)
2. [Installation](#installation)
3. [Technologies Used](#technologies-used)
4. [Screenshots](#screenshots)
5. [Contributing](#contributing)
6. [License](#license)
7. [Contact](#contact)

---

## Features Overview

### User Side
1. **Signup & Approval Process**
   - Users create an account via a signup form.
   - Approval requests are sent to the admin for validation.

2. **Main Dashboard**
   ![image](https://github.com/user-attachments/assets/374446ec-8605-4878-8164-393489aa343e)
   Upon login, users can perform the following actions:
   - **Monitoring**  
     - Run surveillance script.  
     - Load video stream after a 10-second delay.  
     - Stop the video stream and Python process.  
   ![image](https://github.com/user-attachments/assets/ece4b6f8-c9bc-4d4c-bb7d-180b80bc7519)



   - **View Alerts**  
     View all alerts generated during the monitoring process.  
     ![image](https://github.com/user-attachments/assets/6be2a6ad-1915-452a-b792-9107b267f046)


   - **Report Problem**  
     Submit a problem or false positive issue.  
     - Add a picture and detailed text description of the issue.  
     ![image](https://github.com/user-attachments/assets/a01e07a5-aef0-4698-a33d-b430e1c69927)


   - **Receive Notifications**  
     Receive alerts via:
     - Email.
     - SMS.
     - Desktop notifications.  
     ![image](https://github.com/user-attachments/assets/4b453650-f829-485b-a754-745c5d954cb0)


   - **Exit**  
     Safely exit the application.

---

### Admin Side
**Main Dashboard**
![image](https://github.com/user-attachments/assets/40732f96-691f-4806-bf13-0fcbbe32bd0d)

1. **Management Accounts**
   - View pending signup requests.
   - Approve or reject user accounts.  
   ![image](https://github.com/user-attachments/assets/b73ed25d-d58d-4e13-8952-694b1a492924)


2. **Handle Complaints**
   - View complaints with attached pictures and descriptions.
   - Take actions to resolve the reported issues.  
   ![image](https://github.com/user-attachments/assets/df020fd7-bc63-4006-aed3-ca13ab324c19)


---

## Installation
### Clone the Repository
```bash
git clone https://github.com/yourusername/securecam.git
cd securecam

