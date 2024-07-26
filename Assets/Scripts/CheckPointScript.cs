using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private Vector3 checkpointPosition;
    [SerializeField]
    private GameObject checkPoint;
    [SerializeField] private Animator animator;
    private bool isCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        checkPoint = this.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isCheck)
        {
            animator.SetTrigger("isChecked");
            isCheck = !isCheck;
            GameManager.Instance.setCheckPoint(this.gameObject.transform);
        }
    }


}
