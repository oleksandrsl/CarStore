create procedure statsByYear @StatsYear int
as
select sum(price) as Sum, month(dateOrder) as Month, count(Make) as Quantity from (select ma.Name as Make, mo.Name as Model, c.Engine, c.Year, c.Price, b.Name as BodyType, o.dateOrder from dbo.cars as c
left join dbo.models as mo on c.modelId = mo.modelId
left join dbo.bodyTypes as b on b.bodyTypeId = c.bodyTypeId
left join dbo.makes as ma on mo.makeId = ma.makeId
right join dbo.orders as o on c.carId = o.carId) as orders where year(dateOrder) = @StatsYear group by month(dateOrder)