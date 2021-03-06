create procedure popularCars @count int
as
select top(@count) count(Make) as Sales, Make, Model from (select ma.Name as Make, mo.Name as Model, c.Engine, c.Year, c.Price, b.Name as BodyType, o.dateOrder from dbo.cars as c
left join dbo.models as mo on c.modelId = mo.modelId 
left join dbo.bodyTypes as b on b.bodyTypeId = c.bodyTypeId
left join dbo.makes as ma on mo.makeId = ma.makeId
right join dbo.orders as o on c.carId = o.carId) as orders group by Make, Model order by Sales desc
