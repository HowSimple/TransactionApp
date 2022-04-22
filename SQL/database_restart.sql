

DROP TABLE IF EXISTS Balance;
DROP TABLE IF EXISTS HasNotification;
DROP TABLE IF EXISTS Notifications;
DROP TABLE IF EXISTS UserNotification;
DROP TABLE IF EXISTS UserTransactions;
DROP TABLE IF EXISTS Account;
DROP TABLE IF EXISTS Users;




DROP TABLE IF EXISTS Users;
CREATE TABLE Users (
userId INT NOT NULL,
password VARCHAR(15) NOT NULL,
locations varchar(2) NOT NULL,
userName varchar(10) NOT NULL,
PRIMARY KEY (userID),
UNIQUE (userName)
);

DROP TABLE IF EXISTS Account;
CREATE TABLE Account (
accountNumber INT NOT NULL,
userID INT,
PRIMARY KEY (accountNumber),
FOREIGN KEY (userID) REFERENCES Users(userID)
);

DROP TABLE IF EXISTS Balance;
CREATE TABLE Balance (
balanceID INT NOT NULL,
amount FLOAT NOT NULL,
accountNumber INT,
PRIMARY KEY (balanceID),
FOREIGN KEY (accountNumber) REFERENCES Account(accountNumber)
);

DROP TABLE IF EXISTS UserNotification;
CREATE TABLE UserNotification
(
 userNotificationID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
 description VARCHAR(255),

);

DROP TABLE IF EXISTS Notifications;
CREATE TABLE Notifications (
notificationID INT IDENTITY(1,1) NOT NULL,
description VARCHAR(255),
accountNumber INT,
userNotificationID INT,
processingDate DATETIME NOT NULL,
PRIMARY KEY (notificationID),
FOREIGN KEY (userNotificationID) REFERENCES userNotification(userNotificationID),
FOREIGN KEY (accountNumber) REFERENCES Account(accountNumber)
);

DROP TABLE IF EXISTS UserTransactions;
CREATE TABLE UserTransactions (
transactionID INT IDENTITY(1,1) NOT NULL,
transactionType VARCHAR(2),
processingDate DATETIME NOT NULL,
description VARCHAR(255),
locations VARCHAR(2),
amount FLOAT,
accountNumber INT,
PRIMARY KEY (transactionID),
FOREIGN KEY (accountNumber) REFERENCES Account(accountNumber)
);


DROP TABLE IF EXISTS HasNotification;
CREATE TABLE HasNotification
(
 userID INT,
 amount INT NULL,
 hasNotificationID INT IDENTITY(1,1),
 userNotificationID INT,
 hasNotification BIT Not Null, -- used as boolean 0 false, 1 true
 FOREIGN KEY (userNotificationID) REFERENCES UserNotification(userNotificationID),
 FOREIGN KEY(userID) REFERENCES Users(userID),
 PRIMARY KEY (hasNotificationID)


);

INSERT INTO UserNotification(description) VALUES ('Greater Than Value Set');
INSERT INTO UserNotification(description) VALUES ('Out of State');
INSERT INTO UserNotification(description) VALUES ('Lower Than Value Set');


select *
from UserNotification

select *
from Users

INSERT INTO Users(userId,password, locations,userName) VALUES(123, 'password', 'MO', 'morgan');
INSERT INTO Account(accountNumber, userID) VALUES(963852741,123);
INSERT INTO Balance(balanceID,amount,accountNumber) VALUES (963852741, 5000, 963852741);
select *
from HasNotification

select *
from UserTransactions

select *
from Notifications

select *
from Balance

select *
from Account

DROP TRIGGER IF EXISTS UserTransactions_500_Insert;
GO
CREATE TRIGGER UserTransactions_500_INSERT ON UserTransactions
AFTER INSERT
AS
	SET NOCOUNT ON;
	IF 1 = ISNULL((SELECT hasNotification FROM HasNotification Inner Join 
		(SELECT Users.userId FROM Users Inner Join Account On Account.accountNumber = 
			(SELECT accountNumber from inserted)) as far on far.userId = HasNotification.userID 
				where userNotificationID = 1) ,0) --------------<<<<< change userNotificationID = 1 to applicaple number based on userNotifactionID 1 over 500,2 out of state,3 low balance 
		IF (SELECT amount FROM inserted) >= (SELECT amount FROM HasNotification Inner Join 
											(SELECT Users.userId FROM Users Inner Join Account On Account.accountNumber = 
											(SELECT accountNumber from inserted)) as far on far.userId = HasNotification.userID 
											 where userNotificationID = 1)
			INSERT INTO Notifications (description,accountNumber,userNotificationID,processingDate)
			VALUES (CONCAT('The amount of ',(SELECT amount FROM inserted),' was withdrawn from account #',(SELECT accountNumber FROM inserted)), (SELECT accountNumber FROM inserted),1,(SELECT processingDate FROM inserted))	
GO


DROP TRIGGER IF EXISTS UserTransaction_Out_Of_State_Insert;
GO
CREATE TRIGGER UserTransaction_Out_Of_State_Insert ON UserTransactions
AFTER INSERT
AS
	SET NOCOUNT ON;
	IF 1 = ISNULL((SELECT hasNotification FROM HasNotification Inner Join 
		(SELECT Users.userId FROM Users Inner Join Account On Account.accountNumber = 
			(SELECT accountNumber from inserted)) as far on far.userId = HasNotification.userID 
				where userNotificationID = 2) ,0) --------------<<<<< change userNotificationID = 1 to applicaple number based on userNotifactionID 1 over 500,2 out of state,3 low balance 
		IF (SELECT locations FROM inserted) != (SELECT locations FROM Users Inner Join Account on Account.accountNumber = (SELECT accountNumber from inserted) where  Users.userId = Account.userID )
			INSERT INTO Notifications (description,accountNumber,userNotificationID,processingDate)
				VALUES (CONCAT('Out of State Transaction from the state of ',(SELECT locations FROM inserted)), (SELECT accountNumber FROM inserted),2,(SELECT processingDate FROM inserted))	
GO

DROP TRIGGER IF EXISTS UserTransaction_Low_Balance_Insert;
GO
CREATE TRIGGER UserTransaction_Low_Balance_Insert ON Balance
AFTER UPDATE
AS
	SET NOCOUNT ON;
	IF 1 = ISNULL((SELECT hasNotification FROM HasNotification Inner Join 
		(SELECT Users.userId FROM Users Inner Join Balance On Balance.accountNumber = 
			(SELECT accountNumber from inserted)) as far on far.userId = HasNotification.userID 
				where userNotificationID = 3) ,0) --------------<<<<< change userNotificationID = 1 to applicaple number based on userNotifactionID 1 over 500,2 out of state,3 low balance 
		IF (SELECT amount FROM Balance where Balance.accountNumber = (SELECT accountNumber FROM inserted )) < (SELECT amount FROM HasNotification Inner Join 
											(SELECT Users.userId FROM Users Inner Join Account On Account.accountNumber = 
											(SELECT accountNumber from inserted)) as far on far.userId = HasNotification.userID 
											 where userNotificationID = 3)
			INSERT INTO Notifications (description,accountNumber,userNotificationID,processingDate)
				VALUES (CONCAT('You have low balance of ',(SELECT amount FROM Balance where Balance.accountNumber = (SELECT accountNumber FROM inserted ))), (SELECT accountNumber FROM inserted),3, GETDATE())	
GO


