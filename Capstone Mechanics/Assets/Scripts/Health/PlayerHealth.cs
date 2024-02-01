using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float Health, MaxHealth;
    public GameObject Fox;
    public bool hasBeenMet = false;

    [SerializeField]
    private HealthBarUI healthBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);   
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBeenMet)
        {
            if (getFVelocity())
            {
                SetHealth(-10f);
                hasBeenMet = true;
                
            }
        }

        if (Fox.GetComponent<Rigidbody2D>().velocity.y > -7)
        {
            hasBeenMet = false;
        }

        if (Health <= 0)
        {
            ReloadActiveScene();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SetHealth(20f);
        }
    }
    public void SetHealth(float healthChange)
    {
        Health += healthChange;
        Health = Mathf.Clamp(Health, 0, MaxHealth);

        healthBar.SetHealth(Health);
    }
    private void ReloadActiveScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public bool getFVelocity()
    {
        if (Fox.GetComponent<Rigidbody2D>().velocity.y < -15)
        {
            return true;
        }
        return false;
    }
}
