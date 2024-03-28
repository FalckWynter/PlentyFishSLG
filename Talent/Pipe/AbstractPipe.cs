using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ܵ��� û����
/// </summary>
public class AbstractPipe : AbstractContain, IUpgradeAble
{
    
    #region �츳����

    //�ȼ�����
    public int grade { get; set; }

    public int maxGrade { get; set; }

    public Dictionary<ResourceType, float> upgradeCondition { get; set; }

    #endregion


    #region ��������
    // Start is called before the first frame update
    public AbstractPipe()
    {

    }

    public override void InitializeTalent()
    {
        InitializePre();
        InitializeContain();
        InitializeTransport();
        InitializeUpgrade();
    }
    public override void InitializePre()
    {
        base.InitializePre();
        upgradeCondition = new Dictionary<ResourceType, float>();
        Debug.Log("���Գ�ʼ��ID" + imageID);
        maxGrade = 1;
        grade = 1;
        Debug.Log("�������캯��");
    }
    public override void InitializeContain() { }
    /// <summary>
    /// ��ʼ�� - ����� ÿ�δ����� �Ƿ�ʹ�ð����� �ڰ�����
    /// </summary>
    public override void InitializeTransport()
    {
        base.InitializeTransport();
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
        UpdateTransportTime();
        UpdateUpgradeCondition();
        UpdateAutoCollect();
    }
    #endregion
    #region �ȼ�����
    /// <summary>
    /// �����ȼ����¼�
    /// </summary>
    /// <returns>�Ƿ������ɹ�</returns>
    public virtual bool Upgrade()
    {
        foreach (KeyValuePair<ResourceType, float> pair in upgradeCondition)
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
