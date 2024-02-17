namespace Spurious2.Core.LcboImporting.Domain
{
    public partial class Store
    {
        public Store()
        {
            this.Inventories = [];
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int? BeerVolume { get; set; }
        public int? WineVolume { get; set; }
        public int? SpiritsVolume { get; set; }
        //public IGeometry Location { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public virtual ICollection<Inventory> Inventories { get; private set; }
    }
}
