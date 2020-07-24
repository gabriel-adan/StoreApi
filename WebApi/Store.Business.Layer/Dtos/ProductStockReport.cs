namespace Store.Business.Layer.Dtos
{
    public class ProductStockReport : ItemReport
    {
        public string Category { get; set; }
        public double Stock { get; set; }
    }
}
