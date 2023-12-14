using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Etlap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EtlapService etlapService = new EtlapService();
        public MainWindow()
        {
            InitializeComponent();
            DataGridFrissit();
        }

        private void DataGridFrissit()
        {
            //dataGridEtlap.Columns[0] 
            dataGridEtlap.ItemsSource = etlapService.GetAll();
        }

        private void ujFelveteleButton_Click(object sender, RoutedEventArgs e)
        {
            EtlapForm form = new EtlapForm(etlapService);
            form.Closed += (_, _) =>
            {
                DataGridFrissit();
            };
            form.ShowDialog();
        }

        private void torlesButton_Click(object sender, RoutedEventArgs e)
        {
            Etel kivalasztott = dataGridEtlap.SelectedItem as Etel;
            if (kivalasztott == null)
            {
                MessageBox.Show("A törléshez előbb válasszon ki ételt!");
                return;
            }
            MessageBoxResult selectedButton = MessageBox.Show($"Biztos, hogy törölni szeretné az alábbi ételt: {kivalasztott.Nev} ?",
                "Biztos?", MessageBoxButton.YesNo);
            if (selectedButton == MessageBoxResult.Yes)
            {
                if (etlapService.Delete(kivalasztott.Id))
                {
                    MessageBox.Show("A törlés sikeres volt!");
                }
                else
                {
                    MessageBox.Show("Hiba történt a törlés során!");
                }
                DataGridFrissit();
            }
        }

        private void buttonSzazalekEmeles_Click(object sender, RoutedEventArgs e)
        {
            string szazalekText = emelesSzazalekTextBox.Text;
            if (emelesSzazalekTextBox.Text == "")
            {
                MessageBox.Show("Az emelés mezőt kötelező kitölteni!");
                return;
            }
            if (!double.TryParse(szazalekText, out double szazalek))
            {
                MessageBox.Show("Az emelés százalékát szám formátumban kell megadnod!");
                return;
            }

            Etel kivalasztott = dataGridEtlap.SelectedItem as Etel;
            if (kivalasztott != null)
            {
                //egy elem emelése
                MessageBoxResult selectedButton = MessageBox.Show($"Biztos, hogy emelni szeretné a {kivalasztott.Nev} árát?",
                "Biztos?", MessageBoxButton.YesNo);
                if (selectedButton == MessageBoxResult.Yes)
                {
                    Etel ujEtel = new Etel();
                    ujEtel.Id = kivalasztott.Id;
                    ujEtel.Nev = kivalasztott.Nev;
                    ujEtel.Leiras = kivalasztott.Leiras;
                    ujEtel.Ar = kivalasztott.Ar * (1 + (szazalek/100));
                    ujEtel.Kategoria = kivalasztott.Kategoria;
                    if (etlapService.UpdateEgyElemSzazalek(kivalasztott.Id,ujEtel))
                    {
                        MessageBox.Show("Az emelés sikeres volt");
                    }
                    else
                    {
                        MessageBox.Show("Hiba történt az emelés során!");
                    }
                    DataGridFrissit();
                }
            }
            else
            {
                MessageBoxResult selectedButton = MessageBox.Show($"Biztos, hogy emelni szeretné az összes étel árát?",
                "Biztos?", MessageBoxButton.YesNo);
                if (selectedButton == MessageBoxResult.Yes)
                {
                    if (etlapService.UpdateOsszesSzazalek(szazalek))
                    {
                        MessageBox.Show("Az emelés sikeres volt");
                    }
                    else
                    {
                        MessageBox.Show("Hiba történt az emelés során!");
                    }
                    DataGridFrissit();
                }
            }
            
        }

        private void buttonFtEmeles_Click(object sender, RoutedEventArgs e)
        {
            string forintText = emelesFtTextBox.Text;
            if (emelesFtTextBox.Text == "")
            {
                MessageBox.Show("Az emelés mezőt kötelező kitölteni!");
                return;
            }
            if (!double.TryParse(forintText, out double forint))
            {
                MessageBox.Show("Az emelés százalékát szám formátumban kell megadnod!");
                return;
            }

            Etel kivalasztott = dataGridEtlap.SelectedItem as Etel;
            if (kivalasztott != null)
            {
                //egy elem emelése
                MessageBoxResult selectedButton = MessageBox.Show($"Biztos, hogy emelni szeretné a {kivalasztott.Nev} árát?",
                "Biztos?", MessageBoxButton.YesNo);
                if (selectedButton == MessageBoxResult.Yes)
                {
                    Etel ujEtel = new Etel();
                    ujEtel.Id = kivalasztott.Id;
                    ujEtel.Nev = kivalasztott.Nev;
                    ujEtel.Leiras = kivalasztott.Leiras;
                    ujEtel.Ar = kivalasztott.Ar + forint;
                    ujEtel.Kategoria = kivalasztott.Kategoria;
                    if (etlapService.UpdateEgyElemForint(kivalasztott.Id, ujEtel))
                    {
                        MessageBox.Show("Az emelés sikeres volt");
                    }
                    else
                    {
                        MessageBox.Show("Hiba történt az emelés során!");
                    }
                    DataGridFrissit();
                }
            }
            else
            {
                MessageBoxResult selectedButton = MessageBox.Show($"Biztos, hogy emelni szeretné az összes étel árát?",
                "Biztos?", MessageBoxButton.YesNo);
                if (selectedButton == MessageBoxResult.Yes)
                {
                    if (etlapService.UpdateOsszesForint(forint))
                    {
                        MessageBox.Show("Az emelés sikeres volt");
                    }
                    else
                    {
                        MessageBox.Show("Hiba történt az emelés során!");
                    }
                    DataGridFrissit();
                }
            }
        }

        private void dataGridEtlap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Etel kivalasztott = dataGridEtlap.SelectedItem as Etel;
            if (kivalasztott != null)
            {
                textBlockLeiras.Text = kivalasztott.Leiras;
            }
            else
            {
                textBlockLeiras.Text = "";
            }
        }
    }
}