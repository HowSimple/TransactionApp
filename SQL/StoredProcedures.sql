USE CommerceTransactionDB;



--------------------------------------------------------------------------------------
-- Stored Procedures


--------------------------------------------------------------------------------------
--Balance Procedure:
    --Input: userID
    --Output: Balance of Account

DROP PROCEDURE IF EXISTS BalanceProcedure;
GO
CREATE PROCEDURE BalanceProcedure(@userID AS INT)
AS
	SELECT amount
	FROM Balance
	INNER JOIN Account
	ON Account.userID = @userID
	WHERE Account.accountNumber = Balance.accountNumber;
	
GO

--- execute procedure
EXECUTE BalanceProcedure
	@userID = 1;





-------------------------------------------------------------------------------------

--Register Procedure
	--Inputs: userName, password
	--Output: userID

DROP PROCEDURE IF EXISTS RegisterProcedure;

GO
CREATE PROCEDURE RegisterProcedure(@username AS VARCHAR(10), @location AS VARCHAR(2), @password AS VARCHAR(15))
AS
	IF 0 = ISNULL( (SELECT userName FROM Users WHERE userName = @username) , 0 )
	BEGIN
		INSERT INTO Users (password, locations, userName) VALUES (@password, @location, @username);
		INSERT INTO Account (userID) VALUES ((SELECT userId FROM Users WHERE userName = @username AND password = @password));
		INSERT INTO Balance (amount, accountNumber) VALUES (0,(SELECT accountNumber FROM Account WHERE userID = 
				(SELECT userId FROM Users WHERE userName = @username AND password = @password)));
		SELECT userId FROM Users WHERE userName = @username AND password = @password; 
	END
		

GO
--- execute procedure
EXECUTE RegisterProcedure
	@username = 'testing',
	@location = 'KS',
	@password = 'testpassword',
	@accountNumber = 1234567;



--Login Procedure:
    --Inputs: userName, password
    --Output: userID
    
DROP PROCEDURE IF EXISTS LoginProcedure;

GO
CREATE PROCEDURE LoginProcedure(@username AS VARCHAR(10), @password AS VARCHAR(15))
AS
	SELECT userId
	FROM Users
	WHERE userName = @username AND password = @password;
GO

--- execute procedure
EXECUTE LoginProcedure
	@username = 'morgan',
	@password = 'password';


--------------------------------------------------------------------------------------
--Transaction Summary Procedure:
    --Input: userID
    --Output: table All transactions related to UserID accounts. order by date in descending order
    
DROP PROCEDURE IF EXISTS TransactionSummaryProcedure;
GO
CREATE PROCEDURE TransactionSummaryProcedure(@userID AS INT)
AS
	SELECT *
	FROM UserTransactions
	INNER JOIN Account
	ON Account.userID = @userID
	WHERE Account.accountNumber = UserTransactions.accountNumber
	ORDER BY processingDate DESC;
GO


--- execute procedure
EXECUTE TransactionSummaryProcedure
	@userID = 111;


--------------------------------------------------------------------------------------
--Notification Procedure:
	--Input: userID
	--Output: table ALL notifications related to userID accounts, order by date in descending order
	
DROP PROCEDURE IF EXISTS NotificationProcedure;
GO
CREATE PROCEDURE NotificationProcedure(@userId AS INT)
AS
	SELECT *
	FROM Notifications
	INNER JOIN Account
	ON Account.userID = @userID
	WHERE Account.accountNumber = Notifications.accountNumber
	ORDER BY processingDate DESC;
GO


--- execute procedure
EXECUTE NotificationProcedure
	@userID = 123;

DROP PROCEDURE IF EXISTS TransactionProcedure;
GO

CREATE PROCEDURE TransactionProcedure(@userID AS INT, @transactionAmount AS FLOAT, @transactionLocation AS VARCHAR(2),@transactionType as VARCHAR(2), @processingDate as Date, @transactionDescription as VARCHAR(15))
AS
	BEGIN TRANSACTION 
		 INSERT INTO UserTransactions (transactionType,processingDate,description,locations,amount,accountNumber)
		 VALUES (@transactionType, @processingDate, @transactionDescription, @transactionLocation, @transactionAmount, 
				(SELECT accountNumber FROM Account INNER JOIN Users On Account.userID = @userID WHERE Account.userID = Users.userId));
	
	
		 IF (@transactionType = 'CR')
			 UPDATE Balance
			 SET amount = amount + @transactionAmount
			 Where Balance.accountNumber = (SELECT accountNumber FROM Account INNER JOIN Users On Account.userID = @userID WHERE Account.userID = Users.userId);
		 ELSE
			UPDATE Balance
			 SET amount = amount - @transactionAmount
			 Where Balance.accountNumber = (SELECT accountNumber FROM Account INNER JOIN Users On Account.userID = @userID WHERE Account.userID = Users.userId);
	COMMIT;
GO

Select *
FROM Users

--- execute procedure
EXECUTE TransactionProcedure
          @userID = 123,
		  @transactionAmount = 200,
		  @transactionLocation = "MO",
		  @transactionType = "DR",
		  @processingDate = '2020-03-07',
		  @transactionDescription = "Starbucks";

--------------------------------------------------------------------------------------
-- Select Notification Procedures

--Login Procedure:
    --Inputs: userID, userNotificationID
    --Output: None
	--Action: Adds row to HasNotification Table that indicates selected notification

DROP PROCEDURE IF EXISTS SelectNotification;
GO
CREATE PROCEDURE SelectNotification(@userID as INT, @userNotificationID as INT, @amount as FLOAT)
AS
	EXECUTE UnselectNotification
			@userID = @userID,
			@userNotificationID = @userNotificationID;
	INSERT INTO HasNotification (userID,userNotificationID,hasNotification,amount) VALUES (@userID, @userNotificationID,1 ,@amount)
GO


--------------------------------------------------------------------------------------
-- Unselect Notification Procedures

--Login Procedure:
    --Inputs: userID, userNotificationID
    --Output: None
	--Action: Deletes row from HasNotification table
DROP PROCEDURE IF EXISTS UnselectNotification;
GO
CREATE PROCEDURE UnselectNotification(@userID as INT, @userNotificationID as INT)
AS
	DELETE FROM HasNotification WHERE (userID = @userID and @userNotificationID = userNotificationID);
GO
--------------------------------------------------------------------------------------
-- Show Notification Procedures

--Login Procedure:
    --Inputs: userID
    --Output: List of Notifications active on user by number.
	--Action: Adds row to HasNotification Table that indicates selected notification
DROP PROCEDURE IF EXISTS ShowNotification
GO
CREATE PROCEDURE ShowNotification(@userID AS INT)
AS
   SELECT userNotificationID, amount FROM HasNotification WHERE userID=@userID;
GO


SELECT * 
FROM UserTransactions;

SELECT *
FROM Balance
