using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentGrid
{
    public bool isLarge = false;
    public bool isVisible = false;
    public bool isStartPoint = false;
    public Vector2Int position = new Vector2Int();
    #region �ڵ����
    //�ڵ�����
    public string label;
    //������
    public int nodeSeedCode = 10000000;

    //�Ƿ��ѱ������ڵ�����
    public bool isConnected = false;
    //���ӵ��Ľڵ�λ��
    public List<Vector2Int> connectNodeList = new List<Vector2Int>();
    //�Ƿ����ӵ��� �� �ҽڵ�
    public bool connectLeft = false, connectMid = false, connectRight = false;
    //������
    public bool isLocked = true;
    //�ڵ�ͼƬ
    public Sprite sprite;
    //�ڵ�ͼƬ·�� Resources��ʽ
    public string spritePath = "Images/Element/";

    #endregion

    #region ��������
    public TalentGrid()
    {
        label = "NotNull";
        Initialize();
    }
    public virtual void Initialize()
    {
        //��ʼ��ʱ����ͼƬ
        sprite = Resources.Load<Sprite>(spritePath + label);
    }
    public TalentGrid(Vector2Int pos)
    {
        position = pos;
        label = "NotNull";
        Initialize();
    }
    #endregion

    #region ��������
    /// <summary>
    /// ���ýڵ�λ��
    /// </summary>
    /// <param name="pos"></param>
    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }
    /// <summary>
    /// ������ӵ��Ľڵ�
    /// </summary>
    /// <param name="target"></param>
    public void AddConnectNode(Vector2Int target)
    {
        //��������ӵ�������
        if (connectNodeList.Contains(target))
            return;
        //���ڵ���ӵ��б�
        connectNodeList.Add(target);
        //����Ŀ��ڵ�λ����������״̬
        if (target.x > position.x)
            connectRight = true;
        else if (target.x < position.x)
            connectLeft = true;
        else
            connectMid = true;
        //����Ŀ��ڵ�Ϊ�ѱ��ڵ�����
        //RogueCore.Instance.map.levelData[(int)target.y].levelNodes[(int)target.x].isConnected = true;
    }
    #endregion
    
    #region �汾ע��
    /*
     * ��˾���� : Զ��������
     * 

    */
    #endregion
}
public enum GridType
{
    Resource,Alteration,Consume,RoA,RoC,AoC,All
}

