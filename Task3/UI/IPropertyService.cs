namespace Task3.UI
{
    public interface IPropertyService
    {
        string GetStringProperty(CurrentPage currentPage, string emailSubject);
        string GetPageDataPropertyLinkUrl(CurrentPage currentPage, string formName);
    }
}