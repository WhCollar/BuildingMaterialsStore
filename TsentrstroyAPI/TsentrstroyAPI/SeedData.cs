using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using TsentrstroyAPI.Data;

namespace TsentrstroyAPI
{
    public static class SeedData
    {
        private static IConfiguration _configuration;
        private static DatabaseContext _databaseContext;

        private static IInitializableTable _initializeCategories = new InitializeCategories();
        private static IInitializableTable _initializeSubCategories = new InitializeSubCategories();
        private static IInitializableTable _initializeProducts = new InitializeProducts();
        private static IInitializableTable _initializeOverviewProducts = new InitializeProductsOverviews();

        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static void SetDatabaseContext(DatabaseContext databaseContext)
        {
            if (_configuration.GetValue<bool>("InitData") == true)
                _databaseContext = databaseContext;
        }

        public static void Initialize()
        {
            _initializeCategories.Execute();
            _initializeSubCategories.Execute();
            _initializeProducts.Execute();
            _initializeOverviewProducts.Execute();
        }

        private static SubCategory GetSubCategory(string name)
        {
            return _databaseContext.SubCategories.FirstOrDefault(x => x.Name == name);
        }
        
        private static SubCategory GetSubCategory(string name, string slug)
        {
            return _databaseContext.SubCategories.FirstOrDefault(x => x.Category.Slug == slug && x.Name == name);
        }
        
        private static Category GetCategoryByName(string name) => _databaseContext.Categories.First(c => c.Name == name);
            
        private static void AddCategory(string name, string slug)
        {
            Category category = new Category {Name = name, Slug = slug};

            _databaseContext.Categories.Add(category);
            _databaseContext.SaveChanges();
        }

        private static void AddSubCategory(string name, int categoryId, bool isActive)
        {
            Category category = _databaseContext.Categories.First(x => x.Id == categoryId);
            SubCategory subCategory = new SubCategory() {Name = name, Category = category, IsActive = isActive};

            _databaseContext.SubCategories.Add(subCategory);
            _databaseContext.SaveChanges();
        }

        private static void AddProduct(Product product)
        {
            _databaseContext.Products.Add(product);
            _databaseContext.SaveChanges();
        }

        private static void AddOverview(OverviewProduct overviewProduct)
        {
            _databaseContext.OverviewProducts.Add(overviewProduct);
            _databaseContext.SaveChanges();
        }

        private interface IInitializableTable
        {
            public void Execute();
        }

        private class InitializeCategories : IInitializableTable
        {
            public void Execute()
            {
                AddCategory("Штукатурка", "Shtukaturka");
                AddCategory("Полы", "Poli");
                AddCategory("Краска и эмаль", "KraskaIEmal");
                AddCategory("Затирка", "Zatirka");
                AddCategory("Система утепления фасадов", "SistemaUteplenyaFasadov");
                AddCategory("Для ячеистых блоков", "DliaYacheistihBlokov");
                AddCategory("Клей", "Kley");
                AddCategory("Гидроизоляция", "Gidroizolatsia");
                AddCategory("Ремонтные составы", "RemontnieSostavi");
                AddCategory("Шпаклевка", "Shpaklevka");
                AddCategory("Грунтовка", "Gruntovka");
                AddCategory("Декоративная штукатурка", "DecorativnayaShtukaturka");
                AddCategory("Строительный песок", "StroitelniyPesok");
                AddCategory("Химия", "Himia");
                AddCategory("Для ремонта и защиты бетона", "DliaRemontaIZashitiBetona");
            }
        }

        private class InitializeSubCategories : IInitializableTable
        {
            public const string DefaultSubCategoryName = "Все товары";

            public void Execute()
            {
                AddSubCategory("Акриловая", 1, true);
                AddSubCategory("Гипсовая", 1, true);
                AddSubCategory("Цементная", 1, true);

                AddSubCategory("Наливной", 2, true);
                AddSubCategory("Финишный", 2, true);
                AddSubCategory("Стяжка", 2, true);

                AddSubCategory("Клеи для плитки", 7, true);
                AddSubCategory("Клеи для обоев и напольных покрытий", 7, true);
                AddSubCategory("Клеи для систем утепления фасадов", 7, true);
                AddSubCategory("Клеи для ячеистых блоков", 7, true);

                AddSubCategory("Акриловая", 10, true);
                AddSubCategory("Цементная", 10, true);
                AddSubCategory("Гипсовая", 10, true);
                AddSubCategory("Полимерная", 10, true);

                FillOtherSubCategories();
            }

            public void FillOtherSubCategories()
            {
                List<SubCategory> subCategories = _databaseContext.SubCategories.ToList();
                List<Category> categories = _databaseContext.Categories.ToList();

                for (int i = 0; i < categories.Count; i++)
                {
                    Category category = categories[i];

                    bool hasSubCategory = subCategories.Any(x => x.Category.Id == category.Id);

                    if (hasSubCategory == false)
                        AddSubCategory(DefaultSubCategoryName, category.Id, true);
                }
            }
        }

        private class InitializeProducts : IInitializableTable
        {
            public void Execute()
            {
                AddPlaster();
                AddFloors();
                AddPaintAndEnamel();

                AddGrout();
                AddFacadeInsulationSystem();
                AddForHoneycombBlocks();

                AddGlue();
                AddRepairCompounds();
                AddWaterproofing();
                
                AddPutty();
                AddPrimer();
                AddDecorativePlaster();
            }

            #region Plaster

