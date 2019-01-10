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

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigUtil.CreateConfigXml();

            _folderPathStr = ConfigUtil.GetConfigContent(CommonConstant.FOLDER_KEY_ANALYZE);

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

        private int _leftBlank = 220;
        private int _topBlank = 170;
        private int _rightBlank = 220;
        private int _bottomBlank = 170;


        private void button1_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> a1 = new Image<Bgra, byte>("../../测试图片1.bmp"); //模板

            int width = a1.Size.Width - _leftBlank - _rightBlank;
            int height = (a1.Size.Height - _topBlank - _bottomBlank) / 3;
            int x = _leftBlank;
            int y = _topBlank;

            //copyImage = copyImage.Resize(pictureBox1.Width, pictureBox1.Height, Inter.Area);
            var characterDao = Dao.CharacterDao.GetInstance();
            var characterList = characterDao.GetCharacterList();

            //List<int> syncCharacterList = new List<int>(); 

            var copyImage1 = a1.Copy(new Rectangle(x, y, width, height));
            pictureBox1.Image = copyImage1.Bitmap;

            foreach (Character character in characterList)
            {
                int count = 0;
                using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
                {
                    Image<Bgra, byte> characterImage = new Image<Bgra, byte>(character.filePath);
                    long matchTime;
                    count = FindMatch(copyImage1, characterImage, out matchTime, matches);
                }

                if (count > 4)
                {
                    MessageBox.Show(character.name + "---" + count);
                }

            }


            y = y + height;

            var copyImage2 = a1.Copy(new Rectangle(x, y, width, height));
            pictureBox2.Image = copyImage2.Bitmap;

            foreach (Character character in characterList)
            {
                

                int count = 0;
                using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
                {
                    Image<Bgra, byte> characterImage = new Image<Bgra, byte>(character.filePath);
                    long matchTime;
                    count = FindMatch(copyImage2, characterImage, out matchTime, matches);
                }

                if (count > 4)
                {
                    MessageBox.Show(character.name + "---" + count);
                    
                    break;
                }

            }

            y = y + height;

            var copyImage3 = a1.Copy(new Rectangle(x, y, width, height));
            pictureBox3.Image = copyImage3.Bitmap;

            foreach (Character character in characterList)
            {
                

                int count = 0;
                using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
                {
                    Image<Bgra, byte> characterImage = new Image<Bgra, byte>(character.filePath);
                    long matchTime;
                    count = FindMatch(copyImage3, characterImage, out matchTime, matches);
                }

                if (count > 4)
                {
                    MessageBox.Show(character.name + "---" + count);
                    break;
                }

            }
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
