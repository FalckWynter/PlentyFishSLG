using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AbstractTalent : AbstractContain, IProduceResource<AbstractTalent>, IUpgradeAble
{
    #region 天赋参数
    //容器制造参数
    public Dictionary<ResourceType, float> produceList { get; set; }

    public Dictionary<ResourceType, float> produceCost { get; set; }

    public int produceTryCount { get; set; }

    public float produceTime { get; set; }
    public float produceMaxTime { get; set; }

    //等级参数
    public int grade { get; set; }

    public int maxGrade { get; set; }

    public Dictionary<ResourceType, float> upgradeCondition { get; set; }

    #endregion

    #region 基本函数
    // Start is called before the first frame update
    public AbstractTalent()
    {
        
    }

    public override void InitializeTalent()
    {
        InitializePre();
        InitializeContain();
        InitializeTransport();
        InitializeProduce();
        InitializeUpgrade();
    }
    public override void InitializePre()
    {
        resourceContain = new Dictionary<ResourceType, float>();
        resourceTransport = new Dictionary<ResourceType, float>();
        resourceLimit = new Dictionary<ResourceType, float>();
        resourcePermit = new Dictionary<ResourceType, float>();
        produceList = new Dictionary<ResourceType, float>();
        produceCost = new Dictionary<ResourceType, float>();
        upgradeCondition = new Dictionary<ResourceType, float>();
        connectNodes = new List<int>();
        Debug.Log("尝试初始化ID" + imageID);
        sprite = ImageData.Instance.GetImage(imageID);
        isDeleteNullContainResourceType = true;

        transportMaxTime = 10;

        produceMaxTime = 10;
        produceTryCount = 1;

        maxGrade = 1;
        grade = 1;
        Debug.Log("启动构造函数");
    }
    public override void InitializeContain() {}
    /// <summary>
    /// 初始化 - 传输表 每次传输间隔 是否使用白名单 黑白名单
    /// </summary>
    public override void InitializeTransport()
    {

        transportTime = transportMaxTime;
    }
    /// <summary>
    /// 初始化 - 制造表 制造消耗 每次制造间隔 每次制造次数
    /// </summary>
    public virtual void InitializeProduce()
    {

        produceTime = produceMaxTime;
    }
    /// <summary>
    /// 初始化 - 最大等级 当前等级 升级条件
    /// </summary>
    public virtual void InitializeUpgrade()
    {

    }
    // Update is called once per frame
    public override void Update()
    {
        UpdateProduceTime();
        UpdateTransportTime();
        UpdateUpgradeCondition();
    }
    #endregion

    
    #region 制造资源函数
    /// <summary>
    /// 尝试制造资源
    /// </summary>
    /// <returns></returns>
    public virtual bool ProduceResource()
    {
        for (int i = 0; i < produceTryCount; i++)
        {
            foreach (KeyValuePair<ResourceType, float> pair in produceCost)
            {
                //如果不满足消耗
                if (!IsHaveEnoughResource(pair))
                {
                    Debug.LogWarning("资源不足" + this.GetType());
                    return false;
                }
            }
            foreach (KeyValuePair<ResourceType, float> pair in produceList)
            {
                //修饰阐述
                KeyValuePair<ResourceType, float> temp = ModifyOnProduceResource(pair);
                //添加一份资源
                TryAddResource(temp.Key, temp.Value);
                Debug.Log("制造了一份资源" + pair + "来源" + this.GetType());
            }
            TriggerOnProduceResource();

        }
        //制造成功
        return true;
    }


    public virtual void TriggerOnProduceResource()
    {

    }
    /// <summary>
    /// 更新制造计数器
    /// </summary>
    public void UpdateProduceTime()
    {
        if (produceTime > 0)
        {

            produceTime -= Time.deltaTime * ContainManager.Instance.timeSpeed;
            return;
        }
        if (produceTime <= 0)
        {
            ProduceResource();
            produceTime = produceMaxTime;
        }
    }

    #endregion

    #region 等级函数
    /// <summary>
    /// 提升等级及事件
    /// </summary>
    /// <returns>是否提升成功</returns>
    public virtual bool Upgrade()
    {
        foreach(KeyValuePair<ResourceType,float> pair in upgradeCondition)
        {
            TryRemoveResource(pair);
        }
        grade++;

        Debug.Log("天赋ID" + containID + "升级到" + grade);
        return true;
    }
    /// <summary>
    /// 是否满足升级要求
    /// </summary>
    /// <returns>满足返回是</returns>
    public virtual bool IsUpgradeAble()
    {
        foreach (KeyValuePair<ResourceType, float> pair in upgradeCondition)
        {
            if (!IsHaveEnoughResource(pair))
                return false;
        }
        return true;
    }
    /// <summary>
    /// 更新是否满足升级条件
    /// </summary>
    public void UpdateUpgradeCondition()
    {
        if (IsUpgradeAble())
            Upgrade();
    }
    #endregion

    #region 修饰器函数
    public override KeyValuePair<ResourceType, float> ModifyOnAddResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnAddResource(pair);
        return pair;
    }

    public override KeyValuePair<ResourceType, float> ModifyOnRemoveResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnRemoveResource(pair);
        return pair;
    }

    public override KeyValuePair<ResourceType, float> ModifyOnResourceChange(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnResourceChange(pair);
        return pair;
    }

    public override KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnProduceResource(pair);
        return pair;
    }

    public override KeyValuePair<ResourceType, float> ModifyOnCostResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnCostResource(pair);
        return pair;
    }



    #endregion
}