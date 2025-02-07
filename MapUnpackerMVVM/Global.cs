using Avalonia.Threading;
using MapUnpackerMVVM.Views;
using System;

namespace MapUnpacker
{
    static class Global
    {
        public static bool SkipMissingGeometry = true;
        public static bool DefaultExportAll = true;
        public static bool DefaultExportRegMap = true;
        public static bool DefaultExportGeometry = true;
        public static bool DefaultExportJson = true;
        public static bool DefaultExportObj = true;
        public static bool DefaultExportPlaintext = true;

        public static string settingsFilePath = "settings.json";

        public static MainWindow MainWindowInstance;


        public static void PrintLine(string text)
        {
            if (MainWindowInstance?.LogTextBlock != null)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    MainWindowInstance.LogTextBlock.Text += text + Environment.NewLine;
                    MainWindowInstance.LogScrollViewer.ScrollToEnd();
                });
            }
        }

        public static void Print(string text)
        {
            if (MainWindowInstance?.LogTextBlock != null)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    MainWindowInstance.LogTextBlock.Text += text;
                    MainWindowInstance.LogScrollViewer.ScrollToEnd();
                });
            }
        }

        public static void PrintReplace(string text)
        {
            if (MainWindowInstance?.LogTextBlock != null)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    var log = MainWindowInstance.LogTextBlock.Text;

                    if (!string.IsNullOrEmpty(log))
                    {
                        var lines = log.Split(Environment.NewLine);
                        if (lines.Length > 1)
                        {
                            // Replace last line
                            lines[lines.Length - 2] = text;
                            MainWindowInstance.LogTextBlock.Text = string.Join(Environment.NewLine, lines);
                        }
                        else
                        {
                            // If only one line exists, just overwrite it
                            MainWindowInstance.LogTextBlock.Text = text + Environment.NewLine;
                        }
                    }
                    else
                    {
                        // If log is empty, just print normally
                        MainWindowInstance.LogTextBlock.Text = text + Environment.NewLine;
                    }

                    MainWindowInstance.LogScrollViewer.ScrollToEnd();
                });
            }
        }

    }
}
