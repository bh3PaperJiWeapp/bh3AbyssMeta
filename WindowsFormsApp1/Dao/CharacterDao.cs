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
    public class CharacterDao
    {
        public static string DB_ID = "id";
        public static string DB_NAME = "name";
        public static string DB_FILE_PATH = "file_path";
        public static string DB_WEAPON_CATEGORY_ID = "weapon_category_id";


        private static CharacterDao _characterDao;

        private CharacterDao() { }

        public static CharacterDao GetInstance()
        {
            if (_characterDao == null) _characterDao = new CharacterDao();
            return _characterDao;
        }

        public Character GetCharacterById(int id)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "SELECT * FROM " + CommonConstant.TABLE_NAME_CHARACTER + " WHERE " + DB_ID + "=" + id;
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, dbConn.sqLiteConnection);
            SQLiteDataReader dataReader = sqLiteCommand.ExecuteReader();
            if (!dataReader.Read())
            {
                return null;
            }

            return ConvertToVo(dataReader);
        }

        public List<Character> GetCharacterList()
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "SELECT * FROM " + CommonConstant.TABLE_NAME_CHARACTER;
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, dbConn.sqLiteConnection);
            SQLiteDataReader dataReader = sqLiteCommand.ExecuteReader();

            List<Character> list = new List<Character>();
            while (dataReader.Read())
            {
                list.Add(ConvertToVo(dataReader));
            }

            return list;
        }

        public Character GetCharacterByName(string name)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "SELECT * FROM " + CommonConstant.TABLE_NAME_CHARACTER + " WHERE " + DB_NAME + "=='" + name + "'";
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, dbConn.sqLiteConnection);
            SQLiteDataReader dataReader = sqLiteCommand.ExecuteReader();

            if (!dataReader.Read())
            {
                return null;
            }

            return ConvertToVo(dataReader);
        }

        public int Insert(Character vo)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "INSERT INTO " + CommonConstant.TABLE_NAME_CHARACTER + "(" + DB_NAME + ", " + DB_FILE_PATH + ", " + DB_WEAPON_CATEGORY_ID + ")" +
                         " VALUES('" + vo.name + "', '" + vo.filePath + "', " + vo.weaponCategortId.GetHashCode() + ")";

            return dbConn.InsertOrUpdate(sql);
        }

        public int Update(Character vo)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "UPDATE " + CommonConstant.TABLE_NAME_CHARACTER + " SET " + DB_NAME + "='" + vo.name + "', " + DB_FILE_PATH + "='" +
                         vo.filePath + "', " + DB_WEAPON_CATEGORY_ID + "=" + vo.weaponCategortId.GetHashCode() + " WHERE " + DB_ID + "=" + vo.id;
            return dbConn.InsertOrUpdate(sql);
        }


        public Character ConvertToVo(SQLiteDataReader dataReader)
        {
            Character character = new Character();
            character.id = Convert.ToInt32(dataReader[DB_ID]);
            character.name = dataReader[DB_NAME].ToString();
            character.filePath = dataReader[DB_FILE_PATH].ToString();
            character.weaponCategortId = (CommonConstant.WeaponType)dataReader[DB_WEAPON_CATEGORY_ID].GetHashCode();
            return character;
        }

    }
}
