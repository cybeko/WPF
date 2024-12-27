using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace KeyboardTrainer
{
    internal sealed partial class MainWindow : Window
    {
        string sampleText = "Hello world";
        bool isStarted = false;

        Color CorrectColor = Color.FromRgb(90, 240, 170);
        Color WrongColor = Color.FromRgb(255, 100, 90); 

        private bool isCaps = false;
        private int totalCharsTyped = 0; 
        private int fails = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!isStarted)
                return;

            bool isShiftPressed = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);
            string key = e.Key.ToString();

            if (e.Key == Key.CapsLock)
            {
                isCaps = !isCaps;
            }

            if (e.Key == Key.Back && TypingTextBlock.Inlines.Count > 0)
            {
                TypingTextBlock.Inlines.Remove(TypingTextBlock.Inlines.LastInline);
                totalCharsTyped--; 
                return;
            }

            if (key.Length == 1)
            {
                char character = key[0];
                bool isUpperCase = isCaps ^ isShiftPressed;
                char typedChar = isUpperCase ? char.ToUpper(character) : char.ToLower(character);
                AddChar(typedChar);
            }
            else
            {
                switch (e.Key)
                {
                    case Key.Space:
                        AddChar(' ');
                        break;
                    case Key.OemOpenBrackets:
                        AddChar('{');
                        break;
                    case Key.OemCloseBrackets:
                        AddChar('}');
                        break;
                    case Key.OemPipe:
                        AddChar('|');
                        break;
                    case Key.OemTilde:
                        AddChar('~');
                        break;
                    case Key.D1:
                        AddChar('!');
                        break;
                    case Key.D2:
                        AddChar('@');
                        break;
                    case Key.D3:
                        AddChar('#');
                        break;
                    case Key.D4:
                        AddChar('$');
                        break;
                    case Key.D5:
                        AddChar('%');
                        break;
                    case Key.D6:
                        AddChar('^');
                        break;
                    case Key.D7:
                        AddChar('&');
                        break;
                    case Key.D8:
                        AddChar('*');
                        break;
                    case Key.D9:
                        AddChar('(');
                        break;
                    case Key.D0:
                        AddChar(')');
                        break;
                    case Key.OemMinus:
                        AddChar('-');
                        break;
                    case Key.OemPlus:
                        AddChar('+');
                        break;
                    case Key.OemSemicolon:
                        AddChar(';');
                        break;
                    case Key.OemQuotes:
                        AddChar('"');
                        break;
                    case Key.OemComma:
                        AddChar(',');
                        break;
                    case Key.OemPeriod:
                        AddChar('.');
                        break;
                    case Key.OemQuestion:
                        AddChar('?');
                        break;
                    default:
                        break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isStarted = true;
            SampleTextBlock.Text = sampleText;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEndMessage();
            ResetGame();
        }
        private void ShowEndMessage()
        {
            string message = $"Total Characters Typed: {totalCharsTyped}\n" +
                            $"Fails: {fails}\n";

            MessageBox.Show(message, "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ResetGame()
        {
            TypingTextBlock.Inlines.Clear();
            isStarted = false;
            SampleTextBlock.Text = string.Empty;
            totalCharsTyped = 0;  
            fails = 0;
            UpdateInfo();  
        }

        private void AddChar(char character)
        {
            int typedLength = TypingTextBlock.Text.Length;

            Run run = new Run(character.ToString());

            if (typedLength < sampleText.Length && sampleText[typedLength] == character)
            {
                run.Background = new SolidColorBrush(CorrectColor);
            }
            else
            {
                run.Background = new SolidColorBrush(WrongColor);
                fails++;
            }

            TypingTextBlock.Inlines.Add(run);
            totalCharsTyped++;
            UpdateInfo();

            if (TypingTextBlock.Text == sampleText)
            {
                ShowEndMessage();
                ResetGame();
            }
        }

        private void UpdateInfo()
        {
            SpeedTextBlock.Text = totalCharsTyped.ToString();
            FailsTextBlock.Text = fails.ToString();
        }
    }
}
