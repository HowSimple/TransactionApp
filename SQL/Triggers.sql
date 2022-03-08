


USE CommerceTransactionDB;


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
	IF 1 = ISNULL((SELECT hasNotification FROM HasNotification Inner Join (SELECT Users.userId FROM Users Inner Join Account On Account.accountNumber = (SELECT accountNumber from inserted)) as far on far.userId = HasNotification.userID),0)
		IF (SELECT amount FROM inserted) >= 500.00
			INSERT INTO Notifications (description,accountNumber)
			VALUES (CONCAT('The amount of ',(SELECT amount FROM inserted),' was withdrawn from account #',(SELECT accountNumber FROM inserted)), (SELECT accountNumber FROM inserted))	
GO