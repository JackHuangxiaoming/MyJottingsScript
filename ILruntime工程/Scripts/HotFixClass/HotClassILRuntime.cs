using ILRuntime.CLR.Method;
using System;
using UnityEngine;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using System.Collections.Generic;

public class HotClassILRuntime
{
    private string _classFullName;
    private IType _wantType;
    private IMethod _methodctor;
    private ILRuntime.Runtime.Enviorment.AppDomain appDomain;
    private ILTypeInstance _mTypeObj;
    private Dictionary<string, Dictionary<int, IMethod>> _methodsDic;

    /// <summary>
    /// 构造函数
    /// </summary>
    IMethod m_Ctor; 

    public HotClassILRuntime(string classFullName)
    {
        _classFullName = classFullName;
        appDomain = HotScriptManager.Instance.appDomain;
        _methodsDic = new Dictionary<string, Dictionary<int, IMethod>>();
        CtorClass();
    }

    private void CtorClass()
    {
        if (_wantType == null)
            _wantType = appDomain.LoadedTypes[_classFullName];//用全名称，包括命名空间
        if (_wantType == null)
            Utils.LogError("构造脚本失败：" + _classFullName);
        else
            _mTypeObj = ((ILType)_wantType).Instantiate(true);        
    }

    /// <summary>
    /// 自定义调用函数  函数签名长度为唯一标准（做了缓存）
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="prs"></param>
    public object CallMethod(string methodName, params object[] prs)
    {
        if (_mTypeObj == null || _wantType == null || string.IsNullOrEmpty(methodName))
        {
            return null;
        }
        int paramsCount = 0;
        if (prs == null)
            prs = new object[] { };
        else
            paramsCount = prs.Length;
        if (!_methodsDic.ContainsKey(methodName))
        {
            _methodsDic[methodName] = new Dictionary<int, IMethod>();
        }
        IMethod method02 = null;

        if (_methodsDic[methodName].ContainsKey(paramsCount))
        {
            method02 = _methodsDic[methodName][paramsCount];
        }
        else
        {
            method02 = _wantType.GetMethod(methodName, paramsCount);
            _methodsDic[methodName][paramsCount] = method02;
        }
        if (method02 != null)
            return appDomain.Invoke(method02, _mTypeObj, prs);
        return null;
    }

    /// <summary>
    /// 自定义函数调用 根据函数签名类型调用（没有做缓存）
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="obj"></param>
    public void CallMethodByType(string methodName, params object[] obj)
    {
        if (obj != null && obj.Length % 2 != 0)
        {
            Debug.LogError("参数传入不规范,数量不等于2的倍数");
            return;
        }
        if (_mTypeObj == null || _wantType == null || string.IsNullOrEmpty(methodName))
        {
            return;
        }
        List<IType> list = new List<IType>(); ;
        object[] paramArr = null;

        if (obj != null || obj.Length != 0)
        {
            List<object> arr = new List<object>();
            int count = obj.Length / 2;
            for (int i = 0, len = obj.Length; i < len; i++)
            {
                if (i % 2 == 0)
                    arr.Add(obj[i]);
                else
                    list.Add(appDomain.GetType((Type)obj[i]));
            }
            paramArr = arr.ToArray();
        }

        IMethod method02 = _wantType.GetMethod(methodName, list, null);
        if (method02 != null)
            appDomain.Invoke(method02, _mTypeObj, paramArr);
    }

    /// <summary>
    /// 自定义泛型函数调用
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="obj"></param>
    public void CallGenericMethod(string methodName, Type[] genericArguments, params object[] obj)
    {
        if (_mTypeObj == null || _wantType == null || string.IsNullOrEmpty(methodName))
        {
            return;
        }
        if (null == genericArguments || genericArguments.Length == 0)
        {
            Debug.LogError("参数传入不规范,没有泛型类型");
            return;
        }
        List<IType> genericArs = new List<IType>();
        foreach (var type in genericArguments)
        {
            genericArs.Add(appDomain.GetType(type.GetType()));
        }

        object[] paramArr = null;
        List<IType> paramList = null;
        if (obj != null || obj.Length != 0)
        {
            paramList = new List<IType>();
            List<object> arr = new List<object>();
            foreach (var item in obj)
            {
                arr.Add(item);
                paramList.Add(appDomain.GetType(item.GetType()));
            }
            paramArr = arr.ToArray();
        }

        IMethod method02 = _wantType.GetMethod(methodName, paramList, genericArs.ToArray());
        if (method02 != null)
            appDomain.Invoke(method02, _mTypeObj, paramArr);
    }
}
