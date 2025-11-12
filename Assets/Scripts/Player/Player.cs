using System;
using System.Collections;
using System.Collections.Generic;
using Polyperfect.Universal;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerCondition condition;
    //장비 장착
    public ItemData itemData;
    public Action addItem;
    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        movement = GetComponent<PlayerMovement>();
        condition = GetComponent<PlayerCondition>();
        //장비 장착 컴포넌트 가져올 자리
    }
}
