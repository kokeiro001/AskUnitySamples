using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveLerpQuestion
{
    public class Player : MonoBehaviour
    {
        private bool gimmickFlag_ = true;
        public SlideObject child_;

        internal bool GetHaveFlag()
        {
            return gimmickFlag_;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                gimmickFlag_ = !gimmickFlag_;
            }
            if (gimmickFlag_)
            {
                UpdateGimmick(); // この中でUpdateInput()を呼んでいます
            }
        }
        private void UpdateGimmick()
        {
            if (!child_)
            {
                gimmickFlag_ = false;
                return;
            }
            else
            {
                //haveFlag_ = false;
                // 動くオブジェクトの子供がいればUpdateGimmick()
                child_.UpdateInput();
            }
        }
    }
}
