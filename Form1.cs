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
namespace Alrgo_02
{
    public partial class Form1 : Form
    {
        Graphics Hana;
        Pen MyPen = new Pen(Color.FromArgb(150,178,34,34), 4);
        public Form1(){
           InitializeComponent();
           Hana = this.CreateGraphics();
        }
        private void Form1_DoubleClick(object sender, EventArgs e){
            node.Node_s[node.Node_Number] = new node(PointToClient(new Point(MousePosition.X, MousePosition.Y)),this);//建構一新節點
            this.Controls.Add(node.Node_s[node.Node_Number]);//加入至表單
            node.Node_Number++;
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e) {
            node.Doom = true;//拉線完成中途放開滑鼠=需重選起點
            Let_sDraw();//刷新
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!node.Doom){
                Let_sDraw();//刷新
                Hana.DrawLine(MyPen, node.Location_Start, PointToClient(MousePosition));//拉線中的繪圖
            }
        }
        public void Let_sDraw(){
            Hana.Clear(Color.DarkSalmon);   //清空畫面
            for (int i = 0; i < node.Edge_Number; i++)
                DrawEdge(i);           
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Let_sDraw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {   //存檔
                String Out_Contact=null;
                
                Out_Contact += node.Node_Number + ",";
                Out_Contact += node.Edge_Number + ",";
                
                for (int i = 0; i < node.Edge_Number; i++)
                    Out_Contact += node.Edge_s[i]._node1.Name + "," + node.Edge_s[i]._node2.Name + "," + node.Edge_s[i]._value + ",";
                Out_Contact+=node.Node_s[0].Location.X+","+node.Node_s[0].Location.Y;
                for (int i = 1; i < node.Node_Number; i++)
                    Out_Contact += "," + node.Node_s[i].Location.X + "," + node.Node_s[i].Location.Y;
 
                System.IO.StreamWriter fileOut = new System.IO.StreamWriter(saveFileDialog1.FileName);
                fileOut.WriteLine(Out_Contact);
                fileOut.Close();
            }
        }

