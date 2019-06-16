namespace Task3.BL
{
    public interface ILanguage
    {
        string GetLanguageString(string xmlPath);
        string GetCurrentLanguage();
    }
}