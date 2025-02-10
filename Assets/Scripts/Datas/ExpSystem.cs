using System;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    private const float BASE_EXP_THRESHOLD = 5;
    private const float LEVEL_THRESHOLD_MULTIPLIER = 1.25f;
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

    /// <summary>
    /// Modify the exp thershold and set the state to item selection.
    /// </summary>
    [ContextMenu("levelUp")]
    private void LevelUp()
    {
        currentExp -= expThreshold;
        expThreshold *= LEVEL_THRESHOLD_MULTIPLIER;
        OnExpChanged?.Invoke();
        GameManager.SetGameState(GameState.ItemSelection);
    }
    private void Start()
    {
        ResetExp();
    }

    /// <summary>
    /// Reset the experience to go back to level 0.
    /// </summary>
    public void ResetExp()
    {
        expThreshold = BASE_EXP_THRESHOLD;
        currentExp = 0;
        OnExpChanged?.Invoke();
    }

}
