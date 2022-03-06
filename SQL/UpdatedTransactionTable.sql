CREATE TABLE UserTransactions (
transactionID INT NOT NULL,
transactionType VARCHAR(2),
processingDate DATETIME NOT NULL,
description VARCHAR(255),
locations VARCHAR(2),
amount FLOAT,
accountNumber INT,
PRIMARY KEY (transactionID),
FOREIGN KEY (accountNumber) REFERENCES Account(accountNumber)
);

