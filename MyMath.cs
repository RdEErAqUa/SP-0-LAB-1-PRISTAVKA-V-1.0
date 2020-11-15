using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace SP_0_LAB_1_PRISTAVKA_V_1._0
{
    class MyMath
    {
        private List<double> ourNumber_STI(String ourNumber)
        {
            List<double> ourNumber1 = new List<double>();
            try
            {
                double[] a = ourNumber.Split(' ').
                        Where(x => !string.IsNullOrWhiteSpace(x)).
                        Select(x => double.Parse(x)).ToArray();
                ourNumber1 = a.ToList();
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Вибачте, введенні некоректні данні");
                return null;
            }
            return ourNumber1;
        }

        public List<double> ourNumber_OO(List<double> ourNumber)
        {
            double[] a = ourNumber.ToArray();
            List<double> ourNumbers = new List<double>();
            bool okcheck = true;
            foreach (var el in a)
            {
                if(el == 0 && okcheck)
                {
                    ourNumbers.Add(el);
                    okcheck = false;
                }
                if (ourNumbers.Find(ele => ele.Equals(el)) == el)
                {
                }
                else
                {
                    ourNumbers.Add(el);
                }
            }
            return ourNumbers;
        }
        
        public List<double> ourNumber_ourP(String ourNumber)
        {
            List <double> ourNumbers = ourNumber_STI(ourNumber);
            if (ourNumbers == null)
            {
                return null;
            }   
            else
            {
                ourNumbers = ourNumber_OO(ourNumbers);
                ourNumbers.Sort();
                return ourNumbers;
            }
        }

        public List<double> ourN_Find(String ourNumber, List<double> ourNum)
        {
            List<double> ourNumbers = ourNumber_STI(ourNumber);
            List<double> ourn = new List<double>();

            foreach (var el in ourNum)
            {
                double d = 0;
                foreach (var el2 in ourNumbers)
                {
                    if (el == el2)
                    {
                        d++;
                    }
                }
                ourn.Add(d);
            }
            return ourn;

        }

        public List<double> ourP_Find(List<double> ourNumber, List<double> ourn)
        {
            List<double> ourP = new List<double>();

            double size = 0;

            foreach(var el in ourn)
            {
                size += el;
            }
            
            foreach(var el in ourn)
            {
                ourP.Add(el / size);
            }

            return ourP;
        }

        public List<double> ourXi_Find(double ourFirstNum, double h, double M)
        {
            List<double> ournum = new List<double>();

            for(double i = 0; i <= M; i++)
            {
                ournum.Add(ourFirstNum + h * i);
            }
            return ournum;
        }

        public List<double> ourPi_Find(List<double> ourNumber, List<double> ourp,double h, double M, double ourFirstNum)
        {
            List<double> ourp1 = new List<double>();
            double ourp2 = 0;

            for (double i = 0; i < (int)M; i++)
            {
                ourp2 = 0;
                for(int k = 0; k < ourNumber.Count; k++)
                {
                    if (ourNumber[k] >= ourFirstNum + h * i && ourNumber[k] < ourFirstNum + h * (i + 1))
                    {
                        ourp2 += ourp[k];
                    }
                }
                ourp1.Add(ourp2);
            }
            return ourp1;
        }
        
        public double ourM_Find(List<double> ourn, double count)
        {
            double ourM = 0;
            if (count < 100)
            {
                switch ((int)(Math.Sqrt(count) % 2))
                {
                    case 0:
                        ourM = Math.Sqrt(count) - 1.0;
                        break;
                    case 1:
                        ourM = Math.Sqrt(count);
                        break;
                }
            }
            else
            {
                switch ((int)(Math.Pow(count, 1.0/3) % 2))
                {
                    case 0:
                        ourM = Math.Pow(count, 1.0 / 3) - 1.0;
                        break;
                    case 1:
                        ourM = Math.Pow(count, 1.0 / 3);
                        break;
                }
            }
            ourM = (double)(int)ourM;
            return ourM;
        }

        public double ourH_Find(List<double> ourNum1, double ourM)
        {
            try{
                double h = (Math.Abs(ourNum1[0]) + Math.Abs(ourNum1.Last())) / ourM;
                return h;
            }
            catch(Exception)
            {
                MessageBox.Show("Something go wrong, please, restart our application");
                Environment.Exit(1);
                return 1;
            }
        }
        //work done
        public List<double> Change_OurNum(List<double> ourNum1, int ourChoise)
        {
            if (ourChoise == 1)//log
            {
                List<double> ourXi = new List<double> { };
                foreach (var el in ourNum1)
                {
                    double ourNum = Math.Log(el);

                    ourXi.Add(ourNum);
                }
                return ourXi;
            }
            else if (ourChoise == 2)//Стандартизация
            {
                double MED = ourMED(ourNum1);
                double MAD = ourMAD(MED);

                List<double> ourXi = new List<double> { };
                foreach(var el in ourNum1)
                {
                    double ourNum = (el - MED);
                    ourNum /= MAD;

                    ourXi.Add(ourNum);
                }
                return ourXi;
            }
            else if (ourChoise == 3)//Зсув
            {
                List<double> ourXi = new List<double> { };
                foreach (var el in ourNum1)
                {
                    double ourNum = ourNum1.First() + el + 0.1;

                    ourXi.Add(ourNum);
                }
                return ourXi;
            }
            else
            {
                return null;
            }
        }

        public double ourMED(List<double> ourNum1)
        {
            if(ourNum1.Count % 2 == 0)
            {
                int k = ourNum1.Count;
                k /= 2;
                double MED = ourNum1[k] + ourNum1[k + 1];
                MED /= 2;
                return MED;
            }
            else
            {
                int k = ourNum1.Count - 1;
                k /= 2;
                double MED = ourNum1[k + 1];
                return MED;
            }
        }

        public double ourMAD(double ourNum1)
        {
            return (1.483 * ourNum1);
        }

        public List<double> ourRound(List<double> ourMatrix, int ourNumber)
        {
            List<double> ourAnswer = new List<double> { };
            foreach (var el in ourMatrix)
            {
                ourAnswer.Add(Math.Round(el, ourNumber));
            }

            return ourAnswer;
        }

        public List<double> deleteAnomData(List<double> ourNum1)
        {
            double a = 0;

            double b = 0;

            double deltaX = ourDeltaX_Find(ourNum1);

            double t1 = 0;

            t1 = Math.Log10(0.04 * ourNum1.Count);
            t1 *= 0.2;
            t1 += 2;

            double EC = ourECE_Find(ourNum1);

            double t2 = 0;

            t2 = EC + 2;
            t2 = Math.Sqrt(t2);
            t2 *= 19;
            t2 += 1;
            t2 = Math.Sqrt(t2);

            double AC = AC_Find(ourNum1);

            double SC = ourSC_Find(ourNum1);

            if(AC < -0.2)
            {
                a = deltaX - t2 * SC;
                b = deltaX + t1 * SC;
            }
            else if (AC > 0.2)
            {
                a = deltaX - t1 * SC;
                b = deltaX + t2 * SC;
            }
            else if (Math.Abs(AC) <= 0.2)
            {
                a = deltaX - t1 * SC;
                b = deltaX + t1 * SC;
            }

            List<double> ourAnswer = new List<double> { };

            foreach(var el in ourNum1)
            {
                if(el <= b && el >= a)
                {
                    ourAnswer.Add(el);
                }
            }

            return ourAnswer;
        }

        public double ourRound(double ourNumber, int ourNumb)
        {
            double ourAnswer = Math.Round(ourNumber, ourNumb);

            return ourAnswer;
        }

        //Вибіркова дисперсія
        //Зсунена
        public double ourS_Find(List<double> ourNum1)
        {
            double S = middleMomentumN_Find(ourNum1, 2);

            S = Math.Sqrt(S);

            return S;
        }
        //Незсунена
        public double ourSC_Find(List<double> ourNum1)
        {
            double SC = 0;
            double deltaX = ourDeltaX_Find(ourNum1);

            for (int i = 0; i < ourNum1.Count; i++)
            {
                SC += Math.Pow((ourNum1[i] - deltaX), 2);
            }

            SC /= (ourNum1.Count - 1);

            SC = Math.Sqrt(SC);

            return SC;
        }


        //Середнє арифметичне
        public double ourDeltaX_Find(List<double> ourNum1) 
        {
            double deltaX = 0;

            for (int i = 0; i < ourNum1.Count; i++)
            {
                deltaX += ourNum1[i];
            }
            deltaX /= ourNum1.Count;

            return deltaX;
        }

        public double ourDeltaXX_Find(List<double> ourNum1)
        {
            double deltaX = 0;

            for (int i = 0; i < ourNum1.Count; i++)
            {
                deltaX = deltaX + (ourNum1[i] * ourNum1[i]);
            }
            deltaX /= ourNum1.Count;

            return deltaX;
        }
        //СЕРЕДНЬОКВАДРАТИЧНЕ
        public double deltaDevX_Find(List<double> ourNum1)
        {
            double deltaDevX = 0;
            double deltaX = ourDeltaX_Find(ourNum1);

            for (int i = 0; i < ourNum1.Count; i++)
            {
                deltaDevX += Math.Pow((ourNum1[i] - deltaX), 2);
            }
            deltaDevX /= ourNum1.Count;

            deltaDevX = Math.Sqrt(deltaDevX);

            return deltaDevX;
        }

        //Коефіцієнт асиметрії
        //Зсуненинй
        public double A_Find(List<double> ourNum1)
        {
            double middleMomentum = middleMomentumN_Find(ourNum1, 3);
            double deltaDevX = deltaDevX_Find(ourNum1);

            double ourA = middleMomentum / Math.Pow(deltaDevX, 3);

            return ourA;
        }
        //Незсунений
        public double AC_Find(List<double> ourNum1)
        {

            double ourA = A_Find(ourNum1);

            int N = ourNum1.Count;

            double ourAC = ((Math.Sqrt(N * (N - 1))) / (N - 2)) * ourA;

            return ourAC;
        }
        //Коєфцієнт ексцесу
        //Зсунений
        public double ourE_Find(List<double> ourNum1)
        {
            double middleMomentum = middleMomentumN_Find(ourNum1, 4);
            double deltaDevX = deltaDevX_Find(ourNum1);

            double ourE  = middleMomentum / Math.Pow(deltaDevX, 4);

            return ourE;
        }
        //Незсунений
        public double ourEC_Find(List<double> ourNum1)
        {
            double ourE = ourE_Find(ourNum1);

            double N = ourNum1.Count;
            double deltaOurE = (N * N - 1) / ((N - 2) * (N - 3)) * ((ourE - 3) + (6 / (N + 1)));


            return deltaOurE;
        }
        //Коефіцієнт контрексцесу
        public double ourECE_Find(List<double> ourNum1)
        {
            double deltaOurE = Math.Abs(ourEC_Find(ourNum1));

            double ourE1 = 1;
            ourE1 /= Math.Sqrt(deltaOurE);

            return ourE1;
        }
        //Варіація пірсона  
        public double ourW_Find(List<double> ourNum1)
        {
            double deltaDevX = deltaDevX_Find(ourNum1);

            double deltaX = ourDeltaX_Find(ourNum1);

            double ourW = deltaDevX / deltaX;

            return ourW;
        }
            
        public double ourTrun_Average_Find(List<double> ourNum1)
        {
            int k = (int)(0.3 * ourNum1.Count);


            double usicheneSeredne = 0;

            for (int i = k; i < ourNum1.Count - k; i++)
            {
                usicheneSeredne += ourNum1[i];
            }

            usicheneSeredne /= (ourNum1.Count - 2 * k);

            return usicheneSeredne;
        }


        //Done from this ---
        public double middleMomentumN_Find(List<double> ourNum1, int ourN)
        {
            double middleMomentum = 0;
            double ourV1 = ourV(ourNum1, 1);

            for (int i = 0; i < ourNum1.Count; i++)
            {
                middleMomentum += Math.Pow((ourNum1[i] - ourV1), ourN);
            }

            middleMomentum /= ourNum1.Count;

            return middleMomentum;
        }
        //базові статистичні характеристики
        public double ourV(List<double> ourNum1, int ourN)
        {
            double ourAnswer = 0;

            for (int i = 0; i < ourNum1.Count; i++)
            {
                ourAnswer += Math.Pow(ourNum1[i], ourN);
            }

            ourAnswer = ourAnswer * (1.0 / (double)ourNum1.Count);

            return ourAnswer;
        }

        // To This ---


        //O(O) знаходження
        //O(deltaX)
        public double ourODeltaX_Find(List<double> ourNum1)
        {
            double ourAnswer = ourSC_Find(ourNum1);

            ourAnswer /= Math.Sqrt(ourNum1.Count);

            return ourAnswer;
        }
        //O(S)
        public double ourOSC_Find(List<double> ourNum1)
        {
            double ourAnswer = ourSC_Find(ourNum1);

            ourAnswer /= Math.Sqrt(2 * ourNum1.Count);

            return ourAnswer;
        }
        //O(A)
        public double ourOAC_Find(List<double> ourNum1)
        {
            double ourBCount = 4 * ourB_Find(ourNum1, 4);
            double ourAnswer = 0;

            ourBCount -= (12 * ourB_Find(ourNum1, 3));
            ourBCount -= (24 * ourB_Find(ourNum1, 2));
            ourBCount += (9 * ourB_Find(ourNum1, 2) * ourB_Find(ourNum1, 1));
            ourBCount += (35 * ourB_Find(ourNum1, 1));
            ourAnswer = ourBCount;

            ourAnswer -= 36;

            double ourn = 1.0 / (4.0 * (double)ourNum1.Count);

            ourAnswer = ourAnswer * ourn;

            if (ourAnswer > 0)
            {
                ourAnswer = Math.Sqrt(ourAnswer);
            }
            else
            {
                ourAnswer = 1;
                ourAnswer -= (12 / (2 * ourNum1.Count + 7));
                ourAnswer *= 6;
                ourAnswer /= ourNum1.Count;
                ourAnswer = Math.Sqrt(ourAnswer);
            }
            return ourAnswer;
        }

        public double ourOEC_Find(List<double> ourNum1)
        {
            double ourAnswer = 0;

            double ourBCount = ourB_Find(ourNum1, 6);

            ourBCount -= (4 * ourB_Find(ourNum1, 4) * ourB_Find(ourNum1, 2));
            ourBCount -= (8 * ourB_Find(ourNum1, 3));
            ourBCount += (4 * ourB_Find(ourNum1, 2) * ourB_Find(ourNum1, 2) * ourB_Find(ourNum1, 2));
            ourBCount -= (ourB_Find(ourNum1, 2) * ourB_Find(ourNum1, 2));
            ourBCount += (16 * ourB_Find(ourNum1, 2) * ourB_Find(ourNum1, 1));
            ourBCount += (16 * ourB_Find(ourNum1, 1));

            ourAnswer = ourBCount;

            double ourn = 1.0 / (double)ourNum1.Count;
            ourAnswer = ourAnswer * ourn;

            ourAnswer = Math.Sqrt(ourAnswer);

            return ourAnswer;
        }
        //Kvantil Studenta
        public double ourKVANTIL_Find(int v)
        {
            double ourAnswer = 0;

            if (v == 1)
                ourAnswer = 12.7;
            else if (v == 2)
                ourAnswer = 4.3;
            else if (v == 3)
                ourAnswer = 3.18;
            else if (v == 4)
                ourAnswer = 2.78;
            else if (v == 5)
                ourAnswer = 2.57;
            else if (v == 6)
                ourAnswer = 2.45;
            else if (v == 7)
                ourAnswer = 2.36;
            else if (v == 8)
                ourAnswer = 2.31;
            else if (v == 9)
                ourAnswer = 2.26;
            else if (v == 10)
                ourAnswer = 2.23;
            else if (v == 11)
                ourAnswer = 2.20;
            else if (v == 12)
                ourAnswer = 2.18;
            else if (v == 13)
                ourAnswer = 2.16;
            else if (v == 14)
                ourAnswer = 2.14;
            else if (v == 15)
                ourAnswer = 2.13;
            else if (v == 16)
                ourAnswer = 2.12;
            else if (v == 17)
                ourAnswer = 2.11;
            else if (v == 18)
                ourAnswer = 2.10;
            else if (v == 19)
                ourAnswer = 2.09;
            else if (v == 20)
                ourAnswer = 2.09;
            else if (v >= 21 && v < 30)
                ourAnswer = 2.05;
            else if (v >= 30 && v < 120)
                ourAnswer = 2.01;
            else if (v >= 120)
                ourAnswer = 1.98;
            return ourAnswer;
        }
        //ourB Find

        public double ourB_Find(List<double> ourNum, int count)
        {
            if (count % 2 == 0)
            {
                int k = count / 2;

                double middleMomentum2k2 = middleMomentumN_Find(ourNum, 2 * k + 2);
                double middleMomentum2 = middleMomentumN_Find(ourNum, 2);

                double ourAnswer = middleMomentum2k2 / (Math.Pow(middleMomentum2, k + 1.0));

                return ourAnswer;
            }
            else
            {
                int k = (count - 1) / 2;
                double middleMomentum2k3 = middleMomentumN_Find(ourNum, 2 * k + 3);
                double middleMomentum2 = middleMomentumN_Find(ourNum, 2);
                double middleMomentum3 = middleMomentumN_Find(ourNum, 3);

                double ourAnswer = (middleMomentum3 * middleMomentum2k3) / (Math.Pow(middleMomentum2, k + 3.0));

                return ourAnswer;
            }
        }

    }
}
