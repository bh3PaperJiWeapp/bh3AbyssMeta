
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
    public partial class StigmataDataForm : Form
    {
        public StigmataDataForm()
        {
            InitializeComponent();
        }

        private string _folderPathStr;
        private DBConnection _dbConnection;
        private StigmataDao _stigmataDao;

        private void StigmataDataForm_Load(object sender, EventArgs e)
        {
            _dbConnection = DBConnection.GetInstance();

            _folderPathStr = ConfigUtil.GetConfigContent(CommonConstant.FOLDER_KEY_STIGMATA);
            if (!string.IsNullOrEmpty(_folderPathStr)) stigmataFolderPathTextBox.Text = _folderPathStr;

            _stigmataDao = StigmataDao.GetInstance();

            LoadStigmataData();
        }

        private void selectStigmatabutton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_folderPathStr))
            {
                folderBrowserDialog1.SelectedPath = _folderPathStr;
            }

            folderBrowserDialog1.ShowDialog();

            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath)) return;

            _folderPathStr = folderBrowserDialog1.SelectedPath;
            stigmataFolderPathTextBox.Text = _folderPathStr;

            ConfigUtil.SetConfigContent(CommonConstant.FOLDER_KEY_STIGMATA, _folderPathStr);
        }

        private void refreshStigmataDatabutton_Click(object sender, EventArgs e)
        {
            UpdateStigmateDate();
        }

        private void UpdateStigmateDate()
        {
            DirectoryInfo root = new DirectoryInfo(_folderPathStr);
            var images = root.GetFiles("*.png", SearchOption.AllDirectories);

            if (images == null || images.Length == 0)
            {
                MessageBox.Show("目录下没有找到图片文件");
                return;
            }

            _dbConnection.ClearTable(CommonConstant.TABLE_NAME_STIGMATA);

            foreach (FileInfo image in images)
            {
                Stigmata stigmata = new Stigmata();
                string name = Path.GetFileNameWithoutExtension(image.FullName);
                string stigmataCategoryStr = image.Directory.Name;
                CommonConstant.StigmataType stigmataType;
                Enum.TryParse(stigmataCategoryStr, out stigmataType);

                stigmata.name = name;
                stigmata.categoryId = stigmataType;
                stigmata.filePath = image.FullName;

                _stigmataDao.Insert(stigmata);
            }

            LoadStigmataData();
        }

        private void LoadStigmataData()
        {
            var list = _stigmataDao.GetStigmataList();
            dataGridView3.DataSource = list;
        }

    }
}
