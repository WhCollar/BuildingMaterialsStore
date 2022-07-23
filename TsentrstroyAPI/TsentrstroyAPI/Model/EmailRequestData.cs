using System.Text.Json.Serialization;

namespace TsentrstroyAPI.Model
{
    public class EmailRequestData
    {
        [JsonPropertyName("from_email")] public string FromEmail { get; set; }
        [JsonPropertyName("from_name")] public string FromName { get; set; }
        [JsonPropertyName("to")] public string To { get; set; }
        [JsonPropertyName("subject")] public string Subject { get; set; }
        [JsonPropertyName("text")] public string Text { get; set; }
        [JsonPropertyName("html")] public string Html { get; set; }
        [JsonPropertyName("payment")] public string Payment { get; set; }

        [JsonPropertyName("smtp_headers")] public SmtpHeaders SmtpHeaders { get; set; }
    }

    public class SmtpHeaders
    {
        [JsonPropertyName("Client-Id")] public string ClientId { get; set; }
    }

}