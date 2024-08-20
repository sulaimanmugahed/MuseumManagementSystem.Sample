using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MuseumManagementSystem.Domain.Settings;
using MuseumManagementSystem.Web.Dtos;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MuseumManagementSystem.Web.Services
{
    public class ImageSearchClientService(HttpClient httpClient)
    {
        public async Task<ImageSearchResponse> GetSimilerImages(IFormFile queryImage)
        {
            var content = new MultipartFormDataContent();
            
            var fileContent = new StreamContent(queryImage.OpenReadStream());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(queryImage.ContentType);

            content.Add(fileContent, "query_img", queryImage.FileName);

            var httpResponse = await httpClient.PostAsync("/", content);
            if (!httpResponse.IsSuccessStatusCode)
            {
                return new ImageSearchResponse("Error");
            }

            var body = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<ImageSearchResponse>(
                body,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new ImageSearchResponse(response?.Data);
        }
    }
}
