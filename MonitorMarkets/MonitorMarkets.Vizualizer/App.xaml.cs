﻿using System;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace MonitorMarkets.Vizualizer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        [DllImport("user32", CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string cls, string win);
        [DllImport("user32")]
        static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32")]
        static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32")]
        static extern bool OpenIcon(IntPtr hWnd);

        protected override void OnStartup(StartupEventArgs e)
        {
            bool isNew;
            var mutex = new Mutex(true, "My Singleton Instance", out isNew);
            if (!isNew)
            {
                ActivateOtherWindow();
                Shutdown();
            }
        }
        private static void ActivateOtherWindow()
        {
            var other = FindWindow(null, "MainWindow");
            if (other != IntPtr.Zero)
            {
                SetForegroundWindow(other);
                if (IsIconic(other))
                    OpenIcon(other);
            }
        }
    }
}