using Mscc.GenerativeAI;
using OpenAI.Images;

namespace JORN
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void MainPage_Loaded(object sender, EventArgs e)
        {
            try
            {
                // Await the URL string
                string url = await SetBackground();

                // Use the URL to set the ImageSource
                jorn.Source = ImageSource.FromUri(new Uri(url));
                System.Diagnostics.Debug.WriteLine($"Image successfully loaded from URL: {url}");
            }
            catch (Exception ex)
            {
                // 💡 CRITICAL: Log the InnerException and details
                System.Diagnostics.Debug.WriteLine($"--- API CALL FAILED ---");
                System.Diagnostics.Debug.WriteLine($"Message: {ex.Message}");

                // Look for the specific error from the Google SDK here
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner Message: {ex.InnerException.Message}");
                    System.Diagnostics.Debug.WriteLine($"Inner Type: {ex.InnerException.GetType().Name}");
                }
                System.Diagnostics.Debug.WriteLine($"-------------------------");

                // You can also display an alert to the user
                await DisplayAlert("API Error", "Image loading failed. See debug output for details.", "OK");
            }
        }

        public static async Task<string> SetBackground()
        {
            string prompt = $"Generate an image of a JORN named JORN. Take any liberties to create " +
                $"JORN as you see fit. JORN is love, JORN is life.";

            var image = await ChatModel.GenerateImage(prompt);

            return image.ImageUri.ToString();
        }

        async void SendMessage(object sender, EventArgs args)
        {
            textSection.Children.Add(new Label
            {
                Text = jornInput.Text,
                BackgroundColor = Color.FromRgba("1003FF77"),
                MaximumWidthRequest = textSection.Width * 0.75,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Fill
            });

            jornInput.Text = "";

            string prompt = "";
            var labelCount = (textSection.Children.Count < 10) ? textSection.Children.Count : textSection.Children.Count - 10;
            for (int i = textSection.Children.Count; i > labelCount; i--)
            {
                prompt = $"{((Label)textSection.Children.ElementAt(i)).Text}\n\n{prompt}";
            }

            textSection.Add(new Label
            {
                Text = await ChatModel.AskAndReply(prompt),
                BackgroundColor = Color.FromRgba("FF80AC77"),
                MaximumWidthRequest = textSection.Width * 0.75,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Fill
            });
        }
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

        public static async Task<OpenAI.Images.GeneratedImage> GenerateImage(string prompt)
        {
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            // Create the OpenAI ImageClient
            ImageClient client = new ImageClient("dall-e-3", apiKey);

            // Generate the image
            OpenAI.Images.GeneratedImage generatedImage = await client.GenerateImageAsync(prompt,
                new ImageGenerationOptions
                {
                    Size = GeneratedImageSize.W1024xH1024
                });

            Console.WriteLine($"The generated image is ready at:\n{generatedImage.ImageUri}");

            return generatedImage;
        }

        public static async Task<string> AskAndReply(string prompt)
        {
            /// JORN is love, JORN is life.
            prompt = $"Reply as if you are a JORN named JORN.\n\n{prompt}";

            return await GenerateResponse(prompt);
        }
    }
}