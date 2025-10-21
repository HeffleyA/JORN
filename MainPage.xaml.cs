using Mscc.GenerativeAI;

namespace JORN
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //private async void MainPage_Loaded(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Await the URL string
        //        string url = await SetBackground();

        //        // Use the URL to set the ImageSource
        //        jorn.Source = ImageSource.FromUri(new Uri(url));
        //        System.Diagnostics.Debug.WriteLine($"Image successfully loaded from URL: {url}");
        //    }
        //    catch (Exception ex)
        //    {
        //        // 💡 CRITICAL: Log the InnerException and details
        //        System.Diagnostics.Debug.WriteLine($"--- API CALL FAILED ---");
        //        System.Diagnostics.Debug.WriteLine($"Message: {ex.Message}");

        //        // Look for the specific error from the Google SDK here
        //        if (ex.InnerException != null)
        //        {
        //            System.Diagnostics.Debug.WriteLine($"Inner Message: {ex.InnerException.Message}");
        //            System.Diagnostics.Debug.WriteLine($"Inner Type: {ex.InnerException.GetType().Name}");
        //        }
        //        System.Diagnostics.Debug.WriteLine($"-------------------------");

        //        // You can also display an alert to the user
        //        await DisplayAlert("API Error", "Image loading failed. See debug output for details.", "OK");
        //    }
        //}

        //public static async Task<string> SetBackground()
        //{
        //    string prompt = $"Generate an image of a JORN named JORN. Take any liberties to create " +
        //        $"JORN as you see fit. JORN is love, JORN is life.";

        //    var image = await ChatModel.GenerateImage(prompt);

        //    return image.Url;
        //}
    }

    partial class ChatModel
    {
        public static async Task<string> GenerateResponse(string prompt)
        {
            var apiKey = "AIzaSyD3yh-ziLIlRcKGZ5e58515gp7-uUTDAhI";
            
            var googleAI = new GoogleAI(apiKey: apiKey);
            var model = googleAI.GenerativeModel(model: Model.Gemini25Flash);

            var response = await model.GenerateContent(prompt);
            return response.Text;
        }

        //public static async Task<Mscc.GenerativeAI.Image> GenerateImage(string prompt)
        //{
        //    var apiKey = "AIzaSyD3yh-ziLIlRcKGZ5e58515gp7-uUTDAhI"; // Use your actual key

        //    try
        //    {
        //        var vertexAI = new VertexAI(apiKey: apiKey);

        //        // 💡 CRITICAL: Use ImageGenerationModel to create an image
        //        var imageResponse = vertexAI.ImageGenerationModel(model: Model.);

        //        // Generate the content (this is now the image generation call)
        //        var response = await model.GenerateContent(prompt);

        //        // Return the first image prediction
        //        return response.Predictions.First();
        //    }
        //    catch (Mscc.GenerativeAI.GeminiApiException apiEx)
        //    {
        //        // Keep the robust logging for any future API issues
        //        System.Diagnostics.Debug.WriteLine($"!!! REAL API ERROR CAUGHT !!!");
        //        System.Diagnostics.Debug.WriteLine($"Error Data: {apiEx.Data}");
        //        throw new InvalidOperationException($"Gemini API Failed: {apiEx.Message}", apiEx);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"!!! GENERAL EXCEPTION IN GENERATEIMAGE !!!");
        //        throw;
        //    }
        //}

        public static async Task<string> AskAndReply(string prompt)
        {
            prompt = $"Reply as if you are a JORN named JORN. JORN is love, JORN is life.\n\n{prompt}";

            return await GenerateResponse(prompt);
        }
    }
}