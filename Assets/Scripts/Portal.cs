using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public float loadNextScenceDelay = 0.5f;

    protected override void OnCollide(Collider2D collider)
    {
        if (collider.name == "Player")
        {
            GameManager.instance.SaveState();
            Invoke(nameof(LoadNextScence), loadNextScenceDelay);
        }
    }

    private void LoadNextScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
