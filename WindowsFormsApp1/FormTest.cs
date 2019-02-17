using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.XFeatures2D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.OCR;

namespace WindowsFormsApp1
{
    public partial class FormTest : Form
    {

        Image<Bgr, byte> a = new Image<Bgr, byte>("../../测试图片1.bmp");
        //Image<Bgr, byte> 提尔 = new Image<Bgr, byte>("../../Weapon_1.bmp");
        Image<Bgr, byte> 双狼 = new Image<Bgr, byte>("../../Weapon_Fist_2.bmp");
        Image<Bgr, byte> 神恩 = new Image<Bgr, byte>("../../神恩.png");
        Image<Bgr, byte> 空之律者 = new Image<Bgr, byte>("../../空之律者.png");

        public FormTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MatchTemplateImage(空之律者, TemplateMatchingType.CcoeffNormed);
        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    MatchTemplateImage(双狼, TemplateMatchingType.Ccoeff);
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    //MatchTemplateImage(TemplateMatchingType.Ccorr);
        //}

        //private void button4_Click(object sender, EventArgs e)
        //{
        //    //MatchTemplateImage(TemplateMatchingType.CcorrNormed);
        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    //MatchTemplateImage(TemplateMatchingType.Sqdiff);
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //MatchTemplateImage(TemplateMatchingType.SqdiffNormed);
        //}

        private void MatchTemplateImage(Image<Bgr, byte> searchImg, TemplateMatchingType matchingType)
        {
            Image<Gray, float> c = new Image<Gray, float>(a.Width, a.Height);

            int imgHalfPoint = a.Bitmap.Width / 2;

            //int i = 0;
            //for (bool redo = true; redo && i < 3; i++)
            //{
            //}
            //redo = false;
            //if (maxp.X < imgHalfPoint)
            //{
            //}

            double min = 0, max = 0;
            Point maxp = new Point(0, 0);
            Point minp = new Point(0, 0);
            c = a.MatchTemplate(searchImg, matchingType);

            CvInvoke.Normalize(c, c, 0, 1, NormType.MinMax);

            CvInvoke.MinMaxLoc(c, ref min, ref max, ref minp, ref maxp);



            CvInvoke.Rectangle(a, new Rectangle(maxp, new Size(searchImg.Width, searchImg.Height)), new MCvScalar(0, 0, 255), 0);



            //CvInvoke.MatchTemplate();

            //Console.WriteLine(min + " " + max);
            //CvInvoke.Normalize(c, c);
            imageBox1.Image = a;
        }


        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //			Image<Bgra, byte> a = new Image<Bgra, byte>("IMG_20150829_192403.JPG").Resize(0.4, Inter.Area);  //模板
        //    //			Image<Bgra, byte> b = new Image<Bgra, byte>("DSC_0437.JPG").Resize(0.4, Inter.Area);  //待匹配的图像

        //    Image<Bgra, byte> a = new Image<Bgra, byte>("../../测试图片.bmp").Resize(0.4, Inter.Area); //模板
        //    Image<Bgra, byte> b = new Image<Bgra, byte>("../../五行提尔.png").Resize(0.4, Inter.Area); //待匹配的图像

        //    Mat homography = null;
        //    Mat mask = null;
        //    VectorOfKeyPoint modelKeyPoints = new VectorOfKeyPoint();
        //    VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();
        //    VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();

        //    UMat a1 = a.ToUMat();
        //    UMat b1 = b.ToUMat();

        //    SURF surf = new SURF(300);
        //    UMat modelDescriptors = new UMat();
        //    UMat observedDescriptors = new UMat();

        //    surf.DetectAndCompute(a1, null, modelKeyPoints, modelDescriptors, false); //进行检测和计算，把opencv中的两部分和到一起了，分开用也可以
        //    surf.DetectAndCompute(b1, null, observedKeyPoints, observedDescriptors, false);

        //    BFMatcher matcher = new BFMatcher(DistanceType.L2); //开始进行匹配
        //    matcher.Add(modelDescriptors);
        //    matcher.KnnMatch(observedDescriptors, matches, 2, null);
        //    mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
        //    mask.SetTo(new MCvScalar(255));
        //    Features2DToolbox.VoteForUniqueness(matches, 0.8, mask); //去除重复的匹配

        //    int Count = CvInvoke.CountNonZero(mask); //用于寻找模板在图中的位置
        //    if (Count >= 4)
        //    {
        //        Count = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, matches, mask,
        //            1.5, 20);
        //        if (Count >= 4)
        //            homography =
        //                Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints,
        //                    matches, mask, 2);
        //    }

