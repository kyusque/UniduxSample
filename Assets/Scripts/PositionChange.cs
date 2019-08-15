using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace App
{
    [RequireComponent (typeof (NavMeshAgent))]
    [RequireComponent (typeof (Animator))]
    public class PositionChange : MonoBehaviour
    {
        Animator anim;
        NavMeshAgent agent;
        Vector2 smoothDeltaPosition = Vector2.zero;
        Vector2 velocity = Vector2.zero;

        private void OnEnable()
        {
            anim = GetComponent<Animator> ();
            agent = GetComponent<NavMeshAgent> ();
            // 位置を自動的に更新しません
            agent.updatePosition = false;
            
            App.Unidux
                .Subject
                .TakeUntilDisable(this)
                .StartWith(App.Unidux.State)
                .Subscribe(state =>
                {
                    switch (state.positionState.position)
                    {
                        case PositionState.Position.East:
                            agent.destination = new Vector3(-5, 0, 0);
                            break;
                        case PositionState.Position.West:
                            agent.destination = new Vector3(5, 0, 0);
                            break;
                        case PositionState.Position.South:
                            agent.destination = new Vector3(0, 0, 5);
                            break;
                        case PositionState.Position.North:
                            agent.destination = new Vector3(0, 0, -5);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                })
                .AddTo(this);

            this.UpdateAsObservable()
                .TakeUntilDisable(this)
                .Subscribe(_ =>
                {
                    Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

                    // worldDeltaPosition をローカル空間にマップします
                    float dx = Vector3.Dot(transform.right, worldDeltaPosition);
                    float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
                    Vector2 deltaPosition = new Vector2(dx, dy);

                    // deltaMove にローパスフィルターを適用します
                    float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
                    smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

                    // 時間が進んだら、velocity (速度) を更新します
                    if (Time.deltaTime > 1e-5f)
                        velocity = smoothDeltaPosition / Time.deltaTime;

                    bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

                    // アニメーションのパラメーターを更新します
                    anim.SetBool("move", shouldMove);
                    //anim.SetFloat("velx", velocity.x);
                    //anim.SetFloat("vely", velocity.y);

                }).AddTo(this);

        }
        
        void OnAnimatorMove ()
        {
            transform.position = agent.nextPosition;
        }
    }
}
