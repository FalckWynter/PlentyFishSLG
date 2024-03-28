using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentGrid
{
    public bool isLarge = false;
    public bool isVisible = false;
    public bool isStartPoint = false;
    public Vector2Int position = new Vector2Int();
    #region 节点参数
    //节点名称
    public string label;
    //房间码
    public int nodeSeedCode = 10000000;

    //是否已被其他节点连接
    public bool isConnected = false;
    //连接到的节点位置
    public List<Vector2Int> connectNodeList = new List<Vector2Int>();
    //是否连接到左 中 右节点
    public bool connectLeft = false, connectMid = false, connectRight = false;
    //被锁定
    public bool isLocked = true;
    //节点图片
    public Sprite sprite;
    //节点图片路径 Resources方式
    public string spritePath = "Images/Element/";

    #endregion

    #region 基本函数
    public TalentGrid()
    {
        label = "NotNull";
        Initialize();
    }
    public virtual void Initialize()
    {
        //初始化时载入图片
        sprite = Resources.Load<Sprite>(spritePath + label);
    }
    public TalentGrid(Vector2Int pos)
    {
        position = pos;
        label = "NotNull";
        Initialize();
    }
    #endregion

    #region 参数互动
    /// <summary>
    /// 设置节点位置
    /// </summary>
    /// <param name="pos"></param>
    public void SetPosition(Vector2Int pos)
    {
        position = pos;
    }
    /// <summary>
    /// 添加连接到的节点
    /// </summary>
    /// <param name="target"></param>
    public void AddConnectNode(Vector2Int target)
    {
        //如果已连接到则跳过
        if (connectNodeList.Contains(target))
            return;
        //将节点添加到列表
        connectNodeList.Add(target);
        //根据目标节点位置设置连接状态
        if (target.x > position.x)
            connectRight = true;
        else if (target.x < position.x)
            connectLeft = true;
        else
            connectMid = true;
        //设置目标节点为已被节点链接
        //RogueCore.Instance.map.levelData[(int)target.y].levelNodes[(int)target.x].isConnected = true;
    }
    #endregion
    
    #region 版本注释
    /*
     * 公司名称 : 远冬工作室
     * 

    */
    #endregion
}
public enum GridType
{
    Resource,Alteration,Consume,RoA,RoC,AoC,All
}

