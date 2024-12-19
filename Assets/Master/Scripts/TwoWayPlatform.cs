using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TwoWayPlatform : MonoBehaviour
{
    private GameObject currentPlatformCollider;
    [SerializeField] private CapsuleCollider2D PlayerCollider;
    private void Update() 
    {
        Debug.Log("vertical" + Input.GetAxisRaw("Vertical"));
        //下にジャンプするとコルーチンを起動
        if ((Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.DownArrow)) 
        || ((Input.GetAxisRaw("Vertical") < 0) && Input.GetKey(KeyCode.Joystick1Button1)))
        {
            if (currentPlatformCollider != null)
            {
                StartCoroutine(DisablePlatformCoroutine());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "HalfGround")
        {
            currentPlatformCollider = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "HalfGround")
        {
            currentPlatformCollider = null;
        }
    }

    //プラットフォーム用コルーチン
    private IEnumerator DisablePlatformCoroutine()
    {
        TilemapCollider2D platformCollider = currentPlatformCollider.GetComponent<TilemapCollider2D>();

        //プレイヤーとプラットフォームのコリジョン関係を一回消す
        Physics2D.IgnoreCollision(PlayerCollider, platformCollider);

        yield return new WaitForSeconds(0.3f);

        //0.25秒後コリジョンを戻す
        Physics2D.IgnoreCollision(PlayerCollider, platformCollider, false);
    }

}
