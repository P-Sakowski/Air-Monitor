using AirMonitor.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AirMonitor
{
    public class DatabaseHelper
    {
        private static readonly SQLiteOpenFlags SQLiteFlags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex;
        private SQLiteConnection DatabaseConnection { get; set; }
        private static readonly string DatabaseName = "AirMonitor.db";
        private static readonly string DatabaseFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static readonly string DatabasePath = Path.Combine(DatabaseFolder,DatabaseName);
        public DatabaseHelper()
        {
            DatabaseConnection = new SQLiteConnection(DatabasePath, SQLiteFlags);
            DatabaseConnection.CreateTable<InstallationEntity>();
            DatabaseConnection.CreateTable<MeasurementEntity>();
            DatabaseConnection.CreateTable<MeasurementItemEntity>();
            DatabaseConnection.CreateTable<ParameterValue>();
            DatabaseConnection.CreateTable<Index>();
            DatabaseConnection.CreateTable<Standard>();
        }
        public void InsertInstallations(IEnumerable<Installation> installations)
        {
            List<InstallationEntity> installationEntities = new List<InstallationEntity>();
            foreach (Installation installation in installations)
            {
                InstallationEntity installationEntity = new InstallationEntity(installation);
                installationEntities.Add(installationEntity);
            }
            DatabaseConnection.BeginTransaction();
            DatabaseConnection.DeleteAll<InstallationEntity>();
            DatabaseConnection.InsertAll(installationEntities);
            DatabaseConnection.Commit();
        }
    }
}
