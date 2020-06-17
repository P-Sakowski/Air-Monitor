using AirMonitor.Models;
using Newtonsoft.Json.Linq;
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
        public List<Measurement> SelectMeasurements()
        {
            List<MeasurementEntity> measurementEntities = DatabaseConnection.Table<MeasurementEntity>().ToList();
            List<Measurement> measurements = new List<Measurement>();
            foreach (MeasurementEntity measurementEntity in measurementEntities)
            {
                InstallationEntity installationEntity = DatabaseConnection.Get<InstallationEntity>(measurementEntity.InstallationID);
                Installation installation = new Installation(installationEntity);
                MeasurementItemEntity measurementItemEntity = DatabaseConnection.Get<MeasurementItemEntity>(measurementEntity.CurrentID);

                var valuesArray = JArray.Parse(measurementItemEntity.Values);
                var indexesArray = JArray.Parse(measurementItemEntity.Indexes);
                var standardsArray = JArray.Parse(measurementItemEntity.Standards);

                var valueIDs = ParseJSONToIntArray(valuesArray, "Id");
                var indexIDs = ParseJSONToIntArray(indexesArray, "Id");
                var standardIDs = ParseJSONToIntArray(standardsArray, "Id");

                var measurementValues = DatabaseConnection.Table<ParameterValue>().Where(x => valueIDs.Contains(x.Id)).ToList();
                var measurementIndexes = DatabaseConnection.Table<Index>().Where(x => indexIDs.Contains(x.Id)).ToList();
                var measurementStandards = DatabaseConnection.Table<Standard>().Where(x => standardIDs.Contains(x.Id)).ToList();

                MeasurementItem measurementItem = new MeasurementItem(measurementItemEntity, measurementValues, measurementIndexes, measurementStandards);
                Measurement measurement = new Measurement(measurementEntity, installation, measurementItem);
                measurements.Add(measurement);
            }
            return measurements;
        }
        public int[] ParseJSONToIntArray(JArray JSONArray, string key)
        {
            JObject JObject;
            int[] arr = new int[JSONArray.Count];
            for (int i = 0; i < JSONArray.Count; i++)
            {
                JObject = JObject.Parse(JSONArray[i].ToString());
                arr[i] = (int)JObject[key];
            }
            return arr;
        }
        public bool CheckDataTimeliness()
        {
            DateTime UTCDateTime = DateTime.Now.AddHours(-2);

            List<Measurement> measurements = SelectMeasurements();

            if (measurements.Count == 0 || measurements.Any(measurement => measurement.Current.TillDateTime < UTCDateTime))
            {
                return false;
            }
            else
            {
                return true;
            } 
        }
    }
}