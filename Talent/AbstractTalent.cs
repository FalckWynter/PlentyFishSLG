using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AbstractTalent : AbstractContain, IProduceResource<AbstractTalent>, IUpgradeAble
{
    #region �츳����
    //�����������
    public Dictionary<ResourceType, float> produceList { get; set; }

    public Dictionary<ResourceType, float> produceCost { get; set; }

    public int produceTryCount { get; set; }

    public float produceTime { get; set; }
    public float produceMaxTime { get; set; }

    //�ȼ�����
    public int grade { get; set; }

    public int maxGrade { get; set; }

    public Dictionary<ResourceType, float> upgradeCondition { get; set; }

    #endregion

    #region ��������
    // Start is called before the first frame update
    public AbstractTalent()
    {
        
    }

    public override void InitializeTalent()
    {
        InitializePre();
        InitializeContain();
        InitializeTransport();
        InitializeProduce();
        InitializeUpgrade();
    }
    public override void InitializePre()
    {
        resourceContain = new Dictionary<ResourceType, float>();
        resourceTransport = new Dictionary<ResourceType, float>();
        resourceLimit = new Dictionary<ResourceType, float>();
        resourcePermit = new Dictionary<ResourceType, float>();
        produceList = new Dictionary<ResourceType, float>();
        produceCost = new Dictionary<ResourceType, float>();
        upgradeCondition = new Dictionary<ResourceType, float>();
        connectNodes = new List<int>();
        Debug.Log("���Գ�ʼ��ID" + imageID);
        sprite = ImageData.Instance.GetImage(imageID);
        isDeleteNullContainResourceType = true;

        transportMaxTime = 10;

        produceMaxTime = 10;
        produceTryCount = 1;

        maxGrade = 1;
        grade = 1;
        Debug.Log("�������캯��");
    }
    public override void InitializeContain() {}
    /// <summary>
    /// ��ʼ�� - ����� ÿ�δ����� �Ƿ�ʹ�ð����� �ڰ�����
    /// </summary>
    public override void InitializeTransport()
    {

        transportTime = transportMaxTime;
    }
    /// <summary>
    /// ��ʼ�� - ����� �������� ÿ�������� ÿ���������
    /// </summary>
    public virtual void InitializeProduce()
    {

        produceTime = produceMaxTime;
    }
    /// <summary>
    /// ��ʼ�� - ���ȼ� ��ǰ�ȼ� ��������
    /// </summary>
    public virtual void InitializeUpgrade()
    {

    }
    // Update is called once per frame
    public override void Update()
    {
        UpdateProduceTime();
        UpdateTransportTime();
        UpdateUpgradeCondition();
    }
    #endregion

    
    #region ������Դ����
    /// <summary>
    /// ����������Դ
    /// </summary>
    /// <returns></returns>
    public virtual bool ProduceResource()
    {
        for (int i = 0; i < produceTryCount; i++)
        {
            foreach (KeyValuePair<ResourceType, float> pair in produceCost)
            {
                //�������������
                if (!IsHaveEnoughResource(pair))
                {
                    Debug.LogWarning("��Դ����" + this.GetType());
                    return false;
                }
            }
            foreach (KeyValuePair<ResourceType, float> pair in produceList)
            {
                //���β���
                KeyValuePair<ResourceType, float> temp = ModifyOnProduceResource(pair);
                //���һ����Դ
                TryAddResource(temp.Key, temp.Value);
                Debug.Log("������һ����Դ" + pair + "��Դ" + this.GetType());
            }
            TriggerOnProduceResource();

        }
        //����ɹ�
        return true;
    }


    public virtual void TriggerOnProduceResource()
    {

    }
    /// <summary>
    /// �������������
    /// </summary>
    public void UpdateProduceTime()
    {
        if (produceTime > 0)
        {

            produceTime -= Time.deltaTime * ContainManager.Instance.timeSpeed;
            return;
        }
        if (produceTime <= 0)
        {
            ProduceResource();
            produceTime = produceMaxTime;
        }
    }

    #endregion

    #region �ȼ�����
    /// <summary>
    /// �����ȼ����¼�
    /// </summary>
    /// <returns>�Ƿ������ɹ�</returns>
    public virtual bool Upgrade()
    {
        foreach(KeyValuePair<ResourceType,float> pair in upgradeCondition)
        {
            TryRemoveResource(pair);
        }
        grade++;

        Debug.Log("�츳ID" + containID + "������" + grade);
        return true;
    }
    /// <summary>
    /// �Ƿ���������Ҫ��
    /// </summary>
    /// <returns>���㷵����</returns>
    public virtual bool IsUpgradeAble()
    {
        foreach (KeyValuePair<ResourceType, float> pair in upgradeCondition)
        {
            if (!IsHaveEnoughResource(pair))
                return false;
        }
        return true;
    }
    /// <summary>
    /// �����Ƿ�������������
    /// </summary>
    public void UpdateUpgradeCondition()
    {
        if (IsUpgradeAble())
            Upgrade();
    }
    #endregion

    #region ����������
    public override KeyValuePair<ResourceType, float> ModifyOnAddResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnAddResource(pair);
        return pair;
    }

    public override KeyValuePair<ResourceType, float> ModifyOnRemoveResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnRemoveResource(pair);
        return pair;
    }

    public override KeyValuePair<ResourceType, float> ModifyOnResourceChange(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnResourceChange(pair);
        return pair;
    }

    public override KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnProduceResource(pair);
        return pair;
    }

    public override KeyValuePair<ResourceType, float> ModifyOnCostResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnCostResource(pair);
        return pair;
    }



    #endregion
}