Insert into dbo.Handbook (
	nameHandbook,
	tableName,
	keyField,
	selectionField,
	width,
	height,
	request
)
Values (
	N'Продукты',
	'dbo.Product',
	'IdProduct',
	'nameProduct',
	600,
	400,
	N'select * from dbo.Product'
)

GO

Insert into dbo.Product (
	nameProduct
)
Values
	('Product 1'),
	('Product 2'),
	('Product 3'),
	('Product 4'),
	('Product 5')

GO

Insert into dbo.TypeData (
	fullName
)
Values
	(N'Строка'),
	(N'Число'),
	(N'Дата'),
	(N'Справочник'),
	(N'Логический'),
	(N'Текст'),
	(N'Список'),
	(N'Целое число'),
	(N'Картинка'),
	(N'Выпадающий список')

GO

Insert into dbo.Field (
	idHandbook,
	indexField,
	nameToQuery,
	nameVisible,
	idTypeData,
	refHandbookToField,
	isVisible,
	isEdit,
	isNull,
	isCheckDuplicate
)
Values (
	1,
	1,
	'IdProduct',
	N'Идентификатор',
	2,
	NULL,
	1,
	0,
	0,
	0
),
(
	1,
	10,
	'nameProduct',
	N'Название продукта',
	1,
	NULL,
	1,
	1,
	0,
	0
)