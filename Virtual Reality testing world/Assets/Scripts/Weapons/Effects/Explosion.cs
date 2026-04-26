using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float minScale = 1;
    [SerializeField] float maxScale = 5;
    [SerializeField] float explosionTime = 0.5f;
    [SerializeField] float knockbackForce = 10;
    Vector3 startScale;
    [SerializeField] GameObject visual;
    [SerializeField] GameObject particles;
    Material mat;


    public LayerMask damageMask;
    public float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = visual.GetComponent<MeshRenderer>().material;
        startScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        StartCoroutine(Explode());
        Instantiate(particles, transform.position, Quaternion.identity);
    }

    public IEnumerator Explode()
    {
        float time = 0;
        while (time < explosionTime)
        {
            transform.localScale = startScale * Mathf.Lerp(minScale, maxScale, time / explosionTime);
            mat.SetFloat("_Alpha", 2 - (time * 2) / explosionTime);
            //mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 2 - (time * 2) / explosionTime);
            time += Time.deltaTime;
            yield return null;
        }
        time = 0;
        GetComponent<Collider>().enabled = false;
        mat.SetFloat("_Alpha", 0);
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((damageMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            if (other.gameObject.GetComponent<IDamageable>() != null)
            {
                IDamageable target = other.gameObject.GetComponent<IDamageable>();
                target.Damage(damage);
                Vector3 dir = new Vector3(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y, other.transform.position.z - transform.position.z).normalized;
                target.Knockback(dir, knockbackForce);
            }
        }
    }
}
