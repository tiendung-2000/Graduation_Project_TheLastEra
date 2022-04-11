using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquidmentManager : MonoBehaviour
{
    public static EquidmentManager instance;
    public List<SlotEquidment> slotEquidments;
    public GameObject slotEquidment;
    public int countEquidment;
    public Transform equidmentParentTransform;
    public Player player;
    #region singleton
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
    #endregion

    private void Start()
    {
        CreateSlot();
    }

    void CreateSlot()
    {
        for (int i = 0; i <= countEquidment; i++)
        {
            GameObject slotCreate = Instantiate(
                slotEquidment, equidmentParentTransform.position, Quaternion.identity, equidmentParentTransform
            );

            AddSlot(slotCreate.GetComponent<SlotEquidment>());
        }
    }

    void AddSlot(SlotEquidment slot)
    {
        slotEquidments.Add(slot);
    }


    public void AddItem(Item item, int itemCount)
    {
        int indexCurrentSlot = slotEquidments.FindIndex(slot => slot.GetItem() == item);

        if (indexCurrentSlot != -1)
        {
            slotEquidments[indexCurrentSlot].SetItem(item);
            slotEquidments[indexCurrentSlot].SetCount(itemCount);
            return;
        }

        int indexNullSlot = slotEquidments.FindIndex(slot => slot.GetItem() == null);
        slotEquidments[indexNullSlot].SetItem(item);
        slotEquidments[indexNullSlot].SetCount(itemCount);
    }
}
