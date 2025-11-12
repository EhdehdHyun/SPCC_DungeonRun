using System.Collections;
using System.Collections.Generic;
using Polyperfect.Universal;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement movement;
    public PlayerCondition condition;
    //장비 장착
    //장비와 같은 아이템 데이터
    //아이템을 주웠을 때와 같은 스크립트가 들어 올 곳
    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        movement = GetComponent<PlayerMovement>();
        condition = GetComponent<PlayerCondition>();
        //장비 장착 컴포넌트 가져올 자리
    }
}
