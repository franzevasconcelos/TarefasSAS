using System;
using System.IO;
using System.Windows.Forms;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Migrador {
    public partial class Migrador : Form {
        private static string _caminhoBanco;

        public Migrador() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(_caminhoBanco) || !File.Exists(_caminhoBanco)){
                MessageBox.Show(@"Por favor selecione um arquivo");
                return;
            }

            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope()) {
                UpdateDatabase(scope.ServiceProvider);
            }

            MessageBox.Show(@"Migração concluida");
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </sumamry>
        private static IServiceProvider CreateServices() {
            return new ServiceCollection()
                   // Add common FluentMigrator services
                   .AddFluentMigratorCore()
                   .ConfigureRunner(rb => rb
                                          // Add SQLite support to FluentMigrator
                                          .AddSQLite()
                                          // Set the connection string
                                          .WithGlobalConnectionString($"Data Source={_caminhoBanco}")
                                          // Define the assembly containing the migrations
                                          .ScanIn(typeof(Migration001).Assembly)
                                          .For.Migrations())
                   // Enable logging to console in the FluentMigrator way
                   .AddLogging(lb => lb.AddFluentMigratorConsole())
                   // Build the service provider
                   .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </sumamry>
        private static void UpdateDatabase(IServiceProvider serviceProvider) {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = @"Todos os Arquivos | *.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtLocalBanco.Text = fileDialog.FileName;
                _caminhoBanco = fileDialog.FileName;
            }
        }
    }
}