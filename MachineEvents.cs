using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using fastJSON;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RestSharp;

namespace RTEvents
{
    class MachineEvents
    {
        public RTEventsMain rtEventsMain;
        private Machine machine;

        public MachineEvents(RTEventsMain rtMain,Machine machine)
        {
            this.rtEventsMain = rtMain;
            this.machine = machine;
        }

        //当您将手指放在设备的传感器上时，将触发此事件
        public void axCZKEM1_OnConnected()
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent 设备连接" );
        }
        //当您将手指放在设备的传感器上时，将触发此事件
        public void axCZKEM1_OnDisConnected()
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + ">RTEvent 设备断开连接" );
        }

        //当您将手指放在设备的传感器上时，将触发此事件
        public void axCZKEM1_OnFinger()
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + ">指纹验证" );
        }

        //将手指放在传感器上（或将卡刷卡到设备上）后，将触发此事件。
        //如果通过验证，返回值userid为用户注册号，否则返回值为-1；
        public void axCZKEM1_OnVerify(int iUserID)
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + ">指纹验证中,Verifying..." );
            if (iUserID != -1)
            {
                rtEventsMain.ShowMessage(machine.getMachinealias() + ">验证 OK,the UserID is " + iUserID.ToString());
            }
            else
            {
                rtEventsMain.ShowMessage(machine.getMachinealias() + ">验证 Failed... ");
            }
        }
        //队列名称
        const string qName = "att_transaction_queue";
        //路由名称
        const string exchangeName = "exchangeAttendance";
        //路由类型
        const string exchangeType = "fanout";//topic、fanout
        
        //如果您的指纹（或卡）通过验证，将触发此事件
        public void axCZKEM1_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            Thread.Sleep(30);
            rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent 打卡验证成功 OK" );
            rtEventsMain.ShowMessage("...UserID:" + sEnrollNumber);
            //rtEventsMain.ShowMessage("...isInvalid:" + iIsInValid.ToString());
            //rtEventsMain.ShowMessage("...attState:" + iAttState.ToString());
            //rtEventsMain.ShowMessage("...VerifyMethod:" + iVerifyMethod.ToString());
            //rtEventsMain.ShowMessage("...Workcode:" + iWorkCode.ToString());//the difference between the event OnAttTransaction and OnAttTransactionEx
            rtEventsMain.ShowMessage("...Time:" + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());
            JObject checkInfo = new JObject();
            checkInfo.Add("machineId", machine.getUid());
            checkInfo.Add("machineNum", machine.getMachinenumber());
            checkInfo.Add("sn", machine.getSn());
            checkInfo.Add("sEnrollNumber", sEnrollNumber);
            checkInfo.Add("iIsInValid", iIsInValid.ToString());
            checkInfo.Add("iAttState", iAttState.ToString());
            checkInfo.Add("iVerifyMethod", iVerifyMethod.ToString());
            checkInfo.Add("iWorkCode", iWorkCode.ToString());
            checkInfo.Add("time", iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());
            //var factory = new ConnectionFactory
            //{
            //    HostName = "192.168.2.242",
            //    UserName = "hello",
            //    Password = "world",
            //    Port = AmqpTcpEndpoint.UseDefaultPort,//5672
            //    VirtualHost = ConnectionFactory.DefaultVHost,//使用默认值："/"
            //    //AmqpUriSslProtocols = Protocols.DefaultProtocol,
            //    AutomaticRecoveryEnabled = true
            //};
            //using (var connection = factory.CreateConnection())
            using (var connection = RabbitMQFactory.SharedConnection)
            {
                using (var channel = connection.CreateModel())
                {
                    //设置交换器的类型

                    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                    //声明一个队列，设置队列是否持久化，排他性，与自动删除
                    channel.QueueDeclare(qName, durable: true, autoDelete: false, exclusive: false, arguments: null);
                    //绑定消息队列，交换器，routingkey
                    channel.QueueBind(qName, exchangeName, routingKey: qName);
                    var properties = channel.CreateBasicProperties();
                    //队列持久化
                    properties.Persistent = true;
                    var msgBody = Encoding.UTF8.GetBytes(checkInfo.ToString());
                    //发送信息
                    channel.BasicPublish(exchange: exchangeName, routingKey: qName, basicProperties: properties, body: msgBody);
                    rtEventsMain.ShowMessage("加入队列成功!");
                }
            }
        }

        //注册手指后，将触发此事件并返回已注册指纹的质量
        public void axCZKEM1_OnFingerFeature(int iScore)
        {
            if (iScore < 0)
            {
                rtEventsMain.ShowMessage(machine.getMachinealias() + "The quality of your fingerprint is poor");
            }
            else
            {
                rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent OnFingerFeature Has been Triggered...Score:　" + iScore.ToString());
            }
        }

        //当您注册手指时，将触发此事件。
        public void axCZKEM1_OnEnrollFingerEx(string sEnrollNumber, int iFingerIndex, int iActionResult, int iTemplateLength)
        {
            if (iActionResult == 0)
            {
                rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent OnEnrollFigerEx Has been Triggered....");
                rtEventsMain.ShowMessage(".....UserID: " + sEnrollNumber + " Index: " + iFingerIndex.ToString() + " tmpLen: " + iTemplateLength.ToString());
            }
            else
            {
                rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent OnEnrollFigerEx Has been Triggered Error,actionResult=" + iActionResult.ToString());
            }
        }

        //删除一个指纹模板后，将触发此事件。
        public void axCZKEM1_OnDeleteTemplate(int iEnrollNumber, int iFingerIndex)
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent OnDeleteTemplate Has been Triggered...");
            rtEventsMain.ShowMessage("...UserID=" + iEnrollNumber.ToString() + " FingerIndex=" + iFingerIndex.ToString());
        }

        //注册新用户后，将触发此事件。
        public void axCZKEM1_OnNewUser(int iEnrollNumber)
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent OnNewUser Has been Triggered...");
            rtEventsMain.ShowMessage("...NewUserID=" + iEnrollNumber.ToString());
        }

        //当您向设备刷卡时，将触发此事件以显示卡号。
        public void axCZKEM1_OnHIDNum(int iCardNumber)
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent OnHIDNum Has been Triggered...");
            rtEventsMain.ShowMessage("...Cardnumber=" + iCardNumber.ToString());
        }

        //当拆卸机器或强制报警发生时，触发此事件。
        public void axCZKEM1_OnAlarm(int iAlarmType, int iEnrollNumber, int iVerified)
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvnet OnAlarm Has been Triggered...");
            rtEventsMain.ShowMessage("...AlarmType=" + iAlarmType.ToString());
            rtEventsMain.ShowMessage("...EnrollNumber=" + iEnrollNumber.ToString());
            rtEventsMain.ShowMessage("...Verified=" + iVerified.ToString());
        }

        //车门传感器事件
        public void axCZKEM1_OnDoor(int iEventType)
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent Ondoor Has been Triggered...");
            rtEventsMain.ShowMessage("...EventType=" + iEventType.ToString());
        }

        //清空Mifare卡后，将触发此事件。
        public void axCZKEM1_OnEmptyCard(int iActionResult)
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent OnEmptyCard Has been Triggered...");
            if (iActionResult == 0)
            {
                rtEventsMain.ShowMessage("...Empty Mifare Card OK");
            }
            else
            {
                rtEventsMain.ShowMessage("...Empty Failed");
            }
        }

        //写入Mifare卡后，将触发此事件。
        public void axCZKEM1_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
        {
            rtEventsMain.ShowMessage(machine.getMachinealias() + "RTEvent OnWriteCard Has been Triggered...");
            if (iActionResult == 0)
            {
                rtEventsMain.ShowMessage("...Write Mifare Card OK");
                rtEventsMain.ShowMessage("...EnrollNumber=" + iEnrollNumber.ToString());
                rtEventsMain.ShowMessage("...TmpLength=" + iLength.ToString());
            }
            else
            {
                rtEventsMain.ShowMessage("...Write Failed");
            }
        }
    }
}
