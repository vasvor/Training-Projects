using Microsoft.Win32;
using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Windows.Xps.Packaging;
using Path = System.IO.Path;
using MoonPdfLib;
using System.Runtime.ExceptionServices;

namespace Reader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool _isLoaded = false;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Сохранение файла.
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";
            
            if(saveFile.ShowDialog() == true)
            {
                string fileName = saveFile.FileName;
                File.WriteAllText(fileName, saveFile.FileName);
            }
        }

        //Открытие файла.
        [HandleProcessCorruptedStateExceptions] //Если убрать атрибут появится ошибка AccessViolationException. (Нехватка памяти.)
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*";

            if (openFile.ShowDialog().GetValueOrDefault())
            {
                string filePath = openFile.FileName;

                try
                {
                    moonPdfPaanel.OpenFile(filePath);
                    _isLoaded = true;
                }
                catch (Exception)
                {
                    _isLoaded = false;
                }

            }
        }

        //Увеличение масштаба страницы.
        private void ZoomUp_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded) moonPdfPaanel.ZoomIn();

        }
        //Уменьшение масштаба страницы.
        private void ZoomDown_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded) moonPdfPaanel.ZoomOut();

        }
        //Стандартный 100% масштаб страницы.
        private void NormalZoom_Click(object sender, RoutedEventArgs e)
        {
            if (_isLoaded) moonPdfPaanel.Zoom(1.0);

        }
        //Отображение 1й страницы.
        private void Page_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPaanel.ViewType = MoonPdfLib.ViewType.SinglePage;
        }
        //Режим книги.
        private void BookView_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPaanel.ViewType = MoonPdfLib.ViewType.BookView;
        }
        //Полноэкранное отображение файла.
        private void FullPage_Click(object sender, RoutedEventArgs e)
        {
            moonPdfPaanel.ZoomToHeight();
            moonPdfPaanel.ZoomToWidth();
        }
    }
}

