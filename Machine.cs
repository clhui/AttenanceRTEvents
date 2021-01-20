using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTEvents
{
    public class Machine
    {
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        /**   */
        private int uid;
        private String machinealias;
        private int connecttype; //连接类型
        private String ip; //连接ip
        private int port; //连接端口
        private int baudrate; //波特率
        private int serialport; //com端口
        private int machinenumber; //设备编号
        private String commpassword; //连接密码
        private int enabled;    //是否可用  0不可用 1可用
        private String school; //学校
        private String floor; //楼层
        private String position; //位置
        private String skschoolid;//所在学校校区地图id
        private String creat_time; //创建日期
        private string sn;   //序列号

        private int userCount; //用户数
        private int fingerCount; //指纹数
        private int attendanceCount; //出席考勤数
        private int userCapacity;  //用户容量
        private int fingerCapacity; //指纹容量
        private int attendanceCapacity; //考勤容量
        private String firmwareVersion; //固件版本
        private String productType; //产品版本型号

        private double latitude;//,维度
        private double longitude;//经度
        private int failCount;
        private long testTime;

        private MachineEvents machineEvents;
        private RTEventsMain rtMain;

        private int idwErrorCode;
        private bool connected; //连接状态 1 连接  0未连接
        private String state; //当前状态
        private bool registEvent = false;

        public Machine(RTEventsMain rtMain)
        {
            this.machineEvents = new MachineEvents(rtMain,this);
            this.rtMain = rtMain;
        }

        public int FailCount { get => failCount; set => failCount = value; }
        public int getUid()
        {
            return uid;
        }

        public void setUid(int uid)
        {
            this.uid = uid;
        }

        public String getMachinealias()
        {
            return machinealias;
        }

        public void setMachinealias(String machinealias)
        {
            this.machinealias = machinealias;
        }

        public int getConnecttype()
        {
            return connecttype;
        }

        public void setConnecttype(int connecttype)
        {
            this.connecttype = connecttype;
        }

        public String getIp()
        {
            return ip;
        }

        public void setIp(String ip)
        {
            this.ip = ip;
        }

        public int getSerialport()
        {
            return serialport;
        }

        public void setSerialport(int serialport)
        {
            this.serialport = serialport;
        }

        public int getPort()
        {
            return port;
        }

        public void setPort(int port)
        {
            this.port = port;
        }

        public int getBaudrate()
        {
            return baudrate;
        }

        public void setBaudrate(int baudrate)
        {
            this.baudrate = baudrate;
        }

        public int getMachinenumber()
        {
            return machinenumber;
        }

        public void setMachinenumber(int machinenumber)
        {
            this.machinenumber = machinenumber;
        }

        public String getCommpassword()
        {
            return commpassword;
        }

        public void setCommpassword(String commpassword)
        {
            this.commpassword = commpassword;
        }

        public String getSchool()
        {
            return school;
        }

        public void setSchool(String school)
        {
            this.school = school;
        }

        public String getFloor()
        {
            return floor;
        }

        public void setFloor(String floor)
        {
            this.floor = floor;
        }

        public String getPosition()
        {
            return position;
        }

        public void setPosition(String position)
        {
            this.position = position;
        }

        public String getCreat_time()
        {
            return creat_time;
        }

        public void setCreat_time(String creat_time)
        {
            this.creat_time = creat_time;
        }

        public String getState()
        {
            return state;
        }

        public void setState(String state)
        {
            this.state = state;
        }

        public String getSkschoolid()
        {
            return skschoolid;
        }

        public void setSkschoolid(String skschoolid)
        {
            this.skschoolid = skschoolid;
        }

        public int getEnabled()
        {
            return enabled;
        }

        public void setEnabled(int enabled)
        {
            this.enabled = enabled;
        }


        public int getUserCount()
        {
            return userCount;
        }


        public void setUserCount(int userCount)
        {
            this.userCount = userCount;
        }


        public int getFingerCount()
        {
            return fingerCount;
        }


        public void setFingerCount(int fingerCount)
        {
            this.fingerCount = fingerCount;
        }


        public int getAttendanceCount()
        {
            return attendanceCount;
        }


        public void setAttendanceCount(int attendanceCount)
        {
            this.attendanceCount = attendanceCount;
        }


        public int getUserCapacity()
        {
            return userCapacity;
        }


        public void setUserCapacity(int userCapacity)
        {
            this.userCapacity = userCapacity;
        }


        public int getFingerCapacity()
        {
            return fingerCapacity;
        }


        public void setFingerCapacity(int fingerCapacity)
        {
            this.fingerCapacity = fingerCapacity;
        }


        public int getAttendanceCapacity()
        {
            return attendanceCapacity;
        }


        public void setAttendanceCapacity(int attendanceCapacity)
        {
            this.attendanceCapacity = attendanceCapacity;
        }


        public String getFirmwareVersion()
        {
            return firmwareVersion;
        }


        public void setFirmwareVersion(String firmwareVersion)
        {
            this.firmwareVersion = firmwareVersion;
        }


        public String getProductType()
        {
            return productType;
        }


        public void setProductType(String productType)
        {
            this.productType = productType;
        }

        public bool getConnected()
        {
            return connected;
        }

        public void setConnected(bool connected)
        {
            this.connected = connected;
        }

        public String getSn()
        {
            return sn;
        }

        public void setSn(String sn)
        {
            this.sn = sn;
        }

        public double getLatitude()
        {
            return latitude;
        }

        public void setLatitude(double latitude)
        {
            this.latitude = latitude;
        }

        public double getLongitude()
        {
            return longitude;
        }

        public void setLongitude(double longitude)
        {
            this.longitude = longitude;
        }


    
        /**
	 * 测试所有设备连接   类似于心跳检测
	 * 状态   连接状态     连接失败   连接失败 断开连接
	 * 连接状态 1分钟一次心跳
	 * 十次以下  1分钟一次心跳
	 * 十次以上  10分钟一次心跳
	 * 操作:连接时,成功 将状态改为连接状态,失败次数设置为0;失败 将状态设置为网络不通或者连接失败状态,失败次数加1
	 */
        public bool testConect()
        {
            Console.WriteLine("心跳检测开始.....");
            {
                long intervalTime = 60 * 1000;//检测间隔 默认60秒
                if (failCount > 0)
                {
                    //十次以下 每次加1分钟, 十分钟以上20分钟一次 //////
                    intervalTime += failCount > 10 ? 20 * 60 * 1000 : failCount * 60 * 1000;
                }
                if (DateTime.UtcNow.Ticks - testTime > intervalTime * 10000)
                {

                    Console.WriteLine("心跳检测." + this.getMachinealias());
                    try
                    {
                        // 先检测网络通不通
                        if (!NetUtil.isPing(this.getIp(),1000) ||!NetUtil.isHostConnectable(this.getIp(), this.getPort(),2000))
                        {
                            this.setConnected(false);
                            
                            //					printLog(isprint,ip + "网络不通!");
                            //					this.zkTaskList = new ArrayList<ZkTask>();
                            //					return false;
                            failCount += 1;
                            testTime = DateTime.UtcNow.Ticks;
                            Console.WriteLine("心跳检测." + this.getMachinealias() + "网络不通!");
                            this.rtMain.ShowMessage("心跳检测." + this.getMachinealias() + "网络不通!");
                            this.disConnect();
                            return false;
                        }
                        if (!connected) {
                            this.connect();
                        }
                        //通过接口尝试读取信息,读取成功则连接正常
                        bool readSuccess = this.axCZKEM1.GetSerialNumber(machinenumber, out sn);
                        if (readSuccess)
                        {
                            //链接成功
                            this.failCount = 0;
                            this.testTime = DateTime.UtcNow.Ticks;
                            return true;
                        }
                        else
                        {
                            //链接失败,尝试重连

                            Console.WriteLine("心跳检测." + this.getMachinealias() + "尝试重连");
                            this.rtMain.ShowMessage("心跳检测." + this.getMachinealias() + "尝试重连!");
                            //先断开
                            this.disConnect();
                            //尝试重连
                            bool reConnect = this.connect();
                            if (reConnect)
                            {
                                //链接成功
                                this.failCount = 0;
                                this.testTime = DateTime.UtcNow.Ticks;
                                this.connected = true;

                                Console.WriteLine("心跳检测." + this.getMachinealias() + "重连成功");
                                this.rtMain.ShowMessage("心跳检测." + this.getMachinealias() + "重连成功!");
                                return true;
                            }
                            else
                            {
                                //异常失败
                                this.failCount += 1;
                                this.testTime = DateTime.UtcNow.Ticks;
                                Console.WriteLine("心跳检测." + this.getMachinealias() + "重连失败" + this.failCount);
                                this.rtMain.ShowMessage("心跳检测." + this.getMachinealias() + "重连失败" + this.failCount);
                                this.disConnect();
                                return false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        //异常失败
                        this.failCount += 1;
                        this.testTime = DateTime.UtcNow.Ticks;
                        Console.WriteLine("心跳检测." + this.getMachinealias() + "重连失败" + this.failCount);
                        this.rtMain.ShowMessage("心跳检测." + this.getMachinealias() + "重连失败" + this.failCount);
                        this.disConnect();
                        return false;
                    }
                }
                //this.disConnect();
                return false;

            }
        }
        /**
         * 断开连接
         * */
        public void disConnect()
        {

            this.axCZKEM1.OnConnected -= new zkemkeeper._IZKEMEvents_OnConnectedEventHandler(machineEvents.axCZKEM1_OnConnected);
            this.axCZKEM1.OnDisConnected -= new zkemkeeper._IZKEMEvents_OnDisConnectedEventHandler(machineEvents.axCZKEM1_OnDisConnected);
            this.axCZKEM1.OnFinger -= new zkemkeeper._IZKEMEvents_OnFingerEventHandler(machineEvents.axCZKEM1_OnFinger);
            this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(machineEvents.axCZKEM1_OnVerify);
            this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(machineEvents.axCZKEM1_OnAttTransactionEx);
            this.axCZKEM1.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(machineEvents.axCZKEM1_OnFingerFeature);
            this.axCZKEM1.OnEnrollFingerEx -= new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(machineEvents.axCZKEM1_OnEnrollFingerEx);
            this.axCZKEM1.OnDeleteTemplate -= new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(machineEvents.axCZKEM1_OnDeleteTemplate);
            this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(machineEvents.axCZKEM1_OnNewUser);
            this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(machineEvents.axCZKEM1_OnHIDNum);
            this.axCZKEM1.OnAlarm -= new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(machineEvents.axCZKEM1_OnAlarm);
            this.axCZKEM1.OnDoor -= new zkemkeeper._IZKEMEvents_OnDoorEventHandler(machineEvents.axCZKEM1_OnDoor);
            this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(machineEvents.axCZKEM1_OnWriteCard);
            this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(machineEvents.axCZKEM1_OnEmptyCard);
            this.connected = false;
            this.registEvent = false;
            this.rtMain.ShowMessage(machinealias + "设备断开连接!");

            axCZKEM1.Disconnect();
        }
        /**
         * 连接
         * */
        public bool connect()
        {
            if (this.commpassword !=null && !this.commpassword.Equals( "")) {
                this.axCZKEM1.SetCommPassword(Convert.ToInt32(this.commpassword));
            }
            
            connected = axCZKEM1.Connect_Net(ip, port);
            if (connected == true)
            {
                //btnConnect.Text = "DisConnect";
                //btnConnect.Refresh();
                //lblState.Text = "Current State:Connected";
                //iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.

                //this.rtTimer.Enabled = true;
                if (!registEvent && axCZKEM1.RegEvent(machinenumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                {
                    this.axCZKEM1.OnConnected += new zkemkeeper._IZKEMEvents_OnConnectedEventHandler(machineEvents.axCZKEM1_OnConnected);
                    this.axCZKEM1.OnDisConnected += new zkemkeeper._IZKEMEvents_OnDisConnectedEventHandler(machineEvents.axCZKEM1_OnDisConnected);
                    this.axCZKEM1.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(machineEvents.axCZKEM1_OnFinger);
                    this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(machineEvents.axCZKEM1_OnVerify);
                    this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(machineEvents.axCZKEM1_OnAttTransactionEx);
                    this.axCZKEM1.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(machineEvents.axCZKEM1_OnFingerFeature);
                    this.axCZKEM1.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(machineEvents.axCZKEM1_OnEnrollFingerEx);
                    this.axCZKEM1.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(machineEvents.axCZKEM1_OnDeleteTemplate);
                    this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(machineEvents.axCZKEM1_OnNewUser);
                    this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(machineEvents.axCZKEM1_OnHIDNum);
                    this.axCZKEM1.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(machineEvents.axCZKEM1_OnAlarm);
                    this.axCZKEM1.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(machineEvents.axCZKEM1_OnDoor);
                    this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(machineEvents.axCZKEM1_OnWriteCard);
                    this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(machineEvents.axCZKEM1_OnEmptyCard);
                    machineEvents.rtEventsMain.ShowMessage(machinealias + "事件注册成功!");
                    registEvent = true;
                }
                return true;
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                Console.WriteLine("不能连接设备,ErrorCode=" + idwErrorCode.ToString(), "Error");
                return false;
            }
        }

    }
}
