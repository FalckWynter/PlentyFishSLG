using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainResource
{
    //继承该接口的容器能承装资源
    //资源容器
    Dictionary<ResourceType, float> resourceContain { get; set; }
    //是否移除空的资源类型
    bool isDeleteNullContainResourceType { get; set; }

    //输入资源
    bool AddResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None);
    //取出资源
    KeyValuePair<ResourceType, float> TakeOutResource(ResourceType type, float count, TakeOutResourceType takeOutType = TakeOutResourceType.None);
    //是否具有某种资源
    bool IsHaveResourceType(ResourceType type);
    //是否具有足够数量的某种资源
    bool IsHaveEnoughResource(ResourceType type, float count);
    bool IsHaveEnoughResource(KeyValuePair<ResourceType, float> pair);
    //设置某种资源的数量
    bool SetResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None, bool isForceSet = true);
    //获取所具有的某种资源的副本
    KeyValuePair<ResourceType, float> GetResourceCopy(ResourceType type, float count);
    //删除资源
    bool DeleteResource(ResourceType type);

    //触发器 - 输入资源时触发
    void TriggerOnAddResource();
    //触发器 - 输出资源时触发
    void TriggerOnRemoveResource();
    //触发器 - 资源改变时触发
    void TriggerOnResourceChange();
    //触发器 - 某种资源耗尽时触发
    void TriggerOnResourceDepleted();

    //初始化容器
    void InitializeContain();
}
//资源种类 
public enum ResourceType
{
    Exp, Mana
}
//增加资源方式
public enum AddResourceType
{
    Grow, Transport, None
}
//取出资源方式
public enum TakeOutResourceType
{
    Transport,None
}