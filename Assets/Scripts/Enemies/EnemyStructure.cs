using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStructure : MonoBehaviour
{
    // In future there could be implemented, health, attack etc, for now its basic for EnemyLogicScript
    public string Tag { get; set; }
    public float OffsetMoveAnimation { get; set; }
    public float OffsetAttackAnimation { get; set; }
    public float MoveVelocity { get; set; }
    public Animator EnemyAnimator { get; set; }
    public float OffsetMoveAnimationAfterChangeFacing { get; set;}
    public float OffsetVelocityDifferentWay { get; set; }



    public EnemyStructure(
        string tag,
        float offsetMoveAnimation,
        float offsetAttackAnimation,
        float moveVelocity, 
        Animator enemyAnimator,
        float offsetMoveAnimationAfterChangeFacing,
        float offsetVelocityDifferentWay
        )
    {
        Tag = tag;
        OffsetMoveAnimation = offsetMoveAnimation;
        OffsetAttackAnimation = offsetAttackAnimation;
        MoveVelocity = moveVelocity;
        EnemyAnimator = enemyAnimator;
        OffsetMoveAnimationAfterChangeFacing = offsetMoveAnimationAfterChangeFacing;
        OffsetVelocityDifferentWay = offsetVelocityDifferentWay;

    }

}
