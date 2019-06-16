namespace Task3.BL
{
    public class Language : ILanguage
    {
        public string GetLanguageString(string xmlPath)
        {
            return EPiServer.Core.LanguageManager.Instance.Translate(xmlPath, GetCurrentLanguage());
        }

        public string GetCurrentLanguage()
        {
            return EPiServer.Globalization.ContentLanguage.PreferredCulture.Name;
        }
    }
}
