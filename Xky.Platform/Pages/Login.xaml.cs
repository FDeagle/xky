﻿using System;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json.Linq;
using Xky.Core;
using Xky.Core.Common;

namespace Xky.Platform.Pages
{
    /// <summary>
    ///     MyLogin.xaml 的交互逻辑
    /// </summary>
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
            BtnLogin.Click += Login_Click;
            Client.StartAction(() =>
            {
                var json = Common.LoadJson("license");
                if (json != null) Common.UiAction(() => { LicenseKey.Text = json["license"].ToString(); });
            });
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var licensekey = LicenseKey.Text;

            Client.StartAction(() =>
            {
                Common.UiAction(() =>
                {
                    BtnLogin.Text = "正在授权";
                    BtnLogin.IsEnabled = false;
                });

                var response = Client.AuthLicense(licensekey);
                if (response.Result)
                {
                    Common.SaveJson("license", new JObject {["license"] = licensekey});
                    Common.ShowToast("授权成功:" + Client.License.LicenseName, Colors.Lime, "on");
                    Thread.Sleep(500);

                    //启动局域网节点探查器
                   
                    Common.UiAction(() =>
                    {
                        Common.MainWindow.LoginTabItem.Visibility = Visibility.Collapsed;
                        Common.MainWindow.MainControlTabItem.ClickDown(null, null);
                        Common.MyMainControl.LoadNodes();
                        Common.MyMainControl.LoadDevices();
                        Common.MyMainControl.LoadModules();
                    });
                }
                else
                {
                    Common.ShowToast(response.Message, Color.FromRgb(255, 28, 28));
                }

                Common.UiAction(() =>
                {
                    BtnLogin.Text = "授权";
                    BtnLogin.IsEnabled = true;
                });
            });
        }
    }
}