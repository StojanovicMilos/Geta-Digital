namespace Task3.BLL.Interfaces
{
    public interface ILanguage
    {
        string GetLanguageString(string xmlPath);
        string GetCurrentLanguage();
    }
}