        //    Mat result = new Mat();
        //    Features2DToolbox.DrawMatches(a.Convert<Gray, byte>().Mat, modelKeyPoints, b.Convert<Gray, byte>().Mat,
        //        observedKeyPoints, matches, result, new MCvScalar(255, 0, 255), new MCvScalar(0, 255, 255), mask);
        //    //绘制匹配的关系图
        //    if (homography != null) //如果在图中找到了模板，就把它画出来
        //    {
        //        Rectangle rect = new Rectangle(Point.Empty, a.Size);
        //        PointF[] points = new PointF[]
        //        {
        //            new PointF(rect.Left, rect.Bottom),
        //            new PointF(rect.Right, rect.Bottom),
        //            new PointF(rect.Right, rect.Top),
        //            new PointF(rect.Left, rect.Top)
        //        };
        //        points = CvInvoke.PerspectiveTransform(points, homography);
        //        Point[] points2 = Array.ConvertAll<PointF, Point>(points, Point.Round);
        //        VectorOfPoint vp = new VectorOfPoint(points2);
        //        CvInvoke.Polylines(result, vp, true, new MCvScalar(255, 0, 0, 255), 15);
        //    }

        //    imageBox1.Image = result;
        //}

        private void button2_Click(object sender, EventArgs e)
        {

            //Image<Bgra, byte> a = new Image<Bgra, byte>("../../测试图片.bmp"); //模板
            Image<Bgra, byte> a1 = new Image<Bgra, byte>("D:/情报姬项目/深渊Meta数据分析/迪拉克/QQ图片20190211004621.jpg"); //模板
            //Image<Bgra, byte> 神恩 = new Image<Bgra, byte>("../../神恩.png"); //待匹配的图像
            //Image<Bgra, byte> b = new Image<Bgra, byte>("../../空之律者.png"); //待匹配的图像
            //Image<Bgra, byte> 次元 = new Image<Bgra, byte>("../../次元.png"); //待匹配的图像
            //Image<Bgra, byte> 红下 = new Image<Bgra, byte>("../../红下.png"); //待匹配的图像
            //Image<Bgra, byte> b = new Image<Bgra, byte>("../../Weapon_Fist_21.png"); //待匹配的图像
            //Image<Bgra, byte> b = new Image<Bgra, byte>("../../五行提尔.png"); //待匹配的图像
            //Image<Bgra, byte> b = new Image<Bgra, byte>("../../第六夜想曲.png"); //待匹配的图像
            Image<Bgra, byte> b = new Image<Bgra, byte>("D:/情报姬项目/深渊Meta数据分析/主目录/角色/拳套/炽翎.png"); //待匹配的图像
            //Image<Bgra, byte> b = new Image<Bgra, byte>(@"D:\情报姬项目\深渊Meta数据分析\AbyssMetaMain\图鉴库\女武神\十字架\神恩.png"); //待匹配的图像

            int _leftBlank = 220;
            int _topBlank = 170;
            int _rightBlank = 220;
            int _bottomBlank = 170;

            
            var observedImage = b;
            long matchTime = 0;

            //List<Image<Bgra, byte>> observedImageList = new List<Image<Bgra, byte>>();
            //observedImageList.Add(次元);
            //observedImageList.Add(神恩);

            //int width = a1.Size.Width - _leftBlank - _rightBlank;
            //int height = (a1.Size.Height - _topBlank - _bottomBlank) / 3;
            //int x = _leftBlank;
            //int y = _topBlank;
            //y = y + height;

            //var copyImage1 = a1.Copy(new Rectangle(x, y, width, height));
            var modelImage = a1;

            //{
            //    double min = 0, max = 0;
            //    Point maxp = new Point(0, 0);
            //    Point minp = new Point(0, 0);
            //    Image<Gray, float> c = new Image<Gray, float>(modelImage.Width, modelImage.Height);
            //    c = modelImage.MatchTemplate(observedImage, TemplateMatchingType.Ccorr);
            //    CvInvoke.Normalize(c, c, 0, 1, NormType.MinMax);
            //    CvInvoke.MinMaxLoc(c, ref min, ref max, ref minp, ref maxp);
            //    CvInvoke.Rectangle(modelImage, new Rectangle(maxp, new Size(observedImage.Width, observedImage.Height)), new MCvScalar(255, 0, 255), 0);
            //    imageBox1.Image = modelImage;
            //}



            Mat homography;
            VectorOfKeyPoint modelKeyPoints;
            VectorOfKeyPoint observedKeyPoints;

            List<VectorOfKeyPoint> observedKeyPointsList = new List<VectorOfKeyPoint>();

            using (VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch())
            {
                Mat mask = new Mat();
                int count = FindMatch(modelImage, observedImage, out matchTime, out modelKeyPoints, out observedKeyPoints, matches,
                    out mask);

                Mat result = new Mat();
                Features2DToolbox.DrawMatches(modelImage.Convert<Gray, byte>().Mat, modelKeyPoints,
                    observedImage.Convert<Gray, byte>().Mat, observedKeyPoints,
                    matches, result, new MCvScalar(255, 0, 255), new MCvScalar(0, 255, 255), mask);

                imageBox1.Image = result;

                Console.WriteLine(count);

                //List<Mat> maskList = new List<Mat>();
                //FindMatchList(modelImage, observedImageList, out matchTime, out modelKeyPoints, observedKeyPointsList, matches,
                //    maskList);
                //for (int i = 0; i < observedImageList.Count && i < maskList.Count && i < observedKeyPointsList.Count; i++)
                //{
                //    //Draw the matched keypoints
                //    Mat result = new Mat();
                //    Features2DToolbox.DrawMatches(modelImage.Convert<Gray, byte>().Mat, modelKeyPoints,
                //        observedImageList[i].Convert<Gray, byte>().Mat, observedKeyPointsList[i],
                //        matches, result, new MCvScalar(255, 0, 255), new MCvScalar(0, 255, 255), maskList[i]);

                //    imageBox1.Image = result;
                //}



                //#region draw the projected region on the image

                //if (homography != null)
                //{
                //    //draw a rectangle along the projected model
                //    Rectangle rect = new Rectangle(Point.Empty, modelImage.Size);
                //    PointF[] pts = new PointF[]
                //    {
                //        new PointF(rect.Left, rect.Bottom),
                //        new PointF(rect.Right, rect.Bottom),
                //        new PointF(rect.Right, rect.Top),
                //        new PointF(rect.Left, rect.Top)
                //    };
                //    pts = CvInvoke.PerspectiveTransform(pts, homography);

                //    Point[] points = Array.ConvertAll<PointF, Point>(pts, Point.Round);
                //    using (VectorOfPoint vp = new VectorOfPoint(points))
                //    {
                //        CvInvoke.Polylines(result, vp, true, new MCvScalar(255, 0, 0, 255), 5);
                //    }

                //}

                //    //#endregion



            }
        }

