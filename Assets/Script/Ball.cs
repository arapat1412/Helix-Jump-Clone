using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public float bounceForce = 400f;
    public GameObject splitPrefab;

    private void Start()
    {
        // Đã dọn dẹp biến audioManager rườm rà
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);

        // Gọi thẳng âm thanh bằng Singleton
        AudioManager.instance.Play("Land");

        GameObject newsplit = Instantiate(splitPrefab,
            new Vector3(transform.position.x, other.transform.position.y + 0.6f, transform.position.z),
            splitPrefab.transform.rotation);
        newsplit.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
        newsplit.transform.parent = other.transform;

        string materialName = other.transform.GetComponent<MeshRenderer>().material.name;

        if (materialName == "Safe (Instance)")
        {
            Debug.Log("you are safe");
        }
        if (materialName == "UnSafe (Instance)")
        {
            GameManager.gameOver = true;
            // Gọi thẳng âm thanh GameOver
            AudioManager.instance.Play("GameOver");
        }
        if (materialName == "LastRing (Instance)" && !GameManager.gameWin)
        {
            GameManager.gameWin = true;
            // Gọi thẳng âm thanh Win
            AudioManager.instance.Play("LevelWin");
        }
    }
}