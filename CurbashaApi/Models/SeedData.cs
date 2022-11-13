using System;
using System.Diagnostics.Metrics;
using System.Drawing;
using CurbashaApi.Areas.Identity.Entity;
using CurbashaApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CurbashaApi.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CurbashaApiContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<CurbashaApiContext>>()))
            {
                if (context.AspSelections.Any())
                {
                    return;
                }
                context.AspSelections.AddRange(
                    new AspSelections
                    {
                        SelectionName = "Shirts",
                        IsActive = true
                    },
                    new AspSelections
                    {
                        SelectionName = "Sweatshirts",
                        IsActive = true
                    },
                    new AspSelections
                    {
                        SelectionName = "Pants",
                        IsActive = true
                    }
                );
                context.SaveChanges();

                if (context.AspProducts.Any())
                {
                    return;
                }

                context.AspProducts.AddRange(
                    //t-shirts
                    new AspProduct
                    {
                        NameProduct = "Базова Футболка Dark Green",
                        Description = "Футболка стандартного крою з круглим коміром у рубчик і короткими рукавами. Бавовняна тканина з мерсеризованим ефектом.\nКолір: Темно-зелений\nСклад: 100% Бавовна",
                        AspSelections = context.AspSelections.First(s => s.Id == 1),
                        SelectionId = 1,
                        Price = 499,
                        IsActive = true
                    },
                    new AspProduct
                    {
                        NameProduct = "Базова Футболка Grey Marl",
                        Description = "Футболка стандартного крою з круглим коміром у рубчик і короткими рукавами. Бавовняна тканина з мерсеризованим ефектом.\nКолір: Сіро-глинястий\nСклад: 100% Бавовна",
                        AspSelections = context.AspSelections.First(s => s.Id == 1),
                        SelectionId = 1,
                        Price = 499,
                        IsActive = true
                    },
                    new AspProduct
                    {
                        NameProduct = "Базова Футболка Anthracite Grey",
                        Description = "Футболка стандартного крою з круглим коміром у рубчик і короткими рукавами. Бавовняна тканина з мерсеризованим ефектом.\nКолір: Антрацитово-сірий\nСклад: 100% Бавовна",
                        AspSelections = context.AspSelections.First(s => s.Id == 1),
                        SelectionId = 1,
                        Price = 499,
                        IsActive = true
                    },
                    new AspProduct
                    {
                        NameProduct = "Базова Футболка Ecru/Black",
                        Description = "Футболка стандартного крою з круглим коміром у рубчик і короткими рукавами. Бавовняна тканина з мерсеризованим ефектом.\nКолір: Екрю/чорний\nСклад: 100% Бавовна",
                        AspSelections = context.AspSelections.First(s => s.Id == 1),
                        SelectionId = 1,
                        Price = 549,
                        IsActive = true
                    },

                    //Sweatshirts

                    new AspProduct
                    {
                        NameProduct = "Світшот Black",
                        Description = "Світшот об’ємного крою з круглим коміром і довгими рукавами. Окантовка в рубчик по краях.\nКолір: Чорний\nСклад: Головна тканина - 100% Бавовна.",
                        AspSelections = context.AspSelections.First(s => s.Id == 2),
                        SelectionId = 2,
                        Price = 899,
                        IsActive = true
                    },
                    new AspProduct
                    {
                        NameProduct = "Світшот Grey Marl",
                        Description = "Опис: Світшот об’ємного крою з круглим коміром і довгими рукавами. Окантовка в рубчик по краях.\nКолір: Сіро-глинястий\nСклад: Головна тканина - 100% Бавовна.",
                        AspSelections = context.AspSelections.First(s => s.Id == 2),
                        SelectionId = 2,
                        Price = 899,
                        IsActive = true
                    },
                    new AspProduct
                    {
                        NameProduct = "Світшот Anthracite Grey",
                        Description = "Світшот об’ємного крою з круглим коміром і довгими рукавами. Окантовка в рубчик по краях.\nКолір: Антрацитово-сірий\nСклад: Головна тканина - 100% Бавовна.",
                        AspSelections = context.AspSelections.First(s => s.Id == 2),
                        SelectionId = 2,
                        Price = 899,
                        IsActive = true
                    },
                    new AspProduct
                    {
                        NameProduct = "Світшот Ecru",
                        Description = "Світшот об’ємного крою з круглим коміром і довгими рукавами. Окантовка в рубчик по краях.\nКолір: Екрю\nСклад: Головна тканина - 100% Бавовна.",
                        AspSelections = context.AspSelections.First(s => s.Id == 2),
                        SelectionId = 2,
                        Price = 899,
                        IsActive = true
                    },

                    //Pants

                    new AspProduct
                    {
                        NameProduct = "Штани-чінос Black",
                        Description = "Штани вузького крою з еластичної бавовняної тканини. Застібаються на блискавку та ґудзик. Кишені спереду та прорізні кишені з ґудзиком ззаду.\nКолір: Чорний \nСклад: 97% Бавовна · 3% Еластан",
                        AspSelections = context.AspSelections.First(s => s.Id == 3),
                        SelectionId = 3,
                        Price = 1199,
                        IsActive = true
                    },
                    new AspProduct
                    {
                        NameProduct = "Штани-чінос Anthracite Grey",
                        Description = "Штани вузького крою з еластичної бавовняної тканини. Застібаються на блискавку та ґудзик. Кишені спереду та прорізні кишені з ґудзиком ззаду.\nКолір: Антрацитово-сірий\nСклад: 97% Бавовна · 3% Еластан",
                        AspSelections = context.AspSelections.First(s => s.Id == 3),
                        SelectionId = 3,
                        Price = 1199,
                        IsActive = true
                    },
                    new AspProduct
                    {
                        NameProduct = "Штани-чінос Ice",
                        Description = "Штани вузького крою з еластичної бавовняної тканини. Застібаються на блискавку та ґудзик. Кишені спереду та прорізні кишені з ґудзиком ззаду.\nКолір: Колір льоду\nСклад: 97% Бавовна · 3% Еластан",
                        AspSelections = context.AspSelections.First(s => s.Id == 3),
                        SelectionId = 3,
                        Price = 1199,
                        IsActive = true

                    },
                    new AspProduct
                    {
                        NameProduct = "Штани-чінос Navy Blue",
                        Description = "Штани вузького крою з еластичної бавовняної тканини. Застібаються на блискавку та ґудзик. Кишені спереду та прорізні кишені з ґудзиком ззаду.\nКолір: Темно-синій\nСклад: 97% Бавовна · 3% Еластан",
                        AspSelections = context.AspSelections.First(s => s.Id == 3),
                        SelectionId = 3,
                        Price = 1199,
                        IsActive = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

