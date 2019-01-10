using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Entity;
using WindowsFormsApp1.SQLite;

namespace WindowsFormsApp1.Dao
{
    public class StigmataDao
    {

        public static string DB_ID = "id";
        public static string DB_NAME = "name";
        public static string DB_FILE_PATH = "file_path";


        private static StigmataDao _stigmataDao;

        private StigmataDao() { }

        public static StigmataDao GetInstance()
        {
            if (_stigmataDao == null) _stigmataDao = new StigmataDao();
            return _stigmataDao;
        }

        public Stigmata GetStigmataById(int id)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "SELECT * FROM " + CommonConstant.TABLE_NAME_STIGMATA + " WHERE " + DB_ID + "=" + id;
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, dbConn.sqLiteConnection);
            SQLiteDataReader dataReader = sqLiteCommand.ExecuteReader();
            if (!dataReader.Read())
            {
                return null;
            }

            return ConvertToVo(dataReader);
        }

        public List<Stigmata> GetStigmataList()
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "SELECT * FROM " + CommonConstant.TABLE_NAME_STIGMATA;
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, dbConn.sqLiteConnection);
            SQLiteDataReader dataReader = sqLiteCommand.ExecuteReader();

            List<Stigmata> list = new List<Stigmata>();
            while (dataReader.Read())
            {
                list.Add(ConvertToVo(dataReader));
            }

            return list;
        }

        public int Insert(Stigmata vo)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "INSERT INTO " + CommonConstant.TABLE_NAME_STIGMATA + "(" + DB_NAME + ", " +
                         DB_FILE_PATH + ")"
                         + "VALUES('" + vo.name + "', '" + vo.filePath + "')";
            return dbConn.InsertOrUpdate(sql);
        }

        public int Update(Stigmata vo)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "UPDATE " + CommonConstant.TABLE_NAME_STIGMATA + " SET " + DB_NAME + "='" + vo.name +
                         "', "
                         + DB_FILE_PATH + "='" + vo.filePath + "' WHERE " + DB_ID + "=" + vo.id;
            return dbConn.InsertOrUpdate(sql);
        }

        public Stigmata ConvertToVo(SQLiteDataReader dataReader)
        {
            Stigmata vo = new Stigmata();
            vo.id = Convert.ToInt32(dataReader[DB_ID]);
            vo.name = dataReader[DB_NAME].ToString();
            vo.filePath = dataReader[DB_FILE_PATH].ToString();
            return vo;
        }

    }
}
