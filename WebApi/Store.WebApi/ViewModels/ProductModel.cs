namespace Store.WebApi.ViewModels
{
    public class ProductModel
    {
        public string Code { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Mark { get; set; }
        public string Type { get; set; }

        public int Specification { get; set; }
        public int Color { get; set; }
        public int Size { get; set; }
        public int Category { get; set; }
    }
}
