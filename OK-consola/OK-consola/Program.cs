using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace OK_consola
{
    public class Nast
    {
        public int indeks;
        public double wielkosc;
        public Nast nastepny;
    }

    public class Zczytaj
    {
        public int id;
        public double x;
        public double y;
    }


    class Program
    {
        


        static public void LocalSearch(ref double[][] tab, int wierzcholek, int n, int dl_sciezki, ref int[] odwiedzony, ref int[] kolejnosc_optymalna, ref double opt_dlugosc)
        {
            odwiedzony[wierzcholek] = 1;
            kolejnosc_optymalna[dl_sciezki++] = wierzcholek;

            if (dl_sciezki < n)
            {
     

                
                double odleglosc = 0.0;
                int indeks = 0;
                int i = 0;

                //----------------------------DLA SAMEJ MAX ------------------------//

                /*

                for (i = 0; i < n; i++)
                    if (odwiedzony[i] == 0)
                    {

                        if(odleglosc < tab[wierzcholek][i])
                        {
                            odleglosc = tab[wierzcholek][i];
                            indeks = i;
                        }

                       
                    }
                   */     

                
               //-------------------- DLA LOSOWEGO WEJSCIA ----------------------------//
               

                for (i = 0; i < n; i++)
                    if (odwiedzony[i] == 0)
                    {
                        
                            odleglosc = tab[wierzcholek][i];
                            indeks = i;

                            
                        break;
                       }


                opt_dlugosc += odleglosc;


                LocalSearch(ref tab, indeks, n, dl_sciezki, ref odwiedzony, ref kolejnosc_optymalna, ref opt_dlugosc);

            }
            
        }


        static public void Rotuj(ref double[][] tab, ref int[] kolejnosc_optymalna, int n, ref double opt_dlugosc)
        {
           
            double odl = opt_dlugosc;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            TimeSpan ts = new TimeSpan(0, 0, 50);

            Random rnd = new Random();
            int testowa = 1;

            double T = 0.400;
            

            while ( stopWatch.Elapsed < ts)
            {
                if(T < 0.010)
                {
                    T = 0.00001;
                }
                else
                {
                    T -= 0.00001;
                }
                

                odl = opt_dlugosc;

                testowa++;

               int test = (int)(n / 4);

               if(test < 1) { test = 1; }
               

                int qaz = rnd.Next(1, test);

                //Console.WriteLine("Losowa" + qaz);

               
                    
                        for (int i = 1; i < (n - qaz) && stopWatch.Elapsed < ts; i += qaz)
                        {
                            odl = opt_dlugosc;

                            int prawy_indeks = rnd.Next(i + 1, n - 1);
                            
                            odl -= tab[kolejnosc_optymalna[i]][kolejnosc_optymalna[i + 1]];
                            odl -= tab[kolejnosc_optymalna[prawy_indeks]][kolejnosc_optymalna[prawy_indeks - 1]];

                            odl += tab[kolejnosc_optymalna[i]][kolejnosc_optymalna[prawy_indeks - 1]];
                            odl += tab[kolejnosc_optymalna[prawy_indeks]][kolejnosc_optymalna[i + 1]];


                    //Console.WriteLine("OPT = " + opt_dlugosc + " TMP = " + odl);


                            if (odl < opt_dlugosc)
                            {
                                //Console.WriteLine("JESTEM");
                                int[] przenies = new int[prawy_indeks - i - 1];

                                for (int k = 0, j = (prawy_indeks - 1); k < (prawy_indeks - i - 1); k++, j--)
                                {
                                    przenies[k] = kolejnosc_optymalna[j];
                                }

                                for (int j = 0, k = (i + 1); k < prawy_indeks; k++, j++)
                                    kolejnosc_optymalna[k] = przenies[j];
                                

                                opt_dlugosc = odl;

                            } //odl < opt_dlugosc

                            //obecne rozwiązanie jest gorsze lecz sprawdzamy zgododnie ze wzorem
                            if(odl > opt_dlugosc)
                            {
                                double tmp_delta = opt_dlugosc - odl;

                                double tmp_potega = tmp_delta / T;

                                double war = Math.Exp(tmp_potega);

                        //Console.WriteLine("TMP_delta = " + tmp_delta + " T = " + T  + " WAR = " + war);

                                double losowa_double = rnd.NextDouble() * (1 - 0) + 0;

                                if(losowa_double < war)
                                {
                                    int[] przenies = new int[prawy_indeks - i - 1];

                                    for (int k = 0, j = (prawy_indeks - 1); k < (prawy_indeks - i - 1); k++, j--)
                                    {
                                        przenies[k] = kolejnosc_optymalna[j];
                                    }

                                    for (int j = 0, k = (i + 1); k < prawy_indeks; k++, j++)
                                        kolejnosc_optymalna[k] = przenies[j];



                                    opt_dlugosc = odl;

                                }


                               }//else



                        } //for przedzialy

            }//while
            
        } //funkcjaRotuj


        static void Main(string[] args)
        {
           

            //-----------------------------------WCYZTYWANIE ZE STANDARD IO ------------------//

                        List<Zczytaj> lista = new List<Zczytaj>();

                        string zdanie;
                        int line = 0;


                        while (!string.IsNullOrEmpty(zdanie = Console.ReadLine()))
                        {
                            line++;
                            Zczytaj zczytaj = new Zczytaj();
                            string[] rozdziel = zdanie.Split(' ');
                            zczytaj.id = Convert.ToInt32(rozdziel[0]);
                            zczytaj.x = Convert.ToDouble(rozdziel[1]);
                            zczytaj.y = Convert.ToDouble(rozdziel[2]);

                            lista.Add(zczytaj);
                        }


                        double[][] tab = new double[line][];

                        for (int i = 0; i < line; i++)
                        {
                            tab[i] = new double[line];
                        }

                        int[] tab_indeks = new int[line];


                        for (int i = 0; i < line; i++)
                        {
                            tab_indeks[i] = lista[i].id;

                            for (int j = 0; j < line; j++)
                            {
                                if (i != j)
                                {
                                    double x1 = lista[i].x;
                                    double y1 = lista[i].y;
                                    double x2 = lista[j].x;
                                    double y2 = lista[j].y;

                                    double wynik1 = Math.Pow((x1 - x2), 2);
                                    double wynik2 = Math.Pow((y1 - y2), 2);
                                    double wynik3 = wynik1 + wynik2;

                                    //Console.WriteLine(wynik1 + " " + wynik2);
                                    tab[i][j] = Math.Sqrt(wynik3);
                                    tab[j][i] = Math.Sqrt(wynik3);

                                }           

                            }

                        }

            int n = line;

           
                
                    int[] odwiedzony = new int[n];
                    for (int i = 0; i < n; i++)
                        odwiedzony[i] = 0;

                    
                    int[] kolejnosc_all = new int[n];


                    int dl_sciezki = 0;

                    double opt_dlugosc = 0.0;
                   

                    int[] kolejnosc_optymalna = new int[n];

                   
             //Wyznacza pierwsze rozwiązanie      
            
            LocalSearch(ref tab,0, n, dl_sciezki, ref odwiedzony, ref kolejnosc_optymalna, ref opt_dlugosc);

           

            opt_dlugosc += tab[kolejnosc_optymalna[n - 1]][0]; //musimy do pierwszego dojść jeszcze z ostatniego
            
            //Funkcja zamiany

            Rotuj(ref tab, ref kolejnosc_optymalna, n, ref opt_dlugosc);


            for (int j = 0; j < n; j++)
                Console.WriteLine(tab_indeks[kolejnosc_optymalna[j]].ToString());
          
            string woda = Console.ReadLine();
        }
    }
}
