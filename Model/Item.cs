using SQLite;

namespace MauiDataApp.Models
{
    public class Item
    {
        [PrimaryKey]
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }
    }
}