using UnityEngine.EventSystems;

public  class confirmBuyItem : selectItem
{
    public buyItem buyItem;
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        buyItem.item = this.SlotClass;
    }
}