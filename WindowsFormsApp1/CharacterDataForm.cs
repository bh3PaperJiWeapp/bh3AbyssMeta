using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
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
    public partial class CharacterDataForm : Form
    {
        public CharacterDataForm()
        {
            InitializeComponent();
        }

        private string _folderPathStr;
        private DBConnection _dbConnection;
        private CharacterDao _characterDao;

        private void CharacterDataForm_Load(object sender, EventArgs e)
        {
            _dbConnection = DBConnection.GetInstance();

            _folderPathStr = ConfigUtil.GetConfigContent(CommonConstant.FOLDER_KEY_CHARACTER);
            if (!string.IsNullOrEmpty(_folderPathStr)) characterFolderPathTextBox.Text = _folderPathStr;

            _characterDao = CharacterDao.GetInstance();

            LoadCharacterData();
        }

        private void selectCharacterFolderButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_folderPathStr))
            {
                folderBrowserDialog1.SelectedPath = _folderPathStr;
            }

            folderBrowserDialog1.ShowDialog();

            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath)) return;

            _folderPathStr = folderBrowserDialog1.SelectedPath;
            characterFolderPathTextBox.Text = _folderPathStr;

            ConfigUtil.SetConfigContent(CommonConstant.FOLDER_KEY_CHARACTER, _folderPathStr);

            var list = _characterDao.GetCharacterList();
            if (list == null || list.Count == 0) UpdateCharacterData();
        }

        private void refreshCharacterDatabutton_Click(object sender, EventArgs e)
        {
            UpdateCharacterData();
        }

        private void UpdateCharacterData()
        {
            DirectoryInfo root = new DirectoryInfo(_folderPathStr);
            var images = root.GetFiles("*.png", SearchOption.AllDirectories);

            if (images == null || images.Length == 0)
            {
                MessageBox.Show("目录下没有找到图片文件");
                return;
            }

            _dbConnection.ClearTable(CommonConstant.TABLE_NAME_CHARACTER);

            foreach (FileInfo image in images)
            {
                Character character = new Character();
                string name = Path.GetFileNameWithoutExtension(image.FullName);
                string weaponCategortStr = image.Directory.Name;
                CommonConstant.WeaponType weaponType;
                Enum.TryParse(weaponCategortStr, out weaponType);

                character.name = name;
                character.weaponCategortId = weaponType;
                character.filePath = image.FullName;

                _characterDao.Insert(character);

                //var oldData = _characterDao.GetCharacterByName(name);
                //if (oldData == null) _characterDao.Insert(character);
                //else
                //{
                //    character.id = oldData.id;
                //    _characterDao.Update(character);
                //}
            }

            LoadCharacterData();
        }

        private void LoadCharacterData()
        {
            var list = _characterDao.GetCharacterList();
            dataGridView1.DataSource = list;
        }
    }
}
