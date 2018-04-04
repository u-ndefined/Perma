using UnityEditor;
using UnityEngine;

public class HexCreator : EditorWindow
{
    private int width = 1;
    private int height = 1;
    private HexType type;
    private Object hexCell;
    //private HexMetrics hexMetrics;

    //private Vector3 cellPos;
    //private Vector3 rawPos;

    [MenuItem("GameObject/HexGrid", false, 0)]
    static void CreateGrid()
    {
        //create window
        HexCreator window = ScriptableObject.CreateInstance<HexCreator>();
        window.position = new Rect(200, 250 , 250, 150);
        window.ShowPopup();
    }

	private void OnGUI()
	{

        //Get infos in the box
        width = EditorGUILayout.IntField("Width", width);
        height = EditorGUILayout.IntField("Height", height);
        type = (HexType) EditorGUILayout.EnumPopup("Type", type);

        if(GUILayout.Button("Create")) //when create button is pressed
        {

            hexCell = AssetDatabase.LoadAssetAtPath("Assets/_Prefabs/Hex/HexCell.prefab", typeof(GameObject));  //get the prefab in the hierarchy

            GameObject hexGrid = new GameObject("HexGrid"); //create the grid parent


            for (int y = 0; y < width; y++)
            {
                GameObject hexRaw = new GameObject("HexRaw: " + y);     //create the raw parent
                hexRaw.transform.parent = hexGrid.transform;




                for (int x = 0; x < height; x++)
                {
                    GameObject cell = (UnityEngine.GameObject) PrefabUtility.InstantiatePrefab(hexCell);    //create the cell
                    cell.name = "HexCell: " + x;
                    cell.transform.parent = hexRaw.transform;

                    Vector3 cellPos = new Vector3((x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f) ,0,0); //Set cell pos
                    cell.transform.position = cellPos;

                    cell.GetComponent<HexCell>().type = type;
                    cell.GetComponent<HexCell>().coordinates = HexCoordinates.FromOffsetCoordinates(x, y);
                } 


                Vector3 rawPos = new Vector3(0, 0, y * (HexMetrics.outerRadius * 1.5f));        //Set raw pos
                hexRaw.transform.position = rawPos;
            }

            Close();
        }
	}
}