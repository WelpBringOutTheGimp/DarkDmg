using System;
using System.Windows;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;


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
        public List<decimal> Calc(List<int> dmgrange, decimal movemodifier, decimal trink, decimal prot)
        {
            List<decimal> resolvedrange = new List<decimal>();
            dmgrange.ForEach(delegate (int dmg)
            {
                resolvedrange.Add(Math.Floor(Math.Ceiling(dmg * movemodifier * trink) * (1 - prot)));
            });
            return resolvedrange;
        }
        public decimal dmgpercent(List<decimal> baserange, List<decimal> boostrange)
        {
            if (boostrange.Sum() != 0)
            {
                return Decimal.Round(Decimal.Round(baserange.Sum() / boostrange.Sum(), 3) * 100 - 100, 1);
            }
            else return 0;
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

                int minDmg = Convert.ToInt32(Regex.Match(BaseDamage.Text, @"^\d+(?=\D+)").ToString());
                int maxDmg = Convert.ToInt32(Regex.Match(BaseDamage.Text, @"(?<=\D+)\d+$").ToString());

                decimal protection = Convert.ToDecimal(Regex.Match(EnemyProt.Text, @"\d+$").ToString()) / 100;
                decimal trinket = 1;

                List<int> baserange = Enumerable.Range(minDmg, (maxDmg - minDmg + 1)).ToList();
                List<decimal> basemodified1 = Calc(baserange, modifier1, 1, protection);
                List<decimal> basemodified2 = Calc(baserange, modifier2, 1, protection);
                List<decimal> basemodified3 = Calc(baserange, modifier3, 1, protection);
                List<decimal> basemodified4 = Calc(baserange, modifier4, 1, protection);

                


                for (int x = 0; x <= 20; x++)
                {
                    List<decimal> dmgcalc1 = Calc(baserange, modifier1, trinket, protection);
                    List<decimal> dmgcalc2 = Calc(baserange, modifier2, trinket, protection);
                    List<decimal> dmgcalc3 = Calc(baserange, modifier3, trinket, protection);
                    List<decimal> dmgcalc4 = Calc(baserange, modifier4, trinket, protection);

                    Result1.Text += $" {dmgcalc1[0]}-{dmgcalc1[dmgcalc1.Count - 1]} +{dmgpercent(dmgcalc1, basemodified1)}% (x{trinket}) \n";
                    Result2.Text += $" {dmgcalc2[0]}-{dmgcalc2[dmgcalc2.Count - 1]} +{dmgpercent(dmgcalc2, basemodified2)}% (x{trinket}) \n";
                    Result3.Text += $" {dmgcalc3[0]}-{dmgcalc3[dmgcalc3.Count - 1]} +{dmgpercent(dmgcalc3, basemodified3)}% (x{trinket}) \n";
                    Result4.Text += $" {dmgcalc4[0]}-{dmgcalc4[dmgcalc4.Count - 1]} +{dmgpercent(dmgcalc4, basemodified4)}% (x{trinket}) \n";

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
