namespace CounterStrike2RconTool.Shared.Services
{
    public class UpdateConfiguration : IUpdateConfiguration
    {
        /// <summary>
        /// update appsettings.{environment}.json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="environment"></param>
        public void Update<T>(string key, T value, string? environment = null)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"appSettings.{(string.IsNullOrEmpty(environment) ? "" : $"{environment}.")}json");
            string json = File.ReadAllText(filePath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            var sectionPaths = key.Split(":").ToList();
            jsonObj = SetValue(jsonObj, sectionPaths, 0, value);

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, output);
        }

        private dynamic SetValue<T>(dynamic jsonObj, List<string> sectionPaths, int index, T value)
        {
            if (sectionPaths.Count > index)
            {
                jsonObj[sectionPaths[index]] = SetValue(jsonObj[sectionPaths[index]], sectionPaths, ++index, value);
            }
            else
            {
                jsonObj = value;
            }
            return jsonObj;
        }
    }
}
