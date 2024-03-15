using System.Text;
using Microsoft.Maui.Controls;

namespace wisielec
{
    public partial class MainPage : ContentPage
    {
        int CountClickedBtns = 0;

        string visibleMsg;
        string hiddenMsg;

        public MainPage()
        {
            InitializeComponent();
            visibleMsg = ChoosePasswordFromArray();
            DisplayYourOwnPasswordButton();
            DisplayHiddenMessage();
            DisplayButtons();
        }

        private void DisplayHiddenMessage()
        {
            for (int i = 0; i < visibleMsg.Length; i++)
            {
                if (visibleMsg[i] == ' ')
                {
                    hiddenMsg += "  ";
                }
                else
                {
                    hiddenMsg += "_ ";
                }
            }

            // creating label for hiddenMsg
            Label labelMsg = new Label
            {
                Text = hiddenMsg,
                FontSize = 40
            };

            gridHiddenMsg.Children.Add(labelMsg);
        }

        private void ShowPassword()
        {
            hiddenMsg = string.Empty;

            for (int i = 0; i < visibleMsg.Length;i++)
            {
                hiddenMsg += $"{visibleMsg[i]} ";
            }

            // creating label for hiddenMsg
            Label labelMsg = new Label
            {
                Text = hiddenMsg,
                FontSize = 40
            };

            gridHiddenMsg.Children.Clear();
            gridHiddenMsg.Children.Add(labelMsg);
        }

        private void DisplayYourOwnPasswordButton()
        {
            Button btn = new Button
            {
                Text = "Create custom password",
                FontSize = 20,
                WidthRequest = 270,
                HeightRequest = 50,
                BackgroundColor = Colors.White,
                TextColor = Color.FromRgb(61, 49, 149)
            };
            btn.Clicked += CreateCustomPassword;

            displayInfo.Children.Add(btn);
        }

        private void CreateCustomPassword(object sender, EventArgs e)
        {
            // disable button click
            foreach (var child in gridButtons.Children)
            {
                if (child is Grid grid)
                {
                    foreach (var neastedChild in grid.Children)
                    {
                        if (neastedChild is Button btn)
                        {
                            btn.IsEnabled = false;
                        }
                    }
                }
            }

            // display submit button in displayInfo grid
            Button submitBtn = new Button
            {
                Text = "submit",
                FontSize = 20,
                WidthRequest = 150,
                HeightRequest = 50,
                BackgroundColor = Colors.White,
                TextColor = Color.FromRgb(61, 49, 149)
            };
            submitBtn.Clicked += SubmitButtonClicked;

            displayInfo.Children.Clear();
            displayInfo.Children.Add(submitBtn);

            // delete actual password and wait for user input
            gridHiddenMsg.Children.Clear();

            var entry = new Entry
            {
                FontSize = 35,
                HorizontalTextAlignment = TextAlignment.Center,
                MinimumWidthRequest = 100
            };
            gridHiddenMsg.Children.Add(entry);

            visibleMsg = string.Empty;
            hiddenMsg = string.Empty;

            // entry properties
            entry.Focus();
            entry.IsTextPredictionEnabled = false;

            entry.TextChanged += (sender, args) =>
            {
                string newText = args.NewTextValue;
                string filteredText = "";

                foreach (char sign in newText)
                {
                    if (LegalSign(sign))
                    {
                        filteredText += sign;
                    }
                }
                entry.Text = filteredText;
                visibleMsg = filteredText;
            };
        }

        private bool LegalSign(char ch)
        {
            char[] legalSigns = {
                ' ',
                'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',
                'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P',
                'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l',
                'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L',
                'z', 'x', 'c', 'v', 'b', 'n', 'm',
                'Z', 'X', 'C', 'V', 'B', 'N', 'M'
            };
            
            return Array.IndexOf(legalSigns, ch) != -1;
        }

