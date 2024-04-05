using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ParentalComputerControl
{
    class Program
    {
        static void TimeIntervalControl()
        {
            try
            {
                Console.Clear();
                HideConsoleWindow();
                TimeSpan startHour = Properties.Settings.Default.StartHour;
                TimeSpan endHour = Properties.Settings.Default.EndHour;

                while (true)
                {
                    TimeSpan currentTime = DateTime.Now.TimeOfDay;
                    if (!(currentTime >= startHour && currentTime <= endHour))
                    {
                        Console.WriteLine("Computer usage is not allowed during the specified hours!");
                        //Process.Start("shutdown", "/s /t 0");
                        return;
                    }
                    Thread.Sleep(60000);
                }
            }
            catch (Exception ex)
            {
                ShowConsoleWindow();
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ReadKey();
            }
        }
        static void TotalWorkingTimeControl()
        {
            try
            {
                Console.Clear();
                HideConsoleWindow();
                int totalWorkingHours = Properties.Settings.Default.TotalWorkingHours;
                int totalWorkingTime = totalWorkingHours * 60 * 60 * 1000;
                int remainingSeconds = Properties.Settings.Default.RemainingSeconds;
                DateTime startDate = Properties.Settings.Default.StartDate;

                if (startDate.Date == DateTime.Today)
                {
                    if (remainingSeconds <= 0)
                    {
                        Console.WriteLine("The designated time is up, computer is shutting down...");
                        //Process.Start("shutdown", "/s /t 0");
                        return;
                    }
                }
                else
                {
                    remainingSeconds = totalWorkingTime / 1000;
                    startDate = DateTime.Today;
                    Properties.Settings.Default.RemainingSeconds = remainingSeconds;
                    Properties.Settings.Default.StartDate = startDate;
                    Properties.Settings.Default.Save();
                }
                DateTime startTime = DateTime.Now;
                DateTime endTime = startTime.AddMilliseconds(totalWorkingTime);
                while (DateTime.Now < endTime)
                {
                    //Console.WriteLine("Computer is working...");
                    remainingSeconds = (int)(endTime - DateTime.Now).TotalSeconds;
                    Properties.Settings.Default.RemainingSeconds = remainingSeconds;
                    Properties.Settings.Default.Save();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("The designated time is up, computer is shutting down...");
                //Process.Start("shutdown", "/s /t 0");
            }
            catch (Exception ex)
            {
                ShowConsoleWindow();
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ReadKey();
            }
        }
        static void Main(string[] args)
        {
            InitializeSettings();
            RunProgram();
        }

        static void InitializeSettings()
        {
            string jsonFilePath = "settings.json";
            string password = Properties.Settings.Default.Password;

            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                Settings settings = JsonConvert.DeserializeObject<Settings>(json);

                if (settings.Password == password)
                {
                    UpdateSettings(settings);
                    settings.Password = "";
                    string newJson = JsonConvert.SerializeObject(settings, Formatting.Indented);
                    File.WriteAllText(jsonFilePath, newJson);
                }
                else
                {
                    Settings oldSettings = new Settings
                    {
                        StartHour = Properties.Settings.Default.StartHour.ToString(),
                        EndHour = Properties.Settings.Default.EndHour.ToString(),
                        TotalWorkingHours = Properties.Settings.Default.TotalWorkingHours,
                        Password = ""
                    };
                    string oldJson = JsonConvert.SerializeObject(oldSettings, Formatting.Indented);
                    File.WriteAllText(jsonFilePath, oldJson);
                    UpdateSettings(settings);
                }
            }
            else
            {
                var settings = new Settings
                {
                    StartHour = "12:00:00",
                    EndHour = "16:30:00",
                    TotalWorkingHours = 5,
                    Password = ""
                };

                string newJson = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(jsonFilePath, newJson);
                UpdateSettings(settings);
            }
        }

        static void UpdateSettings(Settings settings)
        {
            Properties.Settings.Default.StartHour = TimeSpan.Parse(settings.StartHour);
            Properties.Settings.Default.EndHour = TimeSpan.Parse(settings.EndHour);
            Properties.Settings.Default.TotalWorkingHours = settings.TotalWorkingHours;
            Properties.Settings.Default.Save();
        }
        class Settings
        {
            public string StartHour { get; set; }
            public string EndHour { get; set; }
            public int TotalWorkingHours { get; set; }
            public string Password { get; set; }
        }
        static void RunProgram()
        {
            try
            {
                ShowConsoleWindow();
                Properties.Settings.Default.Reset();
                Console.Clear();
                string choice = Properties.Settings.Default.UserChoice;
                string password = Properties.Settings.Default.Password;

                if (string.IsNullOrEmpty(password))
                {
                    Console.Write("Please set a password to proceed: ");
                    password = Console.ReadLine();
                    Console.WriteLine("Password set successfully!");
                    Properties.Settings.Default.Password = password;
                    Properties.Settings.Default.Save();
                }

                DateTime startTime = DateTime.Now;
                DateTime endTime = startTime.AddSeconds(1);
                Console.WriteLine("To change the selection, the password must be entered within 1 seconds.");
                Console.Write("Enter password to confirm: ");
                StringBuilder inputPasswordBuilder = new StringBuilder();
                ConsoleKeyInfo keyInfo;
                while (DateTime.Now <= endTime)
                {
                    if (Console.KeyAvailable)
                    {
                        keyInfo = Console.ReadKey(true);

                        if (keyInfo.Key == ConsoleKey.Enter)
                            break;
                        
                        if (keyInfo.Key == ConsoleKey.Backspace && inputPasswordBuilder.Length > 0)
                        {
                            inputPasswordBuilder.Remove(inputPasswordBuilder.Length - 1, 1);
                            Console.Write("\b \b");
                        }
                        else if (!char.IsControl(keyInfo.KeyChar))
                        {
                            inputPasswordBuilder.Append(keyInfo.KeyChar);
                            Console.Write("*");
                        }
                    }
                }
                string inputPassword = inputPasswordBuilder.ToString();
                if (inputPassword == password)
                {
                    Console.Clear();
                    Console.WriteLine("Select an option:");
                    Console.WriteLine("1. TotalWorkingTimeControl");
                    Console.WriteLine("2. TimeIntervalControl");
                    Console.Write("Enter your choice: ");
                    choice = Console.ReadLine();
                    Properties.Settings.Default.UserChoice = choice;
                    Properties.Settings.Default.Save();

                    switch (choice)
                    {
                        case "1":
                            TotalWorkingTimeControl();
                            break;
                        case "2":
                            TimeIntervalControl();
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Press any key to try again.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nIncorrect password!");
                }
                if (DateTime.Now >= endTime)
                {
                    Console.WriteLine("Time limit exceeded. Exiting...");
                }
                switch (choice)
                {
                    case "1":
                        TotalWorkingTimeControl();
                        break;
                    case "2":
                        TimeIntervalControl();
                        break;
                    default:
                        Console.WriteLine("Select an option:");
                        Console.WriteLine("1. TotalWorkingTimeControl");
                        Console.WriteLine("2. TimeIntervalControl");
                        Console.Write("Enter your choice: ");

                        choice = Console.ReadLine();
                        Properties.Settings.Default.UserChoice = choice;
                        Properties.Settings.Default.Save();

                        switch (choice)
                        {
                            case "1":
                                TotalWorkingTimeControl();
                                break;
                            case "2":
                                TimeIntervalControl();
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Press any key to try again.");
                                Console.ReadKey();
                                break;
                        }
                        break;
                }
                Console.ReadKey();
                //HideConsoleWindow(); -- Gizleme
                //ShowConsoleWindow(); -- Gösterme
            }
            catch (Exception ex)
            {
                ShowConsoleWindow();
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ReadKey();
            }
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        static void ShowConsoleWindow()
        {
            IntPtr hWnd = GetConsoleWindow();
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 1);
            }
        }
        static void HideConsoleWindow()
        {
            IntPtr hWnd = GetConsoleWindow();
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 0);
            }
        }
    }
}
    