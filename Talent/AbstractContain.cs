using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractContain : ITransportResource, IModifyResource
{
    #region 天赋基本属性
    public string label;
    public int containID;
    public string commnets;
    public int gameIndex;
    public int imageID = 10001;
    public Sprite sprite;
    public static string spritePath = "Images/Talent";
    #endregion

    #region 天赋容器属性
    //资源容器
    public Dictionary<ResourceType, float> resourceContain { get; set; }
    public bool isDeleteNullContainResourceType { get; set; }

    //容器传输参数
    public Dictionary<ResourceType, float> resourceTransport { get; set; }

    public bool isUsePremit { get; set; }
    public Dictionary<ResourceType, float> resourceLimit { get; set; }
    public Dictionary<ResourceType, float> resourcePermit { get; set; }

    public List<int> connectNodes { get; set; }

    public float transportTime { get; set; }
    public float transportMaxTime { get; set; }


    #endregion

    #region 基本函数
    // Start is called before the first frame update
    public AbstractContain()
    {

    }

    public virtual void InitializeTalent()
    {
        InitializePre();
        InitializeContain();
        InitializeTransport();
    }
    public virtual void InitializePre()
    {
        resourceContain = new Dictionary<ResourceType, float>();
        resourceTransport = new Dictionary<ResourceType, float>();
        resourceLimit = new Dictionary<ResourceType, float>();
        resourcePermit = new Dictionary<ResourceType, float>();
        connectNodes = new List<int>();

        sprite = ImageData.Instance.GetImage(imageID);
        isDeleteNullContainResourceType = true;

        transportMaxTime = 10;

        Debug.Log("启动构造函数");
    }
    public virtual void InitializeContain() { }
    /// <summary>
    /// 初始化 - 传输表 每次传输间隔 是否使用白名单 黑白名单
    /// </summary>
    public virtual void InitializeTransport()
    {

        transportTime = transportMaxTime;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        UpdateTransportTime();
    }
    #endregion

    #region 容器函数


    /// <summary>
    /// //添加资源 不需要任何验算
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <param name="addType"></param>
    /// <returns>返还是否添加成功</returns>
    public bool AddResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None)
    {
        if (addType == AddResourceType.Transport)
            TriggerOnGetResourceFromTransport();

        //修饰输入
        KeyValuePair<ResourceType, float> pair = new KeyValuePair<ResourceType, float>(type, count);
        pair = ModifyOnAddResource(pair);

        //如果修饰后值为负数 结束添加
        if (pair.Value <= 0)
            return false;
        //添加资源
        if (!resourceContain.ContainsKey(type))
            resourceContain.Add(type, 0);
        resourceContain[type] += pair.Value;
        //启动触发器
        TriggerOnAddResource();
        return true;
    }


    /// <summary>
    /// 取出资源
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <param name="takeOutType"></param>
    /// <returns>返还取出的数量</returns>
    public KeyValuePair<ResourceType, float> TakeOutResource(ResourceType type, float count, TakeOutResourceType takeOutType = TakeOutResourceType.None)
    {
        //如果不存在该类型 返还0个该元素
        if (!resourceContain.ContainsKey(type))
            return new KeyValuePair<ResourceType, float>(type, 0);
        KeyValuePair<ResourceType, float> pair = new KeyValuePair<ResourceType, float>(type, count);
        //此处修改消耗
        resourceContain[type] -= ModifyOnCostResource(pair).Value;
        TriggerOnRemoveResource();
        //修改取出的量
        pair = ModifyOnRemoveResource(pair);
        if (resourceContain[type] == 0)
        {
            if (isDeleteNullContainResourceType)
                DeleteResource(type);
            //触发资源耗尽
            TriggerOnResourceDepleted();
        }
        return pair;
    }



    /// <summary>
    /// 设置资源数量
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <param name="addType"></param>
    /// <returns>如果不存在该资源返还否</returns>
    public bool SetResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None, bool isForceSet = true)
    {
        if (!resourceContain.ContainsKey(type))
            return false;

        resourceContain[type] = count;

        //触发资源改变触发器
        TriggerOnResourceChange();
        return true;
    }

    //是否有足够的某种资源
    /// <summary>
    /// 查找是否有某种资源
    /// </summary>
    /// <param name="type"></param>
    /// <returns>有则返还是</returns>
    public virtual bool IsHaveResourceType(ResourceType type)
    {
        return resourceContain.ContainsKey(type);
    }
    /// <summary>
    /// 查找是否有足够数量的某种资源
    /// </summary>
    /// <param name="type"></param>
    /// <returns>有则返还是</returns>
    public bool IsHaveEnoughResource(ResourceType type, float count)
    {
        if (!resourceContain.ContainsKey(type))
            return false;
        if (resourceContain[type] < count)
            return false;
        return true;
    }
    public bool IsHaveEnoughResource(KeyValuePair<ResourceType, float> pair)
    {
        return IsHaveEnoughResource(pair.Key, pair.Value);
    }
    /// <summary>
    /// 获得最大Count的Type资源数量的复制
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns>返还资源的复制</returns>
    public KeyValuePair<ResourceType, float> GetResourceCopy(ResourceType type, float count)
    {
        if (IsHaveEnoughResource(type, count))
            return new KeyValuePair<ResourceType, float>(type, count);

        if (!IsHaveResourceType(type))
            return new KeyValuePair<ResourceType, float>(type, 0);

        return new KeyValuePair<ResourceType, float>(type, resourceContain[type]);
    }


    /// <summary>
    /// 将某种资源删除 * 像一个测试版功能
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool DeleteResource(ResourceType type)
    {
        if (!resourceContain.ContainsKey(type))
            return false;
        //应该通过置0进行
        resourceContain.Remove(type);
        return true;
    }

    //添加资源时
    public virtual void TriggerOnAddResource()
    {

    }
    //取出资源时
    public virtual void TriggerOnRemoveResource()
    {

    }
    //资源数量改变的触发器
    public virtual void TriggerOnResourceChange()
    {

    }
    //某种资源耗尽时
    public virtual void TriggerOnResourceDepleted()
    {

    }
    #endregion


    #region 传输资源函数
    /// <summary>
    /// 尝试添加资源
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns>返还未成功添加的资源</returns>
    public KeyValuePair<ResourceType, float> TryAddResource(KeyValuePair<ResourceType, float> pair, AddResourceType addType = AddResourceType.None)
    {
        return TryAddResource(pair.Key, pair.Value);
    }
    public KeyValuePair<ResourceType, float> TryAddResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None)
    {
        //最终能添加的数量
        float addCount = count;
        //如果限制列表存在该元素 黑名单中有
        if (resourceLimit.ContainsKey(type))
        {
            //如果超出容量或禁止输入 返还全部资源
            if (resourceContain[type] >= resourceLimit[type] || resourceLimit[type] < 0)
                return new KeyValuePair<ResourceType, float>(type, count);
            //否则尽可能输入资源
            //要添加的数量是否大于容量 若大于则添加所有，否则添加差额
            addCount = (resourceLimit[type] - resourceContain[type]) >= count ? count : resourceLimit[type] - resourceContain[type];
            //AddResource(type, addCount,addType);
            //返还未添加的差额
        }

        //如果使用白名单
        if (isUsePremit)
        {
            //如果不在白名单中
            if (!resourcePermit.ContainsKey(type))
                //全部返回
                return new KeyValuePair<ResourceType, float>(type, count);
            //否则添加尽可能多的值
            //如果白名单为-1则不做修改
            if (resourcePermit[type] == -1)
                addCount = count;
            else
                addCount = (resourcePermit[type] - resourceContain[type]) >= count ? count : resourcePermit[type] - resourceContain[type];
            //AddResource(type, addCount,addType);

        }

        //常规情况直接添加
        AddResource(type, addCount, addType);
        return new KeyValuePair<ResourceType, float>(type, count - addCount);
    }
    /// <summary>
    /// 尝试取出指定数量的资源
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns>返还成功取出的数量</returns>
    public KeyValuePair<ResourceType, float> TryRemoveResource(KeyValuePair<ResourceType, float> pair)
    {
        return TryRemoveResource(pair.Key, pair.Value);
    }
    public KeyValuePair<ResourceType, float> TryRemoveResource(ResourceType type, float count)
    {
        //KeyValuePair<ResourceType, int> pair = new KeyValuePair<ResourceType, int>(type, 0);
        //不包含该资源或者值为负数
        if (!resourceContain.ContainsKey(type) || resourceContain[type] <= 0)
            return new KeyValuePair<ResourceType, float>(type, 0);
        //有但是数量不足 返回剩余的全部
        if (resourceContain[type] < count)
            return TakeOutResource(type, resourceContain[type]);

        //返回指定数量 * 可能会有BUG
        return TakeOutResource(type, count);
    }
    public bool isContainFull(KeyValuePair<ResourceType, float> pair)
    {
        //如果使用黑名单
        if (resourceLimit.ContainsKey(pair.Key))
        {
            //如果超出容量或禁止输入 返还满
            if (resourceContain[pair.Key] >= resourceLimit[pair.Key] || resourceLimit[pair.Key] < 0)
                return true;
        }

        //如果使用白名单
        if (isUsePremit)
        {
            //如果不在白名单中 或容量超限
            if (!resourcePermit.ContainsKey(pair.Key) && resourceContain[pair.Key] >= resourcePermit[pair.Key])
                //返回满
                return true;

        }
        //返还不满
        return false;
    }
    /// <summary>
    /// 更新传输计时器
    /// </summary>
    public void UpdateTransportTime()
    {
        if (transportTime > 0)
        {
            transportTime -= Time.deltaTime * ContainManager.Instance.timeSpeed;
            return;
        }
        if (transportTime <= 0)
        {
            TransportResource();
            transportTime = transportMaxTime;
        }
    }
    /// <summary>
    /// 进行一次资源运输
    /// </summary>
    public void TransportResource()
    {
        TryTransportResource();
    }
    /// <summary>
    /// 尝试进行对所有目标一次资源运输 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public void TryTransportResource()
    {
        KeyValuePair<ResourceType, float> pair = new KeyValuePair<ResourceType, float>();
        //对列表中每一个节点
        foreach (int node in connectNodes)
        {
            //对每一条要传输的元素
            foreach (KeyValuePair<ResourceType, float> resource in resourceTransport)
            {
                //如果装不下该元素 跳过
                if (ContainManager.Instance.ContainDictionary[node].isContainFull(resource))
                    continue;
                //取出元素
                pair = TryRemoveResource(resource);
                //如果取出空值 跳过
                if (pair.Value == 0)
                    continue;
                //向目标运输 获取剩余值
                pair = TryTransportResource(pair, node);
                //将剩余值存回容器
                TryAddResource(pair);
            }

        }
    }
    /// <summary>
    /// 尝试对指定目标进行一次资源运输
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <param name="targetID"></param>
    public KeyValuePair<ResourceType, float> TryTransportResource(KeyValuePair<ResourceType, float> pair, int targetID)
    {
        return TryTransportResource(pair.Key, pair.Value, targetID);
    }
    public KeyValuePair<ResourceType, float> TryTransportResource(ResourceType type, float count, int targetID)
    {
        return ContainManager.Instance.ContainDictionary[targetID].TryAddResource(type, count, AddResourceType.Transport);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool TryAddConnectNode(int id)
    {
        if (connectNodes.Contains(id))
            return false;
        connectNodes.Add(id);
        return true;
    }
    public virtual void TriggerOnGetResourceFromTransport()
    {

    }

    public virtual void TriggerOnOutputResource()
    {

    }

    public virtual void TriggetOnIncomingResource()
    {

    }

    public virtual void TriggerOnTransportResource()
    {

    }
    #endregion
    #region 修饰器函数
    public virtual KeyValuePair<ResourceType, float> ModifyOnAddResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnAddResource(pair);
        return pair;
    }

    public virtual KeyValuePair<ResourceType, float> ModifyOnRemoveResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnRemoveResource(pair);
        return pair;
    }

    public virtual KeyValuePair<ResourceType, float> ModifyOnResourceChange(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnResourceChange(pair);
        return pair;
    }

    public virtual KeyValuePair<ResourceType, float> ModifyOnCostResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnCostResource(pair);
        return pair;
    }

    public virtual KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnProduceResource(pair);
        return pair;
    }



    #endregion
}
