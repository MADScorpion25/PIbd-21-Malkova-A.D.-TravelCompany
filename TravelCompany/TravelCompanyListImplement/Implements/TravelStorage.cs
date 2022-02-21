using System;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyContracts.BindingModels;
using TravelCompanyContracts.StorageContracts;
using TravelCompanyContracts.ViewModels;
using TravelCompanyListImplement.Models;

namespace TravelCompanyListImplement.Implements
{
    public class TravelStorage : ITravelStorage
    {
        private readonly DataListSingleton source;
        public TravelStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<TravelViewModel> GetFullList()
        {
            var result = new List<TravelViewModel>();
            foreach (var component in source.Travels)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }
        public List<TravelViewModel> GetFilteredList(TravelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<TravelViewModel>();
            foreach (var Travel in source.Travels)
            {
                if (Travel.TravelName.Contains(model.TravelName))
                {
                    result.Add(CreateModel(Travel));
                }
            }
            return result;
        }
        public TravelViewModel GetElement(TravelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var Travel in source.Travels)
            {
                if (Travel.Id == model.Id || Travel.TravelName ==
                model.TravelName)
                {
                    return CreateModel(Travel);
                }
            }
            return null;
        }
        public void Insert(TravelBindingModel model)
        {
            var tempTravel = new Travel
            {
                Id = 1,
                TravelConditions = new
            Dictionary<int, int>()
            };
            foreach (var Travel in source.Travels)
            {
                if (Travel.Id >= tempTravel.Id)
                {
                    tempTravel.Id = Travel.Id + 1;
                }
            }
            source.Travels.Add(CreateModel(model, tempTravel));
        }
        public void Update(TravelBindingModel model)
        {
            Travel tempTravel = null;
            foreach (var Travel in source.Travels)
            {
                if (Travel.Id == model.Id)
                {
                    tempTravel = Travel;
                }
            }
            if (tempTravel == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempTravel);
        }
        public void Delete(TravelBindingModel model)
        {
            for (int i = 0; i < source.Travels.Count; ++i)
            {
                if (source.Travels[i].Id == model.Id)
                {
                    source.Travels.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private static Travel CreateModel(TravelBindingModel model, Travel
        travel)
        {
            travel.TravelName = model.TravelName;
            travel.Price = model.Price;
            foreach (var key in travel.TravelConditions.Keys.ToList())
            {
                if (!model.TravelConditions.ContainsKey(key))
                {
                    travel.TravelConditions.Remove(key);
                }
            }
            foreach (var component in model.TravelConditions)
            {
                if (travel.TravelConditions.ContainsKey(component.Key))
                {
                    travel.TravelConditions[component.Key] =
                    model.TravelConditions[component.Key].Item2;
                }
                else
                {
                    travel.TravelConditions.Add(component.Key,
                    model.TravelConditions[component.Key].Item2);
                }
            }
            return travel;
        }
        private TravelViewModel CreateModel(Travel Travel)
        {
            var TravelConditions = new Dictionary<int, (string, int)>();
            foreach (var pc in Travel.TravelConditions)
            {
                string componentName = string.Empty;
                foreach (var component in source.Conditions)
                {
                    if (pc.Key == component.Id)
                    {
                        componentName = component.ConditionName;
                        break;
                    }
                }
                TravelConditions.Add(pc.Key, (componentName, pc.Value));
            }
            return new TravelViewModel
            {
                Id = Travel.Id,
                TravelName = Travel.TravelName,
                Price = Travel.Price,
                TravelConditions = TravelConditions
            };
        }
    }
}