        public static int FindMatch(Image<Bgra, byte> modelImage, Image<Bgra, byte> observedImage, out long matchTime,
            out VectorOfKeyPoint modelKeyPoints, out VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches,
            out Mat mask/*, out Mat homography*/)
        {
            int k = 2;
            double uniquenessThreshold = 0.8;
            double hessianThresh = 300;

            Stopwatch watch;
            //homography = null;

            modelKeyPoints = new VectorOfKeyPoint();
            observedKeyPoints = new VectorOfKeyPoint();

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
                mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
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


        public static void FindMatchList(Image<Bgra, byte> modelImage, List<Image<Bgra, byte>> observedImageList, out long matchTime,
            out VectorOfKeyPoint modelKeyPoints, List<VectorOfKeyPoint> observedKeyPointsList, VectorOfVectorOfDMatch matches,
            List<Mat> maskList)
        {


            int k = 2;
            double uniquenessThreshold = 0.8;
            double hessianThresh = 300;

            Stopwatch watch;
            //homography = null;

            modelKeyPoints = new VectorOfKeyPoint();

            if (observedImageList == null || observedImageList.Count == 0)
            {
                maskList = null;
                matchTime = 0;
                return;
            }

            maskList = new List<Mat>();

            using (UMat uModelImage = modelImage.ToUMat())
            //				using (UMat uObservedImage = observedImage.ToUMat())
            {
               
                watch = Stopwatch.StartNew();

                SURF surfCPU = new SURF(hessianThresh);
                //extract features from the object image
                UMat modelDescriptors = new UMat();
                surfCPU.DetectAndCompute(uModelImage, null, modelKeyPoints, modelDescriptors, false);

                foreach (Image<Bgra, byte> observedImage in observedImageList)
                {
                    VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();
                    observedKeyPointsList.Add(observedKeyPoints);


                    // extract features from the observed image
                    UMat observedDescriptors = new UMat();
                    surfCPU.DetectAndCompute(observedImage, null, observedKeyPoints, observedDescriptors, false);
                    BFMatcher matcher = new BFMatcher(DistanceType.L2);
                    matcher.Add(modelDescriptors);

                    matcher.KnnMatch(observedDescriptors, matches, k, null);

                    Mat mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                    mask.SetTo(new MCvScalar(255));
                    maskList.Add(mask);

                    Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);

                    // nonZeroCount命中的点 进行两次筛选
                    int nonZeroCount = CvInvoke.CountNonZero(mask);
                    if (nonZeroCount >= 4)
                    {
                        nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints,
                            matches, mask, 1.5, 20);
                        //if (nonZeroCount >= 4)
                        //homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints,
                        //    observedKeyPoints, matches, mask, 3);
                    }
                }

                watch.Stop();
            }

            matchTime = watch.ElapsedMilliseconds;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Image<Bgra, byte> image = new Image<Bgra, byte>("../../test1.bmp");

            var modelImage = image;

            Bgra drawColor = new Bgra(0, 0, 0, 0);
            Tesseract _ocr = new Tesseract(/*"../../test1.bmp", null,OcrEngineMode.Default*/);
            _ocr.SetImage(modelImage);

            //			using (Image<Gray, byte> gray = image.Convert<Gray, Byte>())
            //			{
            _ocr.Recognize();
            Tesseract.Character[] charactors = _ocr.GetCharacters();
            foreach (Tesseract.Character c in charactors)
            {
                //					image.Draw(c.Region, drawColor, 1);
                Console.Write(c.Text);
            }


            var re = _ocr.GetUTF8Text();
            Console.Write(re);

            //			}
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
