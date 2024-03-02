using UnityEngine;
using UnityEngine.Events;

public class EFBLurker : MonoBehaviour
{
    [SerializeField]
    float heightOfHand = 4;

    public UnityEvent OnCaught;
    public UnityEvent<float> OnApproaching;

    EnemyFromBehindSettings Settings => GlobalSettings.Current.enemyFromBehind;
    float flickerThreshold;

    void Awake()
    {
        Settings.curDistanceFromPlayer = Settings.maxDistanceFromPlayer;
        Settings.curMoveSpeedOverPlayer = 0;
        GlobalSettings.Current.player.curVerticalSpeed = GlobalSettings.Current.player.verticalSpeed;
        flickerThreshold = Settings.maxDistanceFromPlayer * .75f;
    }

    void Update()
    {
        // Update distance to player
        float VerticalDelta = Settings.curMoveSpeedOverPlayer != 0
            ? Settings.curMoveSpeedOverPlayer
            : -.35f;

        Settings.curDistanceFromPlayer -= VerticalDelta * Time.deltaTime;
        Settings.curDistanceFromPlayer = Mathf.Clamp(Settings.curDistanceFromPlayer, 0, Settings.maxDistanceFromPlayer);

        if(Settings.curDistanceFromPlayer <= 0)
        {
            OnCaught?.Invoke();
            enabled = false;
        }
        else if(Settings.curDistanceFromPlayer < flickerThreshold)
        {
            float onRatio = Settings.curDistanceFromPlayer / flickerThreshold;
            OnApproaching?.Invoke(onRatio);
        }

        UpdatePosition();
    }
    float GetBottomOfScreen() 
        => Camera.main.transform.position.y - Camera.main.orthographicSize;

    void UpdatePosition()
    {
        float bottomOfScreen = GetBottomOfScreen();

        // Normal for how close the enemy is to the player from 0 to 1
        float normalFromPlayer = 1 - (Settings.curDistanceFromPlayer / Settings.maxDistanceFromPlayer);

        transform.position = new Vector3(GlobalSettings.Current.player.Ref.position.x, bottomOfScreen + heightOfHand * normalFromPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 bottom = new (transform.position.x, GetBottomOfScreen());
        Gizmos.DrawLine(bottom, bottom + Vector3.up * heightOfHand);
    }

}
