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
        private int index, oper;
        private string btnText;
        public object data;
        public Camera _camera;
        public Room _room;
        public Employe _employe; 

        public DataWindow(object data, int ind, int move) //экземпляр класса, его вид(аудитория, сотрудник, камера) и операция (добавление, изменение)
        {
            InitializeComponent();
            index = ind;
            oper = move;
            if (move == 0) btnText = "Добавить";
            else btnText = "Изменить";
            Print(data);
        }

        public void Print(object data)
        {
            string[] names = new string[1], name2 = new string[1];
            switch (index) {
                case 0:
                names = new string[16] { "Номер аудитории", "Ответственный", "Название камеры", "Тип камеры", "Тип матрицы", "Фокусное расстояние", "Разрешение", "Частота", "Ночной режим", "Угол обзора", "Вес", "Цена", "Дата поставки", "Срок амортизации", "Дата выхода из эксп", "Статус" };
                name2 = new string[16] { "Roomid", "Empid", "Name", "Camera_type", "Matrix_type", "Focus", "Resolution", "Speed", "Night_mode", "Angle", "Weigth", "Cost", "Start_date", "Using_term", "Finish_date", "Status" };
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
            this.DataContext = data;

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
                TextBox b = new TextBox { Name = name2[i], Height = 30, IsEnabled = true, Margin=new Thickness(0,0,15,0), VerticalAlignment = VerticalAlignment.Center };
                b.SetBinding(TextBox.TextProperty, bind);
                dwgrid.Children.Add(l);
                dwgrid.Children.Add(b);
                Grid.SetRow(l, i); Grid.SetRow(b, i);
                Grid.SetColumn(l, 0); Grid.SetColumn(b, 1);
            }
            Button btn = new Button { Content = btnText, Height=30, Width=100 };
            btn.Click += new RoutedEventHandler(Button_Click);
            dwgrid.Children.Add(btn); Grid.SetColumn(btn, 1); Grid.SetRow(btn, names.Length);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (oper == 0)
                data = this.DataContext;
            else
            {
                if (index == 0)
                {
                    _camera = (Camera)this.DataContext;
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
