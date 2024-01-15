using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public FirstPersonController player;
    public Weapon weapon;
    public int maxEnemy;
    public XPBar xpBar;

    public GameObject UpgradePanel;
    public AudioSource pauseMusic;
    public AudioSource backgroundMusic;

    private void Awake()
    {
        pauseMusic.time = 1f;
        xpBar.SetMaxXP(player.level_up_threshold);
    }
    void Start()
    {
        StartCoroutine(offSetSpawn(0.3f));
    }

    private IEnumerator offSetSpawn(float offSet)
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemy)
        {
            yield return new WaitForSeconds(offSet);
            //Instantiate(Enemy, new Vector3(Random.Range(player.transform.position.x - 20, player.transform.position.x + 20), 1, Random.Range(player.transform.position.z - 20, player.transform.position.z + 20)), Quaternion.identity);
            Instantiate(enemy, new Vector3(Random.Range(-50, -40), 1, Random.Range(-50, -40)), Quaternion.identity);
            StartCoroutine(offSetSpawn(offSet));    
        }
    }

    public void Death()
    {
        player.transform.position = new Vector3(0, 5, 0); // Respawn Point

        player.currentHealth = 100;
        player.healthBar.SetMaxHP(player.currentHealth); // Reset HP bar
        
    }

    public void RespawnEnemy()
    {
        StartCoroutine(offSetSpawn(10f));
    }

    public void GiveXP(GameObject xp)
    {
        Destroy(xp);

        if ((player.xp + 10) == player.level_up_threshold) {
            LevelUp();
        }

        player.xp += 10;
        xpBar.SetXP(player.xp);
    }

    public void LevelUp()
    {
        player.level++;
        player.xp = 0;

        xpBar.SetXP(player.xp);
        xpBar.levelDisplay(player.level);
        //player.level_up_threshold = next_levelup_threshold;

        // Upgrade management
        StartCoroutine(offSetPause(0.5f,UpgradePanel));
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        backgroundMusic.Pause();
        pauseMusic.Play();
        player.cameraCanMove = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        pauseMusic.Stop();
        backgroundMusic.UnPause();
        player.cameraCanMove = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnClickWeaponSize()
    {
        weapon.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        offSetResumeGame(UpgradePanel);
    }

    public void OnClickPlayerSpeed()
    {
        player.walkSpeed += 1;
        offSetResumeGame(UpgradePanel);
    }

    public void OnClickChatteATaMere()
    {
        player.fov += 10f;
        offSetResumeGame(UpgradePanel);
    }
    public void offSetResumeGame(GameObject panel)
    {
        StartCoroutine(offSetResume(0.5f, panel));
    }
    IEnumerator offSetResume(float offSet, GameObject panel)
    {
        panel.SetActive(false);
        yield return new WaitForSecondsRealtime(offSet);
        UnPauseGame();
    }

    IEnumerator offSetPause(float offSet, GameObject panel)
    {
        PauseGame();
        yield return new WaitForSecondsRealtime(offSet);
        Cursor.visible = true;
        panel.SetActive(true);
    }
}
