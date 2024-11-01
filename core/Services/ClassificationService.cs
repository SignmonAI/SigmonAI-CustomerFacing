using System.Net.Http.Headers;
using core.Data.Contexts;
using core.Data.Outbound;
using core.Data.Payloads;
using core.Errors;
using core.Models;

namespace core.Services
{
    public class ClassificationService(
            UserContext userContext,
            HttpClient httpClient)
    {
        private readonly UserContext _userContext = userContext;
        private readonly HttpClient _httpClient = httpClient;

        public async Task<OutboundClassification> Classify(ClassifyPayload payload)
        {
            if (payload.Image == null || payload.Image.Length == 0)
            {
                throw new Exception();
            }

            using var formData = new MultipartFormDataContent();

            string model = _userContext.SubscriptionModel switch
            {
                ClassificationModel.FREE => "single_db_nn",
                ClassificationModel.INTERMEDIATE => "andre_nn",
                ClassificationModel.ADVANCED => "random_forest",
                _ => throw new Exception("Invalid model."),
            };

            formData.Add(new StringContent(model), "model_type");

            var imageContent = new StreamContent(payload.Image.OpenReadStream());
            imageContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            formData.Add(imageContent, "media", payload.Image.FileName);

            var response = await _httpClient.PostAsync("/classify", formData);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<OutboundClassification>()
                    ?? throw new ExternalResponseException("External API error.");
        }
    }
}