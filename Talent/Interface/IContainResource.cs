using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainResource
{
    //�̳иýӿڵ������ܳ�װ��Դ
    //��Դ����
    Dictionary<ResourceType, float> resourceContain { get; set; }
    //�Ƿ��Ƴ��յ���Դ����
    bool isDeleteNullContainResourceType { get; set; }

    //������Դ
    bool AddResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None);
    //ȡ����Դ
    KeyValuePair<ResourceType, float> TakeOutResource(ResourceType type, float count, TakeOutResourceType takeOutType = TakeOutResourceType.None);
    //�Ƿ����ĳ����Դ
    bool IsHaveResourceType(ResourceType type);
    //�Ƿ�����㹻������ĳ����Դ
    bool IsHaveEnoughResource(ResourceType type, float count);
    bool IsHaveEnoughResource(KeyValuePair<ResourceType, float> pair);
    //����ĳ����Դ������
    bool SetResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None, bool isForceSet = true);
    //��ȡ�����е�ĳ����Դ�ĸ���
    KeyValuePair<ResourceType, float> GetResourceCopy(ResourceType type, float count);
    //ɾ����Դ
    bool DeleteResource(ResourceType type);

    //������ - ������Դʱ����
    void TriggerOnAddResource();
    //������ - �����Դʱ����
    void TriggerOnRemoveResource();
    //������ - ��Դ�ı�ʱ����
    void TriggerOnResourceChange();
    //������ - ĳ����Դ�ľ�ʱ����
    void TriggerOnResourceDepleted();

    //��ʼ������
    void InitializeContain();
}
//��Դ���� 
public enum ResourceType
{
    Exp, Mana
}
//������Դ��ʽ
public enum AddResourceType
{
    Grow, Transport, None
}
//ȡ����Դ��ʽ
public enum TakeOutResourceType
{
    Transport,None
}