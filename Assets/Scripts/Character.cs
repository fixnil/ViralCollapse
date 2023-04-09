using UnityEngine;

public class Character : MonoBehaviour
{
    public int MoveSpeed = 6;
    public CharacterController CharacterController;
    public Animator GunAnimator;

    private void Update()
    {
        this.Move();
    }

    private void Move()
    {
        var v = Input.GetAxis(Constants.Vertical);
        var h = Input.GetAxis(Constants.Horizontal);

        if (v != 0 || h != 0)
        {
            var direction = new Vector3(-v, 0, h);

            CharacterController.SimpleMove(direction * MoveSpeed);

            GunAnimator.SetBool(Constants.IsWalk, true);
        }
        else
        {
            GunAnimator.SetBool(Constants.IsWalk, false);
        }
    }
}
