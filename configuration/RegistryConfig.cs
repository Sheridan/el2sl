using System;
using System.Data;
using Microsoft.Win32;

namespace Bng.Configs.Registry
{
    /// <summary>
    /// Реестр.
    /// </summary>
    public class RegistryConfig
    {
        private static RegistryKey _registryKey = Microsoft.Win32.Registry.LocalMachine;
        private string _registryPath;

        public RegistryConfig(string registryPath)
        {
            _registryPath = registryPath;
        }

        #region Operations

        #region Clearing
        public void ClearConfig()
        {
            if (CategoryExists(_registryPath))
                _registryKey.DeleteSubKeyTree(_registryPath);
        }

        public void ClearCategory(string categoryName)
        {
            if (CategoryExists(categoryName))
                _registryKey.DeleteSubKeyTree(_registryPath + categoryName);
        }
        #endregion

        #region Export
        public void Export(string fileName)
        {
            System.Diagnostics.Process.Start("regedit", "/eas " + fileName + " " + RegistryPath);
        }
        #endregion

        #region Avialability
        private bool CategoryExists(string categoryName)
        {
            categoryName = _registryPath + categoryName;
            RegistryKey regKey = _registryKey.OpenSubKey(categoryName);
            if (regKey == null)
                return false;
            else
            {
                regKey.Close();
                return true;
            }
        }

        private bool PropertyExists(string categoryName, string propertyName)
        {
            if (CategoryExists(categoryName))
            {
                categoryName = _registryPath + categoryName;
                RegistryKey regKey = _registryKey.OpenSubKey(categoryName);
                bool result = regKey.GetValue(propertyName) == null;
                regKey.Close();
                return result;
            }
            return false;
        }
        #endregion

        #endregion

        #region Path
        public string RegistryPath
        {
            get
            {
                return @"HKEY_LOCAL_MACHINE\" + _registryPath;
            }
        }
        #endregion

        #region Data

        #region set and get values
        private void SetValue(string categoryName, string propertyName, object value)
        {
            categoryName = _registryPath + categoryName;
            RegistryKey regKey = _registryKey.OpenSubKey(categoryName, true);
            if (regKey == null)
                regKey = _registryKey.CreateSubKey(categoryName);
            regKey.SetValue(propertyName, value);
            regKey.Close();
        }

        private object GetValue(string categoryName, string propertyName, object Default)
        {
            categoryName = _registryPath + categoryName;
            RegistryKey regKey = _registryKey.OpenSubKey(categoryName);
            if (regKey == null)
                return Default;
            return regKey.GetValue(propertyName, Default);
        }
        #endregion

        #region string
        public void Write(string categoryName, string propertyName, System.String value)
        {
            SetValue(categoryName, propertyName, value);
        }
        public string Read(string categoryName, string propertyName, System.String Default)
        {
            return (System.String)(GetValue(categoryName, propertyName, Default));
        }
        #endregion

        #region int
        public void Write(string categoryName, string propertyName, System.Int32 value)
        {
            SetValue(categoryName, propertyName, value);
        }
        public System.Int32 Read(string categoryName, string propertyName, System.Int32 Default)
        {
            return (System.Int32)(GetValue(categoryName, propertyName, Default));
        }
        #endregion

        #region bool
        public void Write(string categoryName, string propertyName, bool value)
        {
            SetValue(categoryName, propertyName, value ? 1 : 0);
        }
        public bool Read(string categoryName, string propertyName, bool Default)
        {
            return ((int)GetValue(categoryName, propertyName, Default ? 1 : 0)) == 1;
        }
        #endregion

        #region float
        public void Write(string categoryName, string propertyName, float value)
        {
            SetValue(categoryName, propertyName, value);
        }
        public float Read(string categoryName, string propertyName, float Default)
        {
            return ((float)GetValue(categoryName, propertyName, Default));
        }
        #endregion

        #region DataSet
        public void Write(string categoryName, DataSet value)
        {
            string currentCategoryName = categoryName;
            SetValue(currentCategoryName, "__tablesCount", value.Tables.Count);
            for (int iTIndex = 0; iTIndex < value.Tables.Count; iTIndex++)
            {
                DataTable table = value.Tables[iTIndex];

                SetValue(categoryName, "__table_" + iTIndex.ToString() + "_name", table.TableName);

                currentCategoryName = categoryName + @"\" + table.TableName;
                ClearCategory(currentCategoryName); // чтобы не читало потом по новой удаленные записи
                SetValue(currentCategoryName, "__columnsCount", table.Columns.Count);
                DataColumn column;
                for (int iCIndex = 0; iCIndex < table.Columns.Count; iCIndex++)
                {
                    column = table.Columns[iCIndex];
                    SetValue(currentCategoryName, "__column_" + iCIndex.ToString() + "_ColumnName", column.ColumnName);
                    SetValue(currentCategoryName, "__column_" + iCIndex.ToString() + "_Caption", column.Caption);
                    SetValue(currentCategoryName, "__column_" + iCIndex.ToString() + "_DataType", column.DataType.FullName);
                }

                SetValue(currentCategoryName, "__rowsCount", table.Rows.Count);
                for (int iRIndex = 0; iRIndex < table.Rows.Count; iRIndex++)
                {
                    DataRow row = table.Rows[iRIndex];
                    for (int iCIndex = 0; iCIndex < table.Columns.Count; iCIndex++)
                    {
                        column = table.Columns[iCIndex];
                        switch (column.DataType.Name)
                        {
                            default:
                                SetValue(currentCategoryName, "__row_" + iRIndex + "_" + iCIndex + "_Data", row[iCIndex]);
                                break;
                        }

                    }
                }
            }
        }

        public DataSet Read(string categoryName, DataSet Default)
        {
            if (!CategoryExists(categoryName))
                return Default;

            DataSet result = new DataSet();
            string currentCategoryName = categoryName;
            int tablesCount = Read(currentCategoryName, "__tablesCount", 0);
            for (int iTIndex = 0; iTIndex < tablesCount; iTIndex++)
            {
                DataTable table = new DataTable(Read(categoryName, "__table_" + iTIndex.ToString() + "_name", "__error"));
                currentCategoryName = categoryName + @"\" + table.TableName;
                int columnsCount = Read(currentCategoryName, "__columnsCount", 0);
                for (int iCIndex = 0; iCIndex < columnsCount; iCIndex++)
                {
                    DataColumn column = new DataColumn(
                        Read(currentCategoryName, "__column_" + iCIndex.ToString() + "_ColumnName", "__error"),
                        Type.GetType(Read(currentCategoryName, "__column_" + iCIndex.ToString() + "_DataType", "System.Empty"))
                        );
                    column.Caption = Read(currentCategoryName, "__column_" + iCIndex.ToString() + "_Caption", "__error");
                    table.Columns.Add(column);
                }

                int rowsCount = Read(currentCategoryName, "__rowsCount", 0);
                for (int iRIndex = 0; iRIndex < rowsCount; iRIndex++)
                {
                    DataRow row = table.NewRow();
                    for (int iCIndex = 0; iCIndex < columnsCount; iCIndex++)
                    {
                        row[iCIndex] = GetValue(currentCategoryName, "__row_" + iRIndex + "_" + iCIndex + "_Data", null);
                    }
                    table.Rows.Add(row);
                }

                result.Tables.Add(table);
            }
            return result;
        }
        #endregion

        #endregion
    }
}