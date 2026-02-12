using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Update()
    {
        // Optional: rotate coin for effect
        transform.Rotate(0f, 100f * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CoinManager.instance != null)
            {
                CoinManager.instance.AddCoin();
            }

            Destroy(gameObject);
        }
    }
}
