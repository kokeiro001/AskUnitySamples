using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    // resolve for https://teratail.com/questions/77032

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D tapCollision2D = Physics2D.OverlapPoint(tapPoint);

            // ヒットしたオブジェクトが自分自身かどうか確認する。
            // collider2DのgameObjectには、ヒットした相手のgameObjectが格納されているため、
            // これとPianoコンポーネントがアタッチされているGameObjectの比較を行えば良い。
            if (tapCollision2D.gameObject == gameObject)
            {
                Debug.Log("Play My Sound!!! name=" + name);
                //keySound.PlayOneShot(keySound.clip);
            }
        }
    }
}
