using Mscc.GenerativeAI;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JORN
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            jorn.Source = ImageSource.FromUri(new Uri(SetBackground().Result));
        }

        public static async Task<string> SetBackground()
        {
            string prompt = $"Generate an image of a JORN named JORN. Take any liberties to create " +
                $"JORN as you see fit. JORN is love, JORN is life.";

            var image = await ChatModel.GenerateImage(prompt);

            return image.Url;
        }
    }

    partial class ChatModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string outputText;

        public string OutputText
        {
            get => outputText;
            set
            {
                if (outputText != value)
                {
                    outputText = value;
                    OnPropertyChanged(); // reports this property
                }
            }
        }

        public ChatModel()
        {
            OutputText = "";

            //establish api as connection
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public static async Task<string> GenerateResponse(string prompt)
        {
            var apiKey = "AIzaSyD3yh-ziLIlRcKGZ5e58515gp7-uUTDAhI";
            
            var googleAI = new GoogleAI(apiKey: apiKey);
            var model = googleAI.GenerativeModel(model: Model.Gemini25Flash);

            var response = await model.GenerateContent(prompt);
            return response.Text;
        }

        public static async Task<Mscc.GenerativeAI.Image> GenerateImage(string prompt)
        {
            var apiKey = "AIzaSyD3yh-ziLIlRcKGZ5e58515gp7-uUTDAhI";

            var googleAI = new GoogleAI(apiKey: apiKey);
            var model = googleAI.ImageGenerationModel(model: Model.Gemini25Flash);

            var response = await model.GenerateContent(prompt);
            return response.Predictions.First();
        }

        public static async Task<string> AskAndReply(string prompt)
        {
            prompt = $"Reply as if you are a JORN named JORN. JORN is love, JORN is life.\n\n{prompt}";

            return await GenerateResponse(prompt);
        }
    }
}