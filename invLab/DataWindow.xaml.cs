using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        private List<Employe> employes;
        private ApplicationContext db2;
        private int roomid=0, empid = 0;
        private List<Room> rooms;
        private int index, oper;
        private string btnText;
        public object data;
        public Camera _camera;
        public Room _room;
        public Employe _employe;

        public DataWindow(int ind, int move, ApplicationContext db, int _id) //его вид(аудитория, сотрудник, камера), операция (добавление, изменение), context, id выбранного элемента
        {
            InitializeComponent();
            index = ind;
            getEls(db, _id); //получить из контекста конкретный класс
            oper = move;
            if (move == 0) btnText = "Добавить";
            else btnText = "Изменить";
            Print(); //отрисовка элементов
        }

        private void getEls(ApplicationContext db, int _id)
        {
            //получаем коллекцию из классов работников и аудторий
            employes = db.Employes.Local.ToList();
            rooms = db.Rooms.Local.ToList();

            //получаем контекст конкретного класса
            if (_id==0)
                switch (index) //если добавляем нвоый элемент, то создаем пустой экземпляр--
                {
                    case 0:
                        this.DataContext = new Camera();
                        break;
                    case 1:
                        this.DataContext = new Room();
                        break;
                    case 2:
                        this.DataContext = new Employe();
                        break;
                }
            else
            switch (index)
            {
                case 0:
                    this.DataContext = db.Cameras.Where(c => c.id == _id).FirstOrDefault();
                        //вытаскиываем из экземпляра камеры айдишники аудитории и сотрудника для конвертации в комбобокс --
                    roomid = db.Cameras.Where(c => c.id == _id).Select(c => c.Roomid).FirstOrDefault();
                    empid = db.Cameras.Where(c => c.id == _id).Select(c => c.Empid).FirstOrDefault();
                        //--
                    break;
                case 1:
                    this.DataContext = db.Rooms.Where(c => c.id == _id).FirstOrDefault();
                    break;
                case 2:
                    this.DataContext = db.Employes.Where(c => c.id == _id).FirstOrDefault();
                    break;
            }
        }

        public void Print()
        {
            string[] names = new string[1], name2 = new string[1];
            switch (index) {
                case 0:
                names = new string[16] { "Номер аудитории", "Ответственный", "Название камеры", "Тип камеры", "Тип матрицы", "Фокусное расстояние", "Разрешение", "Частота", "Ночной режим", "Угол обзора", "Вес", "Цена", "Дата поставки", "Срок амортизации", "Дата выхода из эксп", "Статус" };
                name2 = new string[16] { "Roomid", "Empid", "Name", "Camera_type", "Matrix_type", "Focus", "Resolution", "Speed", "Night_mode", "Angle", "Weight", "Cost", "Start_date", "Using_term", "Finish_date", "Status" };
                    break;
                case 2:
                    names = new string[3] { "ФИО", "Факультет", "Должность" };
                    name2 = new string[3] { "Fio", "Faculty", "Vacansy" };
                    break;
                case 1:
                    names = new string[3] { "Блок", "Аудитория", "Тип"};
                    name2 = new string[3] { "Housing", "Number", "Type"};
                    break;
            }

            //добавляем нужное количество строк
            //dwgrid.ShowGridLines = true;  //разделительные полосы
            RowDefinitionCollection rd = dwgrid.RowDefinitions;
            ColumnDefinitionCollection cd = dwgrid.ColumnDefinitions;
            for (int o = 0; o <= names.Length; o++)
            {
                rd.Add(new RowDefinition());
            }

            //добавление элементов управления
            for (int i=0;i<names.Length;i++)
            {
                Binding bind = new Binding();
                bind.Path = new PropertyPath(name2[i]);
                bind.Mode = BindingMode.TwoWay;
                Label l = new Label { Content = names[i], VerticalAlignment = VerticalAlignment.Center };
                if (!(index == 0 && (i == 0 || i == 1 || i == 15))) //если нужно добавить НЕ текстбокс, то добавляем его в условие
                {
                    TextBox b = new TextBox { Name = name2[i], Height = 30, IsEnabled = true, Margin = new Thickness(0, 0, 15, 0), VerticalAlignment = VerticalAlignment.Center };
                    b.SetBinding(TextBox.TextProperty, bind); //добавляем биндинг(указываем что будет внутри)
                    dwgrid.Children.Add(b);  //добавляем на страницу
                    Grid.SetRow(b, i); //устанавливаем в нужную строку
                    Grid.SetColumn(b, 1); //устанавливаем в нужный столбец
                }
                else
                {
                    ComboBox b = new ComboBox { Name = name2[i], Height = 30, IsEnabled = true, Margin = new Thickness(0, 0, 15, 0), VerticalAlignment = VerticalAlignment.Center };
                    dwgrid.Children.Add(b); 
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, 1);
                    int si = -1, cc=0;
                    //заполняем itemsource комбобоксов элементами из других таблиц и делаем по дефлоту тот элемент, id которого мы узнали в методе getels
                    if (i==0)
                    {
                        foreach (Room t in rooms)
                        {
                            b.Items.Add(t.Number);
                            if (t.id == roomid) si = cc;
                            cc++;
                        }
                        cc = 0;
                    }
                    if (i == 1)
                    {
                        foreach (Employe t in employes) { 
                            b.Items.Add(t.Fio);
                        if (t.id == empid) si = cc;
                        cc++;
                    }
                        cc = 0;
                    }
                    b.SetBinding(ComboBox.SelectedIndexProperty, bind);
                    b.SelectedIndex = si;
                    if (i==15)
                    {
                        b.ItemsSource = new List<string> { "Не используется", "Используется"};
                    }
                }
                dwgrid.Children.Add(l);
                Grid.SetRow(l, i); 
                Grid.SetColumn(l, 0); 
            }
            Button btn = new Button { Content = btnText, Height=30, Width=100 };
            btn.Click += new RoutedEventHandler(Button_Click); //добавляем обработчик кнопке
            dwgrid.Children.Add(btn); Grid.SetColumn(btn, 1); Grid.SetRow(btn, names.Length);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (oper == 0) //если добавление новго, то с заполненых полей переносим в поле data, к которому обратимся из основного окна
                data = this.DataContext;
            else
            { 
                if (index == 0) //добавляем в пустой экземпляр заполненный контекст
                {
                    _camera = (Camera)this.DataContext;
                    _camera.Roomid = rooms[_camera.Roomid].id; //при помощи списков и id в комбобоксе узнаем id самой аудитории и сотрудника
                    _camera.Empid = employes[_camera.Empid].id;
                }
                if (index == 1) {
                    _room = (Room)this.DataContext; }
                if (index == 2)
                {
                    _employe = (Employe)this.DataContext;
                }
            }
            this.DialogResult = true;
        }
    }
}
