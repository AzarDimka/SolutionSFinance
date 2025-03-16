

---Этот скрипт не выполнять, он написан как пример---

select * from dbo.Employee

select * from [dbo].[Handbook]

select * from [dbo].[Field]


INSERT INTO [dbo].[Handbook] ( nameHandbook, tableName, request, keyField, selectionField, width, height) 
VALUES (N'Сотрудники', 'dbo.Employee', 'select * from dbo.Employee', 'EmployeeId', 'LastName' , '600', '400');


INSERT INTO [dbo].[Field] (  idHandbook, indexField, nameToQuery, nameVisible, idTypeData, refHandbookToField, isVisible, isEdit, isNull, isCheckDuplicate) 
VALUES (6, 1, 'EmployeeId', N'Идентификатор', 2 , null, 1, 0, 0, 0), 
(6, 10, 'LastName', N'Фамилия', 1 , null, 1, 1, 0, 0), 
(6, 15, 'FirstName', N'Имя', 1 , null, 1, 1, 0, 0), 
(6, 20, 'MiddleName', N'Отчество', 1 , null, 1, 1, 0, 0), 
(6, 25, 'Position', N'Должность', 1 , null, 1, 1, 0, 0), 
(6, 30, 'ContactPhone', N'Телефон', 1 , null, 1, 1, 0, 0), 
(6, 35, 'Salary', N'Зарплата', 1 , null, 1, 1, 0, 0)