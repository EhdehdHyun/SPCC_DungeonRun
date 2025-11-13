using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

public class Equip : MonoBehaviour
{
    private GameObject currentItem;

    public void SetItem(GameObject prefab)
    {
        if (currentItem != null) //기존의 아이템을 제거
            Destroy(currentItem);

        if (prefab == null) return; //만약 빈슬롯을 선택하면 아이템이 없으니 아무것도 들지 않음

        currentItem = Instantiate(prefab, transform);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
    }

    public void Clean()
    {
        if (currentItem != null)
            Destroy(currentItem);
    }
}