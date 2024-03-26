using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProduceResource<T> where T : IContainResource
{
    //必须能承装资源才可以制造资源
    //每次制造的资源列表
    Dictionary<ResourceType,float> produceList { get; set; }
    //每次制造的消耗资源
    Dictionary<ResourceType, float> produceCost { get; set; }
    //资源制造剩余时间 资源制造需要时间
    float produceTime { get; set; }
    float produceMaxTime { get; set; }
    //制造资源的尝试次数 * 最终制造数量为 次数*每次产量
    int produceTryCount { get; set; }
    //制造资源
    bool ProduceResource();
    //更新制造资源计时器
    void UpdateProduceTime();
    //触发器 - 制造资源时
    void TriggerOnProduceResource();

    //初始化制造器
    void InitializeProduce();
}