            private void AddPlaster()
            {
                AddProduct(new Product
                {
                    Title = "CRYSTAL PASTA",
                    ShortDescription = @"Готовая декоративная акриловая штукатурка «камешковая»",
                    FullDescription = @"Готовая декоративная акриловая штукатурка «камешковая»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро: 25 кг",
                    SubCategory = GetSubCategory("Акриловая"),
                    ImageUrl = @"ProductsImages/Shtukaturka/1.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKOR PASTA",
                    ShortDescription = @"Готовая декоративная акриловая штукатурка «КОРОЕД»",
                    FullDescription = @"Готовая декоративная акриловая штукатурка «КОРОЕД»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро: 25 кг",
                    SubCategory = GetSubCategory("Акриловая"),
                    ImageUrl = @"ProductsImages/Shtukaturka/2.png"
                });

                AddProduct(new Product
                {
                    Title = "AQUA PUTZ GIPS",
                    ShortDescription = @"Штукатурка гипсовая влагостойкая",
                    FullDescription = @"Штукатурка гипсовая влагостойкая",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Гипсовая"),
                    ImageUrl = @"ProductsImages/Shtukaturka/3.png"
                });
                AddProduct(new Product
                {
                    Title = "M-TEH BAU",
                    ShortDescription = @"Штукатурка гипсовая для машинного нанесения",
                    FullDescription = @"Штукатурка гипсовая для машинного нанесения",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 30 кг",
                    SubCategory = GetSubCategory("Гипсовая"),
                    ImageUrl = @"ProductsImages/Shtukaturka/4.png"
                });
                AddProduct(new Product
                {
                    Title = "SOFT BAND",
                    ShortDescription =
                        @"Штукатурка гипсовая, не требующая шпаклевания, для людей с любым уровнем подготовки",
                    FullDescription =
                        @"Штукатурка гипсовая, не требующая шпаклевания, для людей с любым уровнем подготовки",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 30 кг",
                    SubCategory = GetSubCategory("Гипсовая"),
                    ImageUrl = @"ProductsImages/Shtukaturka/5.png"
                });
                AddProduct(new Product
                {
                    Title = "EASY BAND",
                    ShortDescription =
                        @"Штукатурка гипсовая для людей с любым уровнем подготовки, не требующая шпаклевания",
                    FullDescription =
                        @"Штукатурка гипсовая для людей с любым уровнем подготовки, не требующая шпаклевания",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 30 кг",
                    SubCategory = GetSubCategory("Гипсовая"),
                    ImageUrl = @"ProductsImages/Shtukaturka/6.png"
                });
                AddProduct(new Product
                {
                    Title = "BAU PUTZ GIPS",
                    ShortDescription = @"Штукатурка гипсовая",
                    FullDescription = @"Штукатурка гипсовая",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 30 кг и 5 кг",
                    SubCategory = GetSubCategory("Гипсовая"),
                    ImageUrl = @"ProductsImages/Shtukaturka/7.png"
                });

                AddProduct(new Product
                {
                    Title = "DEKOR 2.0",
                    ShortDescription = @"Штукатурка декоративная короед",
                    FullDescription = @"Штукатурка декоративная короед",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок: 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/8.png"
                });
                AddProduct(new Product
                {
                    Title = "CRYSTAL 2.0",
                    ShortDescription = @"Штукатурка декоративная камешковая",
                    FullDescription = @"Штукатурка декоративная камешковая",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/9.png"
                });
                AddProduct(new Product
                {
                    Title = "BAU PUTZ ZEMENT",
                    ShortDescription = @"Штукатурка цементная для внутренних и наружных работ",
                    FullDescription = @"Штукатурка цементная для внутренних и наружных работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/10.png"
                });
                AddProduct(new Product
                {
                    Title = "CRYSTAL",
                    ShortDescription = "Штукатурка декоративная «камешковая»",
                    FullDescription = "Штукатурка декоративная «камешковая»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/11.png"
                });
                AddProduct(new Product
                {
                    Title = "DIADEMA",
                    ShortDescription = "Штукатурка декоративная «шуба»",
                    FullDescription = "Штукатурка декоративная «шуба»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/12.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKOR FASAD",
                    ShortDescription = "Штукатурка декоративная короед",
                    FullDescription = "Штукатурка декоративная короед",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/13.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKOR",
                    ShortDescription = "Штукатурка декоративная «короед»",
                    FullDescription = "Штукатурка декоративная «короед»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/14.png"
                });
                AddProduct(new Product
                {
                    Title = "UNTER BAU",
                    ShortDescription = "Цементная штукатурка для фасада и цоколя",
                    FullDescription = "Цементная штукатурка для фасада и цоколя",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/15.png"
                });
                AddProduct(new Product
                {
                    Title = "BAU BLOCK",
                    ShortDescription = "Штукатурка-шпаклевка для выравнивания и ремонта ячеистых блоков",
                    FullDescription = "Штукатурка-шпаклевка для выравнивания и ремонта ячеистых блоков",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/16.png"
                });
                AddProduct(new Product
                {
                    Title = "PRIMA INTERIER",
                    ShortDescription = "Штукатурка премиум класса",
                    FullDescription = "Штукатурка премиум класса",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Цементная"),
                    ImageUrl = @"ProductsImages/Shtukaturka/17.png"
                });
            }

            #endregion

            #region Floors

