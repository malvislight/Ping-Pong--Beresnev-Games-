using UnityEngine;

namespace Ball
{
    public class BallInitializer : MonoBehaviour
    {
        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            BallCollisionDetector.OnGoal += Initialize;
        }

        private void Initialize()
        {
            transform.position = Vector3.zero;
            transform.localScale = Vector3.one * Random.Range(0.5f, 1f);
        }

        private void OnDisable()
        {
            BallCollisionDetector.OnGoal -= Initialize;
        }
    }
}