using System.Collections;
using System.Collections.Generic;
using StackMaker.Code.Script.Helper;
using StackMaker.Code.Script.Model.Player;
using StackMaker.Code.Script.Value;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    private Animator animator;
    private const string PLAYER = "Player";
    private VectorHelper vectorHelper = new VectorHelper();
    private Vector3 pushRot = new Vector3(1.0f, 0.0f, -1.0f);

    private void Awake()
    {
        OnInit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER))
        {
            Player player = other.GetComponent<Player>();
            Vector3 pushVector = transform.rotation * pushRot + vectorHelper.GetDirectionVector(player.Movement.MoveDirection);

            player.Action.Stop(transform.position);
            pushVector = PushVector(pushVector);
            Enums.Direction pushDirection = vectorHelper.GetDirection(pushVector);
            Push(player, pushDirection);
        }
    }

    private static Vector3 PushVector(Vector3 pushVector)
    {
        // Code trâu :D em xử lí số liệu theo log nên chưa clean code
        if (Mathf.Abs(pushVector.x) >= 1 && Mathf.Abs(pushVector.z) >= 1)
        {
            if (Mathf.Abs(pushVector.z) > Mathf.Abs(pushVector.x))
            {
                pushVector.z = 0;       
            }
            else
            {
                pushVector.x = 0;
            }
        }
        else
        {
            pushVector.x *= -1;
            pushVector.z *= -1;
        }

        return pushVector;
    }


    void OnInit()
    {
        animator = GetComponent<Animator>();
    }
    void Push(Player player, Enums.Direction direction)
    {
        player.Action.Move(direction);
        Bounce();
    }
    // Animations
    void Bounce()
    {
        animator.SetBool("bounce", true);
        Invoke(nameof(Idle), 1.3f);
    }

    void Idle()
    {
        animator.SetBool("bounce", false);
    }
}