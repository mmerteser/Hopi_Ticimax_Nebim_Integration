using System.Xml.Serialization;

namespace Hoppo.Models.DTOs.Product
{
    [XmlRoot(ElementName = "product")]
    public class Product
    {
        public List<Item> Products { get; set; } = new List<Item>();
    }

    [XmlRoot(ElementName = "item")]
    public class Item
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; } = string.Empty;

        [XmlElement(ElementName = "title")]
        public string Title { get; set; } = string.Empty;

        [XmlElement(ElementName = "description")]
        public string Description { get; set; } = string.Empty;

        [XmlElement(ElementName = "link")]
        public string Link { get; set; } = string.Empty;

        [XmlElement(ElementName = "image_link")]
        public string Image_Link { get; set; } = string.Empty;

        [XmlElement(ElementName = "additional_image1_link")]
        public string Additional_Image1_Link { get; set; } = string.Empty;

        [XmlElement(ElementName = "additional_image2_link")]
        public string Additional_Image2_Link { get; set; } = string.Empty;

        [XmlElement(ElementName = "additional_image3_link")]
        public string Additional_Image3_Link { get; set; } = string.Empty;

        [XmlElement(ElementName = "price")]
        public double Price { get; set; }

        [XmlElement(ElementName = "sale_price")]
        public double Sale_Price { get; set; }

        [XmlElement(ElementName = "google_product_category")]
        public string Google_Product_Category { get; set; } = string.Empty;

        [XmlElement(ElementName = "brand")]
        public string Brand { get; set; } = string.Empty;

        [XmlElement(ElementName = "gtin")]
        public string Gtin { get; set; } = string.Empty;

        [XmlElement(ElementName = "item_group_id")]
        public string Item_Group_Id { get; set; } = string.Empty;

        [XmlElement(ElementName = "condition")]
        public string Condition { get; set; } = string.Empty;

        [XmlElement(ElementName = "color")]
        public string Color { get; set; } = string.Empty;

        [XmlElement(ElementName = "gender")]
        public string Gender { get; set; } = string.Empty;

        [XmlElement(ElementName = "size")]
        public string Size { get; set; } = string.Empty;

        [XmlElement(ElementName = "stock")]
        public int Stock { get; set; }

        [XmlElement(ElementName = "online_stock")]
        public int Online_Stock { get; set; }

        [XmlElement(ElementName = "online_price")]
        public double Online_Price { get; set; }

        [XmlElement(ElementName = "online_sale_price")]
        public double Online_Sale_Price { get; set; }

        [XmlElement(ElementName = "mpn")]
        public string Mpn { get; set; } = string.Empty;

        [XmlElement(ElementName = "year")]
        public string Year { get; set; } = string.Empty;

        [XmlElement(ElementName = "season")]
        public string Season { get; set; } = string.Empty;
    }
}

