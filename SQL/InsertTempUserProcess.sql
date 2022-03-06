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