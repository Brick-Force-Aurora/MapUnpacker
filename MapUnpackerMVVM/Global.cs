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

        public static MainWindow MainWindowInstance;

        public static ExportDialog ExportDialogInstance {  get; set; }


        public static void Print(string text)
        {
            if (MainWindowInstance?.LogTextBlock != null)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    MainWindowInstance.LogTextBlock.Text += text + Environment.NewLine;
                });
            }

            if (ExportDialogInstance?.LogTextBlock != null)
            {
                Dispatcher.UIThread.Post(() =>
                {
                    ExportDialogInstance.LogTextBlock.Text += text + Environment.NewLine;

                    ExportDialogInstance.LogScrollViewer.ScrollToEnd();
                });
            }
        }
    }
}
