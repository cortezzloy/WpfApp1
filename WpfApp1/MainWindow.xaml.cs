using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик выбора фигуры (выбор только одной)
        private void AcceptCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var selectedCheckBox = sender as CheckBox;

            if (selectedCheckBox == pryam && pryam.IsChecked == true)
            {
                treug.IsChecked = false;
                krug.IsChecked = false;
            }
            else if (selectedCheckBox == treug && treug.IsChecked == true)
            {
                pryam.IsChecked = false;
                krug.IsChecked = false;
            }
            else if (selectedCheckBox == krug && krug.IsChecked == true)
            {
                pryam.IsChecked = false;
                treug.IsChecked = false;
            }

            // Очищаем результат при смене фигуры
            ResultText.Text = "—";
        }

        private void AcceptCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Если все чекбоксы сняты
            if (pryam.IsChecked == false && treug.IsChecked == false && krug.IsChecked == false)
            {
                ResultText.Text = "—";
            }
        }

        // Кнопка расчёта
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверяем, выбрана ли фигура
                if (pryam.IsChecked == false && treug.IsChecked == false && krug.IsChecked == false)
                {
                    ResultText.Text = "❌ Ошибка: выберите фигуру";
                    ResultText.Foreground = System.Windows.Media.Brushes.Red;
                    return;
                }

                // Расчёт для прямоугольника
                if (pryam.IsChecked == true)
                {
                    CalculateRectangle();
                }
                // Расчёт для треугольника
                else if (treug.IsChecked == true)
                {
                    CalculateTriangle();
                }
                // Расчёт для круга
                else if (krug.IsChecked == true)
                {
                    CalculateCircle();
                }
            }
            catch (Exception ex)
            {
                ResultText.Text = $"❌ Ошибка: {ex.Message}";
                ResultText.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        // Расчёт площади прямоугольника
        private void CalculateRectangle()
        {
            double side1 = GetDoubleFromTextBox(frstside.Text, "1-я сторона");
            double side2 = GetDoubleFromTextBox(scndside.Text, "2-я сторона");

            if (side1 <= 0 || side2 <= 0)
            {
                ResultText.Text = "❌ Ошибка: стороны должны быть больше 0";
                ResultText.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            double area = side1 * side2;
            ResultText.Text = $"S = {side1:F2} × {side2:F2} = {area:F2} кв. ед.";
            ResultText.Foreground = System.Windows.Media.Brushes.Green;
        }

        // Расчёт площади треугольника
        private void CalculateTriangle()
        {
            double baseSide = GetDoubleFromTextBox(frstside.Text, "1-я сторона (основание)");
            double height = GetDoubleFromTextBox(thrdside.Text, "3-я сторона (высота)");

            if (baseSide <= 0 || height <= 0)
            {
                ResultText.Text = "❌ Ошибка: основание и высота должны быть больше 0";
                ResultText.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            double area = 0.5 * baseSide * height;
            ResultText.Text = $"S = ½ × {baseSide:F2} × {height:F2} = {area:F2} кв. ед.";
            ResultText.Foreground = System.Windows.Media.Brushes.Green;
        }

        // Расчёт площади круга
        private void CalculateCircle()
        {
            double r = GetDoubleFromTextBox(radius.Text, "радиус");

            if (r <= 0)
            {
                ResultText.Text = "❌ Ошибка: радиус должен быть больше 0";
                ResultText.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            double area = Math.PI * r * r;
            ResultText.Text = $"S = π × {r:F2}² = {area:F2} кв. ед.\n(π = {Math.PI:F5})";
            ResultText.Foreground = System.Windows.Media.Brushes.Green;
        }

        // Вспомогательный метод для парсинга чисел
        private double GetDoubleFromTextBox(string text, string paramName)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new Exception($"{paramName} не может быть пустым");

            // Заменяем запятую на точку
            text = text.Replace(',', '.');

            if (!double.TryParse(text, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double result))
            {
                throw new Exception($"{paramName} должно быть числом");
            }

            return result;
        }
    }
}