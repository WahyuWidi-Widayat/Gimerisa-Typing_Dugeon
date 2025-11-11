using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public Player player;
    public SpawnerController spawnerController;
    public TMPro.TextMeshProUGUI mustKillText;
    public TMPro.TextMeshProUGUI healthText;

    public GameObject healthBar;

    void Start()
    {
        if (spawnerController != null)
            spawnerController.hudManager = this;
    }

    public void UpdateMustKillText(int remaining)
    {
        if (mustKillText != null)
        {
            mustKillText.text =  remaining.ToString();
        }
    }

    public void UpdateHealthText(int hp)
    {
        if (healthText != null)
        {
            healthText.text =  hp.ToString();
        }
    }
}
