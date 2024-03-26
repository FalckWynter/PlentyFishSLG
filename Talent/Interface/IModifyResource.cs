using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifyResource 
{
    //修饰器 - 输入资源时修饰
    KeyValuePair<ResourceType, float> ModifyOnAddResource(KeyValuePair<ResourceType, float> pair);
    //修饰器 - 输出资源时修饰 * 修改输出量 在消耗资源之后执行
    KeyValuePair<ResourceType, float> ModifyOnRemoveResource(KeyValuePair<ResourceType, float> pair);
    //修饰器 - 消耗资源时修饰 * 降低消耗成本
    KeyValuePair<ResourceType, float> ModifyOnCostResource(KeyValuePair<ResourceType, float> pair);
    //修饰器 - 资源改变时修饰
    KeyValuePair<ResourceType, float> ModifyOnResourceChange(KeyValuePair<ResourceType, float> pair);
    //修饰器 - 资源制造时修饰
    KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair);
}
