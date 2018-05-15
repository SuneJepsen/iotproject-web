namespace DataLayer.Repository
{
    public class FirebaseConnectionString
    {
        public readonly static string RawDataDoor = "https://raw-data-floor.firebaseio.com/{0}/{1}.json"; // accelometer 
        public readonly static string RawDataFloor = "https://raw-data-floor.firebaseio.com/{0}/{1}.json"; // Promixitmity
        public readonly static string RawDataDoorNodeId = "1CC51E";
        public readonly static string RawDataFloorNodeId = "1CC3A6";



        public readonly static string CopyDataDoor = "https://copy-raw-data-door.firebaseio.com/{0}.json";
        public readonly static string CopyDataFloor = "https://copy-raw-data-floor.firebaseio.com/{0}.json";
        public readonly static string InferredData = "https://inferred-data.firebaseio.com/{0}.json";
        public readonly static string HandShakeData = "https://handshake-data.firebaseio.com/{0}.json";

    }
}
