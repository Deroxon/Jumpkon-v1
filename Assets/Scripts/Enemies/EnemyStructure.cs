using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStructure : Singleton<EnemyStructure>
{
    // In future there could be implemented, health, attack etc, for now its basic for EnemyLogicScript
    public string Tag { get; set; }
    public float OffsetMoveAnimation { get; set; }
    public float OffsetAttackAnimation { get; set; }
    public float MoveVelocity { get; set; }
    public float OffsetChangePosition { get; set;}
    public float OffsetVelocityDifferentWay { get; set; }



    public EnemyStructure(
        string tag,
        float offsetMoveAnimation,
        float offsetAttackAnimation,
        float moveVelocity, 
        float offsetChangePosition,
        float offsetVelocityDifferentWay
        )
    {
        Tag = tag;
        OffsetMoveAnimation = offsetMoveAnimation;
        OffsetAttackAnimation = offsetAttackAnimation;
        MoveVelocity = moveVelocity;
        OffsetChangePosition = offsetChangePosition;
        OffsetVelocityDifferentWay = offsetVelocityDifferentWay;

    }

}
