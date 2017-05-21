using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveLerpQuestion
{
    public class SlideObject : MonoBehaviour
    {
        [SerializeField] float speed_ = 0.5f;

        Vector3 moveStart_;
        Vector3 moveEnd_;
        float time_;

        Player player;
        public void Awake()
        {
            player = FindObjectOfType<Player>();
        }

        public void Update()
        {
            if (!player) return;
            if (!player.GetHaveFlag()) return;

            time_ += Time.deltaTime * speed_;

            var tmp = Vector3.Lerp(moveStart_, moveEnd_, time_);
            transform.position = new Vector3(tmp.x, transform.position.y, transform.position.z);

            UpdateInput();
        }

        public void UpdateInput()
        {
            if (time_ < 1) return;

            if (Input.GetKeyDown(KeyCode.D))
            {
                StartSlide(2);  // 2右に移動する
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                StartSlide(-2);  // 2左に移動する
            }
        }

        private void StartSlide(float moveLength)
        {
            time_ = 0;
            moveStart_ = transform.position;    // 移動開始時の座標を記憶しておく
            moveEnd_ = moveStart_ + new Vector3(moveLength, 0, 0);  // 移動終了の座標を計算する
        }
    }
}
