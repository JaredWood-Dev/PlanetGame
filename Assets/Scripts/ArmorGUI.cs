using UnityEngine;
using UnityEngine.UI;

public class ArmorGUI : MonoBehaviour
{
    public Sprite armorMiddle;
    public Sprite armorEnd;
    public Sprite armorMiddleEmpty;
    public Sprite armorEndEmpty;

    public float initX = 0f;
    public float initY = 0f;
    public float offsetX = 100f;
    public float endOffsetX = -10f;
    public float scale = 1f;

    public GameObject armorCell;
    public GameObject armorCellEnd;

    public int armorTotal = 1;
    public int armor = 0;

    void Start()
    {
        SetArmorCount(armorTotal);
    }
    void Update()
    {
        SetArmorCount(armorTotal);
        //SetFillCount(armor);
    }

    void SetArmorCount(int total)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        for (int i = 0; i < total - 1; i++)
        {
            GameObject cell = Instantiate(armorCell, transform);
            cell.GetComponent<Image>().sprite = armorMiddle;
            cell.GetComponent<RectTransform>().anchoredPosition += new Vector2(i * offsetX + initX, initY);
            cell.transform.localScale = new Vector3(scale, scale, scale);
        }
        GameObject cellEnd = Instantiate(armorCellEnd, transform);
        cellEnd.GetComponent<Image>().sprite = armorEnd;
        cellEnd.GetComponent<RectTransform>().anchoredPosition += new Vector2((total - 2) * offsetX + endOffsetX, initY);
        cellEnd.transform.localScale = new Vector3(scale * 0.8f, scale * 0.8f, scale * 0.8f);
    }

    void SetFillCount(int amount)
    {
        for (int i = transform.childCount - 1; i > -1; i--)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = i >= amount ? armorMiddleEmpty : armorMiddle;
        }

        transform.GetChild(transform.childCount - 1).GetComponent<Image>().sprite = amount == transform.childCount ? armorEnd : armorEndEmpty;
    }
}
