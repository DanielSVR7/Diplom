namespace project1.Models
{
    partial class Categories
    {
        public override string ToString()
        {
            return CategoryName;
        }
    }
    partial class Colors
    {
        public override string ToString()
        {
            return ColorName;
        }
    }
    partial class Manufacturers
    {
        public override string ToString()
        {
            return CompanyName;
        }
    }
    partial class EnergyClasses
    {
        public override string ToString()
        {
            return EnergyClassName;
        }
    }
    partial class ScreenSizes
    {
        public override string ToString()
        {
            return ScreenSizeInInches + "\"";
        }
    }
    partial class ScreenResolutions
    {
        public override string ToString()
        {
            return ScreenResolutionName + '(' + ScreenResolution + ')';
        }
    }
    partial class BacklightTypes
    {
        public override string ToString()
        {
            return BacklightTypeName;
        }
    }
    partial class OperatingSystems
    {
        public override string ToString()
        {
            return OperatingSystemName;
        }
    }
    partial class FreezerLocations
    {
        public override string ToString()
        {
            return FreezerLocationName;
        }
    }
}
