using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour, IDamageable, IHealable
{
    public int Health { get; private set; }
    public static event Action<int> OnHealthChanged;
    private Player player;
    private bool isDamageable;
    private Coroutine immortalCo;

    private void Start()
    {
        if (TryGetComponent<Player>(out player))
        {
            Health = player.playerData.health;
        }
        isDamageable = true;
        OnHealthChanged?.Invoke(Health);
    }
    public void TakeDamage(int damage)
    {
        if (isDamageable)
        {
            Health--;
            isDamageable = false;
            SetImmortal();
            if (Health <= 0)
            {
                GameManager.Instance.SwitchGameState(GameState.Lose);
            }
            else
            {
                player.StateMachine.ChangeState(player.KnockbackState);
            }
            OnHealthChanged?.Invoke(Health);
        }
    }

    public void SetImmortal()
    {
        if (immortalCo != null)
            return;
        immortalCo = StartCoroutine(ImmortalCo());
    }
    private IEnumerator ImmortalCo()
    {
        yield return new WaitForSeconds(.5f);
        immortalCo = null;
        isDamageable = true;
    }

    public void Heal()
    {
        Health++;
        OnHealthChanged?.Invoke(Health);
    }
}
