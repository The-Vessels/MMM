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
  public required Metadata aMetadata { get; set; }

  [JsonPropertyName("_aRecords")]
  public required List<Record> aRecords { get; set; }
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
  public required string ModelName { get; set; }

  [JsonPropertyName("_sName")]
  public required string Name { get; set; }

  [JsonPropertyName("_tsDateAdded")]
  public long DateAdded { get; set; }

  [JsonPropertyName("_tsDateModified")]
  public long DateModified { get; set; }

  [JsonPropertyName("_bHasFiles")]
  public bool HasFiles { get; set; }

  [JsonPropertyName("_aTags")]
  public List<string>? Tags { get; set; }

  [JsonPropertyName("_aSubmitter")]
  public required Submitter Submitter { get; set; }
}

public class Submitter
{
  [JsonPropertyName("_idRow")]
  public int Row { get; set; }

  [JsonPropertyName("_sName")]
  public required string Name { get; set; }
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
  public static async Task<SubmissionItem> GetFeaturedSubmissions(HttpClient httpClient)
  {
      using HttpResponseMessage response = await httpClient.GetAsync("apiv11/Util/List/Featured?_idGameRow=6755&_sModelName=Mod");
      
      var jsonResponse = await response.Content.ReadFromJsonAsync<SubmissionItem>();
      /* Console.WriteLine($"{jsonResponse.GetProperty("_aRecords")}\n"); */
      return jsonResponse;
  }
}