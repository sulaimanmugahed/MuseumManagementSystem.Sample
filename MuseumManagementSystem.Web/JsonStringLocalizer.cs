using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace MuseumManagementSystem.Web
{

    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly IDistributedCache _cache;
        private readonly JsonSerializer _jsonSerializer = new();

        public JsonStringLocalizer(IDistributedCache cache)
        {
            _cache = cache;
        }
        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name,value);
            }
        }

        public LocalizedString this[string name, params object[] arguments] 
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments))
                    : actualValue;
            }
        }


        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";

            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(stream);
            using JsonTextReader jsonTextReader = new(streamReader);

            while(jsonTextReader.Read())
            {
                if (jsonTextReader.TokenType != JsonToken.PropertyName)
                    continue;

                var key = jsonTextReader.Value as string;
                jsonTextReader.Read();
                var value = _jsonSerializer.Deserialize<string>(jsonTextReader);
                yield return new LocalizedString(key, value);
            }
        }

        private string GetString(string key)
        {
            var filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            var fullFilePath = Path.GetFullPath(filePath);

            if (File.Exists(fullFilePath))
            {
                var cacheKey = $"local_{Thread.CurrentThread.CurrentCulture.Name}_{key}";
                var cacheValue = _cache.GetString(cacheKey);

                if (!string.IsNullOrEmpty(cacheValue))
                    return cacheValue;

                var result = GetValueFromJSON(key, fullFilePath);

                if(!string.IsNullOrEmpty(result))
                    _cache.SetString(cacheKey, result);

                return result;
            }

            return string.Empty;
        }

        private string GetValueFromJSON(string propertyName, string filePath)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
                return string.Empty;

            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(stream);
            using JsonTextReader jsonTextReader = new(streamReader);

            while (jsonTextReader.Read())
            {
                if (jsonTextReader.TokenType == JsonToken.PropertyName && jsonTextReader.Value as string == propertyName)
                {
                    jsonTextReader.Read();
                    return _jsonSerializer.Deserialize<string>(jsonTextReader);
                }
            }

            return string.Empty;
        }
    }
}



