using UnityEngine;
using System.Collections;

namespace FrameAnimeState
{
    public enum ActorState
    {
        Idle = 0x01,
        Left_Move,
        Right_Move,
        Up_Move,
        Down_Move,
        AwaitOrders,
    }//人物状态表示
    public class AnimeState : MonoBehaviour
    {

        public Sprite[] idle;
        public Sprite[] Left;
        public Sprite[] Up;
        public Sprite[] Down;
        public Sprite[] AwaitOrders;

        Sprite[] currentState;//表示当前状态
        SpriteRenderer sRenderer;

        float timer = 0;
        public float TimeInterval = 0.18f;
        float MoveTimeInterval = 0.08f;
        void Start()
        {
            sRenderer = GetComponentInChildren<SpriteRenderer>();
            currentState = idle;
        }

        int index = 0;//序列帧索引
        void FixedUpdate()
        {

            if (timer > TimeInterval)
            {
                sRenderer.sprite = currentState[index++ % currentState.Length];
                timer = 0;
            }
            timer += Time.deltaTime;

        }

        public void ChangeState(ActorState state)
        {
            switch (state)
            {
                case ActorState.Idle:
                    currentState = idle; break;
                case ActorState.Down_Move:
                    currentState = Down;  break;
                case ActorState.Up_Move:
                    currentState = Up; break;
                case ActorState.Left_Move:
                    if (sRenderer.flipX)
                        sRenderer.flipX = false;
                    currentState = Left; break;
                case ActorState.Right_Move:
                    if (!sRenderer.flipX)
                        sRenderer.flipX = true;
                    currentState = Left; break;
                case ActorState.AwaitOrders:
                    currentState = AwaitOrders;break;

            }
            if (state == ActorState.Idle || state == ActorState.AwaitOrders)
                TimeInterval = 0.18f;
            else
                TimeInterval = MoveTimeInterval;
           index = 0;
           timer = 0;
        }
    }
}

