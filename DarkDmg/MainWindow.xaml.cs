using System;
using System.Windows;
using System.Text.RegularExpressions;


namespace DarkDmg
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetOutput();
        }
        private void Move_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter) { GetOutput(); }
        }
        public decimal Calc(decimal dmg, decimal movemodifier, decimal trink)
        {
            return Math.Ceiling(Math.Ceiling(dmg * movemodifier) * trink);
        }
        public void GetOutput()
        {
            try
            {
                Result1.Text = ""; Result2.Text = ""; Result3.Text = ""; Result4.Text = "";
                decimal modifier1 = Convert.ToDecimal(Move1.Text);
                decimal modifier2 = Convert.ToDecimal(Move2.Text);
                decimal modifier3 = Convert.ToDecimal(Move3.Text);
                decimal modifier4 = Convert.ToDecimal(Move4.Text);

                decimal minDmg = Convert.ToDecimal(Regex.Match(BaseDamage.Text, @"^\d+(?=\D+)").ToString());
                decimal maxDmg = Convert.ToDecimal(Regex.Match(BaseDamage.Text, @"(?<=\D+)\d+$").ToString());
                decimal trinket = 1;


                for (int x = 0; x <= 30; x++)
                {
                    Result1.Text += $" {Calc(minDmg, modifier1, trinket)}-{Calc(maxDmg, modifier1, trinket)} (x{trinket}) \n";
                    Result2.Text += $" {Calc(minDmg, modifier2, trinket)}-{Calc(maxDmg, modifier2, trinket)} (x{trinket}) \n";
                    Result3.Text += $" {Calc(minDmg, modifier3, trinket)}-{Calc(maxDmg, modifier3, trinket)} (x{trinket}) \n";
                    Result4.Text += $" {Calc(minDmg, modifier4, trinket)}-{Calc(maxDmg, modifier4, trinket)} (x{trinket}) \n";

                    trinket += (decimal)0.05;
                }
            }
            catch
            {
                Result1.Text = "Text formatting was incorrect!";
            }
        }


    }
}
