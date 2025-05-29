using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;

[RequireComponent(typeof(Paddle))]
public class MovePaddleAgent : Agent
{
    public Transform _brickHolder;
    [SerializeField] private Ball _ball;

    public override void OnEpisodeBegin()
    {
        GetComponent<Paddle>().ResetPaddle();
        _brickHolder.GetComponent<BrickSpawner>().Spawn();
        _ball.ResetBall();
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        Vector2 direction = Vector2.right * moveX;

        GetComponent<Paddle>().Move(direction);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector2)transform.localPosition);
        sensor.AddObservation((Vector2)_ball.transform.localPosition);
        sensor.AddObservation(_ball.GetComponent<Rigidbody2D>().linearVelocity);

        foreach (Transform brick in _brickHolder)
        {
            sensor.AddObservation((Vector2)brick.localPosition);
            sensor.AddObservation(brick.gameObject.activeSelf);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
    }
}
