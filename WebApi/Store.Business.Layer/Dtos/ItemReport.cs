namespace Store.Business.Layer.Dtos
{
    public abstract class ItemReport
    {
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual string Mark { get; set; }
        public virtual string Type { get; set; }
        public virtual string Color { get; set; }
        public virtual string Size { get; set; }
        public virtual double UnitCost { get; set; }
        public virtual double UnitPrice { get; set; }
    }
}