        private void SubmitButtonClicked(object sender, EventArgs e)
        {
            gridHiddenMsg.Children.Clear();

            // enable button click
            foreach (var child in gridButtons.Children)
            {
                if (child is Grid grid)
                {
                    foreach (var neastedChild in grid.Children)
                    {
                        if (neastedChild is Button btn)
                        {
                            btn.IsEnabled = true;
                        }
                    }
                }
            }

            displayInfo.Children.Clear();

            if (visibleMsg.Length == 0)
            {
                visibleMsg = ChoosePasswordFromArray();

                DisplayYourOwnPasswordButton();
            }

            DisplayHiddenMessage();
        }

        private void DisplayButtons()
        {
            Grid row1 = new Grid();
            Grid row2 = new Grid();
            Grid row3 = new Grid();

            row1.HorizontalOptions = LayoutOptions.Center;
            row2.HorizontalOptions = LayoutOptions.Center;
            row3.HorizontalOptions = LayoutOptions.Center;

            row1.AddRowDefinition(new RowDefinition());
            row2.AddRowDefinition(new RowDefinition());
            row3.AddRowDefinition(new RowDefinition());

            for (int i = 0; i < 10; i++)
            {
                Button btnRow1 = new Button
                {
                    Text = "?",
                    FontSize = 20,
                    WidthRequest = 50,
                    HeightRequest = 50,
                    Margin = 1,
                    BackgroundColor = Colors.White,
                    TextColor = Color.FromRgb(61, 49, 149)
                };
                btnRow1.Clicked += BtnClick;

                SetBtnText(btnRow1);

                row1.AddColumnDefinition(new ColumnDefinition());

                row1.SetRow(btnRow1, 0);
                row1.SetColumn(btnRow1, i);

                row1.Children.Add(btnRow1);
            }

            for (int i = 0; i < 9; i++)
            {
                Button btnRow2 = new Button
                {
                    Text = "?",
                    FontSize = 20,
                    WidthRequest = 50,
                    HeightRequest = 50,
                    Margin = 1,
                    BackgroundColor = Colors.White,
                    TextColor = Color.FromRgb(61, 49, 149)
                };
                btnRow2.Clicked += BtnClick;

                SetBtnText(btnRow2);

                row2.AddColumnDefinition(new ColumnDefinition());

                row2.SetRow(btnRow2, 0);
                row2.SetColumn(btnRow2, i);

                row2.Children.Add(btnRow2);
            }

            for (int i = 0; i < 7; i++)
            {
                Button btnRow3 = new Button
                {
                    Text = "?",
                    FontSize = 20,
                    WidthRequest = 50,
                    HeightRequest = 50,
                    Margin = 1,
                    BackgroundColor = Colors.White,
                    TextColor = Color.FromRgb(61, 49, 149)
                };
                btnRow3.Clicked += BtnClick;

                SetBtnText(btnRow3);

                row3.AddColumnDefinition(new ColumnDefinition());

                row3.SetRow(btnRow3, 0);
                row3.SetColumn(btnRow3, i);

                row3.Children.Add(btnRow3);
            }

            gridButtons.Add(row1);
            gridButtons.Add(row2);
            gridButtons.Add(row3);
        }

        int globalCount = 0;

        private void SetBtnText(Button btn)
        {
            string letter = "qwertyuiopasdfghjklzxcvbnm";

            btn.Text = letter[globalCount++].ToString();
        }

        bool EnableBtnKeyboardClick = true;

