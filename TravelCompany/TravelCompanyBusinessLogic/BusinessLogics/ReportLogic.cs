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
        private readonly IConditionStorage _conditionStorage;
        private readonly ITravelStorage _travelStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;
        public ReportLogic(ITravelStorage travelStorage, IConditionStorage
       conditionStorage, IOrderStorage orderStorage,
        AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord,
       AbstractSaveToPdf saveToPdf)
        {
            _travelStorage = travelStorage;
            _conditionStorage = conditionStorage;
            _orderStorage = orderStorage;
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
            var conditions = _conditionStorage.GetFullList();
            var travels = _travelStorage.GetFullList();
            var list = new List<ReportTravelConditionViewModel>();
            foreach (var condition in conditions)
            {
                var record = new ReportTravelConditionViewModel
                {
                    ConditionName = condition.ConditionName,
                    Travels = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var travel in travels)
                {
                    if (travel.TravelConditions.ContainsKey(condition.Id))
                    {
                        record.Travels.Add(new Tuple<string, int>(travel.TravelName,
                       travel.TravelConditions[condition.Id].Item2));
                        record.TotalCount +=
                       travel.TravelConditions[condition.Id].Item2;
                    }
                }
                list.Add(record);
            }
            return list;
        }

        public void SaveConditionsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список условий",
                Conditions = _conditionStorage.GetFullList()
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
                Title = "Список условий",
                TravelConditions = GetTravelCondition()
            });
        }
    }
}
