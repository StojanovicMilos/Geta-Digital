namespace Task3.DAL
{
    public class ListItem
    {
        private readonly string _detail;
        private string municipality;

        public ListItem(string municipality)
        {
            this.municipality = municipality;
        }

        public ListItem(string detail, string municipality)
        {
            _detail = detail;
            this.municipality = municipality;
        }
    }
}