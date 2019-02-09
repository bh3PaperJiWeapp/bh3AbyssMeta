using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private CharacterDataForm _characterDataForm;
        private WeaponDataForm _weaponDataForm;
        private StigmataDataForm _stigmataDataForm;
        private string _folderPathStr;

        private List<PictureBox> pictureBoxList = new List<PictureBox>();
        private Dictionary<int, List<Label>> labelDic = new Dictionary<int, List<Label>>();

        private int _leftBlank = 220;
        private int _topBlank = 200;
        private int _rightBlank = 220;
        private int _bottomBlank = 170;

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



        private void button1_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> a1 = new Image<Bgra, byte>("../../测试图片1.jpg"); //模板

            int width = a1.Size.Width - _leftBlank - _rightBlank;
            int height = (a1.Size.Height - _topBlank - _bottomBlank) / 3;
            int x = _leftBlank;
            int y = _topBlank;


            for (int i = 0; i < 3; i++)
            {
                var copyImage = a1.Copy(new Rectangle(x, y, width, height));
                pictureBoxList[i].Image = copyImage.Bitmap;

                AbyssDeploy abyssDeploy = MatchAbyssDeploy(copyImage);
                labelDic[i][0].Text = abyssDeploy.character.name;
                labelDic[i][1].Text = abyssDeploy.weapon.name;
                labelDic[i][2].Text = abyssDeploy.stigmataTop.name;
                labelDic[i][3].Text = abyssDeploy.stigmataCenter.name;
                labelDic[i][4].Text = abyssDeploy.stigmataBottom.name;

                y = y + height;
            }
            

        }

        public AbyssDeploy MatchAbyssDeploy(Image<Bgra, byte> modelImage)
        {
            AbyssDeploy abyssDeploy = new AbyssDeploy();

            Character matchCharacter = MatchCharacter(modelImage);
            Weapon matchWeapon = MatchWeapon(modelImage, matchCharacter);
            Stigmata matchStigmataTop = MatchStigmata(modelImage, stigmataTopList);
            Stigmata matchStigmataCenter = MatchStigmata(modelImage, stigmataCenterList);
            Stigmata matchStigmataBottom = MatchStigmata(modelImage, stigmataBottomList);

            abyssDeploy.character = matchCharacter;
            abyssDeploy.weapon = matchWeapon;
            abyssDeploy.stigmataTop = matchStigmataTop;
            abyssDeploy.stigmataCenter = matchStigmataCenter;
            abyssDeploy.stigmataBottom = matchStigmataBottom;

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

                if (count > 4 && count > maxMatchCount)
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

                if (count > 4 && count > maxMatchCount)
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

                if (count > 4 && count > maxMatchCount)
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
            using (UMat uModelImage = modelImage.ToUMat())
            //				using (UMat uObservedImage = observedImage.ToUMat())
            {
                SURF surfCPU = new SURF(hessianThresh);
                //extract features from the object image
                UMat modelDescriptors = new UMat();
                surfCPU.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);

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

        private void exportButton_Click(object sender, EventArgs e)
        {
            exportFolderBrowserDialog.ShowDialog();
            if (string.IsNullOrEmpty(exportFolderBrowserDialog.SelectedPath)) return;

            string exportPath = exportFolderBrowserDialog.SelectedPath;
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "深渊数据输出.csv";

            TextWriter textWriter = new StreamWriter(Path.Combine(exportPath, fileName), false, Encoding.UTF8);
            var csv = new CsvWriter(textWriter);

            List<AbyssDeploy> abyssDeployList = new List<AbyssDeploy>();

            {
                Image<Bgra, byte> a1 = new Image<Bgra, byte>("../../测试图片1.jpg"); //模板

                int width = a1.Size.Width - _leftBlank - _rightBlank;
                int height = (a1.Size.Height - _topBlank - _bottomBlank) / 3;
                int x = _leftBlank;
                int y = _topBlank;

                for (int i = 0; i < 3; i++)
                {
                    var copyImage = a1.Copy(new Rectangle(x, y, width, height));
                    pictureBoxList[i].Image = copyImage.Bitmap;

                    AbyssDeploy abyssDeploy = MatchAbyssDeploy(copyImage);
                    abyssDeployList.Add(abyssDeploy);

                    labelDic[i][0].Text = abyssDeploy.character.name;
                    labelDic[i][1].Text = abyssDeploy.weapon.name;
                    labelDic[i][2].Text = abyssDeploy.stigmataTop.name;
                    labelDic[i][3].Text = abyssDeploy.stigmataCenter.name;
                    labelDic[i][4].Text = abyssDeploy.stigmataBottom.name;

                    y = y + height;
                }
            }

            for (int i = 0; i < abyssDeployList.Count; i++)
            {
                csv.WriteField<string>(abyssDeployList[i].character.name);
                csv.WriteField<string>(abyssDeployList[i].weapon.name);
                csv.WriteField<string>(abyssDeployList[i].stigmataTop.name);
                csv.WriteField<string>(abyssDeployList[i].stigmataCenter.name);
                csv.WriteField<string>(abyssDeployList[i].stigmataBottom.name);
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
    }
}
