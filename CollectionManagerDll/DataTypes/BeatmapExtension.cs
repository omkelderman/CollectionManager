using System;
using System.Collections.Generic;

namespace CollectionManager.DataTypes
{
    public class BeatmapExtension : Beatmap
    {
        #region ICeBeatmapProps
        public string Name { get { return this.ToString(); } }

        public bool DataDownloaded { get; set; }
        public bool LocalBeatmapMissing { get; set; }
        public bool LocalVersionDiffers { get; set; }
        public string UserComment { get; set; } = "";

        #endregion

        #region Custom Field Stuff

        private Dictionary<string, object> _customFields = new();

        public void SetCustomFieldValues(BeatmapExtension other)
        {
            _customFields = new Dictionary<string, object>();
            foreach(var kvp in other._customFields)
            {
                _customFields.Add(kvp.Key, kvp.Value);
            }
        }

        public void SetCustomFieldValue(string key, object value)
        {
            _customFields[key] = value;
        }

        public object GetCustomFieldValue(string key)
        {
            return _customFields.TryGetValue(key, out var value) ? value : null;
        }

        public IEnumerable<string> GetAllStringCustomFieldValues()
        {
            foreach(var customField in _customFields)
            {
                if(customField.Value is string stringValue) yield return stringValue;
            }
        }

        #endregion
    }
}