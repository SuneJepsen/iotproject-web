using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class FirebaseConnectionString
    {
        public readonly static string RawDataDoor = "https://raw-data-door.firebaseio.com/.json";
        public readonly static string RawDataFloor = "https://raw-data-floor.firebaseio.com/.json";
        public readonly static string CopyDataDoor = "https://copy-raw-data-door.firebaseio.com/.json";
        public readonly static string CopyDataFloor = "https://copy-raw-data-floor.firebaseio.com/.json";
        public readonly static string ProcessedDataDoor = "https://processed-data-door.firebaseio.com/.json";
        public readonly static string ProcessedDataFloor = "https://processed-data-floor.firebaseio.com/.json";
        public readonly static string InferredData = "https://inferred-data.firebaseio.com/.json";
    }
}
