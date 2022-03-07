--------------------------------------------------------------------------------------
-- Stored Procedures

--Login Procedure:
    --Inputs: userName, password
    --Output: userID

CREATE PROCEDURE LoginProcedure(@username AS VARCHAR(10), @password AS VARCHAR(15))
AS
BEGIN
	SELECT userId
	FROM Users
	WHERE userName = @username AND password = @password;
END;

--- execute procedure
EXECUTE LoginProcedure
	@username = 'morgan',
	@password = 'password';


--------------------------------------------------------------------------------------
--Transaction Summary Procedure:
    --Input: userID
    --Output: table All transactions related to UserID accounts. order by date

CREATE PROCEDURE TransactionSummaryProcedure(@userID AS INT)
AS
BEGIN
	SELECT *
	FROM UserTransactions
	INNER JOIN Account
	ON Account.userID = @userID
	WHERE Account.accountNumber = UserTransactions.accountNumber;
END;

--- execute procedure
EXECUTE TransactionSummaryProcedure
	@userID = 111;


--------------------------------------------------------------------------------------
--Notification Procedure:
	--Input: userID
	--Output: table ALL notifications related to userID accounts

CREATE PROCEDURE NotificationProcedure(@userId AS INT)
AS
BEGIN
	SELECT *
	FROM Notifications
	INNER JOIN Account
	ON Account.userID = @userID
	WHERE Account.accountNumber = Notifications.accountNumber;
END;

--- execute procedure
EXECUTE NotificationProcedure
	@userID = 123;