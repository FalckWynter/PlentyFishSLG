using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainManager : Singleton<ContainManager>,IModifyResource
{
    public float timeSpeed = 1f;
    public int level = 0;
    public int currentContainIndex = 0;
    public Dictionary<int,AbstractTalent> ContainDictionary = new Dictionary<int, AbstractTalent>();
    public Dictionary<int, GameObject> ContainMonoData = new Dictionary<int, GameObject>();
    public Dictionary<Vector2Int, GameObject> PipeMonoData = new Dictionary<Vector2Int, GameObject>();
    public Dictionary<ResourceType, float> resourceAvailable = new Dictionary<ResourceType, float>();
    public bool isBuildingMode = false;
    public ContainManager()
    {
        
    }
    public override void Initialize()
    {
        base.Initialize();
        resourceAvailable = new Dictionary<ResourceType, float>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        foreach(KeyValuePair<int,AbstractTalent> talent in ContainDictionary)
        {
            talent.Value.Update();
        }
    }
    public bool AddResourceToTalent(int talent, ResourceType type,float count, AddResourceType addType)
    {
        if (!ContainDictionary.ContainsKey(talent))
        {
            Debug.LogError("�����򲻴��ڵ��츳ID : " + talent + "�����Դ");
            return false;
        }
        if (!ContainDictionary[talent].AddResource(type, count,addType))
            return false;
        return true;
    }
    public bool AddPipeMono(Vector2Int pos,GameObject ob)
    {
        if (PipeMonoData.ContainsKey(pos))
            return false;
        PipeMonoData.Add(pos, ob);
        return true;
    }
    public bool RemovePipeMono(Vector2Int pos)
    {
        if (!PipeMonoData.ContainsKey(pos))
            return false;
        PipeMonoData.Remove(pos);
        return true;
    }
    public bool RemovePipeMono(GameObject ob)
    {
        //Σ�յط��� ����
        if (!PipeMonoData.ContainsValue(ob))
            return false;
        return true;
    }
    public void FlashContain(int id)
    {
        //Debug.Log("��������" + ContainMonoData[id] + "�Ƿ�������" + ContainMonoData.ContainsKey(id));
        if (!ContainMonoData.ContainsKey(id))
            return;
        //Debug.Log("����" + ContainMonoData[id].GetComponent<ContainMono>().gameIndex + "λ��" + ContainMonoData[id].transform.position);
        ContainMonoData[id].GetComponent<ContainMono>().Doflash();
    }
    public int GetContainIndex()
    {
        currentContainIndex++;
        return currentContainIndex;
    }
    public void AddResource(ResourceType type,float count)
    {
        if (!resourceAvailable.ContainsKey(type))
            resourceAvailable.Add(type, 0);
        resourceAvailable[type] += count;
  
    }
    public void AddResource(KeyValuePair<ResourceType,float> pair)
    {
   
        AddResource(pair.Key, pair.Value);
    }
    #region ������
    public KeyValuePair<ResourceType, float> ModifyOnAddResource(KeyValuePair<ResourceType, float> pair)
    {
        return pair;
    }

    public KeyValuePair<ResourceType, float> ModifyOnRemoveResource(KeyValuePair<ResourceType, float> pair)
    {
        return pair;
    }

    public KeyValuePair<ResourceType, float> ModifyOnResourceChange(KeyValuePair<ResourceType, float> pair)
    {
        return pair;
    }

    public KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair)
    {
        return pair;
    }

    public KeyValuePair<ResourceType, float> ModifyOnCostResource(KeyValuePair<ResourceType, float> pair)
    {
        return pair;
    }
    #endregion
}
