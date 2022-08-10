using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{

    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new DataManager();
            return _instance;
        }
    }
    public DataManager()
    {
        _instance = this;
    }

    private void Start()
    {
    }
    public Type GetType(string name)
    {
        return Type.GetType(name);
    }
}