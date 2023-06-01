namespace BusinessObject
{
    public class PagingModel<T>
    {
        public List<T> Items { get; set; } = new List<T>();

        public int TotalPage { get; set; }
    }
}
