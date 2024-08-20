namespace MuseumManagementSystem.Web.Dtos
{
    public class ImageSearchResponse
    {

        public ImageSearchResponse()
        {
            
        }

        public ImageSearchResponse(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public ImageSearchResponse(SimilerImagesDto? data)
        {
            IsSuccess = true;
            Data = data;
        }

        public SimilerImagesDto? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
