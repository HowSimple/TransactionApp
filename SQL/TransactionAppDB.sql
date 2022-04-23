

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
