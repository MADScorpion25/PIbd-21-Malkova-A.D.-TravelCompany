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
            Table table = new Table();
            CreateWord(info);
            CreateTable(table);
            TableRow tableRowHeader = new TableRow();

            TableCell cellHeaderName = new TableCell();
            cellHeaderName.Append(new TableCellProperties(
                new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
            cellHeaderName.Append(new Paragraph(new Run(new Text("Название"))));

            TableCell cellHeaderPerson = new TableCell();
            cellHeaderPerson.Append(new TableCellProperties(
                new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
            cellHeaderPerson.Append(new Paragraph(new Run(new Text("ФИО ответственного"))));

            TableCell cellHeaderDateCreate = new TableCell();
            cellHeaderDateCreate.Append(new TableCellProperties(
                new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
            cellHeaderDateCreate.Append(new Paragraph(new Run(new Text("Дата создания"))));

            tableRowHeader.Append(cellHeaderName);
            tableRowHeader.Append(cellHeaderPerson);
            tableRowHeader.Append(cellHeaderDateCreate);

            table.Append(tableRowHeader);

            foreach (var warehouse in info.Warehouses)
            {
                TableRow tableRow = new TableRow();

                TableCell cellName = new TableCell();
                cellName.Append(new TableCellProperties(
                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                cellName.Append(new Paragraph(new Run(new Text(warehouse.WarehouseName))));

                TableCell cellPerson = new TableCell();
                cellPerson.Append(new TableCellProperties(
                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                cellPerson.Append(new Paragraph(new Run(new Text(warehouse.ResponsibleFullName))));

                TableCell cellDateCreate = new TableCell();
                cellDateCreate.Append(new TableCellProperties(
                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                cellDateCreate.Append(new Paragraph(new Run(new Text(warehouse.CreateDate.ToString()))));

                tableRow.Append(cellName);
                tableRow.Append(cellPerson);
                tableRow.Append(cellDateCreate);

                table.Append(tableRow);
            }
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new
                        WordTextProperties {Bold = true, Size = "24", } ) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
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
        /// <summary>
        /// Создание абзаца с текстом
        /// </summary>
        /// <param name="paragraph"></param>
        /// <returns></returns>
        protected abstract void CreateTable(Table table);
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <param name="info"></param>
        protected abstract void SaveWord(WordInfo info);
    }
}