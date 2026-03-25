using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace mmm;

//TODO: all of the flipping mappings or json data god save me
// GameBanana calls IDs Rows for some reason btw
public class SubmissionItem
{
  [JsonPropertyName("_aMetadata")]
  public required Metadata Metadata { get; set; }

  [JsonPropertyName("_aRecords")]
  public required List<Record> Records { get; set; }
}
public class Metadata
{
  [JsonPropertyName("_nRecordCount")]
  public int RecordCount { get; set; }

  [JsonPropertyName("_nPerpage")]
  public int Perpage { get; set; }

  [JsonPropertyName("_bIsComplete")]
  public bool IsComplete { get; set; }
}

public class Record
{
  [JsonPropertyName("_idRow")]
  public int Row { get; set; }

  [JsonPropertyName("_sModelName")]
  public string? ModelName { get; set; }

  [JsonPropertyName("_sName")]
  public required string Name { get; set; }

  [JsonPropertyName("_tsDateAdded")]
  public long DateAdded { get; set; }

  [JsonPropertyName("_tsDateModified")]
  public long DateModified { get; set; }

  [JsonPropertyName("_bHasFiles")]
  public bool HasFiles { get; set; }

  //tags are structured differently on different endpoints so im quitting tags no one uses them anyway
  /* [JsonPropertyName("_aTags")]
  public List<string>? Tags { get; set; } */

  [JsonPropertyName("_aPreviewMedia")]
  public PreviewMedia? PreviewMedia { get; set; }

  [JsonPropertyName("_aSubmitter")]
  public required Submitter Submitter { get; set; }

  [JsonPropertyName("_akDevelopmentState")]
  public string? DevelopmentStateAbbr { get; set; }

  [JsonPropertyName("_sDevelopmentState")]
  public string? DevelopmentState { get; set; }

  [JsonPropertyName("_iCompletionPercentage")]
  public int? CompletionPercentage { get; set; }

  [JsonPropertyName("_tsDateUpdated")]
  public long DateUpdated { get; set; }

  [JsonPropertyName("_nLikeCount")]
  public int LikeCount { get; set; }

  [JsonPropertyName("_nPostCount")]
  public int PostCount { get; set; }

  [JsonPropertyName("_nViewCount")]
  public int ViewCount { get; set; }

  [JsonPropertyName("_nDownloadCount")]
  public int DownloadCount { get; set; }

  [JsonPropertyName("_aCategory")]
  public Category? Category { get; set; }

  [JsonPropertyName("_aSuperCategory")]
  public SuperCategory? SuperCategory { get; set; }
}

public class PreviewMedia
{
  [JsonPropertyName("_aImages")]
  public List<GBImage>? Images { get; set; }
}

public class GBImage
{
  [JsonPropertyName("_sType")]
  public required string Images { get; set; }

  [JsonPropertyName("_sBaseUrl")]
  public required string BaseUrl { get; set; }

  [JsonPropertyName("_sFile")]
  public required string File { get; set; }

  [JsonPropertyName("_sFile100")]
  public string? File100 { get; set; }

  [JsonPropertyName("_sFile220")]
  public string? File220 { get; set; }

  [JsonPropertyName("_sFile530")]
  public string? File530 { get; set; }

  [JsonPropertyName("_sFile800")]
  public string? File800 { get; set; }
}

public class Submitter
{
  [JsonPropertyName("_idRow")]
  public int Row { get; set; }

  [JsonPropertyName("_sName")]
  public required string Name { get; set; }
}

public class Category
{
  [JsonPropertyName("_idRow")]
  public int Row { get; set; }

  [JsonPropertyName("_sName")]
  public required string Name { get; set; }

  [JsonPropertyName("_sModelName")]
  public required string ModelName { get; set; }
}

public class SuperCategory
{
  [JsonPropertyName("_idRow")]
  public int Row { get; set; }

  [JsonPropertyName("_sName")]
  public required string Name { get; set; }

  [JsonPropertyName("_sModelName")]
  public required string ModelName { get; set; }
}

class GameBanana
{
  public static HttpClient client = new()
  {
      BaseAddress = new Uri("https://gamebanana.com"),
      DefaultRequestHeaders =
    {
      {"Accept", "application/json"},
      {"ContentType", "application/json"}
    }
  };

  public static async Task<Record> GetRecordByModelNameAndRow(string ModelName, int Row)
  {
    using HttpResponseMessage response = await client.GetAsync($"apiv11/{ModelName}/{Row}/ProfilePage");
      
    response.EnsureSuccessStatusCode();

    var jsonResponse = await response.Content.ReadFromJsonAsync<Record>();
    
    return jsonResponse;
  }
  public static async Task<SubmissionItem> GetFeaturedSubmissions()
  {
    using HttpResponseMessage response = await client.GetAsync("apiv11/Util/List/Featured?_idGameRow=6755&_sModelName=Mod");
    
    response.EnsureSuccessStatusCode();

    var jsonResponse = await response.Content.ReadFromJsonAsync<SubmissionItem>();
    /* Console.WriteLine($"{jsonResponse.GetProperty("_aRecords")}\n"); */
    return jsonResponse;
  }

  public static string GetSubmissionImageUrl(GBImage image)
  {
    return $"{image.BaseUrl}/{image.File}";
  }

  public static string GetSubmissionImageUrlByImageSize(GBImage image, ImageSizes imageSize = ImageSizes.SizeBase)
  {
    if (image.File100 != null && imageSize == ImageSizes.Size100)
    {
      return $"{image.BaseUrl}/{image.File100}";
    }
    if (image.File220 != null && imageSize == ImageSizes.Size220)
    {
      return $"{image.BaseUrl}/{image.File220}";
    }
    if (image.File530 != null && imageSize == ImageSizes.Size530)
    {
      return $"{image.BaseUrl}/{image.File530}";
    }
    if (image.File800 != null && imageSize == ImageSizes.Size800)
    {
      return $"{image.BaseUrl}/{image.File800}";
    }
    return $"{image.BaseUrl}/{image.File}";
  }

  public enum ImageSizes
  {
    SizeBase,
    Size100,
    Size220,
    Size530,
    Size800
  }
}