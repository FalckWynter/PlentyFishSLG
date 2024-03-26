using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransportResource : IContainResource
{
    //是否使用白名单
    bool isUsePremit { get; set; }
    //连接到的节点ID
    List<int> connectNodes { get; set; }
    //继承该接口的类能够参与运输资源
    //资源黑白名单
    Dictionary<ResourceType, float> resourceLimit { get; set; }
    Dictionary<ResourceType, float> resourcePermit { get; set; }
    //传输列表
    Dictionary<ResourceType, float> resourceTransport { get; set; }

    //下次传输剩余时间 每次传输消耗时间
    float transportTime { get; set; }
    float transportMaxTime { get; set; }

    //尝试输入资源 返还输入失败的数量
    KeyValuePair<ResourceType, float> TryAddResource(KeyValuePair<ResourceType, float> pair, AddResourceType addType = AddResourceType.None);
    KeyValuePair<ResourceType, float> TryAddResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None);
    //尝试取出资源 返还能取出的最大数量
    KeyValuePair<ResourceType, float> TryRemoveResource(KeyValuePair<ResourceType, float> pair);
    KeyValuePair<ResourceType, float> TryRemoveResource(ResourceType type, float count);
    //更新传输剩余时间
    void UpdateTransportTime();
    //进行资源传输
    void TransportResource();
    //尝试对所有目标进行传输
    void TryTransportResource();
    //尝试对指定ID的目标进行传输
    KeyValuePair<ResourceType, float> TryTransportResource(KeyValuePair<ResourceType, float> pair, int targetID);
    KeyValuePair<ResourceType, float> TryTransportResource(ResourceType type, float count,int targetID);
    //容器是否已满(对黑白名单而言)
    bool isContainFull(KeyValuePair<ResourceType, float> pair);

    //触发器 - 传出资源时触发
    void TriggerOnOutputResource();
    //触发器 - 传入资源时触发
    void TriggetOnIncomingResource();
    //触发器 - 传输资源时触发
    void TriggerOnTransportResource();

    //初始化传输器
    void InitializeTransport();
}
