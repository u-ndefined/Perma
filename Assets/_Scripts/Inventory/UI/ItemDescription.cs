using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDescription : MonoBehaviour {

    public TextMeshProUGUI description;
    public TextMeshProUGUI itemName;
    public Image icon;

    //public Vector3 offset;

    public void Show(Item item, Vector3 position)
    {
        description.text = item.description;
        itemName.text = item.name;
        icon.sprite = item.icon;
        Vector3 pos = transform.position;
        //transform.position = position + offset;
        transform.position = new Vector3(position.x,pos.y,pos.z);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
