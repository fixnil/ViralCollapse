using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Character;
    public GameObject DieEffect;

    private Vector3 _initPosition;

    private void Start()
    {
        _initPosition = this.transform.position;
    }

    private void Update()
    {
        this.transform.LookAt(Character);
    }

    public void Hit(Vector3 hitPoint)
    {
        var effect = Instantiate(DieEffect);
        effect.transform.position = hitPoint;
        effect.transform.rotation = Quaternion.identity;

        effect.SetActive(true);
        this.gameObject.SetActive(false);

        Destroy(effect, 2);
        this.Invoke(nameof(this.GenEnemy), Random.Range(2, 6));
    }

    private void GenEnemy()
    {
        var genPosition = _initPosition;

        genPosition.x += Random.Range(1, 8);
        genPosition.y += Random.Range(-2, 2);
        genPosition.z += Random.Range(-6, 6);

        this.transform.position = genPosition;
        this.transform.LookAt(Character);

        this.gameObject.SetActive(true);
    }
}
