using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

namespace SP_0_LAB_1_PRISTAVKA_V_1._0
{
    class ourWindow : Form
    {
        private String ourNumber = null;
        private String ourTemp1 = null;
        private List<double> ourNum = null;

        double ourHTemp = 0;

        bool doWeNeedToChangeAnomData = false;

        private int ourM_Chang = -1;

        private int ourChoiseToChange = 0;
        int ourChise = 0;

        int ourChoice = 0; //

        int choiseOnGraph = 0; //0 - гістограмна оцінка, 1 - емпірична функція розподілу

        double ourM = 0;

        Chart chart1 = new Chart();

        DataGridView ourData = new DataGridView();
        DataGridView ourData2 = new DataGridView();

        DataGridView ourData4 = new DataGridView();

        DataGridView ourData3 = new DataGridView();
        public ourWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1700, 700);

            createMenu();
            createButton();

        }

        private void createMenu()
        {
            MenuStrip cnt = new MenuStrip();
            cnt.Dock = DockStyle.Top;
            //
            ToolStripMenuItem windowNewMenu = new ToolStripMenuItem("Виберіть варіант", null, new EventHandler(windowNewMenu1_Click));
            cnt.Items.Add(windowNewMenu);
            //
            windowNewMenu = new ToolStripMenuItem("Розподіл");

            windowNewMenu.DropDownItems.Add("Вейбула", null, new EventHandler(ourChoice1_Our2Lab));
            windowNewMenu.DropDownItems.Add("Нормальний", null, new EventHandler(ourChoice2_Our2Lab));
            windowNewMenu.DropDownItems.Add("Рівномірний", null, new EventHandler(ourChoice3_Our2Lab));
            windowNewMenu.DropDownItems.Add("Експоненціальний", null, new EventHandler(ourChoice4_Our2Lab));
            windowNewMenu.DropDownItems.Add("Косинуса", null, new EventHandler(ourChoice5_Our2Lab));


            cnt.Items.Add(windowNewMenu);

            Controls.Add(cnt);
        }

        private void createButton()
        {
            Control button = new Button();
            button.Location = new Point(10, 30);
            button.Text = "Розрахувати";
            button.Size = new Size(100, 30);
            button.Click += button1_Click;
            Controls.Add(button);
        }
        private void button1_Click(Object sender, EventArgs e)
        {
            //
            ourNum = new List<double>();
            MyMath ourOperation = new MyMath();

            double[] a = ourNumber.Split(' ').
                        Where(x => !string.IsNullOrWhiteSpace(x)).
                        Select(x => double.Parse(x)).ToArray();
            List<double> ourNumbers = a.ToList();
            ourNumbers.Sort();
            List<double> waitTill = new List<double> { };
            String ourNumberfing = ourNumber;

            if (doWeNeedToChangeAnomData == true)
            {
                waitTill = ourOperation.deleteAnomData(ourNumbers);
            }

            if (ourChoiseToChange == 0) // Без преобразование
            {

            }
            else if (ourChoiseToChange == 1)//log
            {
                waitTill = ourOperation.Change_OurNum(ourNumbers, 1);
            }
            else if (ourChoiseToChange == 2)//Стандартизация
            {
                waitTill = ourOperation.Change_OurNum(ourNumbers, 2);
            }
            else if (ourChoiseToChange == 3)//Зсув
            {
                waitTill = ourOperation.Change_OurNum(ourNumbers, 3);
            }
            List<double> ourNum1 = null;
            List<double> ourn = null;
            if (waitTill.Count > 0)
            {
                String ourTemp = "";
                foreach (var el in waitTill)
                {
                    ourTemp += el.ToString();
                    ourTemp += " ";
                }
                ourNum1 = ourOperation.ourNumber_ourP(ourTemp);
                ourNumber = ourTemp;
                ourn = ourOperation.ourN_Find(ourTemp, ourNum1);
            }
            else
            {
                ourNumber = ourTemp1;
                ourNum1 = ourOperation.ourNumber_ourP(ourNumber);
                ourn = ourOperation.ourN_Find(ourNumber, ourNum1);
            }
            List<double> ourp = ourOperation.ourP_Find(ourNum1, ourn);
            if (ourNum1 == null)
            {

            }
            else
            {
                if (ourChise == 0)
                {
                    pervunuu_Analyze(ourNum1, ourn, ourp);
                    if (ourChoice != 0)
                    {
                        dataGreedViewCreate(ourNum1);
                        dataGreedViewCreate2(ourNum1);
                    }
                }
                else if (ourChise == 1)
                {


                }
                else if (ourChise == 2)
                {


                }
                else if (ourChise == 3)
                {


                }
                else if (ourChise == 4)
                {


                }
            }
        }
        private void pervunuu_Analyze(List<double> ourNum1, List<double> ourn, List<double> ourp) //x - l, n - l, p - l
        {
            Controls.Clear();
            InitializeComponent();
            ourData = new DataGridView();

            ourData3 = new DataGridView();
            chart1 = new Chart();
            MyMath ourOperation = new MyMath();
            //

            Control cnt = new Button();
            cnt.Location = new Point(400, 25);

            cnt.Text = "Гістограмна оцінка";
            cnt.Size = new Size(100, 20);
            cnt.Click += ourCheckBox_Click1;
            cnt.Enabled = false;
            Controls.Add(cnt);

            cnt = new Button();

            cnt.Text = "Емпірична функція розподілу";

            cnt.Click += ourCheckBox_Click2;
            cnt.Size = new Size(100, 20);
            cnt.Location = new Point(400, 45);

            Controls.Add(cnt);
            // ПЕРЕТВОРЕННЯ ДАННИХ
            cnt = new Button();
            cnt.Location = new Point(510, 25);

            cnt.Text = "Без перетворення";
            cnt.Size = new Size(100, 20);
            cnt.Click += ourButtonTOChange_1;
            Controls.Add(cnt);

            cnt = new Button();

            cnt.Text = "Стандартизувати";
            cnt.Size = new Size(100, 20);
            cnt.Click += ourButtonTOChange_3;
            cnt.Location = new Point(510, 45);

            Controls.Add(cnt);

            cnt = new Button();

            cnt.Text = "Зсув";
            cnt.Click += ourButtonTOChange_4;
            cnt.Size = new Size(100, 20);
            cnt.Location = new Point(620, 25);

            Controls.Add(cnt);

            cnt = new Button();
            cnt.Click += ourButtonTOChange_2;

            cnt.Text = "Логарифмування";

            cnt.Size = new Size(100, 20);
            cnt.Location = new Point(620, 45);

            Controls.Add(cnt);

            cnt = new TextBox();

            if (ourM_Chang <= 0)
                cnt.Text = "M";
            else
                cnt.Text = $"{ourM_Chang}";
            cnt.TextChanged += ourM_Change;

            cnt.Size = new Size(100, 20);
            cnt.Location = new Point(730, 45);

            Controls.Add(cnt);


            {
                Control cnt2 = new Button();
                cnt2.Click += ourButtonTOKillAnom;

                cnt2.Text = "Вилучення аномальних";

                cnt2.Size = new Size(100, 20);
                cnt2.Location = new Point(730, 25);

                Controls.Add(cnt2);
            }
            //Варіаційний ряд
            ourData.Location = new Point(10, 70);
            ourData.Size = new Size(350, 200);
            Controls.Add(ourData);

            ourData.Columns.Add("x", "x");
            ourData.Columns.Add("n", "n");
            ourData.Columns.Add("p", "p");


            for (int i = 0; i < ourNum1.Count; i++)
            {
                ourData.Rows.Add($"{ourNum1[i]}", $"{ourn[i]}", $"{ourp[i]}");
            }
            //
            double count = 0;

            foreach (var el in ourn)
            {
                count += el;
            }

            if (ourM_Chang <= 0)
            {
                ourM = ourOperation.ourM_Find(ourn, count);
                Controls[8].Text = $"{ourM}";
            }
            else
                ourM = ourM_Chang;

            double ourH = ourOperation.ourH_Find(ourNum1, ourM);
            ourHTemp = ourH;

            List<double> ourXi = new List<double>();
            List<double> ourPi = new List<double>();
            ourPi = ourOperation.ourPi_Find(ourNum1, ourp, ourH, ourM, ourNum1[0]);
            ourXi = ourOperation.ourXi_Find(ourNum1[0], ourH, ourM);

            ourH = ourOperation.ourRound(ourH, 2);

            ourNum1 = ourOperation.ourRound(ourNum1, 2);

            ourPi = ourOperation.ourRound(ourPi, 5);

            ourXi = ourOperation.ourRound(ourXi, 2);

            if (choiseOnGraph == 0)
            {
                createOurChart_GridView1(ourNum1, ourPi, ourXi, count, ourH);
            }
            else if (choiseOnGraph == 1)
            {
                createOurChart_GridView2(ourNum1, ourPi, ourXi, count, ourH);
            }
            //

            //

            letsMathsThis(ourPi, ourXi, count, ourH);

            //

        }

        private void letsMathsThis(List<double> ourPi, List<double> ourXi, double count, double h)
        {
            MyMath ourOperation = new MyMath();
            double[] a = ourNumber.Split(' ').
                        Where(x => !string.IsNullOrWhiteSpace(x)).
                        Select(x => double.Parse(x)).ToArray();
            Array.Sort(a);
            List<double> ourNum1 = a.ToList();
            //
            ourData2 = new DataGridView();
            ourData2.Columns.Add("NAME", "NAME");
            ourData2.Columns.Add("COUNT", "COUNT");

            ourData2.Size = new Size(350, 200);
            ourData2.Location = new Point(10, 280);
            //СА
            double deltaX = 0;

            deltaX = ourOperation.ourDeltaX_Find(ourNum1);
            //СЕРЕДНЬОКВАДРАТИЧНОГО
            double deltaDevX = ourOperation.deltaDevX_Find(ourNum1);
            //КОЕФ АСИМЕТРІЇ
            double ourA = ourOperation.A_Find(ourNum1);

            double ourAС = ourOperation.AC_Find(ourNum1);
            //ЗСУНЕНИЙ ЕКСЦЕСУ
            double ourE = ourOperation.ourE_Find(ourNum1);
            //НЕЗСУНЕНИЙ ЕКСЦЕСУ
            double deltaOurE = ourOperation.ourEC_Find(ourNum1);
            // КОНТРЕКСЦЕСУ
            double ourE1 = ourOperation.ourECE_Find(ourNum1);
            // ВАРІАЦІЇ ПІРСОНА
            double ourW = ourOperation.ourW_Find(ourNum1);
            //
            double MED = ourOperation.ourMED(ourNum1);

            double MAD = ourOperation.ourMAD(MED);
            // Усічене середнє
            double usicheneSeredne = ourOperation.ourTrun_Average_Find(ourNum1);
            //
            deltaX = ourOperation.ourRound(deltaX, 2);
            deltaDevX = ourOperation.ourRound(deltaDevX, 2);
            ourA = ourOperation.ourRound(ourA, 2);
            ourAС = ourOperation.ourRound(ourAС, 2);
            ourE = ourOperation.ourRound(ourE, 2);
            ourE1 = ourOperation.ourRound(ourE1, 2);
            ourW = ourOperation.ourRound(ourW, 2);
            MED = ourOperation.ourRound(MED, 2);
            MAD = ourOperation.ourRound(MAD, 2);
            usicheneSeredne = ourOperation.ourRound(usicheneSeredne, 2);

            ourData2.Rows.Add("СА", $"{deltaX}");
            ourData2.Rows.Add("СЕРЕДНЬОКВАДРАТИЧНОГО", $"{deltaDevX}");
            ourData2.Rows.Add("КОЕФ АСИМЕТРІЇ", $"{ourA}");
            ourData2.Rows.Add("КОЕФ АСИМЕТРІЇ", $"{ourAС}");
            ourData2.Rows.Add("ЕКСЦЕСУ", $"{ourE}");
            ourData2.Rows.Add("КОНТРЕКСЦЕСУ", $"{ourE1}");
            ourData2.Rows.Add("ВАРІАЦІЇ ПІРСОНА", $"{ourW}");
            ourData2.Rows.Add("MED", $"{MED}");
            ourData2.Rows.Add("MAD", $"{MAD}");
            ourData2.Rows.Add("УСІЧЕНЕ СЕРЕДНЄ", $"{usicheneSeredne}");

            double ourOdeltaX = ourOperation.ourODeltaX_Find(ourNum1);
            double ourOSC = ourOperation.ourOSC_Find(ourNum1);
            double ourOAC = ourOperation.ourOAC_Find(ourNum1);
            double ourOEC = ourOperation.ourOEC_Find(ourNum1);

            //a == 0.05, y == 0.95
            double ourS = ourOperation.ourSC_Find(ourNum1);

            double ourY = 0.95;
            double oura = 1 - ourY;
            int ourv = ourNum1.Count - 1;

            double ourStudentaT = ourOperation.ourKVANTIL_Find(ourv);

            ourData3.Columns.Add("o1", "o1");

            ourData3.Columns.Add("0.95%", "0.95%");

            ourData3.Columns.Add("o2", "o2");

            ourOdeltaX = ourOperation.ourRound(ourOdeltaX, 2);
            ourOSC = ourOperation.ourRound(ourOSC, 2);
            ourOAC = ourOperation.ourRound(ourOAC, 2);
            ourOEC = ourOperation.ourRound(ourOEC, 2);

            ourData3.Rows.Add(deltaX - ourStudentaT * ourOdeltaX, "СЕРЕДНЄ", deltaX + ourStudentaT * ourOdeltaX);
            ourData3.Rows.Add(ourS - ourStudentaT * ourOSC, "СЕРЕДНЬОКВАДРАТИЧНЕ", ourS + ourStudentaT * ourOSC);
            ourData3.Rows.Add(ourAС - ourStudentaT * ourOAC, "АСИМЕТРІЯ", ourAС + ourStudentaT * ourOAC);
            ourData3.Rows.Add(ourE1 - ourStudentaT * ourOEC, "КОНТРЕКСЦЕСУ", ourE1 + ourStudentaT * ourOEC);


            ourData3.Size = new Size(350, 200);
            ourData3.Location = new Point(400, 280);


            Controls.Add(ourData2);

            Controls.Add(ourData3);
        }
        private void createOurChart_GridView1(List<double> ourNum1, List<double> ourPi, List<double> ourXi, double count, double h)
        {
            MyMath ourOperation = new MyMath();
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            Series series1 = new Series();
            chartArea1.Name = "ChartArea1";
            chartArea1.BackColor = Color.Purple;
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new System.Drawing.Point(400, 70);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.ChartType = SeriesChartType.Line;
            chart1.Series.Add(series1);
            chart1.Size = new System.Drawing.Size(1050, 200);
            if (ourChoice != 0)
            {
                createOurHrap(ourNum1);
            }
            Controls.Add(chart1);
            double ourpall = 0;

            foreach (var el in ourPi)
            {
                if (el > ourpall)
                {
                    ourpall = el;
                }
            }

            double iz = 0;

            iz = 0;
            for (int kz = 0; kz < ourPi.Count; kz++)
            {
                chart1.Series["Series1"].Points.AddXY(ourNum1[0] + h * iz, ourPi[kz]);
                try
                {
                    chart1.Series["Series1"].Points.AddXY(ourNum1[0] + h * (iz + 1), ourPi[kz]);
                }
                catch (Exception)
                {

                }
                iz++;
            }
            // Статистична функція щільності

        }
        //Емпірична функція розподілу.
        private void createOurChart_GridView2(List<double> ourNum1, List<double> ourPi, List<double> ourXi, double count, double h)
        {
            MyMath ourOperation = new MyMath();
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();
            Series series1 = new Series();
            chartArea1.Name = "ChartArea1";
            chartArea1.BackColor = Color.Purple;
            chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new System.Drawing.Point(400, 70);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.ChartType = SeriesChartType.Line;
            chart1.Series.Add(series1);
            chart1.Size = new System.Drawing.Size(1050, 200);
            Controls.Add(chart1);
            if (ourChoice != 0)
            {
                createOurHrap2(ourNum1);
            }
            chart1.ChartAreas[0].AxisX.Minimum = ourNum1[0];
            chart1.ChartAreas[0].AxisX.Maximum = ourNum1.Last() + h;
            chart1.ChartAreas[0].AxisY.Maximum = 1.1;
            double iz = 0;


            for (int kz = 0; kz < ourPi.Count; kz++)
            {
                iz += ourPi[kz];
                chart1.Series["Series1"].Points.AddXY(ourXi[kz], iz);
                try
                {
                    chart1.Series["Series1"].Points.AddXY(ourXi[kz + 1], iz);
                }
                catch (Exception)
                {

                }
            }
            // Статистична функція щільності


        }
        public void windowNewMenu1_Click(Object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;


            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                filePath = openFileDialog.FileName;

                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    ourNumber = reader.ReadToEnd();
                }
            }
            ourNumber = ourNumber.Replace(".", ",");
            ourNumber = ourNumber.Replace("\n", " ");
            ourTemp1 = ourNumber;
        }

        public void ourM_Change(Object sender, EventArgs e)
        {
            if (Int32.TryParse(Controls[8].Text, out ourM_Chang))
                ;
            else
                ourM_Chang = -1;
        }

        public void windowNewMenu2_Click(Object sender, EventArgs e)
        {
            ourChise = 0;
        }

        public void windowNewMenu3_Click(Object sender, EventArgs e)
        {
            ourChise = 1;
        }

        public void windowNewMenu4_Click(Object sender, EventArgs e)
        {
            ourChise = 2;
        }

        public void ourCheckBox_Click1(Object sender, EventArgs e)
        {
            Controls[3].Enabled = true;
            Controls[2].Enabled = false;
            choiseOnGraph = 0;
        }

        public void ourCheckBox_Click2(Object sender, EventArgs e)
        {
            Controls[2].Enabled = true;
            Controls[3].Enabled = false;
            choiseOnGraph = 1;
        }
        public void ourButtonTOChange_1(Object sender, EventArgs e)
        {
            ourChoiseToChange = 0;

            doWeNeedToChangeAnomData = false;
        }
        public void ourButtonTOChange_2(Object sender, EventArgs e)
        {
            ourChoiseToChange = 1;
        }
        public void ourButtonTOChange_3(Object sender, EventArgs e)
        {
            ourChoiseToChange = 2;
        }
        public void ourButtonTOChange_4(Object sender, EventArgs e)
        {
            ourChoiseToChange = 3;
        }

        public void ourButtonTOKillAnom(Object sender, EventArgs e)
        {
            doWeNeedToChangeAnomData = true;
        }


        private void ourChoice1_Our2Lab(Object sender, EventArgs e)
        {
            ourChoice = 1;
        }

        private void ourChoice2_Our2Lab(Object sender, EventArgs e)
        {
            ourChoice = 2;
        }
        private void ourChoice3_Our2Lab(Object sender, EventArgs e)
        {
            ourChoice = 3;
        }
        private void ourChoice4_Our2Lab(Object sender, EventArgs e)
        {
            ourChoice = 4;
        }
        private void ourChoice5_Our2Lab(Object sender, EventArgs e)
        {
            ourChoice = 5;
        }

        //Вейбулла

        private void createOurHrap(List<double> ourNum1)
        {
            Series series1 = new Series();
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series4";
            series1.ChartType = SeriesChartType.Line;
            chart1.Series.Add(series1);

            MyMath ourOperation = new MyMath();

            List<double> ourNum2 = new List<double> { };

            if (ourChoice == 1)//Вейбулла
            {

                double ourA = ourOperation.middleMomentumN_Find(ourNum1, 3) / Math.Pow(ourOperation.middleMomentumN_Find(ourNum1, 2), 3.0 / 2.0);

                Console.WriteLine($"{ourA}");
                double ourAlpha = Math.Exp(-ourA);

                double ourBeta = 1.2;

                foreach (var el in ourNum1)
                {
                    if (el >= 0)
                    {
                        double ourT = ourBeta / ourAlpha;

                        double ourT2 = Math.Pow(el, ourBeta - 1.0);

                        ourT *= ourT2;

                        ourT2 = Math.Exp(-(Math.Pow(el, ourBeta) / ourAlpha));

                        ourT *= ourT2;

                        Console.WriteLine($"{ourT} {ourT2} {el} {ourBeta} {ourAlpha}");

                        ourT *= ourHTemp;

                        ourNum2.Add(ourT);
                    }
                }

                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }//Вейбулла
            else if (ourChoice == 2)
            {
                double ourM = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);

                double ourN1 = ourNum1.Count;

                ourN1 = ourN1 / (ourN1 - 1);

                double ourO = ourN1 * Math.Sqrt((ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));
                

                foreach (var el in ourNum1)
                {
                    double ourT = 1 / (ourO * Math.Sqrt(2 * Math.PI));
                    double ourT2 = Math.Exp(-((Math.Pow(el - ourM, 2) / (2 * Math.Pow(ourO, 2)))));

                    ourT *= ourT2;

                    ourT *= ourHTemp;


                    ourNum2.Add(ourT);

                }
                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }//Нормальний
            else if (ourChoice == 3)
            {
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);
                double ourA = ourDeltaX - Math.Sqrt(3 * (ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));
                double ourB = ourDeltaX + Math.Sqrt(3 * (ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));

                foreach (var el in ourNum1)
                {
                    if (el < ourA || el > ourB)
                    {
                        ourNum2.Add(0);
                    }
                    else
                    {
                        ourNum2.Add(ourHTemp / (ourB - ourA));
                    }
                }
                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }//Рівномірний
            else if (ourChoice == 4)//Експоненціальний
            {
                double ourLambda = 1 / ourOperation.ourDeltaX_Find(ourNum1);
                foreach (var el in ourNum1)
                {
                    if (el < 0)
                    {
                        ourNum2.Add(0);
                    }
                    else
                    {
                        ourNum2.Add(ourHTemp  * ourLambda * (Math.Exp((-ourLambda) * el)));
                    }
                }
                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    }
                    catch (Exception)
                    {

                    }
                }

            }
            else if (ourChoice == 5)//Арксинус
            {
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);
                double ourA = Math.Sqrt(2) * Math.Sqrt(ourDeltaXX - Math.Pow(ourDeltaX, 2.0));
                foreach (var el in ourNum1)
                {
                    if (el > -ourA && el < ourA)
                    {
                        double ourAnswer = 1 / (3.14 * Math.Sqrt(ourA * ourA - el * el));
                        ourAnswer *= ourHTemp;
                        ourNum2.Add(ourAnswer); 
                    }
                    
                }
                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    } 
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private double Erf(double x)
        {
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }
        double[] a = new double[8]
            {
                66500,
                -36100,
                -31400,
                866.97,
                629.33,
                -379.8,
                24.77,
                -1.716

            };
        double[] b = new double[8]
            {
                -115000,
                -135000,
                4760,
                22500,
                -3107.8,
                -1015.2,
                315.35,
                -30.84
            };
        private double Gamma(double e)
        {

            double ourAnswer = 0;

            if(e > 0 && e < 1)
            {
                ourAnswer = Gamma(e + 1) / e;
            }
            else if (e >= 1 && e <= 2)
            {
                double x = e - 1;

                double ourp1 = 0;
                double ourp2 = 0;

                for(int i = 0; i < 8; i++)
                {
                    ourp1 = ourp1 + a[i] * Math.Pow(x, i);

                    ourp2 = ourp2 + b[i]     * Math.Pow(x, i - 1) + Math.Pow(x, 8);
                }
                ourAnswer = 1 + (ourp1 / ourp2);

                Console.WriteLine($"{ourAnswer} 123");

                return ourAnswer;
            }
            else if (e > 2)
            {
                ourAnswer = (e - 1) * Gamma(e - 1);
            }
            else if(e < 0)
            {
                ourAnswer = Gamma(1 - e);
            }
            return ourAnswer;
        }
        //ИСПРАВИТЬ ЛЯМБДУ!
        private double lambdaVeibula(List<double> ourNum1)
        {
            MyMath ourOperation = new MyMath();
            double ourA = ourOperation.middleMomentumN_Find(ourNum1, 3) / Math.Pow(ourOperation.middleMomentumN_Find(ourNum1, 3), 3.0 / 2.0);

            double ourAlpha = Math.Exp(-ourA);
            double ourBeta = 0.5;

            List<double> ourNum2 = new List<double> { };
            foreach (var el in ourNum1)
            {
                if (el >= 0)
                {
                    double ourT = 1;

                    double ourT2 = Math.Exp((-(Math.Pow(el, ourBeta) / ourAlpha)));

                    ourT -= ourT2;

                    ourNum2.Add(ourT);

                }
                else
                {
                    ourNum2.Add(0);
                }
            }

            double ourAnswer = 0;

            for (int i = 0; i < ourNum1.Count - 1; i++)
            {
                if (ourNum1[i] > 0 && ourNum2[i] > 0)
                {
                    double a = Math.Log(Math.Log(1 / (1 - ourNum2[i]))) - ourA - ourBeta * Math.Log(ourNum1[i]);
                    ourAnswer += Math.Pow(a, 2);
                }
            }
            ourAnswer = ourAnswer / (ourNum1.Count - 3);

            return ourAnswer;
        }


        private double ourFFind(double ourNum)
        {
            double ourAnswer = 0;
            if (ourNum < 0)
            {
                ourAnswer = 1;
            }

            ourNum = Math.Abs(ourNum);
            double ourNum1 = 1;

            double ourNum2 = 1 / (Math.Sqrt(6.28));
            double ourNum3 = Math.Exp(-(Math.Pow(ourNum, 2) / 2));
            double ourT = 1 / (1 + 0.2316419 * ourNum);
            double ourNum4 = (0.31938153 * ourT - 0.356563782 * ourT * ourT + 1.781477937 * ourT * ourT * ourT - 1.821255978 * ourT * ourT * ourT * ourT + 1.330274429 * ourT * ourT * ourT * ourT * ourT);

            ourNum1 = ourNum1 - ourNum2 * ourNum3 * ourNum4;

            if (ourAnswer == 1)
                return (ourAnswer - ourNum1);
            else
                return ourNum1;
        }
        private void createOurHrap2(List<double> ourNum1)
        {
            Series series1 = new Series();
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series4";
            series1.ChartType = SeriesChartType.Line;
            chart1.Series.Add(series1);

            MyMath ourOperation = new MyMath();

            List<double> ourNum2 = new List<double> { };

            if (ourChoice == 1)//Вейбулла
            {
                double ourLambda = ourOperation.ourDeltaX_Find(ourNum1);

                double ourA = ourOperation.middleMomentumN_Find(ourNum1, 3) / Math.Pow(ourOperation.middleMomentumN_Find(ourNum1, 2), 3.0 / 2.0);
                double ourAlpha = Math.Exp(-ourA);
                double ourBeta = 1.2;

                foreach (var el in ourNum1)
                {
                    if (el >= 0)
                    {
                        double ourT = 1;

                        double ourT2 = Math.Exp(-(Math.Pow(el, ourBeta) / ourAlpha));

                        Console.WriteLine($"{ourA} {ourBeta} {el} {-(Math.Pow(el, ourBeta) / ourAlpha)} {Math.Exp(-(Math.Pow(el, ourBeta) / ourAlpha))}");

                        ourT -= ourT2;

                        ourNum2.Add(ourT);

                    }
                }

                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }//Вейбулла
            else if (ourChoice == 2)//Нормальний
            {
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);
                double ourM = ourOperation.ourDeltaX_Find(ourNum1);
                double ourN1 = ourNum1.Count;

                ourN1 = ourN1 / (ourN1 - 1);

                double ourO = ourN1 * Math.Sqrt((ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));


                foreach (var el in ourNum1)
                {
                    double ourAnswer = ourFFind((el - ourM) / ourO);

                    Console.WriteLine($"{ourAnswer} {el} {ourM} {ourFFind(0)}");
                    
                    ourNum2.Add(ourAnswer);
                }


                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else if (ourChoice == 3)
            {
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);
                double ourA = ourDeltaX - Math.Sqrt(3 * (ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));
                double ourB = ourDeltaX + Math.Sqrt(3 * (ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));

                foreach (var el in ourNum1)
                {
                    if (el < ourA)
                    {
                        ourNum2.Add(0);
                    }   
                    else if (el >= ourA && el < ourB)
                    {
                        ourNum2.Add((el - ourA) / (ourB - ourA));
                    }
                    else
                    {
                        ourNum2.Add(1);
                    }
                }
                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }//Рівномірний
            else if (ourChoice == 4)//Експоненціальний
            {
                double ourLambda = 1 / ourOperation.ourDeltaX_Find(ourNum1);
                foreach (var el in ourNum1)
                {
                    if (el < 0)
                    {
                        ourNum2.Add(0);
                    }
                    else
                    {
                        ourNum2.Add(1 - (Math.Exp((-ourLambda) * el)));

      
                    }
                }
                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else if (ourChoice == 5)//Арксинус
            {
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);
                double ourA = Math.Sqrt(2) * Math.Sqrt((ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));
                foreach (var el in ourNum1)
                {
                    if (el < -ourA)
                    {
                        ourNum2.Add(0);
                    }
                    else if (el > -ourA && el < ourA)
                    {
                        double temp = 0.5;
                        temp += (0.5 * Math.Asin(el / ourA));

                        if (temp > 1)
                        {
                            temp = 1;
                        }
                        else if (temp < 0)
                        {
                            temp = 0;
                        }
                        ourNum2.Add(temp);
                    }
                    else if (el > ourA)
                    {
                        ourNum2.Add(1);
                    }
                }
                for (int kz = 0; kz < ourNum1.Count; kz++)
                {
                    try
                    {
                        chart1.Series["Series4"].Points.AddXY(ourNum1[kz], ourNum2[kz]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void dataGreedViewCreate(List<double> ourNum1)
        {
            ourData4 = new DataGridView();
            ourData4.Columns.Add("NAME", "NAME");
            ourData4.Columns.Add("COUNT", "COUNT");
            MyMath ourOperation = new MyMath();

            ourData4.Size = new Size(350, 200);
            ourData4.Location = new Point(790, 280);

            if (ourChoice == 1)//Вейбулла
            {

                double ourA = ourOperation.middleMomentumN_Find(ourNum1, 3) / Math.Pow(ourOperation.middleMomentumN_Find(ourNum1, 3), 3.0 / 2.0);

                double ourAlpha = Math.Exp(-ourA);
                double ourBeta = 1.2;
                double ourMatSpodiv = Math.Pow(ourAlpha, 2.0/ourBeta) * Gamma(1 + 1/ourBeta);

                double ourD = Math.Pow(ourAlpha, 2.0 / ourBeta) * (Gamma(1 + 2 / ourBeta) - Math.Pow(Gamma(1 + 1/ourBeta), 2.0));

                double ourE = ourOperation.middleMomentumN_Find(ourNum1, 4) / Math.Pow(ourOperation.middleMomentumN_Find(ourNum1, 2), 2.0) - 3;

                ourData4.Rows.Add("Мат сподівання", ourMatSpodiv);
                ourData4.Rows.Add("Дисперсія", ourD);
                ourData4.Rows.Add("Коеф Асиметрії", ourA);
                ourData4.Rows.Add("Ексцесу", ourE);
            }
            else if (ourChoice == 2)
            {
                double ourM = ourOperation.ourDeltaX_Find(ourNum1);

                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);

                double ourN1 = ourNum1.Count;

                ourN1 = ourN1 / (ourN1 - 1);

                double ourO = ourN1 * Math.Sqrt((ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));

                double ourMatSpodiv = ourM;
                double ourD = ourO * ourO;
                double ourA = 0;

                double ourE = 0;

                ourData4.Rows.Add("Мат сподівання", ourMatSpodiv);
                ourData4.Rows.Add("Дисперсія", ourD);
                ourData4.Rows.Add("Коеф Асиметрії", ourA);
                ourData4.Rows.Add("Ексцесу", ourE);
            }//Нормальний
            else if (ourChoice == 3)
            {
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);
                double ourA = ourDeltaX - Math.Sqrt(3 * (ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));
                double ourB = ourDeltaX + Math.Sqrt(3 * (ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));

                double ourMatSpodiv = (ourA + ourB) / 2;
                double ourD = Math.Pow(-ourA + ourB, 2.0) / 12;
                double ourA1 = 0;

                double ourE = -1.2;

                ourData4.Rows.Add("Мат сподівання", ourMatSpodiv);
                ourData4.Rows.Add("Дисперсія", ourD);
                ourData4.Rows.Add("Коеф Асиметрії", ourA1);
                ourData4.Rows.Add("Ексцесу", ourE);
            }//Рівномірний
            else if (ourChoice == 4)//Експоненціальний
            {
                double ourLambda = 1 / ourOperation.ourDeltaX_Find(ourNum1);

                double ourMatSpodiv = 1 / ourLambda;
                double ourD = 1 / (ourLambda * ourLambda);
                double ourA = 2;

                double ourE = 6;

                ourData4.Rows.Add("Мат сподівання", ourMatSpodiv);
                ourData4.Rows.Add("Дисперсія", ourD);
                ourData4.Rows.Add("Коеф Асиметрії", ourA);
                ourData4.Rows.Add("Ексцесу", ourE);
            }
            else if (ourChoice == 5)//Арксинус
            {
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);
                double ourA1 = Math.Sqrt(2) * Math.Sqrt((ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));

                double ourMatSpodiv = 0;
                double ourD = ourA1 * ourA1 / 2;
                double ourA = 0;

                double ourE = -1.5;

                ourData4.Rows.Add("Мат сподівання", ourMatSpodiv);
                ourData4.Rows.Add("Дисперсія", ourD);
                ourData4.Rows.Add("Коеф Асиметрії", ourA);
                ourData4.Rows.Add("Ексцесу", ourE);
            }

            Controls.Add(ourData4);
        }

        private void dataGreedViewCreate2(List<double> ourNum1)
        {
            ourData4 = new DataGridView();
            ourData4.Columns.Add("", "");
            ourData4.Rows.Add("Довірче оцінювання");
            MyMath ourOperation = new MyMath();

            ourData4.Size = new Size(350, 200);
            ourData4.Location = new Point(1180, 280);

            if (ourChoice == 1)//Вейбулла
            {

                double ourA = ourOperation.middleMomentumN_Find(ourNum1, 3) / Math.Pow(ourOperation.middleMomentumN_Find(ourNum1, 3), 3.0 / 2.0);

                double ourAlpha = Math.Exp(-ourA);
                double ourBeta = 1.2;

                double a11 = ourNum1.Count - 1;
                double a22 = 0;
                for (int i = 0; i < ourNum1.Count; i++)
                {
                    if (ourNum1[i] > 0)
                        a22 += Math.Log(ourNum1[i]);
                }
                double a33 = 0;
                for (int i = 0; i < ourNum1.Count; i++)
                {
                    if(ourNum1[i] > 0)
                        a33 += Math.Pow(Math.Log(ourNum1[i]), 2.0);
                }

                double ourS = lambdaVeibula(ourNum1);

                Console.WriteLine($"{ourS} {a11} {a22} {a33}");

                double ourDA = (a33 * ourS) /(a11 * a33 - a22 * a22);
                double ourDB = (a11 * ourS) / (a11 * a33 - a22 * a22);
                double cov = (a22 * ourS) / (a11 * a33 - a22 * a22);

                cov = -Math.Exp(ourA) * cov;

                ourData4.Columns.Add("A", "A");
                ourData4.Columns.Add("B", "B");

                ourDA = Math.Exp(-2 * ourA) * ourDA;
                ourData4.Rows.Add("Оцінка", ourAlpha, ourBeta);
                ourData4.Rows.Add("Дисперсія", ourDA, ourDB);
                ourData4.Rows.Add("Верхня межа", ourAlpha + Math.Sqrt(ourDA), ourBeta + Math.Sqrt(ourDB));
                ourData4.Rows.Add("Нижня межа", ourAlpha - Math.Sqrt(ourDA), ourBeta - Math.Sqrt(ourDB));
            }
            else if (ourChoice == 2)
            {
                double ourM = ourOperation.ourDeltaX_Find(ourNum1);

                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);

                double ourN1 = ourNum1.Count;

                ourN1 = ourN1 / (ourN1 - 1);

                double ourO = ourN1 * Math.Sqrt((ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));

                double ourDA = Math.Pow(ourO, 2) / ourNum1.Count;

                double ourDB = Math.Pow(ourO, 2) / (2 * ourNum1.Count);

                double cov = 0;

                ourData4.Columns.Add("m", "m");
                ourData4.Columns.Add("SIGMA", "SIGMA");

                ourData4.Rows.Add("Оцінка", ourM, ourO);
                ourData4.Rows.Add("Дисперсія", ourDA, ourDB);
                ourData4.Rows.Add("Верхня межа", ourM + Math.Sqrt(ourDA), ourO + Math.Sqrt(ourDB));
                ourData4.Rows.Add("Нижня межа", ourM - Math.Sqrt(ourDA), ourO - Math.Sqrt(ourDB));
            }//Нормальний
            else if (ourChoice == 3)
            {
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);
                double ourA = ourDeltaX - Math.Sqrt(3 * (ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));
                double ourB = ourDeltaX + Math.Sqrt(3 * (ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));

                double ourDA = Math.Pow((ourB - ourA), 2) / (12 * ourNum1.Count);

                double ourDB = (Math.Pow((ourB - ourA), 4) + 15 * Math.Pow(ourB + ourA, 4) * Math.Pow(ourB - ourA, 2)) / (180 * ourNum1.Count);

                double cov = (ourA + ourB) * Math.Pow(ourB - ourA, 2) / (12 * ourNum1.Count);

                ourData4.Columns.Add("A", "A");
                ourData4.Columns.Add("B", "B");

                ourData4.Rows.Add("Оцінка", ourA, ourB);
                ourData4.Rows.Add("Дисперсія", ourDA, ourDB);
                ourData4.Rows.Add("Верхня межа", ourA + Math.Sqrt(ourDA), ourB + Math.Sqrt(ourDB));
                ourData4.Rows.Add("Нижня межа", ourA - Math.Sqrt(ourDA), ourB - Math.Sqrt(ourDB));
            }//Рівномірний
            else if (ourChoice == 4)//Експоненціальний
            {
                double ourLambda = 1 / ourOperation.ourDeltaX_Find(ourNum1);

                double ourDA = Math.Pow(ourLambda, 2) / ourNum1.Count;

                ourData4.Columns.Add("LAMBDA", "LAMBDA");

                ourData4.Rows.Add("Оцінка", ourLambda);
                ourData4.Rows.Add("Дисперсія", ourDA);
                ourData4.Rows.Add("Верхня межа", ourLambda + Math.Sqrt(ourDA));
                ourData4.Rows.Add("Нижня межа", ourLambda - Math.Sqrt(ourDA));
            }
            else if (ourChoice == 5)//Арксинус
            {
                double ourDeltaX = ourOperation.ourDeltaX_Find(ourNum1);
                double ourDeltaXX = ourOperation.ourDeltaXX_Find(ourNum1);
                double ourA1 = Math.Sqrt(2) * Math.Sqrt((ourDeltaXX - Math.Pow(ourDeltaX, 2.0)));

                double ourDA = Math.Pow(ourA1, 4)/ (8* ourNum1.Count);

                ourData4.Columns.Add("A", "A");

                ourData4.Rows.Add("Оцінка", ourA1);
                ourData4.Rows.Add("Дисперсія", ourDA);

                ourData4.Rows.Add("Верхня межа", ourA1 + Math.Sqrt(ourDA));
                ourData4.Rows.Add("Нижня межа", ourA1 - Math.Sqrt(ourDA));
            }

            Controls.Add(ourData4);
        }

    }
}
