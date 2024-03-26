using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransportResource : IContainResource
{
    //�Ƿ�ʹ�ð�����
    bool isUsePremit { get; set; }
    //���ӵ��Ľڵ�ID
    List<int> connectNodes { get; set; }
    //�̳иýӿڵ����ܹ�����������Դ
    //��Դ�ڰ�����
    Dictionary<ResourceType, float> resourceLimit { get; set; }
    Dictionary<ResourceType, float> resourcePermit { get; set; }
    //�����б�
    Dictionary<ResourceType, float> resourceTransport { get; set; }

    //�´δ���ʣ��ʱ�� ÿ�δ�������ʱ��
    float transportTime { get; set; }
    float transportMaxTime { get; set; }

    //����������Դ ��������ʧ�ܵ�����
    KeyValuePair<ResourceType, float> TryAddResource(KeyValuePair<ResourceType, float> pair, AddResourceType addType = AddResourceType.None);
    KeyValuePair<ResourceType, float> TryAddResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None);
    //����ȡ����Դ ������ȡ�����������
    KeyValuePair<ResourceType, float> TryRemoveResource(KeyValuePair<ResourceType, float> pair);
    KeyValuePair<ResourceType, float> TryRemoveResource(ResourceType type, float count);
    //���´���ʣ��ʱ��
    void UpdateTransportTime();
    //������Դ����
    void TransportResource();
    //���Զ�����Ŀ����д���
    void TryTransportResource();
    //���Զ�ָ��ID��Ŀ����д���
    KeyValuePair<ResourceType, float> TryTransportResource(KeyValuePair<ResourceType, float> pair, int targetID);
    KeyValuePair<ResourceType, float> TryTransportResource(ResourceType type, float count,int targetID);
    //�����Ƿ�����(�Ժڰ���������)
    bool isContainFull(KeyValuePair<ResourceType, float> pair);

    //������ - ������Դʱ����
    void TriggerOnOutputResource();
    //������ - ������Դʱ����
    void TriggetOnIncomingResource();
    //������ - ������Դʱ����
    void TriggerOnTransportResource();

    //��ʼ��������
    void InitializeTransport();
}
