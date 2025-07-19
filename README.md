# 💬 LinkedIn Reply Generator (C# + OpenAI + Playwright)

Este projeto automatiza a geração de comentários técnicos e profissionais no LinkedIn com base em links de posts extraídos de uma planilha Excel. Ele acessa cada post, extrai o conteúdo via navegador headless com **Playwright**, envia para a **API da OpenAI** com o seu perfil técnico (via prompt), e escreve uma resposta otimizada em outro Excel.

---

### 📥 Download direto

👉 [Clique aqui para baixar o executável do LinkedIn Reply Generator](https://github.com/isaias99i/LinkedIn_Reply_Generator/releases/download/v1.0.0/publish.zip)

> Inclua manualmente o `appsettings.json` e a planilha `linksWithPosts.xlsx` para funcionamento completo.

---

## 🔐 Configuração (`appsettings.json`)

Crie um arquivo chamado `appsettings.json` na mesma pasta do executável (ou na raiz do projeto) com o seguinte conteúdo:

```json
{
  "OpenAI": {
    "ApiKey": "sk-sua-chave-aqui",
    "Model": "gpt-3.5-turbo",
    "SystemPrompt": "...",
    "UserPrompt": "..."
  }
}
```

### O que inserir em cada campo:

| Campo          | Descrição                                                                                            |
| -------------- | ---------------------------------------------------------------------------------------------------- |
| `ApiKey`       | Sua chave da API da OpenAI, encontrada em [https://platform.openai.com](https://platform.openai.com) |
| `Model`        | Modelo a ser utilizado (`gpt-3.5-turbo` recomendado, `gpt-4` é mais caro)                            |
| `SystemPrompt` | Mensagem que define o seu perfil técnico como engenheiro de software                                 |
| `UserPrompt`   | Instrução para gerar o comentário com base no conteúdo do post                                       |

### Exemplo completo:

```json
{
  "OpenAI": {
    "ApiKey": "sk-abc123...",
    "Model": "gpt-3.5-turbo",
    "SystemPrompt": "You are a full stack software engineer with 4+ years of experience in .NET, 2+ years with React, and solid practice with Flutter in personal projects. You’ve built scalable APIs, worked with microservices, and applied clean architecture and clean code principles. Your comments are clear, respectful and add technical value.",
    "UserPrompt": "This is the content of the LinkedIn post. Generate a short and professional comment in English (B2 level). Make it valuable to the author, technically relevant, and friendly — something that encourages professional conversation."
  }
}
```

> ⚠️ **Nunca envie esse arquivo para repositórios públicos!** Ele contém sua chave pessoal da OpenAI.

---

## 📁 Estrutura da planilha de entrada

Crie um arquivo chamado `linksWithPosts.xlsx` na pasta de execução APENAS com o link dos posts na coluna A, um abaixo do outro, conforme a estrutura abaixo:

```
|        A1        |
|----------------|
| https://www.linkedin.com/posts/abc |
| https://www.linkedin.com/posts/def |
| https://www.linkedin.com/posts/ghi |
```

---

## 🛠️ Requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* [Playwright para .NET](https://playwright.dev/dotnet/)
* Conta na [OpenAI](https://platform.openai.com/account/api-keys) com uma API key válida (paga)
* Windows, macOS ou Linux

---

## ⚙️ Instalação e Execução via Código Fonte

1. Clone o repositório:

```bash
git clone https://github.com/isaias99i/LinkedIn_Reply_Generator.git
cd LinkedIn_Reply_Generator
```

2. Restaure pacotes:

```bash
dotnet restore
```

3. Instale o Playwright (baixa o navegador headless necessário):

```bash
playwright install
```

Se estiver usando PowerShell e tiver problemas, tente:

```bash
powershell ./bin/Debug/net8.0/playwright.ps1 install
```

4. Crie o arquivo `appsettings.json` com a chave da OpenAI e os prompts (detalhado mais acima)

5. Crie o arquivo `linksWithPosts.xlsx` com os links dos posts a serem processados (detalhado mais acima)

6. Execute:

```bash
dotnet run
```

---

## 📄 Resultado

Será gerada uma planilha `respostasGeradas.xlsx` (ou `respostasGeradas_1.xlsx`, etc.) com as colunas:

| Link | Texto extraído | Resposta gerada |
| ---- | -------------- | --------------- |

---

## 🚀 Executável pronto (.exe)

Se quiser distribuir sem precisar instalar .NET:

1. Gere o executável com:

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o ./publish
```

2. Compartilhe o arquivo `LinkedInScraperOpenAI.exe` da pasta `/publish`

3. Inclua junto o `appsettings.json` e `linksWithPosts.xlsx`

4. Execute diretamente:

```bash
LinkedInScraperOpenAI.exe
```

> ⚠️ O navegador do Playwright será baixado na primeira execução.

---

## 📦 Funcionalidades

* Extração automática de conteúdo de post via navegador sem login
* Geração de respostas via OpenAI com perfil técnico ajustável
* Exportação em Excel formatado
* Geração de múltiplos arquivos sem sobrescrever

---

## 🛡️ Segurança

* A chave da OpenAI é lida do `appsettings.json` (não comitada)
* O navegador é executado em modo headless, sem exposição de interface

---

## 🤝 Contribuições

Pull requests são bem-vindos. Vamos melhorar juntos!

---

## 🔎 Exemplo

| Link                                                             | Texto extraído               | Resposta gerada                                   |
| ---------------------------------------------------------------- | ---------------------------- | ------------------------------------------------- |
| [https://linkedin.com/posts/abc](https://linkedin.com/posts/abc) | "Your dashboard is slow\..." | "Great insight! Modeling makes the difference..." |
