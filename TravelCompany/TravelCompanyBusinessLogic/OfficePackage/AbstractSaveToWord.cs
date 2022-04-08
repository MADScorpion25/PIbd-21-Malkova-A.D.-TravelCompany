using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;
using TravelCompanyBusinessLogic.OfficePackage.HelperModels;

namespace TravelCompanyBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new
WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var travel in info.Travels)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {(travel.TravelName+"    ", new WordTextProperties { Size = "24", Bold = true }),
                        (travel.Price.ToString() , new WordTextProperties{ Size = "24", })},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);
        }
        public void CreateWarehouseDoc(WordInfoWarehouses info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24" }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            CreateTable(new List<string>() { "Название", "ФИО ответственного", "Дата создания" });
            foreach (var warehouse in info.Warehouses)
            {
                AddRowTable(new List<string>() {
                    warehouse.WarehouseName,
                    warehouse.ResponsibleFullName,
                    warehouse.CreateDate.ToShortDateString()
                });
            }
            SaveWord(info);
        }
        /// <summary>
        /// Создание doc-файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void CreateWord(WordInfo info);
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        protected abstract void CreateParagraph(WordParagraph paragraph);
        protected abstract void CreateTable(List<string> tableHeaderInfo);
        protected abstract void AddRowTable(List<string> tableRowInfo);
        protected abstract void SaveWord(WordInfo info);
    }
}