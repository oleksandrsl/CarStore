using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarStore.Models
{
    public class SampleData
    {
        public static async Task InitializeStoreDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<StoreContext>();

                if (await db.Database.EnsureCreatedAsync())
                {
                    await InsertTestData(scopeServiceProvider);
                }
            }
        }

        private static async Task InsertTestData(IServiceProvider serviceProvider)
        {
            await AddOrUpdateAsync(serviceProvider, g => g.MakeId, Makes.Select(m => m.Value));
            await AddOrUpdateAsync(serviceProvider, g => g.ModelId, Models.Select(m => m.Value));
            await AddOrUpdateAsync(serviceProvider, g => g.BodyTypeId, BodyTypes.Select(m => m.Value));
            await AddOrUpdateAsync(serviceProvider, g => g.CarId, Cars.Select(m => m.Value));
            await AddOrUpdateAsync(serviceProvider, g => g.OrderId, Orders);
        }

        private static async Task AddOrUpdateAsync<TEntity>(
           IServiceProvider serviceProvider,
           Func<TEntity, object> propertyToMatch, IEnumerable<TEntity> entities)
           where TEntity : class
        {
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<StoreContext>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<StoreContext>();
                foreach (var item in entities)
                {
                    db.Entry(item).State = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)))
                        ? EntityState.Modified
                        : EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
        }

        private static Dictionary<string, Make> makes;
        public static Dictionary<string, Make> Makes
        {
            get
            {
                if (makes == null)
                {
                    var makesList = new Make[]
                {
                new Make{Name="Audi"},
                new Make{Name="BMW"},
                new Make{Name="Opel"},
                new Make{Name="Lexus"},
                new Make{Name="Fiat"},
                new Make{Name="Nissan"},
                new Make{Name="Lada"},
                new Make{Name="Ford"},
                new Make{Name="Acura"},
                new Make{Name="Mazda"},
                new Make{Name="Volkswagen"},
                new Make{Name="Toyota"},
                new Make{Name="Tesla"},
                new Make{Name="Suzuki"},
                new Make{Name="Subaru"}
                };

                    makes = new Dictionary<string, Make>();
                    foreach (var make in makesList)
                    {
                        makes.Add(make.Name, make);
                    }
                }

                return makes;
            }
        }

        private static Dictionary<string, Model> models;
        public static Dictionary<string, Model> Models
        {
            get
            {
                if (models == null)
                {
                    var modelsList = new Model[] {
                new Model{Name="A2", Make=makes["Audi"]},
                new Model{Name="A1", Make=makes["Audi"]},
                new Model{Name="A4", Make=makes["Audi"]},
                new Model{Name="A3", Make=makes["Audi"]},
                new Model{Name="A6", Make=makes["Audi"]},
                new Model{Name="A8", Make=makes["Audi"]},
                new Model{Name="Q7", Make=makes["Audi"]},
                new Model{Name="TT", Make=makes["Audi"]},
                new Model{Name="Q5", Make=makes["Audi"]},
                new Model{Name="Q3", Make=makes["Audi"]},
                new Model{Name="E21", Make=makes["BMW"]},
                new Model{Name="E24", Make=makes["BMW"]},
                new Model{Name="E23", Make=makes["BMW"]},
                new Model{Name="E28", Make=makes["BMW"]},
                new Model{Name="E30", Make=makes["BMW"]},
                new Model{Name="E31", Make=makes["BMW"]},
                new Model{Name="E32", Make=makes["BMW"]},
                new Model{Name="E34", Make=makes["BMW"]},
                new Model{Name="3", Make=makes["Mazda"]},
                new Model{Name="6", Make=makes["Mazda"]},
                new Model{Name="323", Make=makes["Mazda"]},
                new Model{Name="CX-9", Make=makes["Mazda"]},
                new Model{Name="CX-5", Make=makes["Mazda"]},
                new Model{Name="626", Make=makes["Mazda"]},
                new Model{Name="2", Make=makes["Mazda"]},
                new Model{Name="5", Make=makes["Mazda"]},
                new Model{Name="Focus", Make=makes["Ford"]},
                new Model{Name="Scorpion", Make=makes["Ford"]},
                new Model{Name="Explorer", Make=makes["Ford"]},
                new Model{Name="Golf", Make=makes["Volkswagen"]},
                new Model{Name="Polo", Make=makes["Volkswagen"]},
                new Model{Name="Passat", Make=makes["Volkswagen"]},
                new Model{Name="T1", Make=makes["Volkswagen"]},
                new Model{Name="T2", Make=makes["Volkswagen"]},

            };

                    models = new Dictionary<string, Model>();
                    foreach (var model in modelsList)
                    {
                        models.Add(model.Name, model);
                    }
                }

                return models;
            }
        }

        private static Dictionary<string, BodyType> bodyTypes;
        public static Dictionary<string, BodyType> BodyTypes
        {
            get
            {
                if (bodyTypes == null)
                {
                    var bodyTypesList = new BodyType[] {
                    new BodyType{Name="Sedan"},
                    new BodyType{Name="Coup"},
                    new BodyType{Name="Hatchback"},
                    new BodyType{Name="Pickup"},
                    new BodyType{Name="Van"}
                };
                    bodyTypes = new Dictionary<string, BodyType>();
                    foreach (var bodyType in bodyTypesList)
                    {
                        bodyTypes.Add(bodyType.Name, bodyType);
                    }
                }
                return bodyTypes;
            }
        }

        private static Dictionary<int, Car> cars;
        public static Dictionary<int, Car> Cars
        {
            get
            {
                if (cars == null)
                {
                    var carsList = new Car[] {
                        new Car{Price = 1000, Engine= (decimal)1.6, BodyType=BodyTypes["Sedan"], Year=2006, Model=Models["A4"] },
                        new Car{Price = 3000, Engine= (decimal)2.6, BodyType=BodyTypes["Coup"], Year=2008, Model=Models["CX-5"] },
                        new Car{Price = 1000, Engine= (decimal)1.6,  BodyType=BodyTypes["Van"], Year=2006, Model=Models["T1"] },
                        new Car{Price = 2500, Engine= (decimal)2.0, BodyType=BodyTypes["Coup"], Year=2007, Model=Models["CX-5"] },
                        new Car{Price = 1000, Engine= (decimal)2.0,  BodyType=BodyTypes["Van"], Year=2004, Model=Models["T1"] },
                        new Car{Price = 3000, Engine= (decimal)1.6, BodyType=BodyTypes["Hatchback"], Year=2006, Model=Models["3"] },
                        new Car{Price = 3000, Engine= (decimal)2.6, BodyType=BodyTypes["Hatchback"], Year=2008, Model=Models["3"] },
                        new Car{Price = 1000, Engine= (decimal)1.6,  BodyType=BodyTypes["Van"], Year=2006, Model=Models["T1"] },
                        new Car{Price = 2500, Engine= (decimal)2.0, BodyType=BodyTypes["Coup"], Year=2007, Model=Models["CX-5"] },
                        new Car{Price = 1000, Engine= (decimal)2.0,  BodyType=BodyTypes["Sedan"], Year=2004, Model=Models["A4"] }
                    };
                    cars = new Dictionary<int, Car>();
                    int index = 0;
                    foreach (var car in carsList)
                    {
                        index++;
                        cars.Add(index, car);
                    }
                }
                return cars;
            }

        }

        public static Order[] Orders
        {
            get
            {
                var orders = new Order[] {
                        new Order{FirstName= "Bart", LastName="Simpson", Phone="0543245321", DateOrder=new DateTime(2017,6,4), Car=Cars[1]},
                        new Order{FirstName= "Homer", LastName="Simpson", Phone="0543243221", DateOrder=new DateTime(2017,6,8), Car=Cars[2]},
                        new Order{FirstName= "Liz", LastName="Simpson", Phone="0764554354", DateOrder=new DateTime(2017,7,2), Car=Cars[3]},
                        new Order{FirstName= "Moe", LastName="Sizlak", Phone="0665323444", DateOrder=new DateTime(2017,7,3), Car=Cars[4]},
                        new Order{FirstName= "Vasil", LastName="Ghmih", Phone="0542354355", DateOrder=new DateTime(2017,7,15), Car=Cars[5]},
                        new Order{FirstName= "Valerii", LastName="Piven", Phone="0876543555", DateOrder=new DateTime(2017,8,8), Car=Cars[6]},
                        new Order{FirstName= "Evgen", LastName="Chereshnia", DateOrder=new DateTime(2017,9,11), Phone="0125345435", Car=Cars[8]},
                        new Order{FirstName= "Hon", LastName="Dou", Phone="0934354555", DateOrder=new DateTime(2017,9,20),Car=Cars[9]}
                    };

                return orders;
            }
        }
    }

}

