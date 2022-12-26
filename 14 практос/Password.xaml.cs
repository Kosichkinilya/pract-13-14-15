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

namespace _14_практос
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        private void Window_Activate(object sender, EventArgs e) // фокус курсора на пассфорд бокс  
        {
            Pas.Focus();
        }

        private void Enter_Click(object sender, RoutedEventArgs e) 
        {
            if (Pas.Password == "123") Close();
            else
            {
                MessageBox.Show("Пароль неверен. Повторите ввод.");
                Pas.Focus();
            }
        }

        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Close(); 
        }
    }
}
