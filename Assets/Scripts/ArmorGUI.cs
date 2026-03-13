using UnityEngine;
using UnityEngine.UI;

public class ArmorGUI : MonoBehaviour
{
    public Sprite armorMiddle;
    public Sprite armorEnd;
    public Sprite armorMiddleEmpty;
    public Sprite armorEndEmpty;

    public GameObject armorCell;
    public GameObject armorCellEnd;

    void Start()
    {
        SetGUI(4);
    }

    void SetGUI(int total)
    {
        for (int i = 0; i < total - 1; i++)
        {
            GameObject cell = Instantiate(armorCell, transform, true);
            cell.GetComponent<Image>().sprite = armorMiddle;
            cell.transform.position = transform.position + new Vector3(i * 95f + 125f, 6, 0);
            cell.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        GameObject cellEnd = Instantiate(armorCellEnd, transform, true);
        cellEnd.GetComponent<Image>().sprite = armorEnd;
        cellEnd.transform.position = transform.position + new Vector3((total - 1) * 95f + 125f, 6, 0);
        cellEnd.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
