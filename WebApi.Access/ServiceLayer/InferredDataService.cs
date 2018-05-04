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
            deviceDataInferred.Measurements = _facade.GetAllInferredData();
            if (guid.HasValue && deviceDataInferred.Measurements != null)
            {
                deviceDataInferred.Title = deviceDataInferred.Measurements.FirstOrDefault()?.Title;
                List<Measurement> returnMeasurements = new List<Measurement>();
                int count = 0;
                for (int i = 0; i < deviceDataInferred.Measurements.Count; i++)
                {
                    if (deviceDataInferred.Measurements[i].Id == guid.Value)
                    {
                        count = i;
                        break;
                    }
                }
                deviceDataInferred.Measurements =
                    deviceDataInferred.Measurements.GetRange(count, deviceDataInferred.Measurements.Count);
                return deviceDataInferred;
            }
            else if(deviceDataInferred.Measurements != null)
            {
                deviceDataInferred.Title = deviceDataInferred.Measurements.FirstOrDefault()?.Title;
            }
            return deviceDataInferred;
        }
        public List<DeviceData> GetCopyDataForCurrentDay(Guid? doorGuid, Guid? floorGuid)
        {
            List<DeviceData>deviceDataList = new List<DeviceData>();
            DeviceData deviceDataDoor;
            DeviceData deviceDataFloor;
            var allCopyDataDoor = _facade.GetAllCopyDataDoor();
            var allCopyDataFloor = _facade.GetAllCopyDataFloor();
            if (doorGuid.HasValue && allCopyDataDoor != null)
            {
                deviceDataDoor = new DeviceData();
                deviceDataDoor.Title = allCopyDataDoor.FirstOrDefault()?.Title;
                int count = 0;
                for (int i = 0; i < allCopyDataDoor.Count; i++)
                {
                    if (allCopyDataDoor[i].Id == doorGuid.Value)
                    {
                        count = i;
                        break;
                    }
                }
                deviceDataDoor.Measurements = allCopyDataDoor.GetRange(count, allCopyDataDoor.Count);
                deviceDataList.Add(deviceDataDoor);
            }
            else if(allCopyDataDoor!= null)
            {
                deviceDataDoor = new DeviceData();
                deviceDataDoor.Title = allCopyDataDoor.FirstOrDefault()?.Title;
                deviceDataDoor.Measurements = allCopyDataDoor;
                deviceDataList.Add(deviceDataDoor);
            }
            if (floorGuid.HasValue && allCopyDataFloor != null)
            {
                deviceDataFloor = new DeviceData();
                deviceDataFloor.Title = allCopyDataFloor.FirstOrDefault()?.Title;
                int count = 0;
                for (int i = 0; i < allCopyDataFloor.Count; i++)
                {
                    if (allCopyDataFloor[i].Id == floorGuid.Value)
                    {
                        count = i;
                        break;
                    }
                }
                deviceDataFloor.Measurements= allCopyDataFloor.GetRange(count, allCopyDataFloor.Count);
                deviceDataList.Add(deviceDataFloor);
            }
            else if (allCopyDataFloor != null)
            {
                deviceDataFloor = new DeviceData();
                deviceDataFloor.Title = allCopyDataFloor.FirstOrDefault()?.Title;
                deviceDataFloor.Measurements = allCopyDataFloor;
                deviceDataList.Add(deviceDataFloor);
            }
            return deviceDataList;
        }
    }
}