using System.ComponentModel;
using System.Runtime.CompilerServices;
using Mscc.GenerativeAI;

namespace JORN
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
    }

    class ChatModel : INotifyPropertyChanged
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

        public async Task<string> GenerateResponse(string prompt)
        {
            var apiKey = "AIzaSyD3yh-ziLIlRcKGZ5e58515gp7-uUTDAhI";
            
            var googleAI = new GoogleAI(apiKey: apiKey);
            var model = googleAI.GenerativeModel(model: Model.Gemini25Flash);

            var response = await model.GenerateContent(prompt);
            return response.Text;
        }

        public async Task<string> AskAndReply(string prompt)
        {
            prompt = $"Reply as if you are a JORN named JORN. JORN is love, JORN is life.\n\n{prompt}";

            return await GenerateResponse(prompt);
        }
    }
}