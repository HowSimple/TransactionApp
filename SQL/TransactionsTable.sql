CREATE TABLE dbo.Transactions
(  
account_id int not null primary key,  
account_balance float not null,
transaction_amount float,
 transaction_type nvarchar(10) ,  
 transaction_location nvarchar(100),  
 transaction_description nvarchar(100),  
 processing_date datetime
)  