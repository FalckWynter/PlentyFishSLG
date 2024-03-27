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
    public bool isBuildingMode = false;
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
            Debug.LogError("尝试向不存在的天赋ID : " + talent + "添加资源");
            return false;
        }
        if (!ContainDictionary[talent].AddResource(type, count,addType))
            return false;
        return true;


    }
    public void FlashContain(int id)
    {
        if (!ContainMonoData.ContainsKey(id))
            return;
        ContainMonoData[id].GetComponent<ContainMono>().Doflash();
    }
    public int GetContainIndex()
    {
        currentContainIndex++;
        return currentContainIndex;
    }
    #region 修饰器
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
