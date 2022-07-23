using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.Model;

namespace TsentrstroyAPI.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class SeedDataController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        
        public SeedDataController(DatabaseContext databaseContext)
        {
            SeedData.SetDatabaseContext(databaseContext);
            _databaseContext = databaseContext;
        }
        
        [HttpGet]
        public void Get()
        {
            SeedData.Initialize();
        }

        [HttpGet("SingleAdd")]
        public IActionResult SingleAdd()
        {
            return BadRequest();
            
            MoreAtProduct moreAtProduct = new MoreAtProduct();
            
            moreAtProduct.Gost = "ГОСТ Р 55818-2018";
            
            moreAtProduct.Advantages = new List<string>()
            {
                "Белая",
                "Для внутренних и наружных работ",
                "Устойчива к возникновению усадочных трещин",
                "Долговечная и атмосферостойкая",
                "Паропроницаемая",
                "Моющаяся"
            };
            moreAtProduct.AreasOfUse = new List<AreasOfUseTab>()
            {
                new AreasOfUseTab()
                {
                    Title = "Тип основания",
                    Items = new List<string>()
                    {
                        "Армированный базовый штукатурный слой",
                        "Бетон, железобетон, ячеистый бетон",
                        "Цементно-песчаные, цементно-известковые штукатурки",
                        "Гипсовая штукатурка",
                        "Цементные шпатлевки",
                        "Листовые гипсо-,цементно-, дерево содержащие материалы"
                    }
                },
                new AreasOfUseTab()
                {
                    Title = "Сфера работ",
                    Items = new List<string>()
                    {
                        "Внутренние работы.Нормальная влажность",
                        "Внутренние работы. Повышенная влажность",
                        "Наружные работы. Фасад",
                        "Наружные работы. Цоколь",
                    }
                },
                new AreasOfUseTab()
                {
                    Title = "Качество поверхности",
                    Items = new List<string>()
                    {
                        "Под покраску"
                    }
                },
                new AreasOfUseTab()
                {
                    Title = "Способ нанесения",
                    Items = new List<string>()
                    {
                        "Ручной",
                        "Машинный",
                    }
                }
            };

            moreAtProduct.Compound = "Стирол-акриловая дисперсия, пигменты, наполнители, присадки, консервант";

            moreAtProduct.FeatureList = new List<FeatureListTab>()
            {
                new FeatureListTab()
                {
                    Title   = "Характеристика продукта",
                    Items = new List<string>()
                    {
                        "Вяжущее|Стирол-акриловая дисперсия",
                        "Фракция|1-1,5 мм"
                    }
                },
                new FeatureListTab()
                {
                    Title   = "Характеристика применения",
                    Items = new List<string>()
                    {
                        "Расход на 1 кв. м.|2,5 кг**",
                        "Толщина нанесения|1,5 мм",
                        "Проведение работ при температуре основания|от +5 до +30 С"
                    }
                },
                new FeatureListTab()
                {
                    Title   = "",
                    Items = new List<string>()
                    {
                        "Цвет|Белый",
                        "Плотность|1,85 кг/дм³",
                        "Время корректировки и формирования рисунка|25-35 минут",
                        "Высыхание поверхности|24 часа",
                        "Устойчивость к атмосферным осадкам|не ранее, чем через 24 часа",
                        "Последующие работы|Через 3 суток",
                        "Морозостойкость адгезионного контакта|100 циклов",
                        "Адгезия к бетону|1,8 МПа",
                        "Температура эксплуатации|от –50 до +70°С",
                        "Температура хранения|от +5 до +30°С",
                        "Температура транспортировки*|от +5 до +35°С"
                    }
                }
            };

            moreAtProduct.Notes = new List<string>()
            {
                "*Допускается 5-кратное замораживание общей продолжительностью не более трех недель при температуре не ниже минус 20°С",
                "**Указан нормативный расход для предварительной оценки необходимого объема материала, установленный производителем в эталонных лабораторных условиях. Фактические значения могут отличаться, что обусловлено качеством подготовки основания, его перепадами и неровностями, квалификацией исполнителей работ, используемыми инструментами, а также другими факторами, за которые производитель ответственности – не несет."
            };

            moreAtProduct.Product =  _databaseContext.Products.FirstOrDefault(p => p.Id == 1);
            
            _databaseContext.MoreAtProducts.Add(moreAtProduct);
            _databaseContext.SaveChanges();
            
            return Ok();
        }
    }
}