            private void AddFloors()
            {
                AddProduct(new Product
                {
                    Title = "EASY BODEN",
                    ShortDescription = "Самонивелирующийся наливной пол для людей с любым уровнем подготовки",
                    FullDescription = "Самонивелирующийся наливной пол для людей с любым уровнем подготовки",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Наливной"),
                    ImageUrl = @"ProductsImages/Poli/1.png"
                });
                AddProduct(new Product
                {
                    Title = "BODEN ZEMENT GROSS",
                    ShortDescription = "Наливной пол на цементной основе",
                    FullDescription = "Наливной пол на цементной основе",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Наливной"),
                    ImageUrl = @"ProductsImages/Poli/2.png"
                });
                AddProduct(new Product
                {
                    Title = "BODEN ZEMENT MEDIUM",
                    ShortDescription = "Наливной пол на цементной основе",
                    FullDescription = "Наливной пол на цементной основе",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Наливной"),
                    ImageUrl = @"ProductsImages/Poli/3.png"
                });
                AddProduct(new Product
                {
                    Title = "BODEN TURBO",
                    ShortDescription = "Самовыравнивающийся наливной пол",
                    FullDescription = "Самовыравнивающийся наливной пол",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 20 кг",
                    SubCategory = GetSubCategory("Наливной"),
                    ImageUrl = @"ProductsImages/Poli/4.png"
                });
                AddProduct(new Product
                {
                    Title = "BODEN STREET",
                    ShortDescription = "Наливной пол для наружных и внутренних работ",
                    FullDescription = "Наливной пол для наружных и внутренних работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Наливной"),
                    ImageUrl = @"ProductsImages/Poli/5.png"
                });
                AddProduct(new Product
                {
                    Title = "EASY FINAL",
                    ShortDescription = "Тонкослойный самонивелирующийся пол для любых, в том числе слабых оснований",
                    FullDescription = "Тонкослойный самонивелирующийся пол для любых, в том числе слабых оснований",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Финишный"),
                    ImageUrl = @"ProductsImages/Poli/6.png"
                });
                AddProduct(new Product
                {
                    Title = "BODEN ZEMENT FINAL",
                    ShortDescription = "Тонкий самонивелирующийся пол под любое покрытие",
                    FullDescription = "Тонкий самонивелирующийся пол под любое покрытие",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Финишный"),
                    ImageUrl = @"ProductsImages/Poli/7.png"
                });
                AddProduct(new Product
                {
                    Title = "BODEN NIVELIR",
                    ShortDescription =
                        "Тонкослойный самонивелирующийся пол для любых, в том числе слабых низкомарочных оснований",
                    FullDescription =
                        "Тонкослойный самонивелирующийся пол для любых, в том числе слабых низкомарочных оснований",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Финишный"),
                    ImageUrl = @"ProductsImages/Poli/8.png"
                });
                AddProduct(new Product
                {
                    Title = "BASE",
                    ShortDescription = "Базовый ровнитель на цементной основе",
                    FullDescription = "Базовый ровнитель на цементной основе",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Стяжка"),
                    ImageUrl = @"ProductsImages/Poli/9.png"
                });
                AddProduct(new Product
                {
                    Title = "ERSTE GRUND",
                    ShortDescription = "Выравнивающая стяжка для пола",
                    FullDescription = "Выравнивающая стяжка для пола",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Стяжка"),
                    ImageUrl = @"ProductsImages/Poli/10.png"
                });
            }

            #endregion

            #region PaintAndEnamel

            private void AddPaintAndEnamel()
            {
                AddProduct(new Product
                {
                    Title = "ПФ-115",
                    ShortDescription = "Эмаль алкидная для деревянных, металлических и бетонных поверхностей",
                    FullDescription = "Эмаль алкидная для деревянных, металлических и бетонных поверхностей",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро 25, 6 кг",
                    SubCategory = GetDefaultSubCategory("Краска и эмаль"),
                    ImageUrl = @"ProductsImages/KraskaIEmal/1.png"
                });
                AddProduct(new Product
                {
                    Title = "ЭМАЛЬ 3 В 1",
                    ShortDescription =
                        "Грунт-эмаль для защиты от коррозии, декоративной покраски металлических и бетонных поверхностей",
                    FullDescription =
                        "Грунт-эмаль для защиты от коррозии, декоративной покраски металлических и бетонных поверхностей",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро 22 кг, 5 кг",
                    SubCategory = GetDefaultSubCategory("Краска и эмаль"),
                    ImageUrl = @"ProductsImages/KraskaIEmal/2.png"
                });
                AddProduct(new Product
                {
                    Title = "ПАСТА КОЛЕРОВОЧНАЯ УНИВЕРСАЛЬНАЯ",
                    ShortDescription = "Колерование красок, эмалей, лаков, лазурей для дерева, штукатурок и затирок",
                    FullDescription = "Колерование красок, эмалей, лаков, лазурей для дерева, штукатурок и затирок",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Бутылка: 100 мл",
                    SubCategory = GetDefaultSubCategory("Краска и эмаль"),
                    ImageUrl = @"ProductsImages/KraskaIEmal/3.png"
                });
                AddProduct(new Product
                {
                    Title = "MINERAL TECHNO",
                    ShortDescription = "Краска водно-дисперсионная акриловая для фасадных работ",
                    FullDescription = "Краска водно-дисперсионная акриловая для фасадных работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: 9 и 18 л",
                    SubCategory = GetDefaultSubCategory("Краска и эмаль"),
                    ImageUrl = @"ProductsImages/KraskaIEmal/4.png"
                });
                AddProduct(new Product
                {
                    Title = "MINERAL TECHNO",
                    ShortDescription = "Краска силикатная для фасадных работ",
                    FullDescription = "Краска силикатная для фасадных работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро 18 л",
                    SubCategory = GetDefaultSubCategory("Краска и эмаль"),
                    ImageUrl = @"ProductsImages/KraskaIEmal/5.png"
                });
            }

            #endregion

            #region Grout

            private void AddGrout()
            {
                AddProduct(new Product
                {
                    Title = "ELAST POLYMER",
                    ShortDescription = "Готовая полимерная затирка для межплиточных швов до 10 мм",
                    FullDescription = "Готовая полимерная затирка для межплиточных швов до 10 мм",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Банка 2 кг",
                    SubCategory = GetDefaultSubCategory("Затирка"),
                    ImageUrl = @"ProductsImages/Zatirka/1.png"
                });
                AddProduct(new Product
                {
                    Title = "ELAST PREMIUM",
                    ShortDescription =
                        "Затирка для межплиточных швов до 10 мм с водоотталкивающим и противогрибковым эффектом цветная",
                    FullDescription =
                        "Затирка для межплиточных швов до 10 мм с водоотталкивающим и противогрибковым эффектом цветная",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Банка 2 кг",
                    SubCategory = GetDefaultSubCategory("Затирка"),
                    ImageUrl = @"ProductsImages/Zatirka/2.png"
                });
                AddProduct(new Product
                {
                    Title = "KITT",
                    ShortDescription = "Затирка цветная для межплиточных швов",
                    FullDescription = "Затирка цветная для межплиточных швов",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 2, 5, 25 кг",
                    SubCategory = GetDefaultSubCategory("Затирка"),
                    ImageUrl = @"ProductsImages/Zatirka/3.png"
                });
            }