        private void Kruskal_Click(object sender, EventArgs e){
            //=================================清空畫面=====================================
            Hana.Clear(Color.DarkSalmon);
            Thread.Sleep(1000);
            //=========== 前置作業，移除沒有連線的點，將BePrinted與BossTree重置 ============
            for (int i = 0; i < node.Node_Number; )
                if (!node.Node_s[i].BeLinked) { Thread.Sleep(1000); node.RemoveNode(i); }
                else i++;
            Thread.Sleep(1000);
            for (int i = 0; i < node.Edge_Number; i++)
                node.Edge_s[i].BePrinted = false;
            for (int i = 0; i < node.Node_Number; i++)
                node.Node_s[i].BossTree = -1;
            //================================開始輸出MST=====================================
            
            int OutEdge = 0,Tree_ID=0;//紀錄已輸出的邊數,紀錄是第幾棵樹

            while (OutEdge < node.Node_Number - 1) {
                int IndexOf_MinimumValue=0; //擁有最小權重值之邊的索引值
                bool Have_Find_Minimum = false;//若未找到最小值，則把最小值設為第一個找到的未輸出的邊之權重
                
                //=============================== Step1.找到邊 ================================
                for (int i = 0; i < node.Edge_Number; i++)
                    if (!node.Edge_s[i].BePrinted)
                        if (!Have_Find_Minimum) { IndexOf_MinimumValue = i; Have_Find_Minimum = true;}
                        else if (node.Edge_s[i]._value < node.Edge_s[IndexOf_MinimumValue]._value) IndexOf_MinimumValue = i;               
                //=========================== Step2.加入並檢查迴路 ============================
                node.Edge_s[IndexOf_MinimumValue].BePrinted = true;
                OutEdge++;

                if (node.Edge_s[IndexOf_MinimumValue]._node1.BossTree == node.Edge_s[IndexOf_MinimumValue]._node2.BossTree){
                    //邊上兩點屬於同一個樹
                    if (node.Edge_s[IndexOf_MinimumValue]._node1.BossTree == -1){
                        //完全獨立的邊
                        DrawEdge(IndexOf_MinimumValue);
                        Thread.Sleep(1000);
                        node.Edge_s[IndexOf_MinimumValue]._node1.BossTree = Tree_ID;
                        node.Edge_s[IndexOf_MinimumValue]._node2.BossTree = Tree_ID;
                        Tree_ID++;  //登錄為一棵新的樹
                    }
                    else{
                        //迴路
                        OutEdge--;
                        WrongEdge(IndexOf_MinimumValue);
                        Thread.Sleep(1000);
                        node.RemoveEdge(IndexOf_MinimumValue);
                    }
                }
                else { 
                    //邊上兩點屬於不同樹
                    DrawEdge(IndexOf_MinimumValue);
                    Thread.Sleep(1000);
                    if (node.Edge_s[IndexOf_MinimumValue]._node1.BossTree == -1) node.Edge_s[IndexOf_MinimumValue]._node1.BossTree = node.Edge_s[IndexOf_MinimumValue]._node2.BossTree;
                    else if (node.Edge_s[IndexOf_MinimumValue]._node2.BossTree == -1) node.Edge_s[IndexOf_MinimumValue]._node2.BossTree = node.Edge_s[IndexOf_MinimumValue]._node1.BossTree;
                    else { 
                        //邊上兩點屬於不同樹且兩顆樹擁有的節點皆大於一個
                        //合併至同一樹
                        for (int i = 0; i < node.Edge_Number; i++)
                            if(node.Edge_s[i].BePrinted)
                                if (node.Edge_s[i]._node1.BossTree == node.Edge_s[IndexOf_MinimumValue]._node2.BossTree) {
                                    node.Edge_s[i]._node1.BossTree = node.Edge_s[IndexOf_MinimumValue]._node1.BossTree;
                                    node.Edge_s[i]._node2.BossTree = node.Edge_s[IndexOf_MinimumValue]._node1.BossTree;
                                }
                    } 
                }
            }//While_Loop_End 
            
            //沒有被輸出的邊即移除
            for (int i = 0; i < node.Edge_Number; )
                if (!node.Edge_s[i].BePrinted) { Thread.Sleep(1000); node.RemoveEdge(i);}
                else i++;

            Let_sDraw();
        }//Function_End
        public void  DrawEdge(int i){
            Hana.DrawLine(MyPen, new Point(node.Edge_s[i]._node1.Location.X + 15, node.Edge_s[i]._node1.Location.Y + 15), new Point(node.Edge_s[i]._node2.Location.X + 15, node.Edge_s[i]._node2.Location.Y + 15));
                //↑顯示value
                
            String MSG = "(" + Convert.ToString(node.Edge_s[i]._value) + ")";
            int x_Sub=node.Edge_s[i]._node2.Location.X-node.Edge_s[i]._node1.Location.X;
            int y_Sub=node.Edge_s[i]._node1.Location.Y-node.Edge_s[i]._node2.Location.Y;
            int ValueShowPosi_X = node.Edge_s[i]._node1.Location.X+15+x_Sub/2;//計算權重顯示位置(X)
            int ValueShowPosi_Y = node.Edge_s[i]._node1.Location.Y+15-y_Sub/2;//計算權重顯示位置(Y)
            int X_Axis, Y_Axis;//微調後位置

            //開始微調↓
            if (x_Sub == 0) {
                X_Axis=node.Edge_s[i]._node1.Location.X+5; Y_Axis=ValueShowPosi_Y; } //如果邊為垂直,則顯示在中央點偏右
            else {
                if ((y_Sub * x_Sub) < 0){
                    X_Axis=ValueShowPosi_X; Y_Axis=ValueShowPosi_Y-15;}//如果斜率<0,則顯示在中央點偏上
                else if ((y_Sub * x_Sub) > 0){
                    X_Axis = ValueShowPosi_X - 23; Y_Axis = ValueShowPosi_Y - 15;}//如果斜率>0,則顯示在中央點偏左上
                else {
                    X_Axis = ValueShowPosi_X; Y_Axis = node.Edge_s[i]._node1.Location.Y - 13;}//如果邊為水平,則顯示在中央點偏上
            }
            //最後，輸出權重至畫面上↓
            Hana.DrawString(MSG, new Font("微軟正黑體", 9, FontStyle.Bold), Brushes.LavenderBlush, X_Axis, Y_Axis);
        }
        public void  WrongEdge(int i)
        {
            Hana.DrawLine(new Pen(Color.FromArgb(150, 77, 221, 221), 4), new Point(node.Edge_s[i]._node1.Location.X + 15, node.Edge_s[i]._node1.Location.Y + 15), new Point(node.Edge_s[i]._node2.Location.X + 15, node.Edge_s[i]._node2.Location.Y + 15));
            //↑顯示value

            String MSG = "Cycle!!";
            int x_Sub = node.Edge_s[i]._node2.Location.X - node.Edge_s[i]._node1.Location.X;
            int y_Sub = node.Edge_s[i]._node1.Location.Y - node.Edge_s[i]._node2.Location.Y;
            int ValueShowPosi_X = node.Edge_s[i]._node1.Location.X + 15 + x_Sub / 2;//計算權重顯示位置(X)
            int ValueShowPosi_Y = node.Edge_s[i]._node1.Location.Y + 15 - y_Sub / 2;//計算權重顯示位置(Y)
            int X_Axis, Y_Axis;//微調後位置

            //開始微調↓
            if (x_Sub == 0)
            {
                X_Axis = node.Edge_s[i]._node1.Location.X + 5; Y_Axis = ValueShowPosi_Y;
            } //如果邊為垂直,則顯示在中央點偏右
            else
            {
                if ((y_Sub * x_Sub) < 0)
                {
                    X_Axis = ValueShowPosi_X; Y_Axis = ValueShowPosi_Y - 15;
                }//如果斜率<0,則顯示在中央點偏上
                else if ((y_Sub * x_Sub) > 0)
                {
                    X_Axis = ValueShowPosi_X - 23; Y_Axis = ValueShowPosi_Y - 15;
                }//如果斜率>0,則顯示在中央點偏左上
                else
                {
                    X_Axis = ValueShowPosi_X; Y_Axis = node.Edge_s[i]._node1.Location.Y - 13;
                }//如果邊為水平,則顯示在中央點偏上
            }
            //最後，輸出權重至畫面上↓
            Hana.DrawString(MSG, new Font("微軟正黑體", 9, FontStyle.Bold), Brushes.DarkRed, X_Axis, Y_Axis);
        }

