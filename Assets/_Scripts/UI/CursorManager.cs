using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : ISingleton<CursorManager>
{
    protected CursorManager() {}

    public Texture2D normal, grab, inspectWilt, inspectHealthy, knock, dig, sleep, talk;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private Camera cam;
    private InventoryManager inventory;

	// Use this for initialization
	void Start () 
    {
        cam = Camera.main;
        inventory = InventoryManager.Instance;
        Cursor.SetCursor(normal, hotSpot, cursorMode);
	}

	private void Update()
	{
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            switch (hit.transform.tag)
            {
                case "NPC":
                    Swap(GameData.Cursor.Talk);
                    break;
                case "PlayerHouse":
                    Swap(GameData.Cursor.Sleep);
                    break;
                case "NPCHouse":
                    Swap(GameData.Cursor.Knock);
                    break;
                case "Plant":
                    Plant plant = hit.collider.GetComponentInParent<Plant>();
                    Stack stack = inventory.stacks[inventory.selectedSlotID];
                    if (!stack.empty && stack.item.itemType == ItemType.SHOVEL) Swap(GameData.Cursor.Dig);
                    else if (plant.harvestable) Swap(GameData.Cursor.Grab);
                    else if (plant.wilted) Swap(GameData.Cursor.InspectWilt);
                    else Swap(GameData.Cursor.InspectHealthy);
                    break;
                case "Item":
                    Swap(GameData.Cursor.Grab);
                    break;
                default:
                    Swap(GameData.Cursor.Normal);
                    break;
            }
        }
        else
        {
            Swap(GameData.Cursor.Normal);
        }
	}

	public void Swap(GameData.Cursor cursor)
    {
        Cursor.SetCursor(GetTexture(cursor), hotSpot, cursorMode);
    }

    private Texture2D GetTexture(GameData.Cursor cursor)
    {
        switch(cursor)
        {
            case GameData.Cursor.Normal:
                return normal;
            case GameData.Cursor.Grab:
                return grab;
            case GameData.Cursor.InspectWilt:
                return inspectWilt;
            case GameData.Cursor.InspectHealthy:
                return inspectHealthy;
            case GameData.Cursor.Knock:
                return knock;
            case GameData.Cursor.Dig:
                return dig;
            case GameData.Cursor.Sleep:
                return sleep;
            case GameData.Cursor.Talk:
                return talk;
            default:
                return normal;

        }
    }
}
