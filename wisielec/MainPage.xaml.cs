using System.Text;

namespace wisielec
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DisplayHiddenMessage();
            DisplayButtons();
        }

        int CountClickedBtns = 0;

        string visibleMsg = "hello world";
        string hiddenMsg = "";

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
                    Margin = 1
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
                    Margin = 1
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
                    Margin = 1
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

        private void BtnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            btn.IsEnabled = false;

            if (!PasswordContainLetter(sender, e))
            {
                btn.BackgroundColor = Color.FromRgb(239, 35, 60);
                btn.TextColor = Colors.Black;

                if (++CountClickedBtns >= 11)
                {
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
            Label label = new Label
            {
                Text = msg,
                FontSize = 56,
                TextColor = color
            };

            displayInfo.Add(label);
        }
    }

}
