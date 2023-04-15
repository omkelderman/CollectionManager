﻿using System;
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

        private Dictionary<string, object> _customFields;

        public void SetCustomFieldValues(BeatmapExtension other)
        {
            if(other._customFields == null)
            {
                _customFields = null;
            } else
            {
                _customFields = new Dictionary<string, object>(other._customFields);
            }
        }

        public void SetCustomFieldValue(string key, object value)
        {
            _customFields ??= new Dictionary<string, object>();
            _customFields[key] = value;
        }

        public object GetCustomFieldValue(string key)
        {
            if(_customFields == null ) return null;
            return _customFields.TryGetValue(key, out var value) ? value : null;
        }

        public IEnumerable<string> GetAllStringCustomFieldValues()
        {
            if (_customFields == null) yield break;
            foreach(var customField in _customFields)
            {
                if(customField.Value is string stringValue) yield return stringValue;
            }
        }

        #endregion
    }
}