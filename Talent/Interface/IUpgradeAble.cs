using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradeAble
{
    //�ȼ�
    int grade { get; set; }
    //���ȼ�
    int maxGrade { get; set; }
    //����������
    Dictionary<ResourceType,float> upgradeCondition { get; set; }
   
    //�����ȼ�
    bool Upgrade();
    //�Ƿ�������������
    bool IsUpgradeAble();
    //���������������
    void UpdateUpgradeCondition();

    //��ʼ��������
    void InitializeUpgrade();
}
