using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Tomasulo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            init();
        }
        public static string[] fRegNames =
        {
            "F0","F2","F4","F6","F8","F10","F12","F14","F16","F18","F20","F22","F24","F26","F28","F30"
        };
        public static string[] rRegNames =
        {
            "R0","R1","R2","R3","R4","R5","R6","R7",
            "R8","R9","R10","R11","R12","R13","R14","R15",
            "R16","R17","R18","R19","R20", "R21","R22","R23",
            "R24","R25","R26","R27","R28","R29","R30","R31"
        };
        public static string[] Imm =
        {
             "0","1","2","3","4","5","6","7",
            "8","9","10","11","12","13","14","15",
            "16","17","18","19","20", "21","22","23",
            "24","25","26","27","28","29","30","31"
        };
        public static string nullName = "NULL";

        public static string[] instrType = { "L.D", "NOP", "ADD.D", "DIV.D", "MULT.D", "SUB.D" };

        public static string[] resultNames = { "M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8", "M9", "M10" };
        static int resultIndex = 0;

        public ComboBox[] comInst;
        public ComboBox[] comDst;
        public ComboBox[] comSrcl;
        public ComboBox[] comSrcr;

        private int loadTime;
        private int addTime;
        private int multTime;
        private int divTime;

        private int cycle;

        private int ip;

        public class Instruction
        {
            public int op;
            public int dst;
            public int src1;
            public int src2;

            public int r;
            public int state;
            public int execTime;

            public Instruction(int _op, int _dst, int _src1, int _src2)
            {
                op = _op;
                dst = _dst;
                src1 = _src1;
                src2 = _src2;
            }
            public Instruction() { }
            public string myToString()
            {
                string strTemp = instrType[op] + " " + fRegNames[dst] + ",";
                if (op == 0)//L.D
                {
                    strTemp = strTemp + Imm[src1] + "(" + rRegNames[src2] + ")";
                }
                else
                {
                    strTemp = strTemp + fRegNames[src1] + "," + fRegNames[src2];
                }
                return strTemp;
            }
        }

        List<Instruction> instrs;

        void init()
        {
            comInst = new ComboBox[] { comInst1, comInst2, comInst3, comInst4, comInst5, comInst6, comInst7, comInst8, comInst9, comInst10 };
            comDst = new ComboBox[] { comDst1, comDst2, comDst3, comDst4, comDst5, comDst6, comDst7, comDst8, comDst9, comDst10 };
            comSrcl = new ComboBox[] { comSrc1, comSrc2, comSrc3, comSrc4, comSrc5, comSrc6, comSrc7, comSrc8, comSrc9, comSrc10 };
            comSrcr = new ComboBox[] { comSrc11, comSrc12, comSrc13, comSrc14, comSrc15, comSrc16, comSrc17, comSrc18, comSrc19, comSrc20 };
            for (int i = 0; i < comInst.Length; i++)
            {
                comInst[i].SelectedIndex = 0;
            }
            InitForm();
            loadTime = Convert.ToInt32(textBox1.Text);
            addTime = Convert.ToInt32(textBox2.Text);
            multTime = Convert.ToInt32(textBox3.Text);
            divTime = Convert.ToInt32(textBox3.Text);
        }

        private void InitForm()
        {
            for(int i=0;i<9;i++)
            {
                instrStateTable.Rows.Add();
                if(i<2) loadRSTable.Rows.Add();
                if(i<4) RSTable.Rows.Add();
                if(i<1) RegTable.Rows.Add();
            }
            int[,] defaultIndex = new int[10,4]{
            {0,3,21,2},{0,1,20,3},
            {4,0,1,2},{5,4,3,1},
            {3,5,0,3},{2,3,4,1},
            {1,0,0,0},{1,0,0,0},
            {1,0,0,0},{1,0,0,0}
            };

            for(int i=0;i<10;i++)
            {
                comInst[i].SelectedIndex = defaultIndex[i,0];
                comDst[i].SelectedIndex = defaultIndex[i,1];
                comSrcl[i].SelectedIndex = defaultIndex[i,2];
                comSrcr[i].SelectedIndex = defaultIndex[i,3];
            }
        }

        private void comInst_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("HELLO WORLD!");
            ComboBox comTemp = (ComboBox)sender;
            int i = 0;
            for(i=0;i<comInst.Length;i++)
            {
                if (comTemp == comInst[i]) break;
            }
            comDst[i].Items.Clear();
            comSrcl[i].Items.Clear();
            comSrcr[i].Items.Clear();
            if (comInst[i].SelectedIndex == 1)//NOP
            {
                comDst[i].Items.Add(nullName);
                comSrcr[i].Items.Add(nullName);
                comSrcl[i].Items.Add(nullName);

            }
            else if (comInst[i].SelectedIndex == 0)//L.D
            {
                for (int j = 0; j < fRegNames.Length; j++)
                {
                    comDst[i].Items.Add(fRegNames[j]);
                }
                for (int j = 0; j < Imm.Length; j++)
                {
                    comSrcl[i].Items.Add(Imm[j]);
                }
                for (int j = 0; j < rRegNames.Length; j++)
                {
                    comSrcr[i].Items.Add(rRegNames[j]);
                }
            }
            else
            {
                for (int j = 0; j < fRegNames.Length; j++)
                {
                    comDst[i].Items.Add(fRegNames[j]);
                }
                for (int j = 0; j < fRegNames.Length; j++)
                {
                    comSrcl[i].Items.Add(fRegNames[j]);
                }
                for (int j = 0; j < fRegNames.Length; j++)
                {
                    comSrcr[i].Items.Add(fRegNames[j]);
                }
            }
            comDst[i].SelectedIndex = 0;
            comSrcr[i].SelectedIndex = 0;
            comSrcl[i].SelectedIndex = 0;
        }

        private void butExec_Click(object sender, EventArgs e)
        {
            startToExec();
        }

        private void startToExec()
        {
            panelDisplay.Visible = true;

            if (cycle >= 5) butBack5Steps.Enabled = true;
            else butBack5Steps.Enabled = false;

            if (cycle == 0) butBackSingle.Enabled = false;
            else butBackSingle.Enabled = true;

            
            instrs = new List<Instruction>();
            int i = 0;
            while (comInst[i].SelectedIndex != 1)
            {
                instrs.Add(new Instruction(
                    comInst[i].SelectedIndex, comDst[i].SelectedIndex,
                    comSrcl[i].SelectedIndex, comSrcr[i].SelectedIndex)
                    );
                i++;
            }
            cycle = 0;
            ip = 0;
            resultIndex = 0;

            for (i = 0; i < instrs.Count; i++)
            {
                instrStateTable.Rows[i].Cells[0].Value = instrs[i].myToString();
            }
            for (i = 0; i < 3; i++)
            {
                loadRSTable.Rows[i].Cells[0].Value = "load" + (i + 1).ToString();
                loadRSTable.Rows[i].Cells[1].Value = "N";
            }
            for (i = 0; i < 5; i++)
            {
                if (i < 3) RSTable.Rows[i].Cells[1].Value = "Add" + (i + 1).ToString();
                else RSTable.Rows[i].Cells[1].Value = "Mult" + (i - 2).ToString();
                RSTable.Rows[i].Cells[2].Value = "N";
            }
            labelCurCir.Text = cycle.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void butExecSingle_Click(object sender, EventArgs e)
        {
            execSingleTime();
        }
        
        private void execSingleTime()
        {
            cycle++;
            if (cycle >= 5) butBack5Steps.Enabled = true;
            else butBack5Steps.Enabled = false;

            if (cycle == 0) butBackSingle.Enabled = false;
            else butBackSingle.Enabled = true;
            labelCurCir.Text = cycle.ToString();
            // ******
            for(int i = 0;i<ip;i++)
            {
                Instruction instrTemp2 = instrs[i];
                // 仍处于发射状态
                if(instrTemp2.state == 0)
                {
                    if(instrTemp2.op == 2 || instrTemp2.op == 5 || instrTemp2.op == 3 || instrTemp2.op == 4)
                    {
                        //第一个源操作数没有被占用
                        if (RSTable.Rows[instrTemp2.r].Cells[4].Value != null) ;
                        else if (RegTable.Rows[0].Cells[instrTemp2.src1].Value == null)
                        {
                            RSTable.Rows[instrTemp2.r].Cells[4].Value = "R[" + fRegNames[instrTemp2.src1] + "]";
                        }
                        //已经计算出值
                        else if(RegTable.Rows[1].Cells[instrTemp2.src1].Value != null)
                        {
                            RSTable.Rows[instrTemp2.r].Cells[4].Value = RegTable.Rows[1].Cells[instrTemp2.src1].Value;
                            RSTable.Rows[instrTemp2.r].Cells[6].Value = null;
                        }
                        else
                        {
                            RSTable.Rows[instrTemp2.r].Cells[6].Value = RegTable.Rows[0].Cells[instrTemp2.src1].Value;
                        }
                        //第二个源操作数没有被占用
                        if (RSTable.Rows[instrTemp2.r].Cells[5].Value != null) ;
                        else if (RegTable.Rows[0].Cells[instrTemp2.src2].Value == null)
                        {
                            RSTable.Rows[instrTemp2.r].Cells[5].Value = "R[" + fRegNames[instrTemp2.src2] + "]";
                        }
                        //已经计算出值
                        else if (RegTable.Rows[1].Cells[instrTemp2.src2].Value != null)
                        {
                            RSTable.Rows[instrTemp2.r].Cells[5].Value = RegTable.Rows[1].Cells[instrTemp2.src2].Value;
                            RSTable.Rows[instrTemp2.r].Cells[7].Value = null;
                        }
                        else
                        {
                            RSTable.Rows[instrTemp2.r].Cells[7].Value = RegTable.Rows[0].Cells[instrTemp2.src2].Value;
                        }
                        if(RSTable.Rows[instrTemp2.r].Cells[4].Value != null && RSTable.Rows[instrTemp2.r].Cells[5].Value != null)
                        {
                            instrTemp2.state++;
                            if(instrTemp2.op == 2 || instrTemp2.op == 5)
                            {
                                instrTemp2.execTime = 2;
                            }
                            else if(instrTemp2.op == 3)
                            {
                                instrTemp2.execTime = 40;
                            }
                            else
                            {
                                instrTemp2.execTime = 10;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The instruction in state issue is in L.D type");
                        return;
                    }
                }
                //执行状态
                else if(instrTemp2.state == 1)
                {
                    // L.D 执行
                    if(instrTemp2.op == 0)
                    {
                        if(instrTemp2.execTime == 2)
                        {
                            instrStateTable.Rows[i].Cells[2].Value = cycle.ToString() + "~";
                            loadRSTable.Rows[instrTemp2.r].Cells[2].Value = "R[" + rRegNames[instrTemp2.src2] + "]+" + Imm[instrTemp2.src2];
                        }
                        else if(instrTemp2.execTime == 1)
                        {
                            loadRSTable.Rows[instrTemp2.r].Cells[3].Value = "M[" + loadRSTable.Rows[instrTemp2.r].Cells[2].Value.ToString() + "]";
                        }
                        instrTemp2.execTime--;
                        if (instrTemp2.execTime == 0)
                        {
                            instrStateTable.Rows[i].Cells[2].Value = instrStateTable.Rows[i].Cells[2].Value + cycle.ToString();
                            instrTemp2.state++;
                        }
                    }
                    // SUB and ADD
                    else if(instrTemp2.op == 2 || instrTemp2.op == 5)
                    {
                        if(instrTemp2.execTime == 2)
                        {
                            instrStateTable.Rows[i].Cells[2].Value = cycle.ToString() + "~";
                        }
                        instrTemp2.execTime--;
                        if(instrTemp2.execTime == 0)
                        {
                            instrStateTable.Rows[i].Cells[2].Value = instrStateTable.Rows[i].Cells[2].Value + cycle.ToString();
                            RSTable.Rows[instrTemp2.r].Cells[0].Value = null;
                            instrTemp2.state++;
                        }
                        else
                        {
                            RSTable.Rows[instrTemp2.r].Cells[0].Value = instrTemp2.execTime.ToString();
                        }
                    }
                    //DIV.D
                    else if(instrTemp2.op == 3)
                    {
                        if(instrTemp2.execTime == 40)
                        {
                            instrStateTable.Rows[i].Cells[2].Value = cycle.ToString() + "~";
                        }
                        instrTemp2.execTime--;
                        if (instrTemp2.execTime == 0)
                        {
                            instrStateTable.Rows[i].Cells[2].Value = instrStateTable.Rows[i].Cells[2].Value + cycle.ToString();
                            RSTable.Rows[instrTemp2.r].Cells[0].Value = null;
                            instrTemp2.state++;
                        }
                        else
                        {
                            RSTable.Rows[instrTemp2.r].Cells[0].Value = instrTemp2.execTime.ToString();
                        }
                    }
                    //MUL.D
                    else if (instrTemp2.op == 4)
                    {
                        if (instrTemp2.execTime == 10)
                        {
                            instrStateTable.Rows[i].Cells[2].Value = cycle.ToString() + "~";
                        }
                        instrTemp2.execTime--;
                        if (instrTemp2.execTime == 0)
                        {
                            instrStateTable.Rows[i].Cells[2].Value = instrStateTable.Rows[i].Cells[2].Value + cycle.ToString();
                            RSTable.Rows[instrTemp2.r].Cells[0].Value = null;
                            instrTemp2.state++;
                        }
                        else
                        {
                            RSTable.Rows[instrTemp2.r].Cells[0].Value = instrTemp2.execTime.ToString();
                        }
                    }
                }
                //写回状态
                else if(instrTemp2.state == 2)
                {
                    //L.D
                    if(instrTemp2.op == 0)
                    {
                        instrStateTable.Rows[i].Cells[3].Value = cycle.ToString();
                        loadRSTable.Rows[instrTemp2.r].Cells[1].Value = "N";
                        loadRSTable.Rows[instrTemp2.r].Cells[2].Value = null;
                        loadRSTable.Rows[instrTemp2.r].Cells[3].Value = null;
                        RegTable.Rows[1].Cells[instrTemp2.dst].Value = resultNames[resultIndex++];
                    }
                    else
                    {
                        instrStateTable.Rows[i].Cells[3].Value = cycle.ToString();
                        RSTable.Rows[instrTemp2.r].Cells[2].Value = "N";
                        RSTable.Rows[instrTemp2.r].Cells[3].Value = null;
                        RSTable.Rows[instrTemp2.r].Cells[4].Value = null;
                        RSTable.Rows[instrTemp2.r].Cells[5].Value = null;
                        RSTable.Rows[instrTemp2.r].Cells[6].Value = null;
                        RSTable.Rows[instrTemp2.r].Cells[7].Value = null;
                        RegTable.Rows[1].Cells[instrTemp2.dst].Value = resultNames[resultIndex++];
                    }
                    instrTemp2.state++;
                }
            }
            //*******
            if (ip >= instrs.Count) return;
            Instruction instrTemp = instrs[ip];
            if (instrTemp.op == 0)
            {
                //指令处于发射状态
                instrTemp.state = 0;
                int i = 0;
                //判断缓冲区是否为空
                while(true)
                {
                    if (loadRSTable.Rows[i].Cells[1].Value.ToString() != "N") i++;
                    else break;
                    if (i >= 3) break;
                }
                // 缓冲区不为空
                if(i < 3)
                {
                    //更新缓冲区
                    loadRSTable.Rows[i].Cells[1].Value = "Y";
                    loadRSTable.Rows[i].Cells[2].Value = Imm[instrTemp.src1].ToString();
                    //更新指令状态
                    instrStateTable.Rows[ip].Cells[1].Value = cycle.ToString();
                    //更新寄存器状态
                    RegTable.Rows[0].Cells[instrTemp.dst].Value = loadRSTable.Rows[i].Cells[0].Value;
                    instrTemp.r = i;
                    instrTemp.state++;
                    instrTemp.execTime = 2;
                    ip++;
                }
            }
            else if(instrTemp.op == 2 || instrTemp.op == 5)
            {
                instrTemp.state = 0;
                int i = 0;
                while(true)
                {
                    if (RSTable.Rows[i].Cells[2].Value == "N") break;
                    else i++;
                    if (i >= 3) break;
                }
                if(i<3)
                {
                    instrTemp.r = i;
                    instrStateTable.Rows[ip].Cells[1].Value = cycle.ToString();
                    RSTable.Rows[i].Cells[2].Value = "Y";
                    RSTable.Rows[i].Cells[3].Value = instrType[instrTemp.op];

                    RegTable.Rows[0].Cells[instrTemp.dst].Value = RSTable.Rows[i].Cells[1].Value;
                    //第一个源操作数没有被占用
                    if (RSTable.Rows[instrTemp.r].Cells[4].Value != null) ;
                    else if (RegTable.Rows[0].Cells[instrTemp.src1].Value == null)
                    {
                        RSTable.Rows[instrTemp.r].Cells[4].Value = "R[" + fRegNames[instrTemp.src1] + "]";
                    }
                    //已经计算出值
                    else if (RegTable.Rows[1].Cells[instrTemp.src1].Value != null)
                    {
                        RSTable.Rows[instrTemp.r].Cells[4].Value = RegTable.Rows[1].Cells[instrTemp.src1].Value;
                        RSTable.Rows[instrTemp.r].Cells[6].Value = null;
                    }
                    else
                    {
                        RSTable.Rows[instrTemp.r].Cells[6].Value = RegTable.Rows[0].Cells[instrTemp.src1].Value;
                    }
                    //第二个源操作数没有被占用
                    if (RSTable.Rows[instrTemp.r].Cells[5].Value != null) ;
                    else if (RegTable.Rows[0].Cells[instrTemp.src2].Value == null)
                    {
                        RSTable.Rows[instrTemp.r].Cells[5].Value = "R[" + fRegNames[instrTemp.src2] + "]";
                    }
                    //已经计算出值
                    else if (RegTable.Rows[1].Cells[instrTemp.src2].Value != null)
                    {
                        RSTable.Rows[instrTemp.r].Cells[5].Value = RegTable.Rows[1].Cells[instrTemp.src2].Value;
                        RSTable.Rows[instrTemp.r].Cells[7].Value = null;
                    }
                    else
                    {
                        RSTable.Rows[instrTemp.r].Cells[7].Value = RegTable.Rows[0].Cells[instrTemp.src2].Value;
                    }
                    if (RSTable.Rows[instrTemp.r].Cells[4].Value != null && RSTable.Rows[instrTemp.r].Cells[5].Value != null)
                    {
                        instrTemp.state++;
                        instrTemp.execTime = 2;
                    }
                    ip++;
                }
            }
            else
            {
                instrTemp.state = 0;
                int i = 3;
                while (true)
                {
                    if (RSTable.Rows[i].Cells[2].Value == "N") break;
                    else i++;
                    if (i >= 5) break;
                }
                if (i < 5)
                {
                    instrTemp.r = i;
                    instrStateTable.Rows[ip].Cells[1].Value = cycle.ToString();

                    RSTable.Rows[i].Cells[2].Value = "Y";
                    RSTable.Rows[i].Cells[3].Value = instrType[instrTemp.op];

                    RegTable.Rows[0].Cells[instrTemp.dst].Value = RSTable.Rows[i].Cells[1].Value;
                    //第一个源操作数没有被占用
                    if (RSTable.Rows[instrTemp.r].Cells[4].Value != null) ;
                    else if (RegTable.Rows[0].Cells[instrTemp.src1].Value == null)
                    {
                        RSTable.Rows[instrTemp.r].Cells[4].Value = "R[" + fRegNames[instrTemp.src1] + "]";
                    }
                    //已经计算出值
                    else if (RegTable.Rows[1].Cells[instrTemp.src1].Value != null)
                    {
                        RSTable.Rows[instrTemp.r].Cells[4].Value = RegTable.Rows[1].Cells[instrTemp.src1].Value;
                        RSTable.Rows[instrTemp.r].Cells[6].Value = null;
                    }
                    else
                    {
                        RSTable.Rows[instrTemp.r].Cells[6].Value = RegTable.Rows[0].Cells[instrTemp.src1].Value;
                    }
                    //第二个源操作数没有被占用
                    if (RSTable.Rows[instrTemp.r].Cells[5].Value != null) ;
                    else if (RegTable.Rows[0].Cells[instrTemp.src2].Value == null)
                    {
                        RSTable.Rows[instrTemp.r].Cells[5].Value = "R[" + fRegNames[instrTemp.src2] + "]";
                    }
                    //已经计算出值
                    else if (RegTable.Rows[1].Cells[instrTemp.src2].Value != null)
                    {
                        RSTable.Rows[instrTemp.r].Cells[5].Value = RegTable.Rows[1].Cells[instrTemp.src2].Value;
                        RSTable.Rows[instrTemp.r].Cells[7].Value = null;
                    }
                    else
                    {
                        RSTable.Rows[instrTemp.r].Cells[7].Value = RegTable.Rows[0].Cells[instrTemp.src2].Value;
                    }
                    if (RSTable.Rows[instrTemp.r].Cells[4].Value != null && RSTable.Rows[instrTemp.r].Cells[5].Value != null)
                    {
                        instrTemp.state++;
                        if(instrTemp.op == 3)
                        {
                            instrTemp.execTime = 10;
                        }
                        else
                        {
                            instrTemp.execTime = 40;
                        }
                    }
                    ip++;
                }
            }
        }

        private void butExecSteps_Click(object sender, EventArgs e)
        {
            for(int i=0;i<5;i++)
            {
                execSingleTime();
            }
        }

        private void butExec2End_Click(object sender, EventArgs e)
        {
            bool isEnd = false;
            while (true)
            {
                execSingleTime(); 
                for(int i=0;i<instrs.Count;i++)
                {
                    if (instrStateTable.Rows[i].Cells[3].Value == null)
                    {
                        //MessageBox.Show("BREAK");
                        break;
                    }
                    else if(i == instrs.Count -1)
                    {
                        isEnd = true;
                    }
                }
                if (isEnd == true)
                {
                    break;
                }
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            string strTemp = textBox5.Text;
            bool isInt = Regex.IsMatch(strTemp, "[0-9]*");
            if(isInt == false)
            {
                MessageBox.Show("The input text is invalid!");
                return;
            }
            int cycle2 = Convert.ToInt32(strTemp);
            while(true)
            {
                execSingleTime();
                if (cycle >= cycle2) break;
            }
        }

        private void butExit_Click(object sender, EventArgs e)
        {
            for(int i=0;i<instrStateTable.Rows.Count;i++)
            {
                for(int j=0;j<instrStateTable.Rows[i].Cells.Count;j++)
                {
                    instrStateTable.Rows[i].Cells[j].Value = null;
                }
            }
            for (int i = 0; i < RegTable.Rows.Count; i++)
            {
                for (int j = 0; j < RegTable.Rows[i].Cells.Count; j++)
                {
                    RegTable.Rows[i].Cells[j].Value = null;
                }
            }
            for (int i = 0; i < loadRSTable.Rows.Count; i++)
            {
                for (int j = 0; j < loadRSTable.Rows[i].Cells.Count; j++)
                {
                    loadRSTable.Rows[i].Cells[j].Value = null;
                }
            }
            for (int i = 0; i < RSTable.Rows.Count; i++)
            {
                for (int j = 0; j < RSTable.Rows[i].Cells.Count; j++)
                {
                    RSTable.Rows[i].Cells[j].Value = null;
                }
            }
            panelDisplay.Visible = false;
        }

        private void butResert_Click(object sender, EventArgs e)
        {
            int[,] defaultIndex = new int[10, 4]{
            {0,3,21,2},{0,1,20,3},
            {4,0,1,2},{5,4,3,1},
            {3,5,0,3},{2,3,4,1},
            {1,0,0,0},{1,0,0,0},
            {1,0,0,0},{1,0,0,0}
            };

            for (int i = 0; i < 10; i++)
            {
                comInst[i].SelectedIndex = defaultIndex[i, 0];
                comDst[i].SelectedIndex = defaultIndex[i, 1];
                comSrcl[i].SelectedIndex = defaultIndex[i, 2];
                comSrcr[i].SelectedIndex = defaultIndex[i, 3];
            }
        }

        private void butBackSingle_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < instrStateTable.Rows.Count; i++)
            {
                for (int j = 0; j < instrStateTable.Rows[i].Cells.Count; j++)
                {
                    instrStateTable.Rows[i].Cells[j].Value = null;
                }
            }
            for (int i = 0; i < RegTable.Rows.Count; i++)
            {
                for (int j = 0; j < RegTable.Rows[i].Cells.Count; j++)
                {
                    RegTable.Rows[i].Cells[j].Value = null;
                }
            }
            for (int i = 0; i < loadRSTable.Rows.Count; i++)
            {
                for (int j = 0; j < loadRSTable.Rows[i].Cells.Count; j++)
                {
                    loadRSTable.Rows[i].Cells[j].Value = null;
                }
            }
            for (int i = 0; i < RSTable.Rows.Count; i++)
            {
                for (int j = 0; j < RSTable.Rows[i].Cells.Count; j++)
                {
                    RSTable.Rows[i].Cells[j].Value = null;
                }
            }
            int cycTemp = cycle - 1;
            startToExec();
            while(true)
            {
                if (cycle == cycTemp) break;
                execSingleTime();
            }
        }

        private void butBack5Steps_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < instrStateTable.Rows.Count; i++)
            {
                for (int j = 0; j < instrStateTable.Rows[i].Cells.Count; j++)
                {
                    instrStateTable.Rows[i].Cells[j].Value = null;
                }
            }
            for (int i = 0; i < RegTable.Rows.Count; i++)
            {
                for (int j = 0; j < RegTable.Rows[i].Cells.Count; j++)
                {
                    RegTable.Rows[i].Cells[j].Value = null;
                }
            }
            for (int i = 0; i < loadRSTable.Rows.Count; i++)
            {
                for (int j = 0; j < loadRSTable.Rows[i].Cells.Count; j++)
                {
                    loadRSTable.Rows[i].Cells[j].Value = null;
                }
            }
            for (int i = 0; i < RSTable.Rows.Count; i++)
            {
                for (int j = 0; j < RSTable.Rows[i].Cells.Count; j++)
                {
                    RSTable.Rows[i].Cells[j].Value = null;
                }
            }
            int cycTemp = cycle - 5;
            if (cycTemp < 0) return;
            startToExec();
            while (true)
            {
                if (cycle == cycTemp) break;
                execSingleTime();
            }
        }
    }
    
}