        private void Prim_Click(object sender, EventArgs e){
            //=================================清空畫面=====================================
            Hana.Clear(Color.DarkSalmon);
            Thread.Sleep(1000);
            //=========== 前置作業，移除沒有連線的點，將BePrinted與BossTree重置 ============
            for (int i = 0; i < node.Node_Number; )
                if (!node.Node_s[i].BeLinked) { Thread.Sleep(1000); node.RemoveNode(i); }
                else i++;
            Thread.Sleep(1000);
            for (int i = 0; i < node.Edge_Number; i++)
                node.Edge_s[i].BePrinted = false;
            for (int i = 0; i < node.Node_Number; i++)
                node.Node_s[i].BossTree = -1;
            //================================開始輸出MST=====================================
            
            int OutEdge = 0;//紀錄已輸出的邊數
            node.Node_s[0].BossTree = 555;

            while (OutEdge < node.Node_Number - 1){
                int IndexOf_MinimumValue = 0; //擁有最小權重值之邊的索引值
                bool Have_Find_Minimum = false;//若未找到最小值，則把最小值設為第一個找到的未輸出的邊之權重
                
                //=============================== Step1.找到邊 ================================
                for(int i=0;i<node.Edge_Number;i++)
                    if(!node.Edge_s[i].BePrinted)
                        if ((node.Edge_s[i]._node1.BossTree == 555) && (node.Edge_s[i]._node2.BossTree == -1)) {
                            if (!Have_Find_Minimum) { IndexOf_MinimumValue = i; Have_Find_Minimum = true; }
                            else if (node.Edge_s[i]._value < node.Edge_s[IndexOf_MinimumValue]._value) IndexOf_MinimumValue = i;
                        }
                        else if ((node.Edge_s[i]._node2.BossTree == 555) && (node.Edge_s[i]._node1.BossTree == -1)) {
                            if (!Have_Find_Minimum) { IndexOf_MinimumValue = i; Have_Find_Minimum = true; }
                            else if (node.Edge_s[i]._value < node.Edge_s[IndexOf_MinimumValue]._value) IndexOf_MinimumValue = i;
                        }

                //============================ Step2.加入邊至圖中 =============================
                node.Edge_s[IndexOf_MinimumValue].BePrinted = true;
                OutEdge++;
                DrawEdge(IndexOf_MinimumValue);
                Thread.Sleep(1000);
                node.Edge_s[IndexOf_MinimumValue]._node1.BossTree = 555;
                node.Edge_s[IndexOf_MinimumValue]._node2.BossTree = 555;
            }//While_Loop_End

            //沒有被輸出的邊即移除
            for (int i = 0; i < node.Edge_Number; )
                if (!node.Edge_s[i].BePrinted) { Thread.Sleep(1000); node.RemoveEdge(i); }
                else i++;

            Let_sDraw();
        
        }//Function_End
    }
}

