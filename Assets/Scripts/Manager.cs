using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Juice levels
/// 1: Default
/// 2: Animations
/// 3: Gun SFX
/// 4: Deeper gun sfx
/// 5: More enemies, low hp,
/// 6: Shoot faster
/// 7: Bigger, cooler looking bullets
/// 8: Faster bullets
/// 9: Muzzle Flash
/// 10: Weapon Spread
/// 11: Weapon recoil
/// 12: Enemies get knocked back by bullets
/// 13: Bullet casings
/// 14: impact effects
/// 15: multishot 
/// 16: Make enemies a bit stronger now
/// 17: Camera lerp
/// 18: Look forwards
/// 19: Camera shake
/// 20: Enemy light up on damage
/// 21: Slight slowdown on enemy impact;
/// 22: Enemy corpses and leave bullet casings.
/// 23: I-frames on self
/// </summary>



public class Manager : MonoBehaviour
{
    public static int juiceLevel = 1;
    public TextMeshProUGUI textbox;
    public GameObject enemyPrefab;
    int spawnCount = 2;

    
    // Start is called before the first frame update
    void Start()
    {
        textbox.text = "Level: " + juiceLevel;
        if (juiceLevel >= 5)
            spawnCount = 10;
        if (juiceLevel >= 16)
            spawnCount = 50;
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 rand = Random.insideUnitCircle.normalized * Random.Range(0.5f, 4f);

            Instantiate(enemyPrefab, rand * 10, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallSleep()
    {
        if (Time.timeScale == 1)
        {
            StartCoroutine("Sleep");
        }
    }


    IEnumerator Sleep()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.03f);
        Time.timeScale = 1f;
    }

    public void JuiceUp()
    {
        juiceLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void JuiceDown()
    {
        juiceLevel--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetJuice(int level)
    {
        juiceLevel = level;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
