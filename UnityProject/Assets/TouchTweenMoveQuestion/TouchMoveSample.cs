using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TouchTweenMoveQuestion
{
    public class TouchMoveSample : MonoBehaviour
    {
        float time;
        float duration;
        Vector3 startPosition;
        Vector3 endPosition;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                float x = Input.mousePosition.x;
                float y = Input.mousePosition.y;
                Vector3 currentScreenPoint = new Vector3(x, y, 0);
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);

                Move(touchPosition, 0.2f);
            }

            if (time <= duration)
            {
                time += Time.deltaTime;
                var x = Mathf.Lerp(startPosition.x, endPosition.x, time / duration);
                var y = Mathf.Lerp(startPosition.y, endPosition.y, time / duration);
                transform.position = new Vector3(x, y);
            }

            Vector3 s_position = Input.mousePosition;
            Vector3 w_position = Camera.main.ScreenToWorldPoint(s_position);

            Debug.Log(w_position);
        }

        void Move(Vector3 endPosition, float duration)
        {
            time = 0;
            this.duration = duration;
            startPosition = transform.position;
            this.endPosition = endPosition;
        }
    }
}