public class node : Label {
    public static Edge[] Edge_s = new Edge[325];//儲存目前有的邊
    public static node[] Node_s = new node[100];//儲存目前有的點
    public static int Edge_Number = 0,Edge_value,Node_Number=0;//儲存邊與邊的個數,form2的傳值,節點的數量
    public static char Node_name='A';//作為新增節點名稱的基準
    public static node node_Start;//紀錄拉線的起點
    public static Point Location_Start;//紀錄拉線起點的位置
    public static bool Doom=true;//Doom=拉線中?false:true 
    private static Alrgo_02.Form1 ControlTheForm;
    public static Alrgo_02.Form2 MakeChoise = new Alrgo_02.Form2();//建構共用的form2
    public bool BeLinked = false;
    public int BossTree = -1;
    //============================== Constructor ==================================
    public node(Point locatt,Alrgo_02.Form1 formWeControl) {
        Name = Convert.ToString(Node_name);//物件名稱
        Node_name++;//名稱字母遞增
        BackColor = Color.IndianRed;//背景色
        BorderStyle = BorderStyle.None;//外框樣式
        Cursor = Cursors.Hand;//游標形狀
        Font = new Font("微軟正黑體", 9, FontStyle.Bold);
        ForeColor = Color.LavenderBlush;//文字顏色
        Location = new Point(locatt.X-15,locatt.Y-15);//生成位置
        Size = new System.Drawing.Size(30,30);
        Text = " "+Name;
        TextAlign = ContentAlignment.MiddleCenter;//文字位置
        ControlTheForm = formWeControl;//傳入form1,在需要刷新畫面時會用到form1的graphic物件
    }
    //============================ Member function ================================
    public static void RemoveNode(int Index_Start) {
        ControlTheForm.Controls.Remove(Node_s[Index_Start]);
        for (int i = Index_Start; i < Node_Number - 1; i++) Node_s[i] = Node_s[i+1];  
        Node_s[Node_Number - 1] = null;
        Node_Number--;
    }
    public static void RemoveEdge(int Index_Start) {
        for (int i = Index_Start; i < Edge_Number - 1; i++) Edge_s[i] = Edge_s[i+1];
        Edge_s[Edge_Number - 1] = null;
        Edge_Number--;
    }
    //============================ Override Events ================================
    protected override void OnPaint(PaintEventArgs e){ 
        base.OnPaint(e);//重載paint事件
        System.Drawing.Drawing2D.GraphicsPath _path = new System.Drawing.Drawing2D.GraphicsPath();
        _path.AddEllipse(new Rectangle(0, 0, 30, 30));
        Region = new Region(_path);//改變形狀
        e.Graphics.DrawEllipse(new Pen(Color.Maroon, 7), new Rectangle(0, 0, 30, 30));//畫出外框
    }
    protected override void OnMouseClick(MouseEventArgs e){
        base.OnMouseClick(e);
        if (Doom)
        {   //如果目前沒有拉線的起點
            Location_Start = new Point(this.Location.X + 15, this.Location.Y + 15);
            node_Start = this;//設定起點位置與點
            Doom = false;//拉線中
        }
        else
        {
            //連線成功
            Doom = true;//沒有起點
            bool HaveLinkeD=false;
            for (int i = 0; i < node.Edge_Number; i++)
                if ((node.Edge_s[i]._node1.Name == node_Start.Name) && (node.Edge_s[i]._node2.Name == this.Name)) HaveLinkeD = true;
                else if ((node.Edge_s[i]._node2.Name == node_Start.Name) && (node.Edge_s[i]._node1.Name == this.Name)) HaveLinkeD = true;

            if (!HaveLinkeD && (node_Start!=this)){
                MakeChoise.ShowDialog();
                if (!MakeChoise.IsCancel){
                    Edge_s[Edge_Number] = new Edge(node_Start, this, Edge_value);//加入邊
                    node_Start.BeLinked = true; this.BeLinked = true;
                    Edge_Number++;
                }
            }
            
            ControlTheForm.Let_sDraw();//刷新
        }
    }
    protected override void OnMouseEnter(EventArgs e){
        base.OnMouseEnter(e);
        if(BeLinked) BackColor = Color.DeepPink;
        else BackColor = Color.Goldenrod;
    }
    protected override void OnMouseLeave(EventArgs e){
        base.OnMouseLeave(e);
        BackColor = Color.IndianRed;    
    }

}
public class Edge {
    public node _node1,_node2;//邊上的點
    public int  _value=-1;//權重
    public bool BePrinted = false;//是否已輸出至MST
    public Edge(node node1, node node2,int value){
        _node1 = node1;
        _node2 = node2;
        _value = value;
    }   //constructor
};


