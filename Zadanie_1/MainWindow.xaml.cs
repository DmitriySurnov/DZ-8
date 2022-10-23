using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace klava
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool flagCapsLock = true;
        int namber = 5;
        bool isError = false;
        bool Isreg = false;
        bool flagBackspase = true;
        int Sped = 0;
        int fails = 0;
        int Time = 0;
        DispatcherTimer timer = null;
        bool mesStop = true;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Time++;
            charsChar.Content = Time;
            Speed();
        }

        private void ChangingCase(Button B_clic, bool Bool)
        {
            LetterCaseChanges_1(B_clic, (flagCapsLock) ? Bool : !Bool);
            if (Bool)
                CapitalSymbol();
            else
                LoverSymbol();
        }

        private void CapitalSymbol()
        {
            this.Oem3.Content = "~";
            this.D1.Content = "!";
            this.D2.Content = "@";
            this.D3.Content = "#";
            this.D4.Content = "$";
            this.D5.Content = "%";
            this.D6.Content = "^";
            this.D7.Content = "&";
            this.D8.Content = "*";
            this.D9.Content = "(";
            this.D0.Content = ")";
            this.OemMinus.Content = "_";
            this.OemPlus.Content = "+";
            this.OemOpenBrackets.Content = "{";
            this.Oem6.Content = "}";
            this.Oem5.Content = "|";
            this.Oem1.Content = ":";
            this.OemQuotes.Content = "\"";
            this.OemComma.Content = "<";
            this.OemPeriod.Content = ">";
            this.OemQuestion.Content = "?";
        }

        private void LoverSymbol()
        {
            this.Oem3.Content = "`";
            this.D1.Content = "1";
            this.D2.Content = "2";
            this.D3.Content = "3";
            this.D4.Content = "4";
            this.D5.Content = "5";
            this.D6.Content = "6";
            this.D7.Content = "7";
            this.D8.Content = "8";
            this.D9.Content = "9";
            this.D0.Content = "0";
            this.OemMinus.Content = "-";
            this.OemPlus.Content = "=";
            this.OemOpenBrackets.Content = "[";
            this.Oem6.Content = "]";
            this.Oem5.Content = "\\";
            this.Oem1.Content = ";";
            this.OemQuotes.Content = "'";
            this.OemComma.Content = ",";
            this.OemPeriod.Content = ".";
            this.OemQuestion.Content = "/";
        }

        private void LetterCaseChanges_1(Button B_clic, bool Bool)
        {
            List<Grid> List = new List<Grid>();
            List.Add(row3);
            List.Add(row4);
            List.Add(row5);
            foreach (var it in List)
                foreach (var item in it.Children)
                    if (item is Button)
                    {
                        Button B = item as Button;
                        string S = B.Content.ToString();
                        if (S.Length == 1)
                            B.Content = (Bool) ? S.ToUpper() : S.ToLower();
                    }
        }

        private void LetterCaseChanges(Button B_clic)
        {
            LetterCaseChanges_1(B_clic, flagCapsLock);
            flagCapsLock = !flagCapsLock;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (UIElement it in (this.Content as Grid).Children)
                if (it is Grid)
                    foreach (var item in (it as Grid).Children)
                        if (item is Button)
                        {
                            Button B = item as Button;
                            if ((B.Name == e.Key.ToString()) ||
                                (e.Key.ToString() == "System" &&
                                B.Name == e.SystemKey.ToString()))
                            {
                                B.Opacity = 0.5;
                                if (B.Name == "Capital")
                                    LetterCaseChanges(B);
                                else if (B.Name == "LeftShift" || B.Name == "RightShift")
                                    ChangingCase(B, true);
                                else if (B.Name == "Back")
                                    flagBackspase = false;
                                else
                                    flagBackspase = true;
                            }
                        }
        }

        private void lineUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lineUser.Text.Length > linePrograms.Text.Length)
                return;
            string str = linePrograms.Text.Substring(0, lineUser.Text.Length);
            if (lineUser.Text.Equals(str))
            {
                if (flagBackspase)
                    Speed();
                lineUser.Foreground = new SolidColorBrush(Colors.Black);
                isError = false;
            }
            else
            {
                if (flagBackspase)
                    fails++;
                lineUser.Foreground = new SolidColorBrush(Colors.Red);
                Fails.Content = fails;
                isError = true;
            }
            if (lineUser.Text.Length == linePrograms.Text.Length && !isError)
            {
                timer.Stop();
                Speed();
                lineUser.IsReadOnly = true;
                mesStop = false;
            }
        }
        private void Form_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            lineUser.Focus();
            foreach (UIElement it in (this.Content as Grid).Children)
                if (it is Grid)
                    foreach (var item in (it as Grid).Children)
                        if (item is Button)
                        {
                            Button B = item as Button;
                            if ((B.Name == e.Key.ToString()) ||
                                (e.Key.ToString() == "System" &&
                                B.Name == e.SystemKey.ToString()))
                            {
                                B.Opacity = 1;
                                if (B.Name == "LeftShift" || B.Name == "RightShift")
                                    ChangingCase(B, false);
                            }
                        }
            if (!mesStop)
            {
                MessageBox.Show($"Задание завершенно!\n Коилчество символов {linePrograms.Text.Length}.\n Коилчество ошибок {Fails.Content}.\nДля завершения задания нажмите Stop.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                mesStop = true;
            }
        }
        private void CaseSensitive_Checked_1(object sender, RoutedEventArgs e)
        {
            SliderDifficulty.Maximum = 94;
            Isreg = true;
        }
        private void CaseSensitive_Unchecked(object sender, RoutedEventArgs e)
        {
            SliderDifficulty.Maximum = 47;
            Isreg = false;
        }
        private void SliderDifficulty_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            namber = (int)(sender as Slider).Value;
            Difficulty.Content = namber.ToString();
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            SliderDifficulty.IsEnabled = false;
            CaseSensitive.IsEnabled = false;
            Stop.IsEnabled = true;
            lineUser.IsReadOnly = false;
            lineUser.IsEnabled = true;
            timer.Start();
            lineUser.Focus();
            CollectString();
        }

        private void CollectString()
        {
            string baceString;
            Random rendChar = new Random();
            if (Isreg)
                baceString = "`1234567890-=qwertyuiop[]\\asdfghjkl;'zxcvbnm,./~!@#$%^&*()_+QWERTYUIOP{}|ASDFGHJKL:\"ZXCVBNM<>?";
            else
                baceString = @"`1234567890-=qwertyuiop[]\asdfghjkl;'zxcvbnm,./";

            List<char> spisok = new List<char>();
            spisok.Add(' ');
            do
            {
                char c = baceString[rendChar.Next(0, baceString.Length)];
                if (!spisok.Contains(c))
                    spisok.Add(c);
            } while (spisok.Count() <= namber);
            for(int i=0; i<75;i++) 
                linePrograms.Text+= spisok[rendChar.Next(0, spisok.Count()-1)];
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = true;
            SliderDifficulty.IsEnabled = true;
            CaseSensitive.IsEnabled = true;
            Stop.IsEnabled = false;
            lineUser.Text = "";
            linePrograms.Text = "";
            Sped = 0;
            fails = 0;
            Time = 0;
            SpeedChar.Content = Sped;
            charsChar.Content = Time;
            Fails.Content = fails;
            lineUser.IsReadOnly = true;
            lineUser.IsEnabled = false;
            timer.Stop();
        }
        void Speed()
        {
            SpeedChar.Content = Math.Round(((double)lineUser.Text.Length / Time) * 60).ToString();
        }
    }
}
