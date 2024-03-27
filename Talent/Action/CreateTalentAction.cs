using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTalentAction : AbstractAction
{
    public AbstractTalent talentToCreate;
    public Vector3 spawnPosition;
    public Transform spawnParent;
    public CreateTalentAction(AbstractTalent talent): this(talent, new Vector3(0,0,-0.1f), TalentData.Instance.talentParent.transform) { }
    public CreateTalentAction(AbstractTalent talent,Vector3 position): this(talent, position, TalentData.Instance.talentParent.transform) { }
    public CreateTalentAction(AbstractTalent talent,Vector3 position,Transform parent)
    {
        talentToCreate = talent;
        spawnPosition = position;
        spawnParent = parent;
    }
    public override void Update()
    {
        base.Update();

        //生成物体 设置坐标和父物体
        GameObject ob = GameObject.Instantiate(TalentData.Instance.prefab);
        ob.transform.position = spawnPosition;
        ob.transform.parent = spawnParent;
        //生成对应数据类
        int index = ContainManager.Instance.GetContainIndex();
        ContainManager.Instance.ContainDictionary.Add(index, talentToCreate);
        ContainManager.Instance.ContainMonoData.Add(index,ob);
        
        //设置局内ID
        ob.GetComponent<ContainMono>().SetID(index);
        ob.GetComponent<ContainMono>().ReloadSprite();
        //ob.GetComponent<ContainMono>()


        isCompleted = true;
    }
}
