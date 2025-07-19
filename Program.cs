using Microsoft.Playwright;
using ClosedXML.Excel;
using LinkedInScraperOpenAI;

var config = ConfigHelper.LoadConfiguration();
var openAiKey = config["OpenAI:ApiKey"] ?? throw new Exception("API Key não encontrada em appsettings.json");
var model = config["OpenAI:Model"] ?? "gpt-3.5-turbo";
var systemPrompt = config["OpenAI:SystemPrompt"] ?? throw new Exception("System prompt não definido.");
var userPrompt = config["OpenAI:UserPrompt"] ?? throw new Exception("User prompt não definido.");

string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "linksWithPosts.xlsx");
string outputPath = FileHelper.GenerateUniqueOutputPath("respostasGeradas.xlsx");

using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
var context = await browser.NewContextAsync();
var page = await context.NewPageAsync();

var wb = new XLWorkbook(inputPath);
var ws = wb.Worksheet(1);
var range = ws?.RangeUsed() ?? throw new Exception("Planilha não encontrada ou vazia.");
var rows = range.RowsUsed();

var output = new XLWorkbook();
var outSheet = output.Worksheets.Add("Respostas");
outSheet.Cell(1, 1).Value = "Link";
outSheet.Cell(1, 2).Value = "Texto extraído";
outSheet.Cell(1, 3).Value = "Resposta gerada";

int rowIndex = 2;
foreach (var row in rows)
{
    string url = row.Cell(1).GetString();
    Console.WriteLine($"Acessando: {url}");

    try
    {
        string postText = await PostProcessor.ExtractPostTextAsync(page, url);
        string reply = await PostProcessor.GenerateReplyAsync(openAiKey, model, systemPrompt, userPrompt, postText);

        outSheet.Cell(rowIndex, 1).Value = url;
        outSheet.Cell(rowIndex, 2).Value = postText;
        outSheet.Cell(rowIndex, 3).Value = reply;

        Console.WriteLine("✅ Comentário gerado.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
        outSheet.Cell(rowIndex, 1).Value = url;
        outSheet.Cell(rowIndex, 2).Value = "[Erro ao extrair]";
        outSheet.Cell(rowIndex, 3).Value = "[Erro ao gerar resposta]";
    }

    rowIndex++;
}

output.SaveAs(outputPath);
Console.WriteLine($"✅ Planilha salva com sucesso em: {outputPath}");