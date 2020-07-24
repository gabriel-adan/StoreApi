namespace Store.Business.Layer.Dtos
{
    public abstract class ItemReport
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double UnitCost { get; set; }
        public double UnitPrice { get; set; }
    }
}
