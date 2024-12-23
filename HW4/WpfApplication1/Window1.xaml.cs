using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1
{
    public partial class Window1 : Window
    {
        List<StackPanel> Array { get; set; }

        public Window1()
        {
            InitializeComponent();
            Array = new List<StackPanel>();
            AlphaSlider.Value = 255;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte red = (byte)RedSlider.Value;
            byte green = (byte)GreenSlider.Value;
            byte blue = (byte)BlueSlider.Value;
            byte alpha = (byte)AlphaSlider.Value;

            RedValueText.Text = red.ToString();
            GreenValueText.Text = green.ToString();
            BlueValueText.Text = blue.ToString();
            AlphaValueText.Text = alpha.ToString();

            Color color = Color.FromArgb(alpha, red, green, blue);

            ColorPreview.Fill = new SolidColorBrush(color);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte red = (byte)RedSlider.Value;
            byte green = (byte)GreenSlider.Value;
            byte blue = (byte)BlueSlider.Value;
            byte alpha = (byte)AlphaSlider.Value;

            Color color = Color.FromArgb(alpha, red, green, blue);

            Rectangle rectangle = new Rectangle();
            rectangle.Fill = new SolidColorBrush(color);
            rectangle.Width = 100;
            rectangle.Height = 30;
            rectangle.Margin = new Thickness(10, 0, 0, 0);
            rectangle.StrokeThickness = 0;

            TextBlock textBlock1 = new TextBlock();
            textBlock1.Text = $"RGB({red}, {green}, {blue}, {alpha})";

            Button button1 = new Button();
            button1.Width = 100;
            button1.Margin = new Thickness(10, 0, 0, 0);
            button1.Content = "Delete";
            button1.Click += DeleteButton_Click;

            StackPanel first = new StackPanel();
            first.Orientation = Orientation.Horizontal;
            first.Children.Add(textBlock1);
            first.Children.Add(rectangle);
            first.Children.Add(button1);
            Array.Add(first);

            root.Children.Add(first);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                StackPanel parentStackPanel = clickedButton.Parent as StackPanel;
                if (parentStackPanel != null)
                {
                    root.Children.Remove(parentStackPanel);
                    Array.Remove(parentStackPanel);
                }
            }
        }
    }
}
