using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EquipmentManager : MonoBehaviour
{
    public static UI_EquipmentManager instance;
    public List<SlotEquipment> slotEquipments;
    public GameObject slotEquipment;
    public int countEquipment;
    public Transform equipmentParentTransform;
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
        for (int i = 0; i <= countEquipment; i++)
        {
            GameObject slotCreate = Instantiate(
                slotEquipment, equipmentParentTransform.position, Quaternion.identity, equipmentParentTransform
            );

            AddSlot(slotCreate.GetComponent<SlotEquipment>());
        }
    }

    void AddSlot(SlotEquipment slot)
    {
        slotEquipments.Add(slot);
    }


    public void AddItem(Item item, int itemCount)
    {
        int indexCurrentSlot = slotEquipments.FindIndex(slot => slot.GetItem() == item);

        if (indexCurrentSlot != -1)
        {
            slotEquipments[indexCurrentSlot].SetItem(item);
            slotEquipments[indexCurrentSlot].SetCount(itemCount);
            return;
        }

        int indexNullSlot = slotEquipments.FindIndex(slot => slot.GetItem() == null);
        slotEquipments[indexNullSlot].SetItem(item);
        slotEquipments[indexNullSlot].SetCount(itemCount);
    }
}
