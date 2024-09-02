using System.Text.Json.Serialization;

namespace SmartLibrary.Core.Models;

// Query

public class ImageLinks
{
    [JsonPropertyName("smallThumbnail")]
    public string SmallThumbnail { get; set; }

    [JsonPropertyName("thumbnail")]
    public string Thumbnail { get; set; }

    [JsonPropertyName("large")]
    public string Large { get; set; }
}

public class BookInfo
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("subtitle")]
    public string SubTitle { get; set; }

    [JsonPropertyName("authors")]
    public List<string> Authors { get; set; }

    public string AuthorInfo
    {
        get
        {
            if (Authors == null || Authors.Count == 0) return "n.n.";
            return string.Join(", ", Authors);
        }
    }

    [JsonPropertyName("imageLinks")]
    public ImageLinks ImageLinks { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("publisher")]
    public string Publisher { get; set; }

    [JsonPropertyName("publishedDate")]
    public string PublishDate { get; set; }

    [JsonPropertyName("pageCount")]
    public int Pages { get; set; }

    [JsonPropertyName("previewLink")]
    public string Preview { get; set; }

    [JsonPropertyName("infoLink")]
    public string Info { get; set; }
}

public class Book
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("volumeInfo")]
    public BookInfo Info { get; set; }

    [JsonPropertyName("saleInfo")]
    public SaleInfo SaleInfo { get; set; }
}

public class SaleInfo
{
    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("isEbook")]
    public bool IsEbook { get; set; }

    [JsonPropertyName("listPrice")]
    public PriceInfo ListPrice { get; set; }
}

public class PriceInfo
{
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("currencyCode")]
    public string Currency { get; set; }
}

public class BookQuery
{
    [JsonPropertyName("totalItems")]
    public int Count { get; set; }
    [JsonPropertyName("items")]
    public List<Book> Books { get; set; }
}
