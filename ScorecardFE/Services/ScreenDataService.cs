using Newtonsoft.Json.Linq;

namespace ScorecardFE.Services
{
    public class ScreenDataService
    {
        // string 1 = screen name, string 2 = item name
        private Dictionary<string, Dictionary<string, ScreenData>> Data { get; } = new Dictionary<string, Dictionary<string, ScreenData>>();

        public void RegisterData(string screenName, string inputKey, ScreenData inputData)
        {
            if (!Data.TryGetValue(screenName, out var screenData))
            {
                screenData = new Dictionary<string, ScreenData>();
                Data.Add(screenName, screenData);
            }

            screenData[inputKey] = inputData;
        }

        public void RegisterData(string screenName, Dictionary<string, ScreenData> inputData)
        {
            if (!Data.TryGetValue(screenName, out var screenData))
            {
                screenData = new Dictionary<string, ScreenData>();
                Data.Add(screenName, screenData);
            }

            foreach (var (key, value) in inputData)
            {
                screenData[key] = value;
            }
        }

        public ScreenData? RetrieveData(string screenName, string dataKey)
        {
            if (Data.TryGetValue(screenName, out var screenData) && screenData.TryGetValue(dataKey, out var value))
            {
                return value;
            }

            return null;
        }
    }

    public class ScreenData
    {
    }

    public class ScreenData_Bool : ScreenData
    {
        public bool? Value { get; set; }
    }
}
