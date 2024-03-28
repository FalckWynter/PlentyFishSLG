using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象管道类 没用上
/// </summary>
public class AbstractPipe : AbstractContain, IUpgradeAble
{
    
    #region 天赋参数

    //等级参数
    public int grade { get; set; }

    public int maxGrade { get; set; }

    public Dictionary<ResourceType, float> upgradeCondition { get; set; }

    #endregion


    #region 基本函数
    // Start is called before the first frame update
    public AbstractPipe()
    {

    }

    public override void InitializeTalent()
    {
        InitializePre();
        InitializeContain();
        InitializeTransport();
        InitializeUpgrade();
    }
    public override void InitializePre()
    {
        base.InitializePre();
        upgradeCondition = new Dictionary<ResourceType, float>();
        Debug.Log("尝试初始化ID" + imageID);
        maxGrade = 1;
        grade = 1;
        Debug.Log("启动构造函数");
    }
    public override void InitializeContain() { }
    /// <summary>
    /// 初始化 - 传输表 每次传输间隔 是否使用白名单 黑白名单
    /// </summary>
    public override void InitializeTransport()
    {
        base.InitializeTransport();
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
        UpdateTransportTime();
        UpdateUpgradeCondition();
        UpdateAutoCollect();
    }
    #endregion
    #region 等级函数
    /// <summary>
    /// 提升等级及事件
    /// </summary>
    /// <returns>是否提升成功</returns>
    public virtual bool Upgrade()
    {
        foreach (KeyValuePair<ResourceType, float> pair in upgradeCondition)
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