            #endregion

            #region FacadeInsulationSystem

            private void AddFacadeInsulationSystem()
            {
                AddProduct(new Product
                {
                    Title = "DEKOR 2.0",
                    ShortDescription = "Штукатурка декоративная короед",
                    FullDescription = "Штукатурка декоративная короед",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок: 25 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/1.png"
                });
                AddProduct(new Product
                {
                    Title = "CRYSTAL 2.0",
                    ShortDescription = "Штукатурка декоративная камешковая",
                    FullDescription = "Штукатурка декоративная камешковая",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок: 25 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/2.png"
                });
                AddProduct(new Product
                {
                    Title = "FACADE GRUNT",
                    ShortDescription = "Грунтовка под декоративные штукатурки для наружных и внутренних работ",
                    FullDescription = "Грунтовка под декоративные штукатурки для наружных и внутренних работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро 14 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/3.png"
                });
                AddProduct(new Product
                {
                    Title = "CRYSTAL PASTA",
                    ShortDescription = "Готовая декоративная акриловая штукатурка «камешковая»",
                    FullDescription = "Готовая декоративная акриловая штукатурка «камешковая»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро: 25 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/4.png"
                });
                AddProduct(new Product
                {
                    Title = "CRYSTAL",
                    ShortDescription = "Штукатурка декоративная «камешковая»",
                    FullDescription = "Штукатурка декоративная «камешковая»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/5.png"
                });
                AddProduct(new Product
                {
                    Title = "DIADEMA",
                    ShortDescription = "Штукатурка декоративная «шуба»",
                    FullDescription = "Штукатурка декоративная «шуба»",
                    ManufacturerCompany = "Фасовка: Мешок 25 кг",
                    Packaging = "",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/6.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKOR FASAD",
                    ShortDescription = "Штукатурка декоративная короед",
                    FullDescription = "Штукатурка декоративная короед",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/7.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKOR",
                    ShortDescription = "Штукатурка декоративная «короед»",
                    FullDescription = "Штукатурка декоративная «короед»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/8.png"
                });
                AddProduct(new Product
                {
                    Title = "ISOFIX",
                    ShortDescription = "Клей для пенополистирола, минваты и нанесения армирующего слоя",
                    FullDescription = "Клей для пенополистирола, минваты и нанесения армирующего слоя",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/9.png"
                });
                AddProduct(new Product
                {
                    Title = "ISOFASAD",
                    ShortDescription = "Состав для плит из пенополистирола и минеральной ваты",
                    FullDescription = "Состав для плит из пенополистирола и минеральной ваты",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/10.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKOR PASTA",
                    ShortDescription = "Готовая декоративная акриловая штукатурка «КОРОЕД»",
                    FullDescription = "Готовая декоративная акриловая штукатурка «КОРОЕД»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро: 25 кг",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/11.png"
                });
                AddProduct(new Product
                {
                    Title = "MINERAL TECHNO",
                    ShortDescription = "Краска водно-дисперсионная акриловая для фасадных работ",
                    FullDescription = "Краска водно-дисперсионная акриловая для фасадных работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: 9 и 18 л",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/12.png"
                });
                AddProduct(new Product
                {
                    Title = "MINERAL TECHNO",
                    ShortDescription = "Краска силикатная для фасадных работ",
                    FullDescription = "Краска силикатная для фасадных работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро 18 л",
                    SubCategory = GetDefaultSubCategory("Система утепления фасадов"),
                    ImageUrl = @"ProductsImages/SistemaUteplenyaFasadov/13.png"
                });
            }

            #endregion

            #region ForHoneycombBlocks

            private void AddForHoneycombBlocks()
            {
                AddProduct(new Product
                {
                    Title = "KLEBEN BLOCK",
                    ShortDescription = "Клей для укладки ячеистых блоков",
                    FullDescription = "Клей для укладки ячеистых блоков",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Для ячеистых блоков"),
                    ImageUrl = @"ProductsImages/DliaYacheistihBlokov/1.png"
                });
                AddProduct(new Product
                {
                    Title = "KLEBEN BLOCK ЗИМА",
                    ShortDescription = "Клей для укладки ячеистых блоков",
                    FullDescription = "Клей для укладки ячеистых блоков",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Для ячеистых блоков"),
                    ImageUrl = @"ProductsImages/DliaYacheistihBlokov/2.png"
                });
                AddProduct(new Product
                {
                    Title = "BAU BLOCK",
                    ShortDescription = "Штукатурка-шпаклевка для выравнивания и ремонта ячеистых блоков",
                    FullDescription = "Штукатурка-шпаклевка для выравнивания и ремонта ячеистых блоков",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Для ячеистых блоков"),
                    ImageUrl = @"ProductsImages/DliaYacheistihBlokov/3.png"
                });
            }

            #endregion

            #region Glue

