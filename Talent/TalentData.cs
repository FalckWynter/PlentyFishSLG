using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameCore.Tool;
public class TalentData : Singleton<TalentData>
{
    //Image数据路径表
    public List<List<string[]>> Path;
    //图片名称表路径
    public static string ExcelPath = Application.dataPath + "/Resources/DataExcel/Talent/TalentDataExcel.xls";
    //图片表string位置
    public static int ExcelPathPlace = 1;
    //图片表类间距
    public static int Range = 10000;
    //图片基础路径
    public static string BasicPath = "Images/";
    public static int IDPlace = 0;

    //预制体路径
    public GameObject prefab;

    public string prefabPath = "Prefabs/Contain/Talent";

    public GameObject talentParent;
    public override void Initialize()
    {
        //载入ImagePath表，从公用参数中调用并输入路径
        Path = GameComponent.LoadExcelDataByPath(ExcelPath);
        prefab = Resources.Load<GameObject>(prefabPath);
        talentParent = GameObject.Find("TalentSpawner");
    }
    public string GetPath(int ID)
    {
        string path = null;
        int headID = int.Parse(Path[0].Find(x => x[IDPlace] == GetTypeID(ID).ToString())[0]);
        if (ID <= 10000)
        {
            Debug.LogWarning("错误的ImageID:过大或过小");
            return null;
        }
        try
        {
            //取ID头位数得所属类型，取ID子位数得图片名字
            path = BasicPath + Path[0][headID][ExcelPathPlace]
                + "/" + Path[headID].Find(x => x[IDPlace] == GetCountID(ID).ToString())[ExcelPathPlace];
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message + "获取图片路径不正确!!!请检查输入的ImageID是否越界");

        }
        return path;

    }
    public Sprite GetImage(int imageID)
    {
        //return null;
        Sprite sprite = Resources.Load<Sprite>(GetPath(imageID));
        //Debug.Log("图片路径" + sprite.name);
        return sprite;
    }
    public int GetCountID(int imageID)
    {
        return (imageID - ((imageID / Range) * Range));
    }
    public int GetTypeID(int imageID)
    {
        return imageID / Range;
    }
}
