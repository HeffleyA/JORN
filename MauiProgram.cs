using Microsoft.Extensions.Logging;

namespace JORN
{
    //// Blueprints for our REQUEST to the AI
    //public class GeminiRequest { public Content[]? contents { get; set; } }
    //public class Content { public Part[]? parts { get; set; } }
    //public class Part { public string? text { get; set; } }

    //// Blueprints for the AI's RESPONSE
    //public class GeminiResponse { public Candidate[]? candidates { get; set; } }
    //public class Candidate { public Content? content { get; set; } }

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Console.WriteLine("🤖 AI is generating your 'Hello, World!' message...");
            //// 1. Replace with your actual API key
            //string apiKey = "AIzaSyD3yh-ziLIlRcKGZ5e58515gp7-uUTDAhI";
            //string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent";
            //string prompt = "Generate a creative, friendly 'Hello World' message. Make it sound like it's from a futuristic AI. It should be one short sentence.";

#if DEBUG
            builder.Logging.AddDebug();

#endif

            return builder.Build();
        }
    }
}
