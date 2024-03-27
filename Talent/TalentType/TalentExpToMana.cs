using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentExpToMana : AbstractTalent
{
    public TalentExpToMana()
    {
        label = "ExpToMana";
        containID = 2;
        imageID = 10002;
        InitializeTalent();
    }
    public override void InitializeTransport()
    {

        resourceTransport = new Dictionary<ResourceType, float>()
        {
            { ResourceType.Mana , 1 }
        };

        isUsePremit = true;
        resourcePermit = new Dictionary<ResourceType, float>()
        {
            { ResourceType.Exp , -1},
            { ResourceType.Mana, -1}
        };

        transportMaxTime = 5;
        base.InitializeTransport();
    }
    public override void InitializeProduce()
    {
        produceList = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Mana , 1 }
        };
        produceCost = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Exp , 2 }
        };
        produceTryCount = 2;

        produceMaxTime = 5;

        base.InitializeProduce();
    }
    public override void InitializeUpgrade()
    {
        grade = 1;
        maxGrade = 5;
        upgradeCondition = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Mana , 20 },
            {ResourceType.Exp , 50 }
        };
        base.InitializeUpgrade();
    }
    public override KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair)
    {
        float count = pair.Value;
        //每级提供0.2点额外产出
        if (pair.Key == ResourceType.Mana)
            count += grade * 0.2f;
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
