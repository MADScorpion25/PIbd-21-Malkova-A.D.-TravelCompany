using System;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyBusinessLogic.OfficePackage;
using TravelCompanyBusinessLogic.OfficePackage.HelperModels;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.BusinessLogicsContracts;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;

namespace TravelCompanyBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly ITravelStorage _travelStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(ITravelStorage travelStorage, IOrderStorage orderStorage, IWarehouseStorage warehouseStorage,
        AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord,
       AbstractSaveToPdf saveToPdf)
        {
            _travelStorage = travelStorage;
            _orderStorage = orderStorage;
            _warehouseStorage = warehouseStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
                .Select(x => new ReportOrdersViewModel
                {
                    DateCreate = x.DateCreate,
                    TravelName = x.TravelName,
                    Count = x.Count,
                    Sum = x.Sum,
                    Status = x.Status
                })
                .ToList();
        }

        public List<ReportTravelConditionViewModel> GetTravelCondition()
        {
            var travels = _travelStorage.GetFullList();
            var list = new List<ReportTravelConditionViewModel>();
            foreach (var travel in travels)
            {
                var record = new ReportTravelConditionViewModel
                {
                    TravelName = travel.TravelName,
                    Conditions = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var condition in travel.TravelConditions)
                {
                    record.Conditions.Add(new Tuple<string, int>(condition.Value.Item1, condition.Value.Item2));
                    record.TotalCount += condition.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public void SaveTravelsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список путевок",
                Travels = _travelStorage.GetFullList()
            });
        }

        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }

        public void SaveTravelConditionToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список условий путевок",
                TravelConditions = GetTravelCondition()
            });
        }

        public List<ReportWarehouseConditionViewModel> GetWarehouseConditions()
        {
            var travels = _warehouseStorage.GetFullList();
            var list = new List<ReportWarehouseConditionViewModel>();
            foreach (var travel in travels)
            {
                var record = new ReportWarehouseConditionViewModel
                {
                    WarehouseName = travel.WarehouseName,
                    Conditions = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var condition in travel.WarehouseConditions)
                {
                    record.Conditions.Add(new Tuple<string, int>(condition.Value.Item1, condition.Value.Item2));
                    record.TotalCount += condition.Value.Item2;
                }
                list.Add(record);
            }
            return list;
        }

        public void SaveWarehouseConditionToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateWarehouseReport(new ExcelInfoWarehouses
            {
                FileName = model.FileName,
                Title = "Список условий складов",
                WarehouseConditions = GetWarehouseConditions()
            });
        }

        public void SaveWarehousesToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateWarehouseDoc(new WordInfoWarehouses
            {
                FileName = model.FileName,
                Title = "Список складов",
                Warehouses = _warehouseStorage.GetFullList()
            });
        }
    }
}