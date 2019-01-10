using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Dao;

namespace WindowsFormsApp1.SQLite
{
    public class DBConnection
    {
        public SQLiteConnection sqLiteConnection;
        private static DBConnection _dbConnection;
        
        private DBConnection() { }

        public static DBConnection GetInstance()
        {
            if (_dbConnection == null) _dbConnection = new DBConnection();
            _dbConnection.InitSqliteDb();
            return _dbConnection;
        }

        private void InitSqliteDb()
        {
            CreateNewDb();
            ConnectToDb();

            if (!CheckTableExist(CommonConstant.TABLE_NAME_CHARACTER)) CreateCharacterTable();
            if (!CheckTableExist(CommonConstant.TABLE_NAME_WEAPON)) CreateWeaponTable();
            if (!CheckTableExist(CommonConstant.TABLE_NAME_STIGMATA)) CreateStigmataTable();
        }


        /// <summary>
        /// 创建数据库文件
        /// </summary>
        private void CreateNewDb()
        {
            if (File.Exists("AbyssMeta.sqlite")) return;

            SQLiteConnection.CreateFile("AbyssMeta.sqlite");
        }

        private void ConnectToDb()
        {
            if (sqLiteConnection != null) return;

            sqLiteConnection = new SQLiteConnection("Data Source=AbyssMeta.sqlite;Version=3;");
            sqLiteConnection.Open();
        }

        public void CloseConnectDb()
        {
            if (sqLiteConnection != null) sqLiteConnection.Close();
        }

        private bool CheckTableExist(string tableName)
        {
            string sql = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' and name='" + tableName + "'";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, sqLiteConnection);
            if (0 == Convert.ToInt32(sqLiteCommand.ExecuteScalar()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CreateCharacterTable()
        {
            string sql = "CREATE TABLE 'main'.'" + CommonConstant.TABLE_NAME_CHARACTER + "' " +
                         "('" + CharacterDao.DB_ID + "' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                         " '" + CharacterDao.DB_NAME + "' TEXT NOT NULL," +
                         " '" + CharacterDao.DB_FILE_PATH + "' TEXT NOT NULL, " +
                         " '" + CharacterDao.DB_WEAPON_CATEGORY_ID + "' INTEGER NOT NULL); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();

        }


        private void CreateWeaponTable()
        {
            string sql = "CREATE TABLE 'main'.'" + CommonConstant.TABLE_NAME_WEAPON + "' " +
                         "('id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                         " 'name' TEXT NOT NULL," +
                         " 'file_path' TEXT NOT NULL, " +
                         " 'category_id' INTEGER NOT NULL); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();

        }


        private void CreateStigmataTable()
        {
            string sql = "CREATE TABLE 'main'.'" + CommonConstant.TABLE_NAME_STIGMATA + "' " +
                         "('id' INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT," +
                         " 'name' TEXT NOT NULL," +
                         " 'file_path' TEXT NOT NULL" +
                         " ); ";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, sqLiteConnection);
            sqLiteCommand.ExecuteNonQuery();

        }

        public int ClearTable(string tableName)
        {
            string sql = "DELETE FROM " + tableName;
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, sqLiteConnection);
            return sqLiteCommand.ExecuteNonQuery();
        }

        public int InsertOrUpdate(string sql)
        {
            if (string.IsNullOrEmpty(sql)) return -1;
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, sqLiteConnection);
            return sqLiteCommand.ExecuteNonQuery();
        }

     
    }
}
