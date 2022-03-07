-- Show data in each table
SELECT * FROM Users;

SELECT * FROM Account;

SELECT * FROM UserTransactions;

SELECT * FROM Balance;

SELECT * FROM Notifications;

-- Inserting temporary user for testing
INSERT INTO Users (userId, password, locations, userName)
VALUES (123, 'password', 'MO', 'morgan');

-- Insert user account information
INSERT INTO Account (accountNumber, userID)
VALUES (211111110, 123);

-- Imported values from Project Data Excel Sheet to UserTransactions table

-- Insert updated balance for user
INSERT INTO Balance (balanceID, amount, accountNumber)
VALUES (1, 4571.08, 211111110);

-- Insert Notification data
INSERT INTO Notifications (notificationID, description, accountNumber)
VALUES (2, 'Out Of State', 211111110);



---------------------------------------------------------------------------
-- Inserting temporary user for testing
INSERT INTO Users (userId, password, locations, userName)
VALUES (111, 'password', 'KS', 'smudgecat');

-- Insert user account information
INSERT INTO Account (accountNumber, userID)
VALUES (32222220, 111);

-- Insert a user transaction
INSERT INTO UserTransactions (transactionID, transactionType, processingDate, description, locations, amount, accountNumber)
VALUES (75, 'DR', 1/3/22, 'Hyvee', 'MO', 25.00, 32222220);

-- Insert updated balance for user
INSERT INTO Balance (balanceID, amount, accountNumber)
VALUES (2, 75, 32222220);

-- Insert Notification data
INSERT INTO Notifications (notificationID, description, accountNumber)
VALUES (1, 'Out Of State', 32222220);