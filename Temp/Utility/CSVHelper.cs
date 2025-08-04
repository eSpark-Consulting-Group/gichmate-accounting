using Newtonsoft.Json;
using NLog;
using Sylvan.Data.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FlexMold.Utility
{
    public static class CSVHelper
    {
        public static List<float> ReadCSV_DoubleData(FileInfo filename)
        {
            Logger log = LogManager.GetLogger("CSVHelper");
            var data = new List<float>();
            if (filename != null)
            {
                if (filename.FullName.EndsWith(".csv"))
                {
                    log.Log(LogLevel.Info, $"Reading CSV File {filename.Name}");
                    try
                    {
                        using (var reader = new StreamReader(filename.FullName))
                        {
                            var options = new CsvDataReaderOptions
                            {
                                HasHeaders = false,
                                BufferSize = 0x10000,
                            };
                            var csvr = CsvDataReader.Create(reader, options);

                            while (csvr.Read())
                            {
                                for (int i = 0; i < csvr.FieldCount; i++)
                                {
                                    data.Add(csvr.GetFloat(i));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Log(LogLevel.Error, ex.Message);
                    }
                }
                else
                    log.Log(LogLevel.Warn, $"Selected File Not a .csv File {filename.Name}");
            }
            else
                log.Log(LogLevel.Warn, $"Please Select a valid File");
            log.Info($"CSV data : {JsonConvert.SerializeObject(data)}");
            return data;
        }

        public static string GetCurrentDirectoryPath()
        {
            string filePath = new Uri(Assembly.GetExecutingAssembly().Location).LocalPath;
            return Path.GetDirectoryName(filePath);
        }
    }
}
