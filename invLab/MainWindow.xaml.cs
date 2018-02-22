using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace invLab
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>Ы
    public partial class MainWindow : Window
    {
        ApplicationContext db;
        private object obj;
        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
            db.Cameras.Load();
            db.Employes.Load();
            db.Rooms.Load();
            cameraGrid.DataContext = db.Cameras.Local.ToBindingList();
            roomGrid.DataContext = db.Rooms.Local.ToBindingList();
            teachGrid.DataContext = db.Employes.Local.ToBindingList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int tab = Convert.ToInt32(tabC.SelectedIndex.ToString());
            switch (tab)
            {
                case 0: obj = new Camera();
                    break;
                case 1:
                    obj = new Room();
                    break;
                case 2:
                    obj = new Employe();
                    break;
            }
            DataWindow newW = new DataWindow(obj, tab, 0);
            newW.Show();
        }

        private void change_click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            int tab = Convert.ToInt32(tabC.SelectedIndex.ToString());
            switch (tab)
            {
                case 0: if (cameraGrid.SelectedIndex >= 0)
                        obj = db.Cameras.Local.ElementAt<Camera>(cameraGrid.SelectedIndex);
                    else { MessageBox.Show("Не выделен элемент"); error = true; }
                    break;
                case 1:
                    if (roomGrid.SelectedIndex >= 0)
                        obj = db.Rooms.Local.ElementAt<Room>(roomGrid.SelectedIndex);
                    else { MessageBox.Show("Не выделен элемент"); error = true; }
                    break;
                case 2:
                    if (teachGrid.SelectedIndex >= 0)
                        obj = db.Employes.Local.ElementAt<Employe>(teachGrid.SelectedIndex);
                    else { MessageBox.Show("Не выделен элемент"); error = true; }
                    break;
            }
            if (!error)
            {
                DataWindow newW = new DataWindow(obj, tab, 1);
                newW.Show();
            }
        }
    }
}
