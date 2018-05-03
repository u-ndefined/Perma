using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StackDisplay : MonoBehaviour {

    private Image icon;
    private TextMeshProUGUI quantity;

	private void Start()
	{
        icon = transform.GetChild(0).GetComponent<Image>();
        quantity = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
	}



	public void SetDisplay(Sprite sprite, string qtt)
    {
        icon.enabled = true;
        icon.sprite = sprite;
        if (!qtt.Equals("1")) quantity.text = qtt;
        else quantity.text = null;
    }

	public void Reset()
	{

        icon.enabled = false;
        icon.sprite = null;
        quantity.text = null;
	}

}
