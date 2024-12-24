using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudVidApp
{
    public class EBookReader : MobileDevice
    {
        private string ScreenType;

        private int batteryCapacity;
        public int BatteryCapacity
        {
            get => batteryCapacity;
            set => batteryCapacity = ValidateBatteryCapacity(value);
        }

        public EBookReader(string model = "Unknown", string manufacturer = "Unknown", double price = 0.0, int year = 2000,
                     int screenSize = 14, string screenType = "OLED", string supportedFormats = "txt, html", int batteryCapacity = 2000)
            : base(model, manufacturer, price, year)
        {
            ScreenType = screenType;
            BatteryCapacity = batteryCapacity;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Тип экрана: {ScreenType}, Емкость аккумулятора: {BatteryCapacity} mAh");
        }

        private int ValidateBatteryCapacity(int value)
        {
            if (value <= 0) throw new ArgumentException("Емкость аккумулятора должна быть положительной.");
            return value;
        }
        public override string Description => $"{base.Description} ({GetType().Name})";

    }

}
