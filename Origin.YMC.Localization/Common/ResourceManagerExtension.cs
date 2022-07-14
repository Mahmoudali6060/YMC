using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Localization.Common
{
    public static class ResourceManagerExtension
    {
        public static Dictionary<string, string> GetAllResources(this ResourceManager resourceManager, string locale)
        {
            var resourceSet = resourceManager.GetResourceSet(new System.Globalization.CultureInfo(locale), true, true);
            var dictNumerator = resourceSet.GetEnumerator();
            var result = new Dictionary<string, string>();
            // Get all string resources
            while (dictNumerator.MoveNext())
            {
                // Only string resources
                if (dictNumerator.Value is string)
                {
                    var key = (string)dictNumerator.Key;
                    var value = (string)dictNumerator.Value;
                    result.Add(key, value);
                }
            }
            return result;
        }

        public static string GetGlobalResourceByPreferredLang(this string Resource, int PreferredLang)
        {
            var culture = PreferredLang == 1 ? new CultureInfo("en-US") : new CultureInfo("ar-SA");
            return GlobalStrings.ResourceManager.GetString(Resource, culture);
        }

        public static string GetGlobalMessagesByPreferredLang(this string Resource, int PreferredLang)
        {
            var culture = PreferredLang == 1 ? new CultureInfo("en-US") : new CultureInfo("ar-SA");
            return GlobalMessages.ResourceManager.GetString(Resource, culture);
        }
    }
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        private readonly ResourceManager _resource;
        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            _resource = new ResourceManager(resourceType);
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                string displayName = _resource.GetString(_resourceKey);

                return string.IsNullOrEmpty(displayName)
                    ? string.Format("[[{0}]]", _resourceKey)
                    : displayName;
            }
        }
    }
}
