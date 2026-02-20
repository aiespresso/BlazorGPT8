# BlazorGPT8

A Blazor Server web application built with **.NET 8** that provides a real-time interactive chat interface powered by **OpenAI GPT** models.

## Features

- **Real-time streaming chat** — Responses are streamed token-by-token as they are generated
- **Multiple call modes** — Supports both synchronous and asynchronous API calls, as well as streaming and non-streaming modes
- **Markdown rendering** — Bot responses containing markdown (bold, italic, headings, links, horizontal rules) are automatically converted to HTML
- **Token usage display** — After each streamed response the prompt token count, completion token count, and total token count are shown
- **Finish reason indicator** — Displays why the model stopped generating (e.g. `Stop` or `Length`)
- **Clean chat UI** — Distinct styling for user messages and bot messages with icon avatars

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Blazor Server (Interactive Server render mode) |
| Language | C# / .NET 8 |
| AI SDK | [OpenAI .NET SDK](https://github.com/openai/openai-dotnet) v2.1.0 |
| Serialization | System.Text.Json v9.0.1 |

## Project Structure

```
BlazorGPT8/
├── Components/
│   ├── Pages/
│   │   └── Home.razor          # Main chat page (UI + logic)
│   ├── Layout/                 # Shared layouts
│   ├── App.razor
│   └── Routes.razor
├── Services/
│   └── GPTService.cs           # OpenAI API integration
├── wwwroot/                    # Static assets (CSS, JS, images)
├── Program.cs                  # App startup and DI registration
├── appsettings.json
└── BlazorGPT8.csproj
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- An [OpenAI API key](https://platform.openai.com/api-keys)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/aiespresso/BlazorGPT8.git
cd BlazorGPT8
```

### 2. Set the OpenAI API key

The application reads the API key from the `OPENAI_API_KEY` environment variable.

**Windows (Command Prompt)**
```cmd
set OPENAI_API_KEY=sk-...
```

**Windows (PowerShell)**
```powershell
$env:OPENAI_API_KEY="sk-..."
```

**macOS / Linux**
```bash
export OPENAI_API_KEY=sk-...
```

### 3. Build and run

```bash
cd BlazorGPT8
dotnet run
```

The app will be available at:
- HTTP: `http://localhost:5296`
- HTTPS: `https://localhost:7006`

## How It Works

### GPTService

`Services/GPTService.cs` wraps the OpenAI `ChatClient` and exposes four methods:

| Method | Mode | Async |
|---|---|---|
| `GetResponse(prompt)` | Non-streaming | No |
| `GetResponseAsync(prompt)` | Non-streaming | Yes |
| `GetStreamingResponse(prompt)` | Streaming | No |
| `GetStreamingResponseAsync(prompt)` | Streaming | Yes |

The default model is **gpt-4o**. You can change this by passing a different model name to the `GPTService` constructor.

### Home.razor

The main chat page (`Components/Pages/Home.razor`) renders a conversation thread and calls `GPTService.GetStreamingResponseAsync` when the user clicks **Ask**. Responses are appended incrementally to the UI via `StateHasChanged()` as each streaming update arrives.

## Configuration

| Setting | Where | Description |
|---|---|---|
| `OPENAI_API_KEY` | Environment variable | Your OpenAI secret key |
| `gptModel` | `GPTService` constructor | GPT model to use (default: `gpt-4o`) |
| `MaxOutputTokenCount` | `GPTService.GetStreamingResponseAsync` | Maximum tokens in the response (default: `1000`) |
| `Temperature` | `GPTService.GetStreamingResponseAsync` | Sampling temperature (default: `2.0`) |

## License

This project is provided as-is for educational purposes. See the repository for details.
