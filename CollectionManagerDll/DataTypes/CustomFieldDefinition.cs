using CollectionManager.Enums;

namespace CollectionManager.DataTypes
{
    public class CustomFieldDefinition
    {
        public string Key { get; set; }
        public CustomFieldType Type { get; set; }
        public string DisplayText { get; set; }

        public bool TypeIsNumeric => Type switch
        {
            (>= CustomFieldType.UInt8 and <= CustomFieldType.Int64) or CustomFieldType.Single or CustomFieldType.Double => true,
            _ => false
        };
    }
}
