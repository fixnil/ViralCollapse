using UnityEngine;

public class Character : MonoBehaviour
{
    public int MoveSpeed = 6;
    public int ShootDistance = 100;
    public int MouseSensitivity = 5;

    public Animator GunAnimator;
    public CharacterController CharacterController;

    public Transform ShootPoint;
    public GameObject ShootHitEffect;
    public AudioClip ShootAudioClip;
    public AudioSource ShootAudioSource;

    public Vector2 RotationTemp = new(0, -90);

    private void Update()
    {
        this.Move();
        this.Rotate();
        this.Fire();
    }

    private void Move()
    {
        var h = Input.GetAxis(Constants.Horizontal);
        var v = Input.GetAxis(Constants.Vertical);

        if (h != 0 || v != 0)
        {
            var direction = new Vector3(h, 0, v);

            direction = this.transform.TransformDirection(direction);

            CharacterController.SimpleMove(direction * MoveSpeed * Time.deltaTime);

            GunAnimator.SetBool(Constants.IsWalk, true);
        }
        else
        {
            GunAnimator.SetBool(Constants.IsWalk, false);
        }
    }

    private void Rotate()
    {
        var mouseX = Input.GetAxis(Constants.MouseX);
        var mouseY = Input.GetAxis(Constants.MouseY);

        RotationTemp.x -= mouseY * MouseSensitivity;
        RotationTemp.y += mouseX * MouseSensitivity;

        RotationTemp.x = Mathf.Clamp(RotationTemp.x, -45, 30);

        this.transform.rotation = Quaternion.Euler(RotationTemp.x, RotationTemp.y, 0);
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            GunAnimator.SetTrigger(Constants.IsFire);

            if (Physics.Raycast(ShootPoint.position, ShootPoint.forward, out var hitInfo, ShootDistance))
            {
                ShootAudioSource.PlayOneShot(ShootAudioClip);

                var effect = Instantiate(ShootHitEffect);

                effect.transform.position = hitInfo.point;
                effect.transform.forward = hitInfo.normal;

                effect.SetActive(true);

                Destroy(effect, 2);

                if (hitInfo.transform.TryGetComponent<Enemy>(out var enemy))
                {
                    enemy.Hit(hitInfo.point);
                }
            }
        }
    }
}
