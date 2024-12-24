using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace AudVidApp
{
    public partial class MainWindow : Window
    {
        private const string DataFilePath = "devices.json";
        private List<MobileDevice> devices = new List<MobileDevice>();


        public MainWindow()
        {
            InitializeComponent();
            LoadDevices();
            UpdateDeviceList();
        }

        private void AddSmartphone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string model = smartphoneModelTextBox.Text;
                string manufacturer = smartphoneManufacturerTextBox.Text;
                int price = ParseInt(smartphonePriceTextBox.Text, "цена");
                int year = ParseInt(smartphoneYearTextBox.Text, "год");
                int screenSize = ParseInt(smartphoneScreenSizeTextBox.Text, "размер экрана");
                string resolution = smartphoneResolutionTextBox.Text;
                int cameraSize = ParseInt(smartphoneCameraSizeTextBox.Text, "размер камеры");  
                int ram = ParseInt(smartphoneRamTextBox.Text, "ОЗУ");

                var sm = new Smartphone(model, manufacturer, price, year, screenSize, resolution, cameraSize, ram);
                devices.Add(sm);
                SaveDevices();
                UpdateDeviceList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddEBookReader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string model = EBookReaderModelTextBox.Text;
                string manufacturer = EBookReaderManufacturerTextBox.Text;
                double price = ParseDouble(EBookReaderPriceTextBox.Text, "цена");
                int year = ParseInt(EBookReaderYearTextBox.Text, "год");
                int screenSize = ParseInt(EBookReaderScreenSizeTextBox.Text, "размер экрана");
                string screenType = EBookReaderScreenTypeTextBox.Text;
                string supportedFormats = EBookReaderSupportedFormatsTextBox.Text;
                int battery = ParseInt(EBookReaderBatteryTextBox.Text, "емкость аккумулятора");

                var ebook = new EBookReader(model, manufacturer, price, year, screenSize, screenType, supportedFormats, battery);
                devices.Add(ebook);
                SaveDevices();
                UpdateDeviceList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddMobileDevice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string model = MobileDeviceModelTextBox.Text;
                string manufacturer = MobileDeviceManufacturerTextBox.Text;
                double price = ParseDouble(MobileDevicePriceTextBox.Text, "цена");
                int year = ParseInt(MobileDeviceYearTextBox.Text, "год");

                var md = new MobileDevice(model, manufacturer, price, year);
                devices.Add(md);
                SaveDevices();
                UpdateDeviceList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            if (DevicesListBox.SelectedItem is MobileDevice selectedDevice)
            {
                devices.Remove(selectedDevice);
                SaveDevices();
                UpdateDeviceList();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите устройство для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateDeviceList()
        {
            DevicesListBox.ItemsSource = null;
            DevicesListBox.ItemsSource = devices;
        }

        private void SaveDevices()
        {
            string json = JsonSerializer.Serialize(devices, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(DataFilePath, json);
        }

        private void LoadDevices()
        {
            if (File.Exists(DataFilePath))
            {
                string json = File.ReadAllText(DataFilePath);
                devices = JsonSerializer.Deserialize<List<MobileDevice>>(json) ?? new List<MobileDevice>();
            }
        }

        private double ParseDouble(string input, string fieldName)
        {
            if (!double.TryParse(input, out double result))
                throw new FormatException($"Поле '{fieldName}' должно быть числом.");
            return result;
        }

        private int ParseInt(string input, string fieldName)
        {
            if (!int.TryParse(input, out int result))
                throw new FormatException($"Поле '{fieldName}' должно быть целым числом.");
            return result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
