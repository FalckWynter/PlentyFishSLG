using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentExpManaToAtk : AbstractTalent
{
    public TalentExpManaToAtk()
    {
        label = "ExpManaToAtk";
        containID = 3;
        imageID = 10003;
        InitializeTalent();
    }
    public override void InitializeTransport()
    {

        resourceTransport = new Dictionary<ResourceType, float>()
        {
            { ResourceType.Atk , 1 }
        };

        isUsePremit = true;
        resourcePermit = new Dictionary<ResourceType, float>()
        {
            { ResourceType.Exp , -1},
            { ResourceType.Mana, -1},
            { ResourceType.Atk,-1 }
        };

        transportMaxTime = 5;
        base.InitializeTransport();
    }
    public override void InitializeProduce()
    {
        produceList = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Atk , 1 }
        };
        produceCost = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Exp , 1 },
            {ResourceType.Mana, 1 }
        };
        produceTryCount = 5;

        produceMaxTime = 1;

        base.InitializeProduce();
    }
    public override void InitializeContain()
    {
        isAutoCollect = true;
        resourceAutoCollect = new List<ResourceType>()
        {
            ResourceType.Atk
        };
        base.InitializeContain();
    }
    public override void InitializeUpgrade()
    {
        grade = 1;
        maxGrade = 5;
        upgradeCondition = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Atk , 500 }
        };
        base.InitializeUpgrade();
    }
    public override KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair)
    {
        float count = pair.Value;
        //每级提供0.1点额外产出
        if (pair.Key == ResourceType.Mana)
            count += grade * 0.1f;
        pair = new KeyValuePair<ResourceType, float>(pair.Key, count);

        return base.ModifyOnProduceResource(pair);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
