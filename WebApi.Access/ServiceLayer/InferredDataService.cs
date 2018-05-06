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
                deviceDataDoor.Title = allCopyDataDoor.FirstOrDefault(x=>x.Title!=null)?.Title;
                deviceDataDoor.Type = allCopyDataDoor.FirstOrDefault(x=>x.Type != null)?.Type;
                int count = 0;
                for (int i = 0; i < allCopyDataDoor.Count; i++)
                {
                    if (allCopyDataDoor[i].Id == doorGuid.Value)
                    {
                        count = i+1;
                        break;
                    }
                }
                if (count == allCopyDataDoor.Count - 1) // take the last element
                {
                    deviceDataDoor.Measurements.Add(allCopyDataDoor[count]);
                }
                else if (count > allCopyDataDoor.Count - 1) // no more elements to return
                {
                    deviceDataDoor.Measurements = new List<Measurement>();
                }
                else
                {
                    deviceDataDoor.Measurements=allCopyDataDoor.GetRange(count, allCopyDataDoor.Count - count);

                }
                deviceDataList.Add(deviceDataDoor);
            }
            else if(allCopyDataDoor!= null)
            {
                deviceDataDoor = new DeviceData();
                deviceDataDoor.Title = allCopyDataDoor.FirstOrDefault(x => x.Title!= null)?.Title;
                deviceDataDoor.Type = allCopyDataDoor.FirstOrDefault(x=>x.Type!=null)?.Type;
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
                        count = i+1;
                        break;
                    }
                }
                if (count == allCopyDataFloor.Count - 1)// take the last element
                {
                    deviceDataFloor.Measurements.Add(allCopyDataFloor[count]);

                }
                else if (count > allCopyDataFloor.Count - 1) // no more elements to return
                {
                    deviceDataFloor.Measurements = new List<Measurement>();
                }
                else
                {
                    deviceDataFloor.Measurements = allCopyDataFloor.GetRange(count, allCopyDataFloor.Count - count);
                }
                deviceDataList.Add(deviceDataFloor);
            }
            else if (allCopyDataFloor != null)
            {
                deviceDataFloor = new DeviceData();
                deviceDataFloor.Title = allCopyDataFloor.FirstOrDefault(x=>x.Title!=null)?.Title;
                deviceDataFloor.Type = allCopyDataFloor.FirstOrDefault(x => x.Type != null)?.Type;
                deviceDataFloor.Measurements = allCopyDataFloor;
                deviceDataList.Add(deviceDataFloor);
            }
            return deviceDataList;
        }
    }
}