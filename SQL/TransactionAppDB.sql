CREATE SCHEMA userServices;

GO

CREATE TABLE Users (
userId INT NOT NULL,
password VARCHAR(15) NOT NULL,
locations varchar(2) NOT NULL,
userName varchar(10) NOT NULL,
PRIMARY KEY (userID),
UNIQUE (userName)
);


CREATE TABLE Account (
accountNumber INT NOT NULL,
userID INT,
PRIMARY KEY (accountNumber),
FOREIGN KEY (userID) REFERENCES Users(userID)
);


CREATE TABLE Balance (
balanceID INT NOT NULL,
amount FLOAT NOT NULL,
accountNumber INT,
PRIMARY KEY (balanceID),
FOREIGN KEY (accountNumber) REFERENCES Account(accountNumber)
);


CREATE TABLE Notifications (
notificationID INT IDENTITY(1,1) NOT NULL,
description VARCHAR(255),
accountNumber INT,
PRIMARY KEY (notificationID),
FOREIGN KEY (accountNumber) REFERENCES Account(accountNumber)
);


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

CREATE TABLE UserNotification
(
 userNotificatonID INT NOT NULL PRIMARY KEY,
 description VARCHAR(255),

);

CREATE TABLE HasNotification
(
 userID INT,
 hasNotificationID INT IDENTITY(1,1),
 userNotificationID INT,
 hasNotification BIT, -- used as boolean 0 false, 1 true
 FOREIGN KEY (userNotificationID) REFERENCES UserNotification(userNotificatonID),
 FOREIGN KEY(userID) REFERENCES Users(userID),
 PRIMARY KEY (hasNotificationID)


);
