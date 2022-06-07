using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace 톨게이트프로그램
{
    
    public partial class 톨게이트 : Form
    {
        private List<Point> mainLoad_WayPoint = new List<Point>();
        private List<Point> tol_WayPoint = new List<Point>();

        private int CarNameCount = 1;
        private int Past_loadNum = -1;
        private Button Past_Car;

        public 톨게이트()
        {
            InitializeComponent();
                       
            mainLoad_WayPoint.Add(new Point(656, 27));                  //1차선
            mainLoad_WayPoint.Add(new Point(537, 27));                  //2차선
            mainLoad_WayPoint.Add(new Point(422, 27));                  //3차선

            tol_WayPoint.Add(new Point(3, 213));                     //톨게이트진입점1
            tol_WayPoint.Add(new Point(3, 143));                     //톨게이트진입점2
            tol_WayPoint.Add(new Point(3, 73));                    //톨게이트진입점3
            tol_WayPoint.Add(new Point(3, 3));                    //톨게이트진입점4
        }

        private void 톨게이트_Load(object sender, EventArgs e)
        {
            tol_1.BackColor = Color.Red;
            tol_4.BackColor = Color.Blue;
            tol_7.BackColor = Color.Yellow;
        }

        private void CarMake_Click(object sender, EventArgs e)
        {
            if (CarTimer.Enabled == false && TolTimer.Enabled == false)
            {
                CarTimer.Start();
                CarTimer.Tick += new EventHandler(CarStart_Event);
                CarTimer.Enabled = true;

                TolTimer.Start();
                TolTimer.Tick += new EventHandler(TolStart_Event);
                TolTimer.Enabled = true;

                HiPass.Start();
                HiPass.Tick += new EventHandler(HiStart_Event);
                HiPass.Enabled = true;

                CarMake.Enabled = false;
            }
            else if (CarTimer.Enabled == false && TolTimer.Enabled == true)
            {
                CarTimer.Start();
                CarTimer.Tick += new EventHandler(CarStart_Event);
                CarTimer.Enabled = true;
            }
        }

        void CarStart_Event(object sender, System.EventArgs e)
        {
            Random rand = new Random();
            Button btn = new Button();

            int loadNum = rand.Next(0, 3);

            btn.Text = CarNameCount++.ToString();
            btn.BackColor = Color.Tomato;
            btn.Font = new Font("consolas", 12);
            btn.Size = new Size(36, 64);
            btn.FlatStyle = FlatStyle.Popup;
            btn.Location = mainLoad_WayPoint[loadNum];
            pictureBox1.Controls.Add(btn);

            if (Past_Car != null)
            {
                List<car> list = new List<car>();
                List<Panel> tol_List = new List<Panel>();
                List<Button> tol_Light = new List<Button>();

                tol_List.Add(panel_1);
                tol_List.Add(panel_2);
                tol_List.Add(panel_3);
                tol_List.Add(panel_4);
                tol_List.Add(panel_5);
                tol_List.Add(panel_6);
                tol_List.Add(panel_7);
                tol_List.Add(panel_8);
                tol_List.Add(panel_9);

                tol_Light.Add(tol_1);
                tol_Light.Add(tol_2);
                tol_Light.Add(tol_3);
                tol_Light.Add(tol_4);
                tol_Light.Add(tol_5);
                tol_Light.Add(tol_6);
                tol_Light.Add(tol_7);
                tol_Light.Add(tol_8);
                tol_Light.Add(tol_9);

                for (int i = 0; i < 9; i++)
                {
                    car _car = new car();
                    _car.road_num = i;
                    _car.car_count = tol_List[i].Controls.Count;
                    list.Add(_car);
                }
                QuickSort hi_pass = new QuickSort();
                hi_pass.sort(list, 0, 8);

                int[] input_array = new int[9];
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (i == list[j].road_num)
                        {
                            input_array[i] = list[j].car_count;
                        }
                    }
                }

                //1차선 함수
                if (Past_loadNum == 0)
                {
                    int light = hi_pass_method(input_array);


                    if (light != 0)
                    {
                        tol_Light[0].BackColor = Color.Gray;
                        tol_Light[1].BackColor = Color.Gray;
                        tol_Light[2].BackColor = Color.Gray;

                        tol_Light[light - 1].BackColor = Color.Red;

                        if (tol_List[light - 1].Controls.Count < 4)
                        {
                            Past_Car.Location = tol_WayPoint[tol_List[light - 1].Controls.Count];
                            tol_List[light - 1].Controls.Add(Past_Car);
                        }
                        else
                        {
                            CarTimer.Stop();
                            CarMake.Enabled = true;
                        }
                    }
                }
                //2차선
                else if (Past_loadNum == 1)
                {
                    int light = mid_road_method(input_array);


                    if (light != 0)
                    {
                        tol_Light[3].BackColor = Color.Gray;
                        tol_Light[4].BackColor = Color.Gray;
                        tol_Light[5].BackColor = Color.Gray;

                        tol_Light[light - 1].BackColor = Color.Blue;

                        if (tol_List[light - 1].Controls.Count < 4)
                        {
                            Past_Car.Location = tol_WayPoint[tol_List[light - 1].Controls.Count];
                            tol_List[light - 1].Controls.Add(Past_Car);
                        }
                        else
                        {
                            try
                            {
                                Past_Car.Location = tol_WayPoint[tol_List[6].Controls.Count];
                                tol_List[6].Controls.Add(Past_Car);
                            } catch(Exception ex)
                            {
                                CarTimer.Stop();
                                CarMake.Enabled = true;
                            }
                        }
                    }
                }
                //3차선
                else if (Past_loadNum == 2)
                {
                    int light = final_road_method(input_array);

                    if (light != 0)
                    {
                        tol_Light[6].BackColor = Color.Gray;
                        tol_Light[7].BackColor = Color.Gray;
                        tol_Light[8].BackColor = Color.Gray;

                        tol_Light[light - 1].BackColor = Color.Yellow;

                        if (tol_List[light - 1].Controls.Count < 4)
                        {
                            Past_Car.Location = tol_WayPoint[tol_List[light - 1].Controls.Count];
                            tol_List[light - 1].Controls.Add(Past_Car);
                        }
                        else
                        {
                            try
                            {
                                Past_Car.Location = tol_WayPoint[tol_List[5].Controls.Count];
                                tol_List[5].Controls.Add(Past_Car);
                            }
                            catch (Exception ex)
                            {
                                CarTimer.Stop();
                                CarMake.Enabled = true;
                            }
                    }
                    }
                }
            }
            Past_Car = btn;
            Past_loadNum = loadNum;
        }
        void TolStart_Event(object sender, System.EventArgs e)
        {
            Tol_Carlocation(panel_4);
            Tol_Carlocation(panel_5);
            Tol_Carlocation(panel_6);
            Tol_Carlocation(panel_7);
            Tol_Carlocation(panel_8);
            Tol_Carlocation(panel_9);
        }

        void HiStart_Event(object sender, System.EventArgs e)
        {
            Tol_Carlocation(panel_1);
            Tol_Carlocation(panel_2);
            Tol_Carlocation(panel_3);
        }

        void Tol_Carlocation(Panel panelTemp)
        {
            if (panelTemp.Controls.Count > 0)
            {
                for (int i = 0; i < panelTemp.Controls.Count - 1; i++)
                {
                    panelTemp.Controls[i].Text = panelTemp.Controls[i + 1].Text;
                }
                panelTemp.Controls.RemoveAt(panelTemp.Controls.Count - 1);
            }
        }


        public static int hi_pass_method(int[] input_array)
        {
            if (input_array[0] >= 9 && input_array[1] >= 9 && input_array[2] >= 9)                          //hi-pass 도로 모두 막혔을때 옆 두차선 고려
            {
                if (input_array[3] <= input_array[4])
                {
                    return 4;
                }
                else if (input_array[4] < input_array[3])
                {
                    return 5;
                }
                else
                {
                    return 0;   //모든 차선 막힌상태
                }
            }
            else if (input_array[0] < 9 || input_array[1] < 9 || input_array[2] < 9)                         //hi-pass 도로 정체 아닐때
            {
                if (input_array[0] <= input_array[1] && input_array[0] <= input_array[2])
                {
                    return 1;
                }
                else if (input_array[1] <= input_array[2] && input_array[1] < input_array[0])
                {
                    return 2;
                }
                else if (input_array[2] < input_array[0] && input_array[2] <= input_array[1])
                {
                    return 3;
                }
                else
                {
                    return 0;
                }
            }
            else
                return 0;

        }
        public static int mid_road_method(int[] input_array)
        {
            if (input_array[3] >= 5 && input_array[4] >= 5 && input_array[5] >= 5)           //전부 초과 일때
            {
                if (input_array[6] <= input_array[7])
                {
                    return 7;
                }
                else if (input_array[7] < input_array[6])
                {
                    return 8;
                }
                else
                {
                    return 0;   //중간차선, 우측차선2도로 모두 막힘
                }
            }
            else if (input_array[3] < 4 || input_array[4] < 4 || input_array[5] < 4 || input_array[6] < 4)              //전부 초과 아닐때          
            {
                if (input_array[3] <= input_array[4] && input_array[3] <= input_array[5])
                {
                    return 4;
                }
                else if (input_array[4] <= input_array[5] && input_array[4] < input_array[3])
                {
                    return 5;
                }
                else if (input_array[5] < input_array[4] && input_array[5] <= input_array[3])
                {
                    return 6;
                }
                else
                {
                    return 0;
                }
            }
            else
                return 0;
        }
        public static int final_road_method(int[] input_array)
        {
            if (input_array[6] >= 5 && input_array[7] >= 5)
            {
                if (input_array[5] <= input_array[4])
                {
                    return 6;
                }
                else if (input_array[4] < input_array[5])
                {
                    return 5;
                }
                else
                    return 0;           //모든차선 막힘
            }
            else if (input_array[6] < 4 || input_array[7] < 4 || input_array[8] < 4)
            {
                if (input_array[6] <= input_array[7] && input_array[6] <= input_array[8])
                {
                    return 7;
                }
                else if (input_array[6] <= input_array[8] && input_array[7] < input_array[6])
                {
                    return 8;
                }
                else if (input_array[8] < input_array[6] && input_array[8] <= input_array[7])
                {
                    return 9;
                }
                else
                {
                    return 0;
                }
            }
            else
                return 0;
        }
        private void CarTimer_Tick(object sender, EventArgs e)
        {

        }

        private void HiPass_Tick(object sender, EventArgs e)
        {

        }
    }
}
