using UnityEngine;
using UnityEngine.SceneManagement;
public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
           playerDeath();
        }
    }
   private void playerDeath()
    {
        SceneManager.LoadScene("save");
    }
}