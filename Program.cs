using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using HtmlLiveEditor.Services;

namespace HtmlLiveEditor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);
            using var serviceProvider = services.BuildServiceProvider();

            var form = serviceProvider.GetRequiredService<Form1>();
            Application.Run(form);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IAppLogger, AppLogger>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IEditorBridge, EditorBridge>();
            services.AddSingleton<IPreviewBridge, PreviewBridge>();
            services.AddSingleton<ISettingsManager, SettingsManager>();

            services.AddTransient<Form1>();
        }
    }
}