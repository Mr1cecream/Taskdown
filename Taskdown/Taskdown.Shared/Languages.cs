using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Taskdown
{
    struct Language
    {
        public string Tag { get; }
        public string DisplayName { get;  }
        public string LocalName { get; }

        public Language(string tag, string displayName, string localName = null)
        {
            this.Tag = tag;
            this.DisplayName = displayName;
            if (localName != null)
                this.LocalName = localName;
            else
                this.LocalName = displayName;
        }

        public override string ToString()
        {
            return $"{DisplayName} ({LocalName})";
        }
    }

    class Languages
    {
        private static readonly Language english = new Language("en", "English");
        private static readonly Language hebrew = new Language("he", "Hebrew", "עברית");

        public static readonly ObservableCollection<Language> languages = new ObservableCollection<Language>()
        {
            english,
            hebrew,
        };

        public static void SetAppLanguage(string tag)
        {
            var culture = new System.Globalization.CultureInfo(tag);
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.IetfLanguageTag;
        }

        public static void SetAppLanguage(Language lang)
        {
            SetAppLanguage(lang.Tag);
        }
    }
}
