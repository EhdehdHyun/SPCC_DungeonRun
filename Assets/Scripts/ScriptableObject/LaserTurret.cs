using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damage = 10f;
    public float damageInterval = 0.5f;
    public float changeLength = 99f;

    private float lastDamageTime;
    private float lastLength;
    private BoxCollider col;

    private void Start()
    {
        col = GetComponent<BoxCollider>();
        col.isTrigger = true;

        lastLength = changeLength;
        UpdateLaserLenght();
    }

    void Update()
    {
        if (!Mathf.Approximately(changeLength, lastLength)) // A와 B의 값이 '거의' 같지 않으면
        {
            UpdateLaserLenght();
            lastLength = changeLength;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                IDamagable target = other.GetComponentInParent<IDamagable>();
                if (target != null)
                {
                    target.TakePhysicalDamage((int)damage);
                    lastDamageTime = Time.time;
                }
            }
        }
    }

    void UpdateLaserLenght()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = changeLength;
        transform.localScale = newScale;
    }
}
