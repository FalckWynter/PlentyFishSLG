using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractContain : ITransportResource, IModifyResource
{
    #region �츳��������
    public string label;
    public int containID;
    public string commnets;
    public int gameIndex;
    public int imageID = 10001;
    public Sprite sprite;
    public static string spritePath = "Images/Talent";
    #endregion

    #region �츳��������
    //��Դ����
    public Dictionary<ResourceType, float> resourceContain { get; set; }
    public bool isDeleteNullContainResourceType { get; set; }

    //�����������
    public Dictionary<ResourceType, float> resourceTransport { get; set; }

    public bool isUsePremit { get; set; }
    public Dictionary<ResourceType, float> resourceLimit { get; set; }
    public Dictionary<ResourceType, float> resourcePermit { get; set; }

    public List<int> connectNodes { get; set; }

    public float transportTime { get; set; }
    public float transportMaxTime { get; set; }
    public bool isAutoCollect { get ; set ; }
    public List<int> beConnectNodes { get; set; }
    public int maxConnecteNodes { get; set; }
    public int maxBeConnectNodes { get; set ; }
    public List<ResourceType> resourceAutoCollect { get; set; }


    #endregion

    #region ��������
    // Start is called before the first frame update
    public AbstractContain()
    {

    }

    public virtual void InitializeTalent()
    {
        InitializePre();
        InitializeContain();
        InitializeTransport();
    }
    public virtual void InitializePre()
    {
        resourceContain = new Dictionary<ResourceType, float>();
        resourceTransport = new Dictionary<ResourceType, float>();
        resourceLimit = new Dictionary<ResourceType, float>();
        resourcePermit = new Dictionary<ResourceType, float>();
        resourceAutoCollect = new List<ResourceType>();
        connectNodes = new List<int>();
        beConnectNodes = new List<int>();
        maxConnecteNodes = -1;
        maxBeConnectNodes = -1;

        isAutoCollect = false;

        sprite = ImageData.Instance.GetImage(imageID);
        isDeleteNullContainResourceType = true;
        isUsePremit = false;

        transportMaxTime = 10;

        Debug.Log("�������캯��");
    }
    public virtual void InitializeContain() { }
    /// <summary>
    /// ��ʼ�� - ����� ÿ�δ����� �Ƿ�ʹ�ð����� �ڰ�����
    /// </summary>
    public virtual void InitializeTransport()
    {

        transportTime = transportMaxTime;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        UpdateTransportTime();
        UpdateAutoCollect();
    }
    #endregion

    #region ��������


    /// <summary>
    /// //�����Դ ����Ҫ�κ�����
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <param name="addType"></param>
    /// <returns>�����Ƿ���ӳɹ�</returns>
    public bool AddResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None)
    {
        if (addType == AddResourceType.Transport)
            TriggerOnGetResourceFromTransport();

        //��������
        KeyValuePair<ResourceType, float> pair = new KeyValuePair<ResourceType, float>(type, count);
        pair = ModifyOnAddResource(pair);

        //������κ�ֵΪ���� �������
        if (pair.Value <= 0)
            return false;
        //�����Դ
        if (!resourceContain.ContainsKey(type))
            resourceContain.Add(type, 0);
        resourceContain[type] += pair.Value;
        //����������
        TriggerOnAddResource();
        return true;
    }


    /// <summary>
    /// ȡ����Դ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <param name="takeOutType"></param>
    /// <returns>����ȡ��������</returns>
    public KeyValuePair<ResourceType, float> TakeOutResource(ResourceType type, float count, TakeOutResourceType takeOutType = TakeOutResourceType.None)
    {
        //��������ڸ����� ����0����Ԫ��
        if (!resourceContain.ContainsKey(type))
            return new KeyValuePair<ResourceType, float>(type, 0);
        KeyValuePair<ResourceType, float> pair = new KeyValuePair<ResourceType, float>(type, count);
        //�˴��޸�����
        resourceContain[type] -= ModifyOnCostResource(pair).Value;
        TriggerOnRemoveResource();
        //�޸�ȡ������
        pair = ModifyOnRemoveResource(pair);
        if (resourceContain[type] == 0)
        {
            if (isDeleteNullContainResourceType)
                DeleteResource(type);
            //������Դ�ľ�
            TriggerOnResourceDepleted();
        }
        return pair;
    }



    /// <summary>
    /// ������Դ����
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <param name="addType"></param>
    /// <returns>��������ڸ���Դ������</returns>
    public bool SetResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None, bool isForceSet = true)
    {
        if (!resourceContain.ContainsKey(type))
            return false;

        resourceContain[type] = count;

        //������Դ�ı䴥����
        TriggerOnResourceChange();
        return true;
    }

    //�Ƿ����㹻��ĳ����Դ
    /// <summary>
    /// �����Ƿ���ĳ����Դ
    /// </summary>
    /// <param name="type"></param>
    /// <returns>���򷵻���</returns>
    public virtual bool IsHaveResourceType(ResourceType type)
    {
        return resourceContain.ContainsKey(type);
    }
    /// <summary>
    /// �����Ƿ����㹻������ĳ����Դ
    /// </summary>
    /// <param name="type"></param>
    /// <returns>���򷵻���</returns>
    public bool IsHaveEnoughResource(ResourceType type, float count)
    {
        if (!resourceContain.ContainsKey(type))
            return false;
        if (resourceContain[type] < count)
            return false;
        return true;
    }
    public bool IsHaveEnoughResource(KeyValuePair<ResourceType, float> pair)
    {
        return IsHaveEnoughResource(pair.Key, pair.Value);
    }
    /// <summary>
    /// ������Count��Type��Դ�����ĸ���
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns>������Դ�ĸ���</returns>
    public KeyValuePair<ResourceType, float> GetResourceCopy(ResourceType type, float count)
    {
        if (IsHaveEnoughResource(type, count))
            return new KeyValuePair<ResourceType, float>(type, count);

        if (!IsHaveResourceType(type))
            return new KeyValuePair<ResourceType, float>(type, 0);

        return new KeyValuePair<ResourceType, float>(type, resourceContain[type]);
    }


    /// <summary>
    /// ��ĳ����Դɾ�� * ��һ�����԰湦��
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool DeleteResource(ResourceType type)
    {
        if (!resourceContain.ContainsKey(type))
            return false;
        //Ӧ��ͨ����0����
        resourceContain.Remove(type);
        return true;
    }
    /// <summary>
    /// �Զ��ռ���Դ
    /// </summary>
    /// <returns></returns>
    public bool AutoCollectResource()
    {
        if (resourceContain.Count == 0)
            return false;
        foreach(ResourceType type in resourceAutoCollect)
        {
            KeyValuePair<ResourceType, float> pair = TryRemoveResource(type);
            if (pair.Value <= 0)
                continue;
            ContainManager.Instance.AddResource(pair);
        }
        //foreach(KeyValuePair<ResourceType,float> pair in resourceContain)
        //{
        //    if (pair.Value == 0)
        //        continue;
        //    ContainManager.Instance.AddResource(TryRemoveResource(pair));
        //}
        return true;
    }
    /// <summary>
    /// ������Դ�ռ�
    /// </summary>
    public void UpdateAutoCollect()
    {
        if (!isAutoCollect)
            return;
        AutoCollectResource();
    }
    //�����Դʱ
    public virtual void TriggerOnAddResource()
    {

    }
    //ȡ����Դʱ
    public virtual void TriggerOnRemoveResource()
    {

    }
    //��Դ�����ı�Ĵ�����
    public virtual void TriggerOnResourceChange()
    {

    }
    //ĳ����Դ�ľ�ʱ
    public virtual void TriggerOnResourceDepleted()
    {

    }
    #endregion


    #region ������Դ����
    /// <summary>
    /// ���������Դ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns>����δ�ɹ���ӵ���Դ</returns>
    public KeyValuePair<ResourceType, float> TryAddResource(KeyValuePair<ResourceType, float> pair, AddResourceType addType = AddResourceType.None)
    {
        return TryAddResource(pair.Key, pair.Value);
    }
    public KeyValuePair<ResourceType, float> TryAddResource(ResourceType type, float count, AddResourceType addType = AddResourceType.None)
    {
        Debug.Log("�����Դ" + label + "����" + type + "����" + count);
        //��������ӵ�����
        float addCount = count;
        //��������б���ڸ�Ԫ�� ����������
        if (resourceLimit.ContainsKey(type))
        {
            //��������������ֹ���� ����ȫ����Դ
            if (resourceContain[type] >= resourceLimit[type] || resourceLimit[type] < 0)
                return new KeyValuePair<ResourceType, float>(type, count);
            //���򾡿���������Դ
            //Ҫ��ӵ������Ƿ�������� ��������������У�������Ӳ��
            addCount = (resourceLimit[type] - resourceContain[type]) >= count ? count : resourceLimit[type] - resourceContain[type];
            //AddResource(type, addCount,addType);
            //����δ��ӵĲ��
        }

        //���ʹ�ð�����
        if (isUsePremit)
        {
            //������ڰ�������
            if (!resourcePermit.ContainsKey(type))
                //ȫ������
                return new KeyValuePair<ResourceType, float>(type, count);
            //������Ӿ����ܶ��ֵ
            //���������Ϊ-1�����޸�
            if (resourcePermit[type] == -1)
                addCount = count;
            else
                addCount = (resourcePermit[type] - resourceContain[type]) >= count ? count : resourcePermit[type] - resourceContain[type];
            //AddResource(type, addCount,addType);

        }

        //�������ֱ�����
        AddResource(type, addCount, addType);
        return new KeyValuePair<ResourceType, float>(type, count - addCount);
    }
    /// <summary>
    /// ����ȡ��ָ����������Դ
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <returns>�����ɹ�ȡ��������</returns>
    public KeyValuePair<ResourceType, float> TryRemoveResource(KeyValuePair<ResourceType, float> pair)
    {
        return TryRemoveResource(pair.Key, pair.Value);
    }
    public KeyValuePair<ResourceType, float> TryRemoveResource(ResourceType type)
    {
        if(!resourceContain.ContainsKey(type))
            return new KeyValuePair<ResourceType, float>(type, 0); 
        return TryRemoveResource(type,resourceContain[type]);
    }
    public KeyValuePair<ResourceType, float> TryRemoveResource(ResourceType type, float count)
    {
        //KeyValuePair<ResourceType, int> pair = new KeyValuePair<ResourceType, int>(type, 0);
        //����������Դ����ֵΪ����
        if (!resourceContain.ContainsKey(type))
            return new KeyValuePair<ResourceType, float>(type, 0);
        if (resourceContain[type] <= 0)
            return new KeyValuePair<ResourceType, float>(type, 0);
        //�е����������� ����ʣ���ȫ��
        if (resourceContain[type] < count)
            return TakeOutResource(type, resourceContain[type]);

        //����ָ������ * ���ܻ���BUG
        return TakeOutResource(type, count);
    }
    public bool isContainFull(KeyValuePair<ResourceType, float> pair)
    {
        //���ʹ�ú�����
        if (resourceLimit.ContainsKey(pair.Key))
        {
            //��������������ֹ���� ������
            if (resourceContain[pair.Key] >= resourceLimit[pair.Key] || resourceLimit[pair.Key] < 0)
                return true;
        }

        //���ʹ�ð�����
        if (isUsePremit)
        {
            //������ڰ������� ����������
            if (!resourcePermit.ContainsKey(pair.Key))
                return true;
            if (!resourceContain.ContainsKey(pair.Key))
                resourceContain.Add(pair.Key, 0);
            if (resourcePermit[pair.Key] >= 0 && resourceContain[pair.Key] >= resourcePermit[pair.Key])
                //������
                return true;

        }
        //��������
        return false;
    }
    /// <summary>
    /// ���´����ʱ��
    /// </summary>
    public void UpdateTransportTime()
    {
        if (transportTime > 0)
        {
            transportTime -= Time.deltaTime * ContainManager.Instance.timeSpeed;
            return;
        }
        if (transportTime <= 0)
        {
            TransportResource();
            transportTime = transportMaxTime;
            Debug.Log("���贫���ʱ��" + transportTime);
        }
    }
    /// <summary>
    /// ����һ����Դ����
    /// </summary>
    public void TransportResource()
    {
        TryTransportResource();
    }
    /// <summary>
    /// ���Խ��ж�����Ŀ��һ����Դ���� 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public void TryTransportResource()
    {
        KeyValuePair<ResourceType, float> pair = new KeyValuePair<ResourceType, float>();
        //���б���ÿһ���ڵ�
        foreach (int node in connectNodes)
        {
            //��ÿһ��Ҫ�����Ԫ��
            foreach (KeyValuePair<ResourceType, float> resource in resourceTransport)
            {
                Debug.Log("Ҫȡ����Ԫ��" + resource.Key + "����" + resource.Value + "��" + label);
                //���װ���¸�Ԫ�� ����
                if (ContainManager.Instance.ContainDictionary[node].isContainFull(resource))
                {
                    Debug.Log("Ŀ��" +node+"װ������");
                    continue;
                }
                //ȡ��Ԫ��
                pair = TryRemoveResource(resource);
                //���ȡ����ֵ ����
                if (pair.Value == 0)
                    continue;
                //��Ŀ������ ��ȡʣ��ֵ
                pair = TryTransportResource(pair, node);
                //��ʣ��ֵ�������
                TryAddResource(pair);
            }

        }
    }
    /// <summary>
    /// ���Զ�ָ��Ŀ�����һ����Դ����
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    /// <param name="targetID"></param>
    public KeyValuePair<ResourceType, float> TryTransportResource(KeyValuePair<ResourceType, float> pair, int targetID)
    {
        return TryTransportResource(pair.Key, pair.Value, targetID);
    }
    public KeyValuePair<ResourceType, float> TryTransportResource(ResourceType type, float count, int targetID)
    {
        return ContainManager.Instance.ContainDictionary[targetID].TryAddResource(type, count, AddResourceType.Transport);
    }
    /// <summary>
    /// ������ӽڵ�
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool TryAddConnectNode(int id)
    {
        //���Ҫ��ӵĽڵ����������������
        if (maxConnecteNodes != -1 && connectNodes.Count >= maxConnecteNodes)
            return false;
        //������иýڵ������
        if (connectNodes.Contains(id))
            return false;
        //���������ýڵ�������� ���
        if(ContainManager.Instance.ContainDictionary[id].CanBeConnect(id))
        //��Ӹýڵ�
        connectNodes.Add(id);
        //���ýڵ���ӵ��Է� * ���� Ӧ�ø�Ϊ�Թܵ����� * ��Ӱ��
        ContainManager.Instance.ContainDictionary[id].TryAddBeConnectNode(gameIndex);

        return true;
    }
    /// <summary>
    /// ����Ƿ��ܹ���������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool CanConnect(int id)
    {
        //���Ҫ��ӵĽڵ����������������
        if (maxConnecteNodes != -1 && connectNodes.Count >= maxConnecteNodes)
            return false;
        //������иýڵ������
        if (connectNodes.Contains(id))
            return false;
        return true;
    }
    public bool TryAddBeConnectNode(int id)
    {
        //ֱ�Ӹ���Ӧ��û����
        //����Ѿ���ýڵ������ϵ ���
        if (connectNodes.Contains(id) || beConnectNodes.Contains(id))
            return false;
        //����������������� �ҳ������� ��
        if (maxBeConnectNodes != -1 && beConnectNodes.Count >= maxBeConnectNodes)
            return false;
        return true;
    }
    /// <summary>
    /// ����Ƿ��ܲ�������
    /// </summary>
    /// <param name="id">�������ӵĽڵ�</param>
    /// <returns></returns>
    public bool CanBeConnect(int id)
    {
        //����Ѿ���ýڵ������ϵ ���
        if (connectNodes.Contains(id) || beConnectNodes.Contains(id))
            return false;
        //����������������� �ҳ������� ��
        if (maxBeConnectNodes != -1 && beConnectNodes.Count >= maxBeConnectNodes)
            return false;
        return true;
    }
    public virtual void TriggerOnGetResourceFromTransport()
    {

    }

    public virtual void TriggerOnOutputResource()
    {

    }

    public virtual void TriggetOnIncomingResource()
    {

    }

    public virtual void TriggerOnTransportResource()
    {

    }
    #endregion
    #region ����������
    public virtual KeyValuePair<ResourceType, float> ModifyOnAddResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnAddResource(pair);
        return pair;
    }

    public virtual KeyValuePair<ResourceType, float> ModifyOnRemoveResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnRemoveResource(pair);
        return pair;
    }

    public virtual KeyValuePair<ResourceType, float> ModifyOnResourceChange(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnResourceChange(pair);
        return pair;
    }

    public virtual KeyValuePair<ResourceType, float> ModifyOnCostResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnCostResource(pair);
        return pair;
    }

    public virtual KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair)
    {
        pair = ContainManager.Instance.ModifyOnProduceResource(pair);
        return pair;
    }





    #endregion
}
