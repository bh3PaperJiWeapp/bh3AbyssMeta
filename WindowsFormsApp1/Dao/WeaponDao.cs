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
    public class WeaponDao
    {
        public static string DB_ID = "id";
        public static string DB_NAME = "name";
        public static string DB_FILE_PATH = "file_path";
        public static string DB_WEAPON_CATEGORY_ID = "category_id";

        private static WeaponDao _weaponDao;

        private WeaponDao() { }

        public static WeaponDao GetInstance()
        {
            if (_weaponDao == null) _weaponDao = new WeaponDao();
            return _weaponDao;
        }

        public Weapon GetWeaponById(int id)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "SELECT * FROM " + CommonConstant.TABLE_NAME_WEAPON + " WHERE " + DB_ID + "=" + id;
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, dbConn.sqLiteConnection);
            SQLiteDataReader dataReader = sqLiteCommand.ExecuteReader();
            if (!dataReader.Read())
            {
                return null;
            }

            return ConvertToVo(dataReader);
        }

        public List<Weapon> GetWeaponsByCategoryId(CommonConstant.WeaponType type)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "SELECT * FROM " + CommonConstant.TABLE_NAME_WEAPON + " WHERE " + DB_WEAPON_CATEGORY_ID + "=" + type.GetHashCode();
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, dbConn.sqLiteConnection);
            SQLiteDataReader dataReader = sqLiteCommand.ExecuteReader();

            List<Weapon> list = new List<Weapon>();
            while (dataReader.Read())
            {
                list.Add(ConvertToVo(dataReader));
            }

            return list;
        }

        public List<Weapon> GetWeaponList()
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "SELECT * FROM " + CommonConstant.TABLE_NAME_WEAPON;
            SQLiteCommand sqLiteCommand = new SQLiteCommand(sql, dbConn.sqLiteConnection);
            SQLiteDataReader dataReader = sqLiteCommand.ExecuteReader();

            List<Weapon> list = new List<Weapon>();
            while (dataReader.Read())
            {
                list.Add(ConvertToVo(dataReader));
            }

            return list;
        }

        public int Insert(Weapon vo)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "INSERT INTO " + CommonConstant.TABLE_NAME_WEAPON + "(" + DB_NAME + ", " + DB_FILE_PATH + ", " + DB_WEAPON_CATEGORY_ID + ")" +
                         " VALUES('" + vo.name + "', '" + vo.filePath + "', " + vo.categortId.GetHashCode() + ")";

            return dbConn.InsertOrUpdate(sql);
        }

        public int Update(Weapon vo)
        {
            var dbConn = DBConnection.GetInstance();
            string sql = "UPDATE " + CommonConstant.TABLE_NAME_WEAPON + " SET " + DB_NAME + "='" + vo.name + "', " + DB_FILE_PATH + "='" +
                         vo.filePath + "', " + DB_WEAPON_CATEGORY_ID + "=" + vo.categortId.GetHashCode() + " WHERE " + DB_ID + "=" + vo.id;
            return dbConn.InsertOrUpdate(sql);
        }

        private Weapon ConvertToVo(SQLiteDataReader dataReader)
        {
            Weapon weapon = new Weapon();
            weapon.id = Convert.ToInt32(dataReader[DB_ID]);
            weapon.name = dataReader[DB_NAME].ToString();
            weapon.filePath = dataReader[DB_FILE_PATH].ToString();
            weapon.categortId = (CommonConstant.WeaponType) dataReader[DB_WEAPON_CATEGORY_ID].GetHashCode();
            return weapon;
        }

    }
}
