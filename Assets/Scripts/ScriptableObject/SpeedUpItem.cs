using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    [Header("아이템 움직임")]
    public float UpDown = 0.5f;
    public float UpDownSpeed = 2f;
    public float rotationSpeed = 30f;

    [Header("아이템 효과")]
    public float speedMultiplier = 1.5f;
    public float Duration = 5f;

    private Vector3 startPos;
    private float originalSpeed;

    void Start()
    {
        startPos = transform.position;
        SphereCollider col = GetComponent<SphereCollider>();
        col.isTrigger = true;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * UpDownSpeed) * UpDown;
        transform.position = new Vector3(startPos.x, newY, startPos.z);

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                GetComponent<Collider>().enabled = false;
                var renderer = GetComponentInChildren<MeshRenderer>();
                if (renderer != null) renderer.enabled = false; //2번 못 먹게 콜라이더와 랜더러? 모델?을 숨김

                StartCoroutine(SpeedBuff(player));
            }
        }
    }

    private System.Collections.IEnumerator SpeedBuff(PlayerMovement player)
    {
        originalSpeed = player.speed;
        player.speed = speedMultiplier;

        yield return new WaitForSeconds(Duration);
        Debug.Log("Test");
        player.speed = originalSpeed;

        Destroy(gameObject);
    }
}
