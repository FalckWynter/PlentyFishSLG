using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameCore.Tool;
public class TalentData : Singleton<TalentData>
{
    //Image����·����
    public List<List<string[]>> Path;
    //ͼƬ���Ʊ�·��
    public static string ExcelPath = Application.dataPath + "/Resources/DataExcel/Talent/TalentDataExcel.xls";
    //ͼƬ��stringλ��
    public static int ExcelPathPlace = 1;
    //ͼƬ������
    public static int Range = 10000;
    //ͼƬ����·��
    public static string BasicPath = "Images/";
    public static int IDPlace = 0;

    //Ԥ����·��
    public GameObject prefab;

    public string prefabPath = "Prefabs/Contain/Talent";

    public GameObject talentParent;
    public override void Initialize()
    {
        //����ImagePath���ӹ��ò����е��ò�����·��
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
            Debug.LogWarning("�����ImageID:������С");
            return null;
        }
        try
        {
            //ȡIDͷλ�����������ͣ�ȡID��λ����ͼƬ����
            path = BasicPath + Path[0][headID][ExcelPathPlace]
                + "/" + Path[headID].Find(x => x[IDPlace] == GetCountID(ID).ToString())[ExcelPathPlace];
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message + "��ȡͼƬ·������ȷ!!!���������ImageID�Ƿ�Խ��");

        }
        return path;

    }
    public Sprite GetImage(int imageID)
    {
        //return null;
        Sprite sprite = Resources.Load<Sprite>(GetPath(imageID));
        //Debug.Log("ͼƬ·��" + sprite.name);
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
