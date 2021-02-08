using System;

namespace WpfApp20.Models
{
    class forecast
    {
        public int Id { get; set; }
        public string Level0 { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Level4 { get; set; }
        public string ItemName { get; set; }
        public int ItemId { get; set; }
        public string InventLocationId { get; set; }
        public DateTime Date { get; set; }
        public float qty20 { get; set; }
        public float forecast20 { get; set; }
        public float qty19 { get; set; }
    }
}
