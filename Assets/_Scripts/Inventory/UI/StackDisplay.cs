using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StackDisplay : MonoBehaviour {

    private Image icon;
    private TextMeshProUGUI quantity;
    public GameObject quantityDisplay;

	private void Awake()
	{
        Inititalisation();
        //Reset();
	}

	private void Inititalisation()
	{
        icon = GetComponentInChildren<Image>();
        quantity = GetComponentInChildren<TextMeshProUGUI>();
	}

    private void ChangeSize()
    {
        quantity.rectTransform.sizeDelta = new Vector2(35, 35);
        quantity.fontSize = 15;
    }



	public void SetDisplay(Sprite sprite, string qtt)
    {
        //if (icon == null) Inititalisation();

        gameObject.SetActive(true);

        icon.sprite = sprite;
        quantity.text = qtt;
        if (!qtt.Equals("1"))
        {
            quantityDisplay.SetActive(true);
        }
        else
        {
            quantityDisplay.SetActive(false);
        }


    }

	public void Reset()
	{
        //Debug.Log(gameObject.name);

        if (icon == null) Inititalisation();

        gameObject.SetActive(false);

        icon.sprite = null;
        quantity.text = "";
	}

}
