using AirMonitor.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            DatabaseConnection.RunInTransaction(() =>
            {
                DatabaseConnection.DeleteAll<InstallationEntity>();
                DatabaseConnection.InsertAll(installationEntities);
            });
        }
        public void InsertMeasurements(IEnumerable<Measurement> measurements)
        {
            DatabaseConnection.RunInTransaction(() =>
            {
                DatabaseConnection.DeleteAll<MeasurementEntity>();
                DatabaseConnection.DeleteAll<MeasurementItemEntity>();
                DatabaseConnection.DeleteAll<ParameterValue>();
                DatabaseConnection.DeleteAll<Index>();
                DatabaseConnection.DeleteAll<Standard>();
                foreach (Measurement measurement in measurements)
                {
                    DatabaseConnection.InsertAll(measurement.Current.Values, false);
                    DatabaseConnection.InsertAll(measurement.Current.Indexes, false);
                    DatabaseConnection.InsertAll(measurement.Current.Standards, false);

                    MeasurementItemEntity measurementItemEntity = new MeasurementItemEntity(measurement);
                    DatabaseConnection.Insert(measurementItemEntity);
                    MeasurementEntity measurementEntity = new MeasurementEntity(measurement, measurementItemEntity);
                    DatabaseConnection.Insert(measurementEntity);
                }
            });
        }
        public List<Installation> SelectInstallations()
        {
            List<InstallationEntity> installationsEntities = DatabaseConnection.Table<InstallationEntity>().ToList();
            List<Installation> installations = new List<Installation>();
            foreach (InstallationEntity installationEntity in installationsEntities)
            {
                Installation installation = new Installation(installationEntity);
                installations.Add(installation);
            }
            return installations;
        }
    }
}