        private void BtnClick(object sender, EventArgs e)
        {
            // remove button on first click
            if (CountClickedBtns == 0)
            {
                displayInfo.Children.Clear();
            }

            if (!EnableBtnKeyboardClick) { return; }

            Button btn = (Button)sender;

            btn.IsEnabled = false;

            if (!PasswordContainLetter(sender, e))
            {
                btn.BackgroundColor = Color.FromRgb(239, 35, 60);
                btn.TextColor = Colors.Black;

                if (++CountClickedBtns >= 11)
                {
                    ShowPassword();
                    GameOver("you lost", Color.FromRgb(239, 35, 60));
                }

                UpdateImage();

                return;
            }

            // when password contains clicked letter

            btn.BackgroundColor = Color.FromRgb(204, 255, 51);
            btn.TextColor = Colors.Black;

            StringBuilder newHiddenMsg = new StringBuilder(hiddenMsg);


            for (int i = 0; i < visibleMsg.Length; i++)
            {
                if (visibleMsg[i].ToString().ToLower() == btn.Text)
                {
                    newHiddenMsg[i * 2] = visibleMsg[i];
                }
            }
            hiddenMsg = newHiddenMsg.ToString();

            Label labelMsg = (Label)gridHiddenMsg.Children[0];
            labelMsg.Text = hiddenMsg;

            // win game condition
            if (!hiddenMsg.Contains('_'))
            {
                GameOver("you won", Color.FromRgb(204, 255, 51));
                return;
            }
        }

        private void UpdateImage()
        {
            string[] strNumber = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven" };

            hangmanImg.Source = "hangman_" + strNumber[CountClickedBtns] + ".png";
        }

        private string ChoosePasswordFromArray()
        {
            string res;

            string[] passwordsDatabase = {
                "szybki biegacz",
                "stary drewniany domek",
                "parabola",
                "Pan Tadeusz",
                "inkrementacja",
                "antyterrorysta",
                "europarlamentarzysta",
                "podatek dochodowy",
                "Adam Mickiewicz",
                "Elon Musk",
                "Denzel Washington",
                "Rzeczpospolita Polska",
                "co ty tutaj robisz",
                "pijemy za lepszy czas",
                "kung fu panda",
                "kebab na cienkim",
                "Pan Dzierwa jest cool",
                "onomatopeja",
                "Bruce Lee karate mistrz",
                "Missisipi",
                "Massachusetts",
                "Document Object Model",
                "Domain Name System",
                "gra w wisielca"
            };

            Random rand = new Random();

            int idx = rand.Next() % passwordsDatabase.Length;

            res = passwordsDatabase[idx];

            return res;
        }

        private bool PasswordContainLetter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            foreach (char letter in visibleMsg)
            {
                if (letter.ToString().ToLower() == btn.Text)
                {
                    return true;
                }
            }
            return false;
        }

        private void GameOver(string msg, Color color)
        {
            EnableBtnKeyboardClick = false;

            Label label = new Label
            {
                Text = msg,
                FontSize = 56,
                TextColor = color
            };

            Button btn = new Button
            {
                Text = "Next one",
                FontSize = 25,
                WidthRequest = 150,
                HeightRequest = 50,
                BackgroundColor = Colors.White,
                TextColor = Color.FromRgb(61, 49, 149),
                Margin = new Thickness(100, 0, 0, 0)
            };

            btn.Clicked += ResetGame;

            StackLayout layout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center
            };

            layout.Children.Add(label);
            layout.Children.Add(btn);

            displayInfo.Children.Add(layout);
        }

        private void ResetGame(Object sender, EventArgs e)
        {
            // Clear the current hidden message
            hiddenMsg = string.Empty;

            // Reset clicked buttons and their appearances
            foreach (var child in gridButtons.Children)
            {
                if (child is Grid grid)
                {
                    foreach (var neastedChild in grid.Children)
                    {
                        if (neastedChild is Button btn)
                        {
                            btn.IsEnabled = true;
                            btn.BackgroundColor = Colors.White;
                            btn.TextColor = Color.FromRgb(61, 49, 149);
                        }
                    }
                }
            }

            // Reset hangman image
            CountClickedBtns = 0;
            UpdateImage();

            // Enable button clicks
            EnableBtnKeyboardClick = true;

            // Remove previous game over message and button next one
            displayInfo.Children.Clear();

            // display custom password button
            DisplayYourOwnPasswordButton();

            // Remove previous password
            gridHiddenMsg.Children.Clear();

            // Start a new game
            visibleMsg = ChoosePasswordFromArray();
            DisplayHiddenMessage();
        }
    }

}
