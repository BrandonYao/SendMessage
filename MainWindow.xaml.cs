using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication5
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string url = "http://utf8.sms.webchinese.cn/?";//发送短信平台网址SMS  
        private string strUid = "Uid=";//注册的SMS平台的账号ID  
        private string strKey = "&key=5e137241d8aa91d71050";//注册的SMS平台的接口密匙  
        private string strMob = "&smsMob=";//手机号码  
        private string strContent = "&smsText=";// 发送的内容  
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            url = url + strUid + "yaobin219" + strKey + strMob + "13771770219" + strContent + "验证码：123421";
            string Result = GetHtmlFromUrl(url);
            CheckSend(Result);
        }
        public string GetHtmlFromUrl(string url)
        {
            string strRet = null;
            if (url == null || url.Trim().ToString() == "")
            {
                return strRet;
            }
            string targeturl = url.Trim().ToString();
            try
            {
                HttpWebRequest hr = (HttpWebRequest)WebRequest.Create(targeturl);
                hr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
                hr.Method = "GET";
                hr.Timeout = 30 * 60 * 1000;
                WebResponse hs = hr.GetResponse();
                Stream sr = hs.GetResponseStream();
                StreamReader ser = new StreamReader(sr, Encoding.Default);
                strRet = ser.ReadToEnd();
            }
            catch (Exception ex)
            {
                strRet = null;
            }
            return strRet;
        }

        void CheckSend(string result)
        {
            string msg = null;
            switch (int.Parse(result))
            {
                case -1:
                    msg = "没有该用户账户";
                    break;
                case -2:
                    msg = "接口密钥不正确[查看密钥]不是账户登陆密码";
                    break;
                case -21:
                    msg = "MD5接口密钥加密不正确";
                    break;
                case -3:
                    msg = "短信数量不足";
                    break;
                case -11:
                    msg = "该用户被禁用";
                    break;
                case -14:
                    msg = "短信内容出现非法字符";
                    break;
                case -4:
                    msg = "手机号格式不正确";
                    break;
                case -41:
                    msg = "手机号码为空";
                    break;
                case -42:
                    msg = "短信内容为空";
                    break;
                case -51:
                    msg = "短信签名格式不正确，接口签名格式为：【签名内容】";
                    break;
                case -6:
                    msg = "IP限制";
                    break;
                default:
                    msg = "发送成功";
                    break;
            }
            MessageBox.Show(msg);
        }
    }
}
