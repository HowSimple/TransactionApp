


EXECUTE SelectNotification
	@userID = 123,
	@userNotificationID = 1;

EXECUTE TransactionProcedure
          @userID = 123,
		  @transactionAmount = 200,
		  @transactionLocation = "MO",
		  @transactionType = "DR",
		  @processingDate = '2020-03-07',
		  @transactionDescription = "Starbucks";

EXECUTE TransactionProcedure
          @userID = 123,
		  @transactionAmount = 600,
		  @transactionLocation = "MO",
		  @transactionType = "DR",
		  @processingDate = '2020-03-07',
		  @transactionDescription = "Starbucks";

Select * from HasNotification;
Select * from Notifications;

DROP PROCEDURE IF EXISTS UnselectNotification;
GO
CREATE PROCEDURE UnselectNotification(@userID as INT, @userNotificationID as INT)
AS
	DELETE FROM HasNotification WHERE (userID = @userID and @userNotificationID = userNotificationID);
GO

DROP PROCEDURE IF EXISTS SelectNotification;
GO
CREATE PROCEDURE SelectNotification(@userID as INT, @userNotificationID as INT)
AS
	EXECUTE UnselectNotification
			@userID = @userID,
			@userNotificationID = @userNotificationID;
	INSERT INTO HasNotification (userID,userNotificationID,hasNotification) VALUES (@userID, @userNotificationID,1)
GO