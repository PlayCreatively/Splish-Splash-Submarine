using UnityEngine;
using UnityEngine.Events;

public class EFBLurker : MonoBehaviour
{
    [SerializeField]
    float heightOfHand = 4;

    public UnityEvent OnCaught;
    public UnityEvent<float> OnApproaching;

    GameState State => GameState.Get;

    float maxDistanceFromPlayer;
    float distanceFromPlayer;

    void Awake()
    {
        maxDistanceFromPlayer = GlobalSettings.Current.enemyFromBehind.maxDistanceFromPlayer;

        State.efbDistanceFromPlayer = GlobalSettings.Current.enemyFromBehind.maxDistanceFromPlayer;
        State.PlayerVerticalSpeed = GlobalSettings.Current.player.verticalSpeed;
        State.LatchedEnemyCount = 0;
    }

    void Update()
    {
        distanceFromPlayer = State.efbDistanceFromPlayer;

        // Update distance to player
        float VerticalDelta = State.LatchedEnemyCount > 0
            ? State.LatchedEnemyCount
            : -GlobalSettings.Current.enemyFromBehind.detractingSpeedRatio;

        distanceFromPlayer -= VerticalDelta * Time.deltaTime;
        distanceFromPlayer = Mathf.Clamp(distanceFromPlayer, 0, GlobalSettings.Current.enemyFromBehind.maxDistanceFromPlayer);

        if(distanceFromPlayer <= 0)
        {
            OnCaught?.Invoke();
            enabled = false;
        }
        else
        {
            float onRatio = distanceFromPlayer / maxDistanceFromPlayer;
            OnApproaching?.Invoke(onRatio);
        }

        UpdatePosition();

        State.efbDistanceFromPlayer = distanceFromPlayer;
    }
    float GetBottomOfScreen() 
        => Camera.main.transform.position.y - Camera.main.orthographicSize;

    void UpdatePosition()
    {
        float bottomOfScreen = GetBottomOfScreen();

        // Normal for how close the enemy is to the player from 0 to 1
        float normalFromPlayer = 1 - (GameState.Get.efbDistanceFromPlayer / maxDistanceFromPlayer);

        transform.position = new Vector3(GlobalSettings.Current.player.Ref.position.x, bottomOfScreen + heightOfHand * normalFromPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 bottom = new (transform.position.x, GetBottomOfScreen());
        Gizmos.DrawLine(bottom, bottom + Vector3.up * heightOfHand);
    }

}
