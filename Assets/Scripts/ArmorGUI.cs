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
        EventManager.ArmorUpdate(6);
    }
    void Update()
    {
        //SetArmorCount(armorTotal);
        //SetFillCount(armor);
    }

    void SetArmorCount(int total)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        for (int i = 0; i < total - 2; i++)
        {
            GameObject cell = Instantiate(armorCell, transform);
            cell.GetComponent<Image>().sprite = armorMiddle;
            cell.GetComponent<RectTransform>().anchoredPosition += new Vector2(i * offsetX + initX, initY);
            cell.transform.localScale = new Vector3(scale, scale, scale);
        }
        GameObject cellEnd = Instantiate(armorCellEnd, transform);
        cellEnd.GetComponent<Image>().sprite = armorEnd;
        cellEnd.GetComponent<RectTransform>().anchoredPosition += new Vector2((total - 3) * offsetX + endOffsetX, initY);
        cellEnd.transform.localScale = new Vector3(scale * 0.8f, scale * 0.8f, scale * 0.8f);
        
        SetFillCount(armor);
    }

    void SetFillCount(int amount)
    {
        if (amount > armorTotal)
            amount = armorTotal; armor = amount;
            
        for (int i = transform.childCount - 1; i > -1; i--)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = i >= amount ? armorMiddleEmpty : armorMiddle;
        }

        transform.GetChild(transform.childCount - 1).GetComponent<Image>().sprite = amount == transform.childCount ? armorEnd : armorEndEmpty;
    }

    void ArmorSlotUpdate(GameObject target, GameObject attacker, int amount)
    {
        if (target.CompareTag("Player"))
            SetFillCount(amount);
    }

    void OnEnable()
    {
        EventManager.OnArmorHit += ArmorSlotUpdate;
        EventManager.OnArmorUpdate += SetArmorCount;
    }

    void OnDisable()
    {
        EventManager.OnArmorHit -= ArmorSlotUpdate;
        EventManager.OnArmorUpdate -= SetArmorCount;
    }
}
