using UnityEngine;
using UnityEngine.AI;


public class Player : BaseCharacter
{
    protected override void UpdateHealthBar()
    {
        // base.UpdateHealthBar();

        StatsUI statsUI = UIManager.Instance?.hudCanvas.transform.GetComponentInChildren<StatsUI>();

        Transform healthBar = statsUI.PlayerHealthBar.transform;

        float healthPercent = Mathf.Clamp(currentHealth / maxHealth, 0, 1);

        healthBar.localScale = new Vector3(healthPercent, healthBar.localScale.y, healthBar.localScale.z);
    }
}