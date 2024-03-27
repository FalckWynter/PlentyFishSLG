using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeAble
{
    //等级
    int grade { get; set; }
    //最大等级
    int maxGrade { get; set; }
    //升级条件表
    Dictionary<ResourceType,float> upgradeCondition { get; set; }
   
    //提升等级
    bool Upgrade();
    //是否满足升级条件
    bool IsUpgradeAble();
    //尝试升级条件检测
    void UpdateUpgradeCondition();

    //初始化级别器
    void InitializeUpgrade();
}
