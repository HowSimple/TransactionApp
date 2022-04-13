-----------------------------------------------------------
------------Triggers
------------UserTransaction_500_Insert
------------If user has greater than or equal to 500 notification set up a notification will appear.

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
			INSERT INTO Notifications (description,accountNumber)
			VALUES (CONCAT('The amount of ',(SELECT amount FROM inserted),' was withdrawn from account #',(SELECT accountNumber FROM inserted)), (SELECT accountNumber FROM inserted))	
GO


-----------------------------------------------------------------
------------UserTransaction_Out_Of_State_Insert
------------If user has greater than or equal to 500 notification set up a notification will appear.
------------If user has out of state notification one and if locations different from user location and trigger will set off and log in notificaiton table

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
			INSERT INTO Notifications (description,accountNumber)
				VALUES (CONCAT('Out of State Transaction from the state of ',(SELECT locations FROM inserted)), (SELECT accountNumber FROM inserted))	
GO


-----------------------------------------------------------------
------------UserTransaction_Low_Balance_Insert
------------If user has balance lower then 100 then a warning notification of low balance goes out

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
			INSERT INTO Notifications (description,accountNumber)
				VALUES (CONCAT('You have low balance of ',(SELECT amount FROM Balance where Balance.accountNumber = (SELECT accountNumber FROM inserted ))), (SELECT accountNumber FROM inserted))	
GO
