using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseItemInv : MonoBehaviour
{
    public GameObject useItemCanvas;

    private ItemData currentItem;
    private PlayerController controller;
    private PlayerCondition condition;

    public ItemSlot[] itemUseSlot;  // 아이템 효과 지속시간동안 옮겨줄 슬롯

    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;

        useItemCanvas.SetActive(true);

        for (int i = 0; i < itemUseSlot.Length; i++)
        {
            itemUseSlot[i].gameObject.SetActive(false);
        }


    }

    public void ReceiveItem(ItemSlot slotItem)
    {

        ItemSlot emptySlot = GetEmptyItemUseSlot();

        if (emptySlot != null)
        {
            emptySlot.item = slotItem.item;
            emptySlot.gameObject.SetActive(true);


            StartCoroutine(UseItem(emptySlot));
            UpdateUI();

        }

    }

    void UpdateUI()
    {
        for (int i = 0; i < itemUseSlot.Length; i++)
        {
            if (itemUseSlot[i].item != null)
            {
                itemUseSlot[i].Set();
            }
            else
            {
                itemUseSlot[i].Clear();
            }
        }
    }

    private IEnumerator ApplySpeedUp(float value, float duration)
    {
        CharacterManager.Instance.Player.controller.AddMoveSpeed(value);
        yield return new WaitForSeconds(duration);
        CharacterManager.Instance.Player.controller.AddMoveSpeed(-value);
    }

    private IEnumerator ApplyJumpPowerUp(float value, float duration)
    {
        CharacterManager.Instance.Player.controller.AddJumpPower(value);
        yield return new WaitForSeconds(duration);
        CharacterManager.Instance.Player.controller.AddJumpPower(-value);
    }

    public IEnumerator UseItem(ItemSlot emptySlot)
    {

        float _duration = 0f;



        for (int i = 0; i < emptySlot.item.consumables.Length; i++)
        {
            ItemDataConsumable consumable = emptySlot.item.consumables[i];


            if (_duration < consumable.duration)
            {

                _duration = consumable.duration;
            }

            switch (consumable.type)
            {
                case ConsumableType.SpeedUp:
                    StartCoroutine(ApplySpeedUp(consumable.value, consumable.duration));
                    break;

                case ConsumableType.God:

                    break;

                case ConsumableType.RandomEffect:

                    break;

                case ConsumableType.JumpPower:
                    StartCoroutine(ApplyJumpPowerUp(consumable.value, consumable.duration));
                    break;

                case ConsumableType.Heal:
                    condition.Heal(consumable.value);
                    break;
            }

        }

        yield return new WaitForSeconds(_duration + 0.1f);
        RemoveItemFromUseSlot(emptySlot);

    }

    private void RemoveItemFromUseSlot(ItemSlot slot)
    {
        if (slot != null)
        {
            slot.Clear();
            slot.gameObject.SetActive(false);
        }
    }

    private ItemSlot GetEmptyItemUseSlot()
    {
        for (int i = 0; i < itemUseSlot.Length; i++)
        {
            if (itemUseSlot[i].item == null)
                return itemUseSlot[i];

        }
        return null;
    }


}
