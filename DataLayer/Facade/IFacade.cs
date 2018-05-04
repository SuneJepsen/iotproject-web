using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;

namespace DataLayer.Facade
{
    public interface IFacade
    {
        List<Measurement> GetAllRawDataFloorAsMeasurement();
        List<Measurement> GetAllRawDataDoorAsMeasurement();
        void SaveCopyFloorMeasurements(List<Measurement> measurements);
        void SaveCopyDoorMeasurements(List<Measurement> measurements);
        void SaveInferredMeasurements(List<Measurement> measurements);
        void DeleteRawDataFloor();
        void DeleteRawDataDoor();
        List<Measurement> GetAllInferredData();
        List<Measurement> GetAllCopyDataDoor();
        List<Measurement> GetAllCopyDataFloor();

    }
}
