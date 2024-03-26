using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentManager : Singleton<TalentManager>,IModifyResource
{
    public float timeSpeed = 1f;
    public int level = 0;
    public Dictionary<int,AbstractTalent> talentDictionary = new Dictionary<int, AbstractTalent>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        foreach(KeyValuePair<int,AbstractTalent> talent in talentDictionary)
        {
            talent.Value.Update();
        }
    }
    public bool AddResourceToTalent(int talent, ResourceType type,float count, AddResourceType addType)
    {
        if (!talentDictionary.ContainsKey(talent))
        {
            Debug.LogError("�����򲻴��ڵ��츳ID : " + talent + "�����Դ");
            return false;
        }
        if (!talentDictionary[talent].AddResource(type, count,addType))
            return false;
        return true;


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
