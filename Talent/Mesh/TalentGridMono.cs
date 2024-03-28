using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class TalentGridMono : MonoHasElement
{
    public Vector2Int position = new Vector2Int();
    public Light2D light2D;
    public GameObject spriteObject;
    public void SetTalentData(Vector2Int pos)
    {
        position = pos;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuildObject()
    {
        TalentMesh.Instance.VisibleNodes(position);
        TalentMesh.Instance.UnlockNodes(position);
    }
    public void LargeObject()
    {
        this.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void HideGameobject()
    {
        this.gameObject.SetActive(false);
    }
    public void LockGameObject()
    {
        light2D.intensity = 0f;
        spriteObject.SetActive(false);
    }
    public void UnLockGameObject()
    {
        spriteObject.SetActive(true);
        light2D.intensity = 0.5f;
    }
    public void OnMouseDown()
    {
        BuildObject();
    }
}
