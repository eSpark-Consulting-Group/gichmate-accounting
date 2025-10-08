using System;
using System.Windows;


namespace AccountsPro.MVVM.View.PropertyManagement
{
    public partial class UtilityPerUnitCostCharges : Window
    {
        public UtilityPerUnitCostCharges()
        {
            InitializeComponent();
        }


        private void NumericOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9.]+$");
        }


        private void SaveCurrentChargesBtn_Click(object sender, RoutedEventArgs e)
        {
            //Save current Charges cost rate per unit of each
        }


        private void UpdateCurrentChargesBtn_Click(object sender, RoutedEventArgs e)
        {
            //Update current Charges cost rate per unit of each
        }


        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            //clear all the fields
            WaterChargesPerUnitTxtBox.Text = string.Empty; //Water field TxtBox
            SanitationChargesPerUnitTxtBox.Text = string.Empty; //Sanitation field TxtBox
            GasChargesPerUnitTxtBox.Text = string.Empty; //Gas field TxtBox
            GasChargesPerUnitTxtBox.Text = string.Empty; //Gas charges TxtBox
            ElectricityChargesPerUnitTxtBox.Text = string.Empty; //Electricity field TxtBox
            InternetChargesPerUnitTxtBox.Text = string.Empty; //Internet field TxtBox
            MiscChargesPerUnitTxtBox.Text = string.Empty; //Misc field TxtBox
            LaundryChargesPerUnitTxtBox.Text = string.Empty; //Laundry TxtBox
            TVChargesPerUnitTxtBox.Text = string.Empty; //TV TxtBox
        }
    }
}