            private void AddGlue()
            {
                AddProduct(new Product
                {
                    Title = "KERAMIK",
                    ShortDescription = "Клей (С0) для керамической плитки",
                    FullDescription = "Клей (С0) для керамической плитки",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 20 кг, 25 кг",
                    SubCategory = GetSubCategory("Клеи для плитки"),
                    ImageUrl = @"ProductsImages/Kley/1.png"
                });
                AddProduct(new Product
                {
                    Title = "KERAMIK PLUS",
                    ShortDescription = "Усиленный плиточный клей",
                    FullDescription = "Усиленный плиточный клей",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок: 25 кг",
                    SubCategory = GetSubCategory("Клеи для плитки"),
                    ImageUrl = @"ProductsImages/Kley/2.png"
                });
                AddProduct(new Product
                {
                    Title = "KERAMIK PRO",
                    ShortDescription = "Клей усиленный для плитки и керамогранита, С1Т",
                    FullDescription = "Клей усиленный для плитки и керамогранита, С1Т",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Фасовка: Мешок 5, 20 и 25 кг",
                    SubCategory = GetSubCategory("Клеи для плитки"),
                    ImageUrl = @"ProductsImages/Kley/3.png"
                });
                AddProduct(new Product
                {
                    Title = "KERAMIK PROFI",
                    ShortDescription = "Клей универсальный",
                    FullDescription = "Клей универсальный",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Клеи для плитки"),
                    ImageUrl = @"ProductsImages/Kley/4.png"
                });
                AddProduct(new Product
                {
                    Title = "MAXIMUM PLUS",
                    ShortDescription =
                        "Клей (С1ТЕ) для всех видов плитки и любых оснований, от «теплого пола\" до фасада",
                    FullDescription =
                        "Клей (С1ТЕ) для всех видов плитки и любых оснований, от «теплого пола\" до фасада",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 25 кг",
                    SubCategory = GetSubCategory("Клеи для плитки"),
                    ImageUrl = @"ProductsImages/Kley/5.png"
                });
                AddProduct(new Product
                {
                    Title = "MOSAIK",
                    ShortDescription = "Клей (С2ТЕ) белый для мозаики и прозрачной плитки",
                    FullDescription = "Клей (С2ТЕ) белый для мозаики и прозрачной плитки",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 25 кг",
                    SubCategory = GetSubCategory("Клеи для плитки"),
                    ImageUrl = @"ProductsImages/Kley/6.png"
                });
                AddProduct(new Product
                {
                    Title = "KERAMIK TERMO",
                    ShortDescription = "Термостойкий клей для печей, каминов и теплого пола",
                    FullDescription = "Термостойкий клей для печей, каминов и теплого пола",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок: 25 кг",
                    SubCategory = GetSubCategory("Клеи для плитки"),
                    ImageUrl = @"ProductsImages/Kley/7.png"
                });
                AddProduct(new Product
                {
                    Title = "GRANIT",
                    ShortDescription = "Клей (С2ТЕ) для керамогранита, природного и искусственного камня",
                    FullDescription = "Клей (С2ТЕ) для керамогранита, природного и искусственного камня",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 25 кг",
                    SubCategory = GetSubCategory("Клеи для плитки"),
                    ImageUrl = @"ProductsImages/Kley/8.png"
                });
                AddProduct(new Product
                {
                    Title = "BODEN FIXER",
                    ShortDescription = "Клей для напольных покрытий профессиональный",
                    FullDescription = "Клей для напольных покрытий профессиональный",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро: 10, 3 кг",
                    SubCategory = GetSubCategory("Клеи для обоев и напольных покрытий"),
                    ImageUrl = @"ProductsImages/Kley/9.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKO FIXER",
                    ShortDescription = "Клей для стеклообоев и стеклохолста",
                    FullDescription = "Клей для стеклообоев и стеклохолста",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро: 10, 3 кг",
                    SubCategory = GetSubCategory("Клеи для обоев и напольных покрытий"),
                    ImageUrl = @"ProductsImages/Kley/10.png"
                });
                AddProduct(new Product
                {
                    Title = "ISOFIX",
                    ShortDescription = "Клей для пенополистирола, минваты и нанесения армирующего слоя",
                    FullDescription = "Клей для пенополистирола, минваты и нанесения армирующего слоя",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Клеи для систем утепления фасадов"),
                    ImageUrl = @"ProductsImages/Kley/11.png"
                });
                AddProduct(new Product
                {
                    Title = "ISOFASAD",
                    ShortDescription = "Состав для плит из пенополистирола и минеральной ваты",
                    FullDescription = "Состав для плит из пенополистирола и минеральной ваты",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Клеи для систем утепления фасадов"),
                    ImageUrl = @"ProductsImages/Kley/12.png"
                });
                AddProduct(new Product
                {
                    Title = "KLEBEN BLOCK",
                    ShortDescription = "Клей для укладки ячеистых блоков",
                    FullDescription = "Клей для укладки ячеистых блоков",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Клеи для ячеистых блоков"),
                    ImageUrl = @"ProductsImages/Kley/13.png"
                });
                AddProduct(new Product
                {
                    Title = "KLEBEN BLOCK ЗИМА",
                    ShortDescription = "Клей для укладки ячеистых блоков",
                    FullDescription = "Клей для укладки ячеистых блоков",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Клеи для ячеистых блоков"),
                    ImageUrl = @"ProductsImages/Kley/14.png"
                });
            }

            #endregion

            #region Waterproofing

            private void AddWaterproofing()
            {
                AddProduct(new Product
                {
                    Title = "HYDRO-TEC MEMBRANE",
                    ShortDescription = "Гидроизоляционная мастика под плиточные облицовки",
                    FullDescription = "Гидроизоляционная мастика под плиточные облицовки",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро: 4 и 13 кг",
                    SubCategory = GetDefaultSubCategory("Гидроизоляция"),
                    ImageUrl = @"ProductsImages/Gidroizolatsia/1.png"
                });
                AddProduct(new Product
                {
                    Title = "HYDROSTOP",
                    ShortDescription = "Цементная гидроизоляция обмазочного типа",
                    FullDescription = "Цементная гидроизоляция обмазочного типа",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 20 кг",
                    SubCategory = GetDefaultSubCategory("Гидроизоляция"),
                    ImageUrl = @"ProductsImages/Gidroizolatsia/2.png"
                });
                AddProduct(new Product
                {
                    Title = "HYDROLENTA",
                    ShortDescription = "Гидроизоляционная лента",
                    FullDescription = "Гидроизоляционная лента",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: 10 метров",
                    SubCategory = GetDefaultSubCategory("Гидроизоляция"),
                    ImageUrl = @"ProductsImages/Gidroizolatsia/3.png"
                });
                AddProduct(new Product
                {
                    Title = "HYDROPLOMBA",
                    ShortDescription = "Сверхбыстротвердеющая смесь для устранения активных протечек",
                    FullDescription = "Сверхбыстротвердеющая смесь для устранения активных протечек",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро 600 г",
                    SubCategory = GetDefaultSubCategory("Гидроизоляция"),
                    ImageUrl = @"ProductsImages/Gidroizolatsia/4.png"
                });
            }

