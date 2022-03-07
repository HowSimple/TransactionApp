USE CommerceTransactionDB;



--------------------------------------------------------------------------------------
-- Stored Procedures

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
    --Output: table All transactions related to UserID accounts. order by date

DROP PROCEDURE IF EXISTS TransactionSummaryProcedure;
GO
CREATE PROCEDURE TransactionSummaryProcedure(@userID AS INT)
AS
	SELECT *
	FROM UserTransactions
	INNER JOIN Account
	ON Account.userID = @userID
	WHERE Account.accountNumber = UserTransactions.accountNumber;
GO


--- execute procedure
EXECUTE TransactionSummaryProcedure
	@userID = 111;


--------------------------------------------------------------------------------------
--Notification Procedure:
	--Input: userID
	--Output: table ALL notifications related to userID accounts
DROP PROCEDURE IF EXISTS NotificationProcedure;
GO
CREATE PROCEDURE NotificationProcedure(@userId AS INT)
AS
	SELECT *
	FROM Notifications
	INNER JOIN Account
	ON Account.userID = @userID
	WHERE Account.accountNumber = Notifications.accountNumber;
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

SELECT * 
FROM UserTransactions;

SELECT *
FROM Balance