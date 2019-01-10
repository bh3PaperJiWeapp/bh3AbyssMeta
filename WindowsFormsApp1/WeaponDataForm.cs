
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Dao;
using WindowsFormsApp1.Entity;
using WindowsFormsApp1.SQLite;

namespace WindowsFormsApp1
{
    public partial class WeaponDataForm : Form
    {
        public WeaponDataForm()
        {
            InitializeComponent();
        }

        private string _folderPathStr;
        private DBConnection _dbConnection;
        private WeaponDao _weaponDao;

        private void WeaponDataForm_Load(object sender, EventArgs e)
        {
            _dbConnection = DBConnection.GetInstance();

            _folderPathStr = ConfigUtil.GetConfigContent(CommonConstant.FOLDER_KEY_WEAPON);
            if (!string.IsNullOrEmpty(_folderPathStr)) weaponFolderPathTextBox.Text = _folderPathStr;

            _weaponDao = WeaponDao.GetInstance();

            LoadWeaponData();

        }

        private void selectWeaponFolderbutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_folderPathStr))
            {
                folderBrowserDialog1.SelectedPath = _folderPathStr;
            }

            folderBrowserDialog1.ShowDialog();

            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath)) return;

            _folderPathStr = folderBrowserDialog1.SelectedPath;
            weaponFolderPathTextBox.Text = _folderPathStr;

            ConfigUtil.SetConfigContent(CommonConstant.FOLDER_KEY_WEAPON, _folderPathStr);
        }

        private void refreshWeaponDatabutton_Click(object sender, EventArgs e)
        {
            UpdateWeaponData();
        }

        private void UpdateWeaponData()
        {
            DirectoryInfo root = new DirectoryInfo(_folderPathStr);
            var images = root.GetFiles("*.png", SearchOption.AllDirectories);

            if (images == null || images.Length == 0)
            {
                MessageBox.Show("目录下没有找到图片文件");
                return;
            }

            _dbConnection.ClearTable(CommonConstant.TABLE_NAME_WEAPON);

            foreach (FileInfo image in images)
            {
                Weapon weapon = new Weapon();
                string name = Path.GetFileNameWithoutExtension(image.FullName);
                string weaponCategortStr = image.Directory.Name;
                CommonConstant.WeaponType weaponType;
                Enum.TryParse(weaponCategortStr, out weaponType);

                weapon.name = name;
                weapon.categortId = weaponType;
                weapon.filePath = image.FullName;

                _weaponDao.Insert(weapon);
            }

            LoadWeaponData();

        }

        private void LoadWeaponData()
        {
            var list = _weaponDao.GetWeaponList();
            dataGridView2.DataSource = list;
        }
    }
}