            #endregion

            #region RepairCompounds

            private void AddRepairCompounds()
            {
                AddProduct(new Product
                {
                    Title = "EASY MASTER",
                    ShortDescription = "Ремонтный универсальный состав на цементной основе для восстановления поверхностей",
                    FullDescription = "Ремонтный универсальный состав на цементной основе для восстановления поверхностей",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 кг",
                    SubCategory = GetDefaultSubCategory("Ремонтные составы"),
                    ImageUrl = @"ProductsImages/RemontnieSostavi/1.png"
                });
            }

            #endregion

            #region Putty

            private void AddPutty()
            {
                AddProduct(new Product
                {
                    Title = "UNI PASTA",
                    ShortDescription = "Влагостойкая готовая к употреблению финишная полимерная шпатлевка",
                    FullDescription = "Влагостойкая готовая к употреблению финишная полимерная шпатлевка",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Банка 25, 5 кг",
                    SubCategory = GetSubCategory("Акриловая", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/1.png"
                });
                AddProduct(new Product
                {
                    Title = "EASY FINISH",
                    ShortDescription = "Универсальная шпаклевка",
                    FullDescription = "Универсальная шпаклевка",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 20 кг",
                    SubCategory = GetSubCategory("Цементная", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/2.png"
                });
                AddProduct(new Product
                {
                    Title = "FINISH ZEMENT",
                    ShortDescription = "Финишная шпаклевка на цементной основе",
                    FullDescription = "Финишная шпаклевка на цементной основе",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 20 кг",
                    SubCategory = GetSubCategory("Цементная", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/3.png"
                });
                AddProduct(new Product
                {
                    Title = "GLATTE ZEMENT",
                    ShortDescription = "Базовая шпаклевка на цементной основе для цоколя и фасада",
                    FullDescription = "Базовая шпаклевка на цементной основе для цоколя и фасада",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetSubCategory("Цементная", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/4.png"
                });
                AddProduct(new Product
                {
                    Title = "UNI FINISH",
                    ShortDescription = "Базовая универсальная цементная шпаклёвка",
                    FullDescription = "Базовая универсальная цементная шпаклёвка",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 20 кг",
                    SubCategory = GetSubCategory("Цементная", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/5.png"
                });
                AddProduct(new Product
                {
                    Title = "FUGEN GIPS",
                    ShortDescription = "Универсальная шпаклевка на гипсовой основе",
                    FullDescription = "Универсальная шпаклевка на гипсовой основе",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 25 кг",
                    SubCategory = GetSubCategory("Гипсовая", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/6.png"
                });
                AddProduct(new Product
                {
                    Title = "FINISH GIPS",
                    ShortDescription = "Финишная шпаклевка на гипсовой основе, трещиностойкая",
                    FullDescription = "Финишная шпаклевка на гипсовой основе, трещиностойкая",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 18 кг",
                    SubCategory = GetSubCategory("Гипсовая", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/7.png"
                });
                AddProduct(new Product
                {
                    Title = "SILK GIPS",
                    ShortDescription = "Финишная шпаклевка на гипсовой основе",
                    FullDescription = "Финишная шпаклевка на гипсовой основе",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 18 кг",
                    SubCategory = GetSubCategory("Гипсовая", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/8.png"
                });
                AddProduct(new Product
                {
                    Title = "FINISH POLYMER+",
                    ShortDescription = "Суперфинишная шпаклевка на полимерной основе",
                    FullDescription = "Суперфинишная шпаклевка на полимерной основе",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок: 5 и 20 кг",
                    SubCategory = GetSubCategory("Полимерная", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/9.png"
                });
                AddProduct(new Product
                {
                    Title = "FINISH PLAST",
                    ShortDescription = "Финишная шпаклевка на полимерной основе",
                    FullDescription = "Финишная шпаклевка на полимерной основе",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок: 5 и 20 кг",
                    SubCategory = GetSubCategory("Полимерная", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/10.png"
                });
                AddProduct(new Product
                {
                    Title = "SILK POLYMER+",
                    ShortDescription = "Финишная шпаклевка на полимерной основе",
                    FullDescription = "Финишная шпаклевка на полимерной основе",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 5 и 25 кг",
                    SubCategory = GetSubCategory("Полимерная", "Shpaklevka"),
                    ImageUrl = @"ProductsImages/Shpaklevka/11.png"
                });
            }

            #endregion

            #region Primer

            private void AddPrimer()
            {
                AddProduct(new Product
                {
                    Title = "TIEFGRUNT COLOR",
                    ShortDescription =
                        "Грунтовка глубокого проникновения для наружных и внутренних работ с цветовым индикатором нанесения",
                    FullDescription =
                        "Грунтовка глубокого проникновения для наружных и внутренних работ с цветовым индикатором нанесения",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Канистра 10, 5 л",
                    SubCategory = GetDefaultSubCategory("Грунтовка"),
                    ImageUrl = @"ProductsImages/Gruntovka/1.png"
                });
                AddProduct(new Product
                {
                    Title = "ГФ-021",
                    ShortDescription = "Антикоррозийный грунт для наружных и внутренних работ",
                    FullDescription = "Антикоррозийный грунт для наружных и внутренних работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро 25, 6 кг",
                    SubCategory = GetDefaultSubCategory("Грунтовка"),
                    ImageUrl = @"ProductsImages/Gruntovka/2.png"
                });
                AddProduct(new Product
                {
                    Title = "FACADE GRUNT",
                    ShortDescription = "Грунтовка под декоративные штукатурки для наружных и внутренних работ",
                    FullDescription = "Грунтовка под декоративные штукатурки для наружных и внутренних работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро 14 кг",
                    SubCategory = GetDefaultSubCategory("Грунтовка"),
                    ImageUrl = @"ProductsImages/Gruntovka/3.png"
                });
                AddProduct(new Product
                {
                    Title = "TIEFGRUNT",
                    ShortDescription = "Грунтовка глубокого проникновения для наружных и внутренних работ",
                    FullDescription = "Грунтовка глубокого проникновения для наружных и внутренних работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Канистра 10 и 5 л",
                    SubCategory = GetDefaultSubCategory("Грунтовка"),
                    ImageUrl = @"ProductsImages/Gruntovka/4.png"
                });
                AddProduct(new Product
                {
                    Title = "AQUA GRUNT",
                    ShortDescription = "Грунтовка-концентрат для наружных и внутренних работ",
                    FullDescription = "Грунтовка-концентрат для наружных и внутренних работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Канистра 10 и 5 л",
                    SubCategory = GetDefaultSubCategory("Грунтовка"),
                    ImageUrl = @"ProductsImages/Gruntovka/5.png"
                });
                AddProduct(new Product
                {
                    Title = "BETON KONTAKT",
                    ShortDescription = "Сцепляющая (адгезионная) акриловая грунтовка для наружных и внутренних работ",
                    FullDescription = "Сцепляющая (адгезионная) акриловая грунтовка для наружных и внутренних работ",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро 14 и 7 кг",
                    SubCategory = GetDefaultSubCategory("Грунтовка"),
                    ImageUrl = @"ProductsImages/Gruntovka/6.png"
                });
                AddProduct(new Product
                {
                    Title = "PRIMAGRUNT",
                    ShortDescription = "Грунтовка универсальная",
                    FullDescription = "Грунтовка универсальная",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Канистра 10 и 5 л",
                    SubCategory = GetDefaultSubCategory("Грунтовка"),
                    ImageUrl = @"ProductsImages/Gruntovka/7.png"
                });
            }

            #endregion

            #region DecorativePlaster

            private void AddDecorativePlaster()
            {
                AddProduct(new Product
                {
                    Title = "DEKOR 2.0",
                    ShortDescription = "Штукатурка декоративная короед",
                    FullDescription = "Штукатурка декоративная короед",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок: 25 кг",
                    SubCategory = GetDefaultSubCategory("Декоративная штукатурка"),
                    IsActive = true,
                    ImageUrl = @"ProductsImages/DecorativnayaShtukaturka/1.png"
                });
                AddProduct(new Product
                {
                    Title = "CRYSTAL PASTA",
                    ShortDescription = "Готовая декоративная акриловая штукатурка «камешковая»",
                    FullDescription = "Готовая декоративная акриловая штукатурка «камешковая»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро: 25 кг",
                    SubCategory = GetDefaultSubCategory("Декоративная штукатурка"),
                    IsActive = true,
                    ImageUrl = @"ProductsImages/DecorativnayaShtukaturka/2.png"
                });
                AddProduct(new Product
                {
                    Title = "CRYSTAL",
                    ShortDescription = "Штукатурка декоративная «камешковая»",
                    FullDescription = "Штукатурка декоративная «камешковая»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Декоративная штукатурка"),
                    ImageUrl = @"ProductsImages/DecorativnayaShtukaturka/3.png"
                });
                AddProduct(new Product
                {
                    Title = "DIADEMA",
                    ShortDescription = "Штукатурка декоративная «шуба»",
                    FullDescription = "Штукатурка декоративная «шуба»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Декоративная штукатурка"),
                    ImageUrl = @"ProductsImages/DecorativnayaShtukaturka/4.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKOR FASAD",
                    ShortDescription = "Штукатурка декоративная короед",
                    FullDescription = "Штукатурка декоративная короед",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Декоративная штукатурка"),
                    ImageUrl = @"ProductsImages/DecorativnayaShtukaturka/5.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKOR",
                    ShortDescription = "Штукатурка декоративная «короед»",
                    FullDescription = "Штукатурка декоративная «короед»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Мешок 25 кг",
                    SubCategory = GetDefaultSubCategory("Декоративная штукатурка"),
                    ImageUrl = @"ProductsImages/DecorativnayaShtukaturka/6.png"
                });
                AddProduct(new Product
                {
                    Title = "DEKOR PASTA",
                    ShortDescription = "Готовая декоративная акриловая штукатурка «КОРОЕД»",
                    FullDescription = "Готовая декоративная акриловая штукатурка «КОРОЕД»",
                    ManufacturerCompany = "",
                    Packaging = "Фасовка: Ведро: 25 кг",
                    SubCategory = GetDefaultSubCategory("Декоративная штукатурка"),
                    ImageUrl = @"ProductsImages/DecorativnayaShtukaturka/7.png"
                });
            }

            #endregion

            private SubCategory GetSubCategory(string name)
            {
                return _databaseContext.SubCategories.FirstOrDefault(x => x.Name == name);
            }
            
            private SubCategory GetSubCategory(string name, string slug)
            {
                return _databaseContext.SubCategories.FirstOrDefault(x => x.Category.Slug == slug && x.Name == name);
            }

            private SubCategory GetDefaultSubCategory(string categoryName)
            {
                return _databaseContext.SubCategories.FirstOrDefault(x =>
                    x.Category.Name == categoryName && x.Name == InitializeSubCategories.DefaultSubCategoryName);
            }
        }
        
        private class InitializeProductsOverviews : IInitializableTable
        {
            public void Execute()
            {
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Штукатурка"),
                    Title = "Штукатурки",
                    Subtitle = "Непревзойденная прочность",
                    Description = @"Штукатурка – это материал, который предназначен для отделки поверхностей, придания им формы, фактуры, текстуры, защиты стен и помещений от влаги и перепадов температуры, подготовки поверхностей под покраску, окончательную декоративную отделку. Виды штукатурки стен зависят от условий будущей эксплуатации и поверхности, на которую они будут наноситься.",
                    ImageUrl = "OverviewBackgroundImages/Shpaklevka.png"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Полы"),
                    Title = "Полы",
                    Subtitle = "Идеальная поверхность",
                    Description = @"Выравнивание пола – одна из самых важных ремонтных задач. Необходимость в выравнивании возникает в процессе капитальной реконструкции и во время косметического ремонта, когда снимается старое покрытие, и открываются все недостатки базового пола.Ровнители служат для устранения различных дефектов, которые может иметь пол после окончания процесса строительства.",
                    ImageUrl = "OverviewBackgroundImages/Poli.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Краска и эмаль"),
                    Title = "Краска и эмаль",
                    Subtitle = "Для яркой жизни",
                    Description = @"Краска защитит от грибка и плесени, легкая колеровка в любые цвета, походит для систем утепления фасадов.Эмаль для защиты от коррозии, декоративной покраски металлических и бетонных поверхностей.",
                    ImageUrl = "OverviewBackgroundImages/KraskaIEmal.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Затирка"),
                    Title = "Затирка",
                    Subtitle = "Швы под защитой",
                    Description = @"Затирка для швов плитки — это смесь, предназначенная для заполнения и герметизации швов. Затирка выполняет не только декоративную функцию, она позволяет укрепить плитку - без нее покрытие вскоре начнет отделяться от стен, требуя повторного ремонта. Затирка предотвращает попадание влаги и пыли в плиточные швы, а также предупреждает развитие плесени.",
                    ImageUrl = "OverviewBackgroundImages/Zatirka.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Система утепления фасадов"),
                    Title = "Система утепления фасадов",
                    Subtitle = "Для самого крутого фасада",
                    Description = @"Система, которая поможет сократить затраты на тепловую, электрическую энергию, увеличит звукоизолирующие свойства внешней стены.",
                    ImageUrl = "OverviewBackgroundImages/SistemaUtepleniaFasadov.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Для ячеистых блоков"),
                    Title = "Кладочные смеси",
                    Subtitle = "Надежная и прочная кладка   ",
                    Description = @"Кладочная смесь подходит для работы с основными строительными материалами:Кирпич (керамический, силикатный, клинкерный), блок (керамический, силикатный, бетонный).",
                    ImageUrl = "OverviewBackgroundImages/YacheistiiSmesi.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Клей"),
                    Title = "Клей для плитки",
                    Subtitle = "Уверенность в результате",
                    Description = @"Плиточный клей используется для проведения работ по монтажу керамической плитки, керамогранита, камня и иных материалов.Правильный выбор клея для укладки играет большую роль. Ведь это напрямую влияет на качество облицовки и ее долговечность.",
                    ImageUrl = "OverviewBackgroundImages/Kley.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Гидроизоляция"),
                    Title = "Гидроизоляция",
                    Subtitle = "Влага не пройдет",
                    Description = @"Гидроизоляция – защита строительных конструкций от пагубного воздействия воды и прочих жидкостей. Гидроизоляция обеспечивает долгосрочную эксплуатацию сооружений и конструкций путем повышения их долговечности и надежности. «Влагозащитные работы» востребованы на всех этапах строительства – на этапе закладки фундамента, во время установки перекрытий, возведения стен и при производстве стяжки пола.",
                    ImageUrl = "OverviewBackgroundImages/Gidroizolyatsiya.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Ремонтные составы"),
                    Title = "Ремонтный состав",
                    Subtitle = "Универсальный продукт",
                    Description = @"Подходит для ремонта и заделки различных отверстий и сколов перед дальнейшей отделкой, выравнивания локальных участков, крепления маяков на вертикальные и горизонтальные поверхности.",
                    ImageUrl = "OverviewBackgroundImages/Depositphotos.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Шпаклевка"),
                    Title = "Шпаклевка",
                    Subtitle = "Гладкий результат",
                    Description = @"Шпаклевка представляет собой специальный состав, который используется для выравнивания, а также декоративной отделки стен и потолков в помещении. Они бывают разных типов и отличаются в зависимости от своего состава, назначения и степени готовности к использованию.
По составу шпаклевки делятся на гипсовые, цементные и полимерные.",
                    ImageUrl = "OverviewBackgroundImages/Shtukaturka.png"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Грунтовка"),
                    Title = "Грунтовка",
                    Subtitle = "Для уверенного грунтования",
                    Description = @"Грунтовка – это специальный жидкий состав, который наносится на обрабатываемую поверхность перед покраской, шпаклёвкой и другими работами.
Грунтовка помогает сцеплению двух разных по фактуре материалов, способствуют легкому нанесению шпатлевки, штукатурки, ровному распределению краски, предотвращает накопление влаги внутри пор оснований.",
                    ImageUrl = "OverviewBackgroundImages/Gruntovka.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Декоративная штукатурка"),
                    Title = "Декоративная штукатурка",
                    Subtitle = "Для самых изысканных интерьеров",
                    Description = @"",
                    ImageUrl = "OverviewBackgroundImages/Maxresdefault.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Строительный песок"),
                    Title = "Строительный песок",
                    Subtitle = @"Многопрофильный инертный материал, высокого качества, предназначен для широкого спектра назначения, таких как:
Производство кирпича и бетона
При строительстве дорог
Металлургия
Производство ССС
Стекольная промышленность",
                    Description = @"Контакты менеджера: Паклин Антон Александрович
8-982-675-93-62
Почта: a.paklin@bergauf.ru",
                    ImageUrl = "OverviewBackgroundImages/Pesok.jpg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Химия"),
                    Title = "Формиат кальция",
                    Subtitle = "Формиат кальция применяется в качестве добавки в бетон, клей, штукатурку. Обладает сильно выраженными противоморозными свойствами. Позволяет производить работы при отрицательных температурах, повышая прочность используемого материала.",
                    Description = @"По вопросам закупки можно обращаться:
Ведущий специалист по закупкам
Жуковская Елена
Моб . +7 (912) 255-49-42
E-mail: E.Zhukovskaya@bergauf.ru",
                    ImageUrl = "OverviewBackgroundImages/Himiya_2.jpeg"
                });
                AddOverview(new OverviewProduct
                {
                    Category = GetCategoryByName("Для ремонта и защиты бетона"),
                    Title = "Для ремонта и защиты бетона",
                    Subtitle = "Для ремонта и защиты бетона",
                    Description = @"Для ремонта и защиты бетона",
                    ImageUrl = "OverviewBackgroundImages/.png"
                });
            }
        }
    }
}