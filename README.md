# 💬 LinkedIn Reply Generator (C# + OpenAI + Playwright)

Este projeto automatiza a geração de comentários técnicos e profissionais no LinkedIn com base em links de posts extraídos de uma planilha Excel. Ele acessa cada post, extrai o conteúdo via navegador headless com **Playwright**, envia para a **API da OpenAI** com o seu perfil técnico (via prompt), e escreve uma resposta otimizada em outro Excel.

---

## 🛠️ Requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
* [Playwright para .NET](https://playwright.dev/dotnet/)
* Conta na [OpenAI](https://platform.openai.com/account/api-keys) com uma API key válida (paga)
* Windows, macOS ou Linux

---

## ⚙️ Instalação

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

---

## 🔐 Configuração

Crie um arquivo `appsettings.json` na raiz do projeto com o seguinte conteúdo:

```json
{
  "OpenAI": {
    "ApiKey": "sk-sua-chave-aqui",
    "Model": "gpt-3.5-turbo", // ou "gpt-4" se quiser mais qualidade, com maior custo
    "SystemPrompt": "You are a full stack software engineer with 4+ years of experience in .NET, 2+ years with React, and solid practice with Flutter in personal projects. You’ve built scalable APIs, worked with microservices, and applied clean architecture and clean code principles. Your comments are clear, respectful and add technical value.",
    "UserPrompt": "This is the content of the LinkedIn post. Generate a short and professional comment in English (B2 level). Make it valuable to the author, technically relevant, and friendly — something that encourages professional conversation."
  }
}
```

> ⚠️ **Não envie esse arquivo para o repositório público.** Ele contém sua chave da OpenAI.

---

## 📁 Estrutura da planilha de entrada

Crie um arquivo chamado `linksWithPosts.xlsx` na pasta de execução APENAS com o link dos posts na coluna A, um abaixo do outro, exemplo:
https://www.linkedin.com/posts/...
https://www.linkedin.com/posts/...

---

## ▶️ Executar

```bash
dotnet run
```

---

## 📄 Resultado

Será gerada uma planilha `respostasGeradas.xlsx` (ou `respostasGeradas_1.xlsx`, `respostasGeradas_2.xlsx`, etc.), com as seguintes colunas:

| Link | Texto extraído | Resposta gerada |
| ---- | -------------- | --------------- |

---

## 📦 Funcionalidades

* Extração automática de conteúdo de post via navegador sem login
* Geração de respostas via OpenAI com perfil técnico ajustável
* Exportação em Excel formatado
* Geração de múltiplos arquivos sem sobrescrever

---

## 🛡️ Segurança

* A API Key é lida do `appsettings.json`, que deve estar no `.gitignore`
* O Playwright é executado em modo headless, sem exposição de interface

---

## 🤝 Contribuições

Pull requests são bem-vindos. Vamos melhorar juntos!
