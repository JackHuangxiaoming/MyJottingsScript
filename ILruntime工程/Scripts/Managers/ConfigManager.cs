using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ConfigManager
{
    /// <summary>
    /// 包体风格
    /// </summary>
    public string PackageStyle { get; set; }
    /// <summary>
    /// 包体编号
    /// </summary>
    public string PackageNum { get; set; }
    /// <summary>
    /// 包体Logo编号
    /// </summary>
    public string LogoNum { get; set; }
    /// <summary>
    /// 是否主动退出游戏
    /// </summary>
    public bool IsExitAccount { get; set; }
    /// <summary>
    /// 产品名称
    /// </summary>
    public string ProductName { get; set; }
    /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }
    /// <summary>
    /// 包名
    /// </summary>
    public string PackageName
    {
        get
        {
            return CompanyName + ProductName;
        }
    }


    private static ConfigManager _instance = null;
    public static ConfigManager Instance
    {
        get
        {
            if (null == _instance)
            {
                new ConfigManager();
            }
            return _instance;
        }
    }

    private ConfigManager()
    {
        _instance = this;
    }

    public void Init()
    {
        IsExitAccount = false;
        PackageStyle = "1";
        PackageNum = "0";
        LogoNum = "0";
        ProductName = Application.productName;
        CompanyName = Application.companyName;
    }


}
