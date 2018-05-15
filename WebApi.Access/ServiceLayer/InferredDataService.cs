using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Domain;
using DataLayer.Facade;
using WebApi.Access.Domain;

namespace WebApi.Access.ServiceLayer
{
    public class InferredDataService
    {
        private IFacade _facade;
        public InferredDataService(IFacade facade)
        {
            _facade = facade;
        }
        public DeviceData GetInferredDataForCurrentDay(Guid? guid)
        {
            DeviceData deviceDataInferred = new DeviceData();
            var allInferredData = _facade.GetAllInferredData();
            deviceDataInferred.Measurements =new List<Measurement>();
            if (guid.HasValue && allInferredData != null)
            {
                deviceDataInferred.Title = allInferredData.FirstOrDefault()?.Title;
                int count = 0;
                for (int i = 0; i < allInferredData.Count; i++)
                {
                    if (allInferredData[i].Id == guid.Value)
                    {
                        count = i+1;
                        break;
                    }
                }
                if (count == allInferredData.Count - 1) // take the last element
                {
                    deviceDataInferred.Measurements = new List<Measurement>();
                    deviceDataInferred.Measurements.Add(allInferredData[count]);
                }
                else if(count > allInferredData.Count - 1) // no more elements to return
                {
                    deviceDataInferred.Measurements = new List<Measurement>();
                }
                else
                {
                    deviceDataInferred.Measurements =
                        allInferredData.GetRange(count, (allInferredData.Count - count));
                }

                return deviceDataInferred;
            }
            else if(allInferredData != null)
            {
                deviceDataInferred.Measurements = allInferredData;
                deviceDataInferred.Title = allInferredData.FirstOrDefault()?.Title;
            }
            return deviceDataInferred;
        }
        public List<DeviceData> GetCopyDataForCurrentDay(DateTime? endDate)
        {
            List<DeviceData>deviceDataList = new List<DeviceData>();
            var allCopySensorData = _facade.GetAllCopyData();
            DeviceData deviceData = null;
            List<Measurement> filteredCopySensorData = allCopySensorData.GroupBy(sensor => sensor.Type).Select(group => group.First()).ToList();
            foreach (var sensor in filteredCopySensorData)
            {
                var measurements = allCopySensorData.Where(x => x.Type == sensor.Type).ToList();
                if (measurements.Any())
                {
                    deviceData = new DeviceData();
                    deviceData.Title = measurements.FirstOrDefault(x => x.Title != null)?.Title;
                    deviceData.Type = measurements.FirstOrDefault(x=>x.Type != null)?.Type;

                    if (endDate.HasValue)
                    {
                        deviceData.Measurements = measurements.Where(x => x.EndDate > endDate.Value).ToList();
                    }
                    else
                    {
                        deviceData.Measurements = measurements;
                    }
                    deviceDataList.Add(deviceData);
                }
            }
            return deviceDataList;
        }
    }
}