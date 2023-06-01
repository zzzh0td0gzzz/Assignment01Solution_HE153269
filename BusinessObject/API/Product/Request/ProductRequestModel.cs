namespace BusinessObject.API.Product.Request
{
    public class ProductRequestModel : PagingRequestModel
    {
        public string? Name { get; set; }

        public bool? UnitPriceSortAsc { get; set; }
    }
}
