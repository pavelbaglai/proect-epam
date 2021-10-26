namespace Music_Store.Models.Interfaces
{
    public interface IPurchasable
    {
        int ID { get; set; }
        int ArtistID { get; set; }
        string Name { get; set; }
        string ImagePath { get; set; }
        float Price { get; set; }

        Artist Artist { get; set; }

        string GetCategory();
    }
}
