using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW1_2
{
    public partial class MainWindow : Window
    {
        private string expression = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            string? buttonContent = button?.Content.ToString();

            if (buttonContent == null) return;

            switch (buttonContent)
            {
                case "CE":
                    expression = "";
                    UpdateInput();
                    break;
                case "C":
                    expression = "";
                    Display.Text = "0";
                    UpdateInput();
                    break;
                case "<":
                    if (expression.Length > 0)
                    {
                        expression = expression.Remove(expression.Length - 1);
                    }
                    UpdateInput();
                    break;
                case "=": 
                    CalculateResult();
                    break;
                default:
                    HandleInput(buttonContent);
                    break;
            }
        }

        private void HandleInput(string input)
        {
            if (input == "." && expression.EndsWith(".")) return;
            if (input == "." && expression == "") expression = "0";
            if ("/*-+".Contains(input) && (expression == "" || "/*-+.".Contains(expression[^1].ToString()))) return;
            expression += input;
            UpdateInput();
        }

        private void UpdateInput()
        {
            InputTextBox.Text = expression == "" ? "0" : expression;
        }

        private void CalculateResult()
        {
            try
            {
                var dataTable = new DataTable();
                var result = dataTable.Compute(expression, null);
                Display.Text = result.ToString();
                expression = result.ToString();
                UpdateInput();
            }
            catch
            {
                Display.Text = "Error";
            }
        }
    }
}