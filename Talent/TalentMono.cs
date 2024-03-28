using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.GFUIManager;
using UnityEngine.Rendering.Universal;
public class ContainMono : MonoHasElement
{
    public int gameIndex;
    public int containID;
    public Light2D lanternLight;
    public SpriteRenderer talentImage;
    public bool isFlashing,isFlashingDown = false;
    public float flashTarget = 0.75f, flashStart = 2f;
    public float flashSpeed = 1f;
    public LineRenderer parentLineRender;
    public bool isBuilding = false;
    public void Doflash()
    {
       
        isFlashing = true;
        lanternLight.intensity = flashStart;

        UpdateFlash();

    }
    public void UpdateFlash()
    {

        if (isFlashing)
        {

            lanternLight.intensity += Time.deltaTime * 3 * flashSpeed;
            if(lanternLight.intensity > flashStart)
            {
                isFlashing = false;
                isFlashingDown = true;
                lanternLight.intensity = flashStart;
            }
        }
        if(isFlashingDown)
        {

            if (lanternLight.intensity > flashTarget)
            {
                lanternLight.intensity -= Time.deltaTime * flashSpeed ;
            }
            else
            {
                lanternLight.intensity = flashTarget;
                isFlashing = false;
            }
        }
    }

    public override void Awake()
    {
        base.Awake();
        GetChildrenComponent<Light2D>();
        GetChildrenComponent<LineRenderer>();

        //lineRender = GetControl<LineRenderer>("Icon");
        //lineRender.positionCount = 2;
        //lineRender.SetPosition(0, transform.position);

        parentLineRender = GetComponent<LineRenderer>();

        parentLineRender.positionCount = 2;
        parentLineRender.SetPosition(0, transform.position);

        lanternLight = GetControl<Light2D>("Lantern");
        talentImage = GetControl<SpriteRenderer>("Icon");


    }
    public void ReloadSprite()
    {
        talentImage.sprite = ContainManager.Instance.ContainDictionary[gameIndex].sprite;
    }
    // Start is called before the first frame update
    public override void Start()
    {


    }
    public void SetID(int id)
    {
        gameIndex = id;
    }
    // Update is called once per frame
    public void Update()
    {
 
        UpdateFlash();
        UpdateBuilding();
    }
    public void UpdateBuilding()
    {
               
    }
    public void OnMouseDown()
    {
    }
    public void RefreshPipePoint()
    {
        //这个功能交给建造器完成
        return;
        /*
        List<int> point = new List<int>();
        parentLineRender.positionCount = 0;
        int i = 0;
        foreach(int node in ContainManager.Instance.ContainDictionary[gameIndex].connectNodes)
        {
            parentLineRender.positionCount += 2;
            parentLineRender.SetPosition(2 * i,transform.position);

           parentLineRender.SetPosition(2* i + 1 ,ContainManager.Instance.ContainMonoData[node].transform.position);
            i += 1;
        }
        */
    }
    public void TryEnterBuild()
    {

    }
}
