using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifyResource 
{
    //������ - ������Դʱ����
    KeyValuePair<ResourceType, float> ModifyOnAddResource(KeyValuePair<ResourceType, float> pair);
    //������ - �����Դʱ���� * �޸������ ��������Դ֮��ִ��
    KeyValuePair<ResourceType, float> ModifyOnRemoveResource(KeyValuePair<ResourceType, float> pair);
    //������ - ������Դʱ���� * �������ĳɱ�
    KeyValuePair<ResourceType, float> ModifyOnCostResource(KeyValuePair<ResourceType, float> pair);
    //������ - ��Դ�ı�ʱ����
    KeyValuePair<ResourceType, float> ModifyOnResourceChange(KeyValuePair<ResourceType, float> pair);
    //������ - ��Դ����ʱ����
    KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair);
}
