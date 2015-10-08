using System;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

public class GMapsValueConverter : PropertyValueConverterBase
{
    public override bool IsConverter(PublishedPropertyType propertyType)
    {
        return "Netaddicts.GMaps".Equals(propertyType.PropertyEditorAlias);
    }

    public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
    {
        if (source == null || string.IsNullOrWhiteSpace(source.ToString())) return null;
     
        var coordinates = source.ToString().Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
        var loc = new GMapsLocation();
        loc.Lat = decimal.Parse(coordinates[0]);
        loc.Lng = decimal.Parse(coordinates[1]);
        return loc;
    }
}