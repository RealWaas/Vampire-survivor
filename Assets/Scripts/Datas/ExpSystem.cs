using System;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    private const float BASE_EXP_THRESHOLD = 5;
    public float expThreshold { get; private set; } = BASE_EXP_THRESHOLD;
    public float currentExp { get; private set; } = 0;

    public event Action OnExpChanged;

    public void GainExp(float _expIn)
    {
        currentExp += _expIn;
        if (currentExp >= expThreshold)
            LevelUp();
        else
            OnExpChanged?.Invoke();
    }

    private void LevelUp()
    {
        currentExp -= expThreshold;
        expThreshold *= 2;
        OnExpChanged?.Invoke();
        GameManager.SetGameState(GameState.ItemSelection);
    }
    private void Start()
    {
        ResetExp();
    }
    public void ResetExp()
    {
        expThreshold = BASE_EXP_THRESHOLD;
        currentExp = 0;
        OnExpChanged?.Invoke();
    }

}
