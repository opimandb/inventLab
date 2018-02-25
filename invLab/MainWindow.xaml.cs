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
            DataWindow newW = new DataWindow(tab, 0, db, 0);
            if (newW.ShowDialog()==true)
            {
                obj = newW.data;
                switch (tab)
                {
                    case 0:
                        db.Cameras.Add((Camera)obj);
                        break;
                    case 1:
                        db.Rooms.Add((Room)obj);
                        break;
                    case 2:
                        db.Employes.Add((Employe)obj);
                        break;
                }
                db.SaveChanges();
            }
        }

        private void change_click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            int _id = 0;
            int tab = Convert.ToInt32(tabC.SelectedIndex.ToString());
            switch (tab)
            {
                case 0: if (cameraGrid.SelectedIndex >= 0)
                    {
                        obj = db.Cameras.Local.ElementAt<Camera>(cameraGrid.SelectedIndex);
                        _id = db.Cameras.Local.ElementAt<Camera>(cameraGrid.SelectedIndex).id;
                    }
                    else { MessageBox.Show("Не выделен элемент"); error = true; }
                    break;
                case 1:
                    if (roomGrid.SelectedIndex >= 0)
                    {
                        obj = db.Rooms.Local.ElementAt<Room>(roomGrid.SelectedIndex);
                        _id = db.Rooms.Local.ElementAt<Room>(roomGrid.SelectedIndex).id;
                    }
                    else { MessageBox.Show("Не выделен элемент"); error = true; }
                    break;
                case 2:
                    if (teachGrid.SelectedIndex >= 0)
                    {
                        obj = db.Employes.Local.ElementAt<Employe>(teachGrid.SelectedIndex);
                        _id = db.Employes.Local.ElementAt<Employe>(teachGrid.SelectedIndex).id;
                    }
                    else { MessageBox.Show("Не выделен элемент"); error = true; }
                    break;
            }
            if (!error)
            {
                DataWindow newW = new DataWindow(tab, 1, db, _id);
                Camera _cam; Room _room; Employe _emp;
                if (newW.ShowDialog() == true)
                {
                    switch (tab)
                    {
                        case 0:
                            _cam = db.Cameras.Find(newW._camera.id);
                            if (_cam != null)
                            {
                                _cam = newW._camera;
                                db.Entry(_cam).State = EntityState.Modified;
                            }
                            break;
                        case 1:
                            _room = db.Rooms.Find(newW._room.id);
                            if (_room != null)
                            {
                                _room = newW._room;
                                db.Entry(_room).State = EntityState.Modified;
                            }
                            break;
                        case 2:
                            _emp = db.Employes.Find(newW._employe.id);
                            if (_emp != null)
                            {
                                _emp = newW._employe;
                                db.Entry(_emp).State = EntityState.Modified;
                            }
                            break;
                    }
                    db.SaveChanges();
                }
            }
        }

        private void remove_click(object sender, RoutedEventArgs e)
        {
            //можно добавить форму с просьбой подтвердить удаление
            int tab = Convert.ToInt32(tabC.SelectedIndex.ToString());
            switch (tab)
            {
                case 0:
                    if (cameraGrid.SelectedIndex >= 0)
                    {
                        obj = db.Cameras.Local.ElementAt<Camera>(cameraGrid.SelectedIndex);
                        db.Cameras.Remove((Camera)obj);
                    }
                    else  MessageBox.Show("Не выделен элемент");
                    break;
                case 1:
                    if (roomGrid.SelectedIndex >= 0)
                    {
                        obj = db.Rooms.Local.ElementAt<Room>(roomGrid.SelectedIndex);
                        db.Rooms.Remove((Room)obj);
                    }
                    else MessageBox.Show("Не выделен элемент");
                    break;
                case 2:
                    if (teachGrid.SelectedIndex >= 0)
                    {
                        obj = db.Employes.Local.ElementAt<Employe>(teachGrid.SelectedIndex);
                        db.Employes.Remove((Employe)obj);
                    }
                    else MessageBox.Show("Не выделен элемент");
                    break;
            }
            db.SaveChanges();
        }
    }
}
