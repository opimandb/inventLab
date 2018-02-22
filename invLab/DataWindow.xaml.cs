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
        private int index;
        private string btnText;

        public DataWindow(object data, int ind, int move)
        {
            InitializeComponent();
            index = ind;
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
                case 1:
                    names = new string[3] { "ФИО", "Факультет", "Должность" };
                    name2 = new string[3] { "Fio", "Faculty", "Vacansy" };
                    break;
                case 2:
                    names = new string[3] { "Блок", "Аудитория", "Тип"};
                    name2 = new string[3] { "Housing", "Number", "Type"};
                    break;
            }
            //получение данных из экземпляра
            string[] datas = new string[names.Length+1];
            int j = 0;
            foreach (var kv in GetDictionary(data))
            {
                datas[j] = kv.Value;
                j++;
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
                Label l = new Label { Content = names[i], VerticalAlignment = VerticalAlignment.Center };
                TextBox b = new TextBox { Text = datas[i+1],  Name = name2[i], Height = 30, IsEnabled = true, Margin=new Thickness(0,0,15,0), VerticalAlignment = VerticalAlignment.Center };
                dwgrid.Children.Add(l);
                dwgrid.Children.Add(b);
                Grid.SetRow(l, i); Grid.SetRow(b, i);
                Grid.SetColumn(l, 0); Grid.SetColumn(b, 1);
            }
            Button btn = new Button { Content = btnText, Height=30, Width=100};
            dwgrid.Children.Add(btn); Grid.SetColumn(btn, 1); Grid.SetRow(btn, names.Length);
        }
        
        static Dictionary<string, string> GetDictionary(object o)
        {
            return o.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(o)?.ToString());
        }
    }
}
