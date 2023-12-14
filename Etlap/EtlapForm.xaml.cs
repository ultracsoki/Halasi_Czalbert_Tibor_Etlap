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
using System.Windows.Shapes;

namespace Etlap
{
    /// <summary>
    /// Interaction logic for EtlapForm.xaml
    /// </summary>
    public partial class EtlapForm : Window
    {
        private EtlapService etlapService;
        private Etel etelFrissitese = new Etel();
        public EtlapForm(EtlapService etlapService)
        {
            InitializeComponent();
            this.etlapService = etlapService;
        }

        private void buttonHozzaAd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Etel etel = Validacio();
                if (etlapService.Create(etel))
                {
                    MessageBox.Show("Sikeres hozzáadás");
                    tbNev.Text = "";
                    tbLeiras.Text = "";
                    tbAr.Text = "";
                    cbKategoria.Text = "";
                }
                else
                {
                    MessageBox.Show("Hiba történt a hozzáadás során");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Etel Validacio()
        {
            string nev = tbNev.Text.Trim();
            string leiras = tbLeiras.Text.Trim();
            string arText = tbAr.Text.Trim();
            string kategoria = cbKategoria.Text.Trim();
            if (!int.TryParse(arText, out int ar))
            {
                throw new Exception("Az ár csak szám lehet!");
            }


            Etel etel = new Etel();
            etel.Nev = nev;
            etel.Leiras = leiras;
            etel.Ar = ar;
            etel.Kategoria = kategoria;
            return etel;
        }
    }
}
