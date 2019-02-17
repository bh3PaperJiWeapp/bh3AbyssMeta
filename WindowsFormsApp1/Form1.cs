using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using WindowsFormsApp1.Common;
using WindowsFormsApp1.Dao;
using WindowsFormsApp1.Entity;
using CsvHelper;
using WaitHandle = System.Threading.WaitHandle;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private CharacterDataForm _characterDataForm;
        private WeaponDataForm _weaponDataForm;
        private StigmataDataForm _stigmataDataForm;
        private string _folderPathStr;

        private List<PictureBox> pictureBoxList = new List<PictureBox>();
        private Dictionary<int, List<Label>> labelDic = new Dictionary<int, List<Label>>();

        private double _leftBlankCoefficient = 0;
        private double _topBlankCoefficient = 0;
        private double _rightBlankCoefficient = 0;
        private double _bottomBlankCoefficient = 0;

        private CharacterDao characterDao = CharacterDao.GetInstance();
        private WeaponDao weaponDao = WeaponDao.GetInstance();
        private StigmataDao stigmataDao = StigmataDao.GetInstance();

        private List<Stigmata> stigmataTopList;
        private List<Stigmata> stigmataCenterList;
        private List<Stigmata> stigmataBottomList;


        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigUtil.CreateConfigXml();

            _folderPathStr = ConfigUtil.GetConfigContent(CommonConstant.FOLDER_KEY_ANALYZE);

            pictureBoxList.Add(pictureBox1);
            pictureBoxList.Add(pictureBox2);
            pictureBoxList.Add(pictureBox3);

            List<Label> labelList = new List<Label>();
            labelList.Add(characterLabel1);
            labelList.Add(weaponLabel1);
            labelList.Add(stigmataLabel1);
            labelList.Add(stigmataLabel2);
            labelList.Add(stigmataLabel3);
            labelDic.Add(0, labelList);

            labelList = new List<Label>();
            labelList.Add(characterLabel2);
            labelList.Add(weaponLabel2);
            labelList.Add(stigmataLabel4);
            labelList.Add(stigmataLabel5);
            labelList.Add(stigmataLabel6);
            labelDic.Add(1, labelList);

            labelList = new List<Label>();
            labelList.Add(characterLabel3);
            labelList.Add(weaponLabel3);
            labelList.Add(stigmataLabel7);
            labelList.Add(stigmataLabel8);
            labelList.Add(stigmataLabel9);
            labelDic.Add(2, labelList);

            stigmataTopList = stigmataDao.GetStigmatasByCategoryId(CommonConstant.StigmataType.上);
            stigmataCenterList = stigmataDao.GetStigmatasByCategoryId(CommonConstant.StigmataType.中);
            stigmataBottomList = stigmataDao.GetStigmatasByCategoryId(CommonConstant.StigmataType.下);
        }

        private void RefreshMetaButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_folderPathStr))
            {
                folderBrowserDialog1.SelectedPath = _folderPathStr;
            }

            folderBrowserDialog1.ShowDialog();
            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath)) return;

            _folderPathStr = folderBrowserDialog1.SelectedPath;
            AbyssMetaFolderTextBox.Text = _folderPathStr;

            ConfigUtil.SetConfigContent(CommonConstant.FOLDER_KEY_ANALYZE, _folderPathStr);

            LoadAbyssDeployImage();
        }

        private void 角色数据窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_characterDataForm == null || _characterDataForm.IsDisposed) _characterDataForm = new CharacterDataForm();
            
            if (_characterDataForm.Created) _characterDataForm.Select();
            else _characterDataForm.Show();

        }

        private void 武器数据窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_weaponDataForm == null || _weaponDataForm.IsDisposed) _weaponDataForm = new WeaponDataForm();

            if (_weaponDataForm.Created) _weaponDataForm.Select();
            else _weaponDataForm.Show();
        }

        private void 圣痕数据窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_stigmataDataForm == null || _stigmataDataForm.IsDisposed) _stigmataDataForm = new StigmataDataForm();

            if (_stigmataDataForm.Created) _stigmataDataForm.Select();
            else _stigmataDataForm.Show();
        }


        private void LoadAbyssDeployImage()
        {
            string folderPathStr = AbyssMetaFolderTextBox.Text;
            try
            {
                DirectoryInfo root = new DirectoryInfo(folderPathStr);
                var pngImageList = root.GetFiles("*.png", SearchOption.AllDirectories);
                var jpgImageList = root.GetFiles("*.jpg", SearchOption.AllDirectories);

                List<FileInfo> imageList = new List<FileInfo>();
                imageList.AddRange(pngImageList);
                imageList.AddRange(jpgImageList);

                if (imageList == null || imageList.Count == 0)
                {
                    MessageBox.Show("目录下没有找到图片文件");
                    return;
                }

                UpdateMatchImageDataGridView(imageList);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void UpdateMatchImageDataGridView(List<FileInfo> imageList)
        { 
            if (imageList == null) return;

            dataGridView1.Rows.Clear();

            foreach (FileInfo fileInfo in imageList)
            {
                int rowIndex = dataGridView1.Rows.Add();
                dataGridView1.Rows[rowIndex].Cells[0].Value = rowIndex + 1;
                dataGridView1.Rows[rowIndex].Cells[1].Value = Path.GetFileNameWithoutExtension(fileInfo.FullName);
                dataGridView1.Rows[rowIndex].Cells[2].Value = fileInfo.FullName;
            }
            
        }


        public AbyssDeploy MatchAbyssDeploy(Image<Bgra, byte> modelImage, int index)
        {
            AbyssDeploy abyssDeploy = new AbyssDeploy();

            Character matchCharacter = MatchCharacter(modelImage);
            if (matchCharacter != null)
            {
                abyssDeploy.character = matchCharacter;
                labelDic[index][0].Text = abyssDeploy.character.name;
            }

            Weapon matchWeapon = MatchWeapon(modelImage, matchCharacter);
            if (matchWeapon != null)
            {
                abyssDeploy.weapon = matchWeapon;
                labelDic[index][1].Text = abyssDeploy.weapon.name;
            }

            Stigmata matchStigmataTop = MatchStigmata(modelImage, stigmataTopList);
            if (matchStigmataTop != null)
            {
                abyssDeploy.stigmataTop = matchStigmataTop;
                labelDic[index][2].Text = abyssDeploy.stigmataTop.name;
            }

            Stigmata matchStigmataCenter = MatchStigmata(modelImage, stigmataCenterList);
            if (matchStigmataCenter != null)
            {
                abyssDeploy.stigmataCenter = matchStigmataCenter;
                labelDic[index][3].Text = abyssDeploy.stigmataCenter.name;
            }

            Stigmata matchStigmataBottom = MatchStigmata(modelImage, stigmataBottomList);
            if (matchStigmataBottom != null)
            {
                abyssDeploy.stigmataBottom = matchStigmataBottom;
                labelDic[index][4].Text = abyssDeploy.stigmataBottom.name;
            }


            return abyssDeploy;
        }

        public Character MatchCharacter(Image<Bgra, byte> modelImage)
        {
            //角色
            List<Character> characterList = characterDao.GetCharacterList();
            Character matchCharacter = null;
            int maxMatchCount = 0;
            foreach (Character character in characterList)
            {
                int count = 0;
                using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
                {
                    Image<Bgra, byte> characterImage = new Image<Bgra, byte>(character.filePath);
                    long matchTime;
                    count = FindMatch(modelImage, characterImage, out matchTime, matches);
                }

                if (count >= 4 && count > maxMatchCount)
                {
                    matchCharacter = character;
                    maxMatchCount = count;
                }
            }

            return matchCharacter;
        }

        public Weapon MatchWeapon(Image<Bgra, byte> modelImage, Character matchCharacter)
        {
            List<Weapon> weaponList = matchCharacter == null
                ? weaponDao.GetWeaponList()
                : weaponDao.GetWeaponsByCategoryId(matchCharacter.weaponCategortId);

            //武器
            Weapon matchWeapon = null;
            int maxMatchCount = 0;
            foreach (Weapon weapon in weaponList)
            {
                int count = 0;
                using (VectorOfVectorOfDMatch match = new VectorOfVectorOfDMatch())
                {
                    Image<Bgra, byte> weaponImage = new Image<Bgra, byte>(weapon.filePath);
                    long matchTime;
                    count = FindMatch(modelImage, weaponImage, out matchTime, match);
                }

                if (count >= 4 && count > maxMatchCount)
                {
                    matchWeapon = weapon;
                    maxMatchCount = count;
                }
            }

            return matchWeapon;
        }

        public Stigmata MatchStigmata(Image<Bgra, byte> modelImage, List<Stigmata> matchStigmataList)
        {
            if (matchStigmataList == null) return null;
            //圣痕
            Stigmata matchStigmata = null;
            int maxMatchCount = 0;
            foreach (Stigmata stigmata in matchStigmataList)
            {
                int count = 0;
                using (VectorOfVectorOfDMatch match = new VectorOfVectorOfDMatch())
                {
                    Image<Bgra, byte> stigmataImage = new Image<Bgra, byte>(stigmata.filePath);
                    long matchTime;
                    count = FindMatch(modelImage, stigmataImage, out matchTime, match);
                }

                if (count >= 4 && count > maxMatchCount)
                {
                    //MessageBox.Show(stigmata.name + " -- " + count);
                    maxMatchCount = count;
                    matchStigmata = stigmata;
                }
            }

            return matchStigmata;
        }


        public int FindMatch(Image<Bgra, byte> modelImage, Image<Bgra, byte> observedImage, out long matchTime,
           VectorOfVectorOfDMatch matches)
        {
            int k = 2;
            double uniquenessThreshold = 0.8;
            double hessianThresh = 300;

            Stopwatch watch;
            //homography = null;

            VectorOfKeyPoint modelKeyPoints = new VectorOfKeyPoint();
            VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();

            int count = 0;
            //using (UMat uModelImage = modelImage.ToUMat())
            //				using (UMat uObservedImage = observedImage.ToUMat())
            {
                SURF surfCPU = new SURF(hessianThresh);
                //extract features from the object image
                UMat modelDescriptors = new UMat();
                //surfCPU.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);
                surfCPU.DetectRaw(modelImage, modelKeyPoints);
                surfCPU.Compute(modelImage, modelKeyPoints, modelDescriptors);

                watch = Stopwatch.StartNew();

                // extract features from the observed image
                UMat observedDescriptors = new UMat();
                surfCPU.DetectAndCompute(observedImage, null, observedKeyPoints, observedDescriptors, false);
                BFMatcher matcher = new BFMatcher(DistanceType.L2);
                matcher.Add(modelDescriptors);

                matcher.KnnMatch(observedDescriptors, matches, k, null);
                Mat mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                mask.SetTo(new MCvScalar(255));
                Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                // nonZeroCount命中的点 进行两次筛选
                int nonZeroCount = CvInvoke.CountNonZero(mask);
                if (nonZeroCount >= 4)
                {
                    nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                        matches, mask, 1.5, 20);
                    //if (nonZeroCount >= 4)
                    //    homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                    //        observedKeyPoints, matches, mask, 3);
                }

                count = nonZeroCount;
                watch.Stop();
            }

            matchTime = watch.ElapsedMilliseconds;
            return count;
        }

        private string exportPath = "";
        private string fileName = "";
        private List<AbyssDeploy> exportDataList;
        private int threadCount = 0;
        private int threadEndNum = 0;

        
        private void StartThreadAnalyze()
        {
            exportDataList = new List<AbyssDeploy>();

            List<string> filePathList = new List<string>();
            List<string> fileNameList = new List<string>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                fileNameList.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                filePathList.Add(dataGridView1.Rows[i].Cells[2].Value.ToString());
            }

            threadCount = filePathList.Count;
            threadEndNum = 0;

            int thread1StartIndex = 0;
            int thread1EndIndex = threadCount / 2;
            int thread2StartIndex = thread1EndIndex;
            int thread2EndIndex = threadCount;

            //开两个线程来完成该分析
            Thread thread1 = new Thread(new ParameterizedThreadStart((x) =>
            {
                for (int i = thread1StartIndex; i < thread1EndIndex; i++)
                {
                    analyzeAbyssDeploy(filePathList[i], fileNameList[i]);
                    threadEndNum += 1;
                    CheckIsAnalyzeEnd();
                }
            }));
            thread1.IsBackground = false;
            thread1.Start();

            Thread thread2 = new Thread(new ParameterizedThreadStart((x) =>
            {
                for (int i = thread2StartIndex; i < thread2EndIndex; i++)
                {
                    analyzeAbyssDeploy(filePathList[i], fileNameList[i]);
                    threadEndNum += 1;
                    CheckIsAnalyzeEnd();
                }
            }));
            thread2.IsBackground = false;
            thread2.Start();
        }

        private void CheckIsAnalyzeEnd()
        {
            if (threadCount == threadEndNum)
            {
                ExportCsv();
            }
        }

        private void ExportCsv()
        {
            if (string.IsNullOrEmpty(exportPath) || string.IsNullOrEmpty(fileName))
            {
                return;
            }

            TextWriter textWriter = new StreamWriter(Path.Combine(exportPath, fileName), false, Encoding.UTF8);
            var csv = new CsvWriter(textWriter);

            if (exportDataList == null) return;

            for (int i = 0; i < exportDataList.Count; i++)
            {
                csv.WriteField<string>(exportDataList[i].fileName);
                csv.WriteField<string>(exportDataList[i].character.name);
                csv.WriteField<string>(exportDataList[i].weapon.name);
                csv.WriteField<string>(exportDataList[i].stigmataTop.name);
                csv.WriteField<string>(exportDataList[i].stigmataCenter.name);
                csv.WriteField<string>(exportDataList[i].stigmataBottom.name);
                csv.NextRecord();
            }

            //for (int i = 0; i < guidList.Count; i++)
            //{
            //    csv.WriteField(guidList[i]);
            //    csv.NextRecord();
            //}

            textWriter.Flush();
            textWriter.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string fileName = (string) dataGridView1.CurrentRow.Cells[1].Value;
            string filePath = (string) dataGridView1.CurrentRow.Cells[2].Value;

            Thread thread = new Thread(new ParameterizedThreadStart((x) =>
            {
                analyzeAbyssDeploy(filePath, fileName);
            }));
            thread.IsBackground = true;
            thread.Start();
            
        }

        public void analyzeAbyssDeploy(string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(filePath)) return;

            List<AbyssDeploy> abyssDeployList = new List<AbyssDeploy>();

            Image<Bgra, byte> a1 = new Image<Bgra, byte>(filePath);

            int _leftBlank = (int) (a1.Size.Width / _leftBlankCoefficient);
            int _rightBlank = (int) (a1.Size.Width - a1.Size.Width * _rightBlankCoefficient);
            int _topBlank = (int) (a1.Size.Height / _topBlankCoefficient);
            int _bottomBlank = (int) (a1.Size.Height - a1.Size.Height * _bottomBlankCoefficient);

            int width = a1.Size.Width - _leftBlank - _rightBlank;
            int height = (a1.Size.Height - _topBlank - _bottomBlank) / 3;
            int x = _leftBlank;
            int y = _topBlank;

            for (int i = 0; i < 3; i++)
            {
                var copyImage = a1.Copy(new Rectangle(x, y, width, height));
                pictureBoxList[i].Image = copyImage.Bitmap;

                AbyssDeploy abyssDeploy = MatchAbyssDeploy(copyImage, i);
                abyssDeploy.fileName = fileName;
                abyssDeployList.Add(abyssDeploy);

                y = y + height;
            }
            exportDataList.AddRange(abyssDeployList);
            //return abyssDeployList;
        }


        private void TestMethod()
        {

            Image<Bgra, byte> a1 = new Image<Bgra, byte>("D:/情报姬项目/深渊Meta数据分析/迪拉克/QQ图片20190211004621.jpg"); //模板
            

            var _leftBlank = (int) (a1.Size.Width / 8);
            var _rightBlank = (int) (a1.Size.Width - a1.Size.Width * 0.97);
            var _topBlank = (int)(a1.Size.Height / 8);
            var _bottomBlank = (int) (a1.Size.Height - a1.Size.Height * 0.75);

            int width = a1.Size.Width - _leftBlank - _rightBlank;
            int height = (a1.Size.Height - _topBlank - _bottomBlank) / 3;
            int x = _leftBlank;
            int y = _topBlank;


            for (int i = 0; i < 3; i++)
            {
                var copyImage = a1.Copy(new Rectangle(x, y, width, height));
                pictureBoxList[i].Image = copyImage.Bitmap;

                AbyssDeploy abyssDeploy = MatchAbyssDeploy(copyImage, i);

                y = y + height;
            }
        }

        private void exportPTSYButton_Click(object sender, EventArgs e)
        {
            _leftBlankCoefficient = 8;
            _rightBlankCoefficient = 0.97;
            _topBlankCoefficient = 5.5;
            _bottomBlankCoefficient = 0.88;

            exportFolderBrowserDialog.ShowDialog();
            if (string.IsNullOrEmpty(exportFolderBrowserDialog.SelectedPath)) return;

            exportPath = exportFolderBrowserDialog.SelectedPath;
            fileName = DateTime.Now.ToString("yyyy-MM-dd") + "普通深渊数据输出.csv";

            StartThreadAnalyze();
            //TestMethod();
        }

        private void exportDLKButton_Click(object sender, EventArgs e)
        {
            //_leftBlankCoefficient = 8;
            //_rightBlankCoefficient = 0.97;
            //_topBlankCoefficient = 8;
            //_bottomBlankCoefficient = 0.75;

            //exportFolderBrowserDialog.ShowDialog();
            //if (string.IsNullOrEmpty(exportFolderBrowserDialog.SelectedPath)) return;

            //exportPath = exportFolderBrowserDialog.SelectedPath;
            //fileName = DateTime.Now.ToString("yyyy-MM-dd") + "迪拉克深渊数据输出.csv";

            //StartThreadAnalyze();
            TestMethod();
        }
    }
}
