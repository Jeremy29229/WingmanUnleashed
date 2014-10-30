using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryButton : MonoBehaviour {
    public string objectName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void equip()
    {
        Sprite image = gameObject.transform.FindChild("ItemImage").GetComponent<Image>().sprite;
        GameObject.Find("EquipmentPanel").GetComponent<EquipmentPanel>().Equip(objectName,image);
    }
}
