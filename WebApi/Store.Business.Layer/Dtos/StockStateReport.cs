namespace Store.Business.Layer.Dtos
{
    public class StockStateReport : ItemReport
    {
        public int Orders { get; set; }
        public int Sales { get; set; }
    }
}
