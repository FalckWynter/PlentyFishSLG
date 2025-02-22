using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentBasicExp : AbstractTalent
{
    public TalentBasicExp()
    {
        label = "ExpSpawner";
        containID = 1;
        imageID = 10001;
        InitializeTalent();
    }
    public override void InitializeTransport()
    {

        resourceTransport = new Dictionary<ResourceType, float>()
        {
            { ResourceType.Exp , 5 }
        };

        isUsePremit = true;
        resourcePermit = new Dictionary<ResourceType, float>()
        {
            { ResourceType.Exp , -1}
        };

        transportMaxTime = 3;
        base.InitializeTransport();
    }
    public override void InitializeProduce()
    {
        produceList = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Exp , 10 }
        };
        produceMaxTime = 1;
        base.InitializeProduce();
    }
    public override void InitializeUpgrade()
    {
        grade = 1;
        maxGrade = 5;
        upgradeCondition = new Dictionary<ResourceType, float>()
        {
            {ResourceType.Exp , 1000 }
        };
        base.InitializeUpgrade();
    }
    public override KeyValuePair<ResourceType, float> ModifyOnProduceResource(KeyValuePair<ResourceType, float> pair)
    {
        float count = pair.Value;
        if (count == 0)
            return pair;
        //每级提供1点额外产出
        if (pair.Key == ResourceType.Exp)
            count += grade;
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
