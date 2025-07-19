using Microsoft.Playwright;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace LinkedInScraperOpenAI;

public static class PostProcessor
{
    public static async Task<string> ExtractPostTextAsync(IPage page, string url)
    {
        await page.GotoAsync(url, new PageGotoOptions { WaitUntil = WaitUntilState.NetworkIdle });

        var postElement = await page.QuerySelectorAsync("p.attributed-text-segment-list__content");
        return postElement != null ? await postElement.InnerTextAsync() ?? "[Não encontrado]" : "[Não encontrado]";
    }

    public static async Task<string> GenerateReplyAsync(string apiKey, string model, string systemPrompt, string userPrompt, string postText)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model,
            messages = new object[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt + "\n\n" + postText }
            },
            temperature = 0.7
        };

        var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json"));
        var json = await response.Content.ReadAsStringAsync();

        if (json.Contains("error"))
        {
            Console.WriteLine($"Erro da API: {json}");
            return "[Erro da API OpenAI]";
        }

        using var doc = JsonDocument.Parse(json);
        return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString()?.Trim() ?? "[Resposta vazia]";
    }
}
