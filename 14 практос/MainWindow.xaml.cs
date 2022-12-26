using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using LibMatrix;

namespace _14_практос
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Matrix<int> matr = new Matrix<int>(0, 0);
        int[] array;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void About_programm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Косичкин Илья ИСП-34\nПрактическая №14\n1. Создать копию программы из задания №13\n2. Добавить окно «Пароль» и организовать авторизацию в программе. Для упрощения проверки пароль задать «123».\n3. Создать окно «Настройка». Назначение окна – изменение размера таблицы. При установке размера таблицы сохранять настройки в файл конфигурации «config.ini».\n4. При запуске программы задавать размер таблицы согласно файлу конфигурации.\n5. Использовать исключения. Например: при чтении файла конфигурации если он отсутствует размер таблицы не задавать.\n6. При закрытии программы вывести диалоговое окно с подтверждением завершения работы.\n7. Оформить программу комментариями");
        }

        private void Exit_Click(object sender, RoutedEventArgs e) // выход с подтверждением 
        {
            MessageBoxResult result; 
            result = MessageBox.Show("Закрыть программу?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Original_Matr.ItemsSource = null;
            Result_Matr.ItemsSource = null;
            Size.Clear();
            Number_cell.Clear();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            matr.Deserialize(@".\matrix.txt");
            Original_Matr.ItemsSource = matr.ToDataTable().DefaultView;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            matr.Serialize(@".\matrix.txt");
            MessageBox.Show("Сохранение прошло успешно");
        }

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamReader reader = new StreamReader("config.ini")) // используем для чтения стримридер 
                {
                    int rows = Convert.ToInt32(reader.ReadLine()); 
                    int columns = Convert.ToInt32(reader.ReadLine());

                    ExtensionMatrix.Fill(matr, rows, columns);

                    Size.Text = $"{rows} x {columns}";
                }

                Original_Matr.ItemsSource = matr.ToDataTable().DefaultView; // в исходную матрицу записываем  данные 
            }
            catch (Exception)
            {
                MessageBox.Show("Файл настроек не найден. Перейдите в 'Настройки' и задайте параметры матрицы");
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (matr.Rows != 0 && matr.Columns != 0)
            {
                array = ExtensionMatrix.Calculate(matr);
                Result_Matr.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
            }
            else
            {
                MessageBox.Show("Заполните матрицу");
            }
        }

        private void Original_Matr_BeginningEdit(object sender, DataGridBeginningEditEventArgs e) // отслежиаем адрес яйчейки
        {
            Number_cell.Text = $"{e.Row.GetIndex() + 1} x {e.Column.DisplayIndex + 1}";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // открытие окна пароля при запуске программы 
        {
            Password pas = new Password();  
            pas.Owner = this; // получение ссылки родителя 
            pas.ShowDialog(); // открываем окно пароля поверх основого окна 
        }

        private void Settings_Click(object sender, RoutedEventArgs e) 
        {
            Settings settings = new Settings(); // новое окно настроек 
            settings.Owner = this; // установка владельца окна настроек
            settings.ShowDialog(); // открываем окно настроек поверх основого окна 
        }
    }
}
