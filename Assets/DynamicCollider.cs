using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCollider : MonoBehaviour
{

    public BoxCollider2D trampolineCollider;
    public Animator trampolineAnimator;
    private bool extracted = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator AdjustColliderHeight( )
    {
            Vector2 savedSize = trampolineCollider.size;
            Vector2 savedOffset = trampolineCollider.offset;

            float newHeight = savedSize.y * 0.5f;

            float heightDifference = savedSize.y - newHeight;

            // Set new size
            trampolineCollider.size = new Vector2(savedSize.x, newHeight);

            // set new offset from the top
            trampolineCollider.offset = new Vector2(savedOffset.x, savedOffset.y - heightDifference / 2);

            yield return new WaitForSeconds(0.2f);

            trampolineCollider.size = savedSize;
            trampolineCollider.offset = savedOffset;
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AdjustColliderHeight());
            StartCoroutine(extract());
        }
    }

    public IEnumerator extract()
    {

        if(!extracted )
        {
            extracted = true;
            trampolineAnimator.SetBool("isStand", true);
            yield return new WaitForSeconds(0.2f);

            PlayerManager.Instance.rb.velocity = new Vector2(PlayerManager.Instance.rb.velocity.x, 40);
            trampolineAnimator.SetBool("isStand", false);
            extracted = false;

        }

        
    }


}
