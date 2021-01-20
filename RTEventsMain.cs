/**********************************************************
 * Demo for Standalone SDK.Created by Darcy on Oct.15 2009*
***********************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using Microsoft.Win32;
using System.Collections;
using RabbitMQ.Client;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Configuration;
using RabbitMQ.Client.Exceptions;

namespace RTEvents
{
    public partial class RTEventsMain : Form
    {
        public RTEventsMain()
        {
            InitializeComponent();
            //初始化设备处理对象
            baseURLBox.Text = ConfigurationManager.AppSettings["BasePath"];
            urlBox.Text = ConfigurationManager.AppSettings["UrlPath"];
            textBoxMQIP.Text = ConfigurationManager.AppSettings["MQHost"];
            textBoxMQPort.Text = ConfigurationManager.AppSettings["MQPort"];
            textBoxMQUser.Text = ConfigurationManager.AppSettings["MQUser"];
            textBoxMQPsw.Text = ConfigurationManager.AppSettings["MQPassword"];
        }

        //Create Standalone SDK class dynamicly.
        
        Hashtable machineTable = new Hashtable(); // 设备列表



        /********************************************************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.                                           *
        * This part is for demonstrating the communication with your device.There are 3 communication ways: "TCP/IP","Serial Port" and "USB Client".*
        * The communication way which you can use duing to the model of the device.                                                                 *
        * *******************************************************************************************************************************************/
        #region Communication
        //private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        //private int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.

        //If your device supports the TCP/IP communications, you can refer to this.
        //when you are using the tcp/ip communication,you can distinguish different devices by their IP address.
        /**连接方法*/
        private void btnConnect_Click(object sender, EventArgs e)
        {

            if (baseURLBox.Text.Trim() == "")
            {
                MessageBox.Show("URl接口地址不可为空", "Error");
                return;
            }

            if (textBoxMQIP.Text.Trim() == ""|| textBoxMQPort.Text.Trim() == "" 
                || textBoxMQUser.Text.Trim() == ""|| textBoxMQPsw.Text.Trim() == "")
            {
                MessageBox.Show("MQ参数不可为空", "Error");
                return;
            }

            RabbitMQFactory.ip = textBoxMQIP.Text;
            RabbitMQFactory.mqport = Convert.ToInt32(textBoxMQPort.Text);
            RabbitMQFactory.user = textBoxMQUser.Text;
            RabbitMQFactory.password = textBoxMQPsw.Text;

            Cursor = Cursors.WaitCursor;
            try {
                bool mqConnectable = NetUtil.isHostConnectable(RabbitMQFactory.ip, RabbitMQFactory.mqport, 2000);
                if (!mqConnectable) {
                    this.ShowMessage("MQ连接参数有误异常：");
                    return;
                }
                var rabbitconnect = RabbitMQFactory.SharedConnection;
                if (rabbitconnect == null) {
                    return;
                }
                //请求设备信息,初始化设备信息
                bool getMachine = initMachineList();
                if (!getMachine) {
                    return;
                }
                rtTimer_Tick(null, null);
                //testConnect();
                testConnectTimer.Enabled = true;
                testConnectTimer.Start();
                lblState.Text = "当前状态:启动监听和心跳检测";
                lblState.ForeColor = Color.GreenYellow;
            }
            catch (BrokerUnreachableException ex) {
                this.ShowMessage("异常：MQ账号密码验证失败");
                Console.Out.Write(ex.InnerException);
            }
            catch (Exception ex) {
                this.ShowMessage("异常：" + ex.GetType());
                Console.Out.Write(ex.InnerException);
                lblState.ForeColor = Color.GreenYellow;
            }
            finally {

                Cursor = Cursors.Default;
            }
        }


        #endregion

        /*************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first.*
        * This part is for demonstrating the RealTime Events that triggered  by your operations          *
        **************************************************************************************************/
        #region RealTime Events


        bool timering = false;
        //调用函数GetRTLog（）后，将触发实时事件。 
        //当您使用这两个功能时，它将从设备中请求数据。
        private void rtTimer_Tick(object sender, EventArgs e)
        {
            //if (axCZKEM1.ReadRTLog(iMachineNumber))
            //{
            //    while (axCZKEM1.GetRTLog(iMachineNumber))
            //    {
            //        ;
            //    }
            //}
            
            Task task = new Task(() =>
            {
                //*****以下是异步执行的代码*****
                ShowMessage("心跳定时器执行.......");
                //检测是否正在执行定时器
                if (!timering)
                {
                    try
                    {
                        //设置为正在执行
                        timering = true;
                        //初始化设备
                        initMachineList();
                        //测试连接状态
                        testAllConnect();
                    }
                    catch (Exception ex)
                    {
                        ShowMessage("心跳定时器异常:" + ex.Message);
                    }
                    finally {
                        //设置为执行结束
                        timering = false;
                    }
                    
                }
                //*****以上是异步执行的代码*****
            });
            task.Start();
        }

        #endregion

        private void disConnect_Click(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor;
            Thread machineInfoThread = new Thread((ThreadStart)(delegate ()
            {
                // 此处应该警惕值类型装箱造成的”性能陷阱”
                machineListView.Invoke((MethodInvoker)delegate ()
                {
                    testConnectTimer.Enabled = false;
                    testConnectTimer.Stop();
                    lblState.Text = "当前状态:停止监听事件";
                    lblState.ForeColor = Color.Red;
                    //遍历方法二：遍历哈希表中的值
                    foreach (Machine machine in machineTable.Values)
                    {

                        Console.WriteLine("disConnect>>>" + machine.getMachinealias());
                        machine.disConnect();

                    }

                    Cursor = Cursors.Default;
                });
                
            }));

            machineInfoThread.Start();

            //return;
        }

        //初始化设备列表
        public bool initMachineList() {
            RestClientManager restClientManager = new RestClientManager();
            string baseUrl = baseURLBox.Text;
            string url = urlBox.Text;
            JObject restResult =  restClientManager.Get(baseUrl, url, null);
            if (restResult.Count==0) {
                ShowMessage("请求设备列表接口失败");
                return false;
            }
            if (restResult.GetValue("code").Value<int>() == 0)
            {
                JArray machineList = (JArray)restResult.GetValue("machineList");
                //更新到hashtable中
                for (int i=0;i<machineList.Count;i++) {
                    if (machineList[i]["enabled"].Value<int>() == 1 && !machineTable.ContainsKey(machineList[i]["uid"].ToString())) {
                        Machine machine = new Machine(this);
                        machine.setUid(machineList[i]["uid"].Value<int>());
                        machine.setIp(machineList[i]["ip"].ToString());
                        machine.setPort(machineList[i]["port"].Value<int>());
                        machine.setCommpassword(machineList[i]["commpassword"].ToString());
                        machine.setMachinealias(machineList[i]["machinealias"].ToString());
                        machine.setMachinenumber(machineList[i]["machinenumber"].Value<int>());
                        machine.setConnected(false);
                        machineTable.Add(machineList[i]["uid"].ToString(), machine);
                        //this.updateMachine(machine);
                    }
                }
            }
            return true;
        }
        //测试连接
        public void testAllConnect()
        {
            List<string> delKey = new List<string>();
            //遍历方法二：遍历哈希表中的值
            foreach (Machine machine in machineTable.Values)
            {

                Console.WriteLine("testConnect>>>" + machine.getMachinealias());
                machine.testConect();
                if (machine.FailCount > 100) {
                    delKey.Add(machine.getUid().ToString());
                }
            }
            //删除连不上的设备
            for (int i = 0;i< delKey.Count; i++) {

                machineTable.Remove(delKey[i]);
            }
        }

        //事件代理
        public delegate void ListViewDelegate<T>(T obj);
        /// <summary>
        /// ShowMessage重载
        /// </summary>
        /// <param name="msg"></param>
        public void ShowMessage(string msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ListViewDelegate<string>(ShowMessage), msg);
            }
            else
            {
                ListViewItem item = new ListViewItem(new string[] { DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), msg });
                lbRTShow.Items.Insert(0, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss>>>>>>")+ msg);
                if (lbRTShow.Items.Count > 500) {
                    lbRTShow.Items.RemoveAt(lbRTShow.Items.Count - 1);
                }
            }
        }

        /// <summary>
        /// ShowMessage重载
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        //private void ShowMessage(string format, params object[] args)
        //{
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(new MethodInvoker(delegate ()
        //        {
        //            lbRTShow.Items.Insert(0, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss>>>>")+string.Format(format, args));
        //        }));
        //    }
        //    else
        //    {
        //        lbRTShow.Items.Insert(0, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss>>>")+ string.Format(format, args));
        //    }
        //}

        //Dictionary<string, ListViewItem> machineViewItemsMap = new Dictionary<string, ListViewItem>();  
        /// <summary>
        /// ShowMessage重载
        /// </summary>
        /// <param name="msg"></param>
        public void updateMachine(Machine machine)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ListViewDelegate<Machine>(updateMachine), machine);
            }
            else
            {
                if (machineListView.Items.ContainsKey(machine.getUid().ToString())) // True
                {

                    //ListViewItem item = machineListView.FindItemWithText(machine.getUid().ToString(), true, 0);
                    ListViewItem item = machineListView.Items[machine.getUid().ToString()];
                    item.SubItems[0].Text = machine.getMachinenumber().ToString();
                    item.SubItems[1].Text = machine.getMachinealias();
                    item.SubItems[2].Text = machine.getIp();
                    item.SubItems[3].Text = machine.getConnected() ? "连接" : "断开";
                    item.SubItems[4].Text = machine.FailCount.ToString();
                    item.Name = machine.getUid().ToString();
                    item.BackColor = machine.getConnected() ? Color.LightSkyBlue : Color.Red;
                }else {
                    ListViewItem item = new ListViewItem(new string[] {
                        machine.getMachinenumber().ToString(),
                        machine.getMachinealias(),
                        machine.getIp() ,
                        machine.getConnected()?"连接":"断开" ,
                        machine.FailCount.ToString()
                    });
                    item.Name = machine.getUid().ToString();
                    item.BackColor = machine.getConnected() ? Color.LightSkyBlue : Color.Red;
                    machineListView.Items.Insert(0, item);
                    //machineViewItemsMap.Add(machine.getUid().ToString(), item);
                }

                
                
            }
        }
        //定时刷新设备信息页面
        private void timerRefreshMachine_Tick(object sender, EventArgs e)
        {
            Thread machineInfoThread = new Thread((ThreadStart)(delegate ()
            {
                try {

                    foreach (Machine m in machineTable.Values)
                    {
                        // 此处应该警惕值类型装箱造成的”性能陷阱”
                        machineListView.Invoke((MethodInvoker)delegate ()
                        {
                            updateMachine(m);
                        });
                    }
                    for (int i = 0; i < machineListView.Items.Count; i++)
                    {

                        // 此处应该警惕值类型装箱造成的”性能陷阱”
                        machineListView.Invoke((MethodInvoker)delegate ()
                        {
                            if (!machineTable.ContainsKey(machineListView.Items[i].Name))
                            {
                                machineListView.Items.RemoveAt(i);
                                i--;
                            }
                        });

                    }
                } catch (Exception ex){
                    ShowMessage(ex.Message);
                } finally { 
                
                }
            }));

            machineInfoThread.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RTEventsMain_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 如果窗体最小化，则还原
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                //设置程序允许显示在任务栏

                this.ShowInTaskbar = true;

                //设置窗口可见

                this.Visible = true;

                //设置窗口状态

                this.WindowState = FormWindowState.Normal;

                //设置窗口为活动状态，防止被其他窗口遮挡。

                this.Activate();
            }

        }
        /**设置退出缩小到托盘时使用
         */
        private void RTEventsMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 取消关闭窗体
            e.Cancel = true;

            // 将窗体变为最小化
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false; //不显示在系统任务栏 
            notifyIcon1.Visible = true; //托盘图标可见 
            notifyIcon1.ShowBalloonTip(10);//显示气泡
        }

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //退出
            this.Close();
            Application.Exit();
        }

        private void RTEventsMain_SizeChanged(object sender, EventArgs e)
        {
            //当点击最小化按钮时，执行操作

            if (this.WindowState == FormWindowState.Minimized)
            {
                //将程序从任务栏移除显示
                this.ShowInTaskbar = false;
                //隐藏窗口
                this.Visible = false;
                //显示托盘图标
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(10);//显示气泡
            }
        }

        private void 打开主窗体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 如果窗体最小化，则还原
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                //设置程序允许显示在任务栏

                this.ShowInTaskbar = true;

                //设置窗口可见

                this.Visible = true;

                //设置窗口状态

                this.WindowState = FormWindowState.Normal;

                //设置窗口为活动状态，防止被其他窗口遮挡。

                this.Activate();
            }

        }

        private void RTEventsMain_FormClosing_1(object sender, FormClosingEventArgs e)
        {

            notifyIcon1.Visible = false; //托盘图标bu可见 
        }

        /// <summary>
        /// ShowMessage重载
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        //private void updateMachine(Machine format, params object[] args)
        //{
        //    if (InvokeRequired)
        //    {
        //        BeginInvoke(new MethodInvoker(delegate ()
        //        {
        //            ListViewItem item = new ListViewItem(new string[] { DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), string.Format(format, args) });
        //            machineListView.Items.Insert(0, item);
        //        }));
        //    }
        //    else
        //    {
        //        ListViewItem item = new ListViewItem(new string[] { DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), string.Format(format, args) });
        //        machineListView.Items.Insert(0, item);
        //    }
        //}

    }
} 