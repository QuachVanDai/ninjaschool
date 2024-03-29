﻿
using UnityEngine;

public class SelectSkill : MonoBehaviour
{
    [SerializeField] protected Transform[] lstSkill;
    private void Reset()
    {
       // select = transform.Find("select");
       this.loadPrefabs();
    }
    protected virtual void loadPrefabs()
    {
        lstSkill = new Transform[5];
        // Tìm tất cả các GameObject có tên là "select" hoặc "select1" trong scene
        GameObject[] selectObjects = GameObject.FindObjectsOfType<GameObject>();

        // Lọc các đối tượng có tên là "select" hoặc "select1"
        foreach (GameObject selectObject in selectObjects)
        {
            if (selectObject.name == "select1")
            {
                this.lstSkill[0]=selectObject.transform;
            }
            if (selectObject.name == "select5")
            {
                this.lstSkill[1] = selectObject.transform;
            }
            if (selectObject.name == "select10")
            {
                this.lstSkill[2] = selectObject.transform;
            }
            if (selectObject.name == "select15")
            {
                this.lstSkill[3] = selectObject.transform;
            }
            if (selectObject.name == "select20")
            {
                this.lstSkill[4] = selectObject.transform;
            }
        }
    }
    public void selectUseSkill(int index)
    {
        if (UseSkill.Instance.getIsUseSkill(index)==false) { return; }
        hidePrefabs();
        this.lstSkill[index].gameObject.SetActive(true);
    }
    private void Start()
    {
        hidePrefabs();
        GameObject skillDefaul = FindAnyObjectByType<GameObject>();
        lstSkill[0].gameObject.SetActive(true);
 
    }
    public void hidePrefabs()
    {
        for(int i=4;i>=0; i--)
        {
            this.lstSkill[i].gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
}
