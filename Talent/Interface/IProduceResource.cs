using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProduceResource<T> where T : IContainResource
{
    //�����ܳ�װ��Դ�ſ���������Դ
    //ÿ���������Դ�б�
    Dictionary<ResourceType,float> produceList { get; set; }
    //ÿ�������������Դ
    Dictionary<ResourceType, float> produceCost { get; set; }
    //��Դ����ʣ��ʱ�� ��Դ������Ҫʱ��
    float produceTime { get; set; }
    float produceMaxTime { get; set; }
    //������Դ�ĳ��Դ��� * ������������Ϊ ����*ÿ�β���
    int produceTryCount { get; set; }
    //������Դ
    bool ProduceResource();
    //����������Դ��ʱ��
    void UpdateProduceTime();
    //������ - ������Դʱ
    void TriggerOnProduceResource();

    //��ʼ��������
    void InitializeProduce();
}
