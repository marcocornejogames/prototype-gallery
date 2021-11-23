using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrajectoryPrediction : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int _maxIterations;
    [SerializeField] private GameObject[] _obstacles;

    private List<GameObject> _dummyObstacles;

    private Scene _predictionScene;
    private PhysicsScene _predictionPhysicsScene;

    private Scene _currentScene;
    private PhysicsScene _currentPhysicsScene;

    private GameObject _dummyObject;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _dummyObstacles = new List<GameObject>();

    }
    void Start()
    {
        Physics.autoSimulation = false;

        _currentScene = SceneManager.GetActiveScene();
        _currentPhysicsScene = _currentScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        _predictionScene = SceneManager.CreateScene("Prediction", parameters);
        _predictionPhysicsScene = _predictionScene.GetPhysicsScene();

        _obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        ImportObjstacles();

    }

    private void FixedUpdate()
    {
        if(_currentPhysicsScene.IsValid())
        {
            _currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }

    public void Predict(Vector3 force)
    {
        //Debug.Log("Trying to predict");
        if (_currentPhysicsScene.IsValid() && _predictionScene.IsValid())
        {
            //Debug.Log("Predicting");
            if (_dummyObject == null) //Instantiate dummy object if not available
            {
                _dummyObject = Instantiate(gameObject);
                SceneManager.MoveGameObjectToScene(_dummyObject, _predictionScene);
            }

            _dummyObject.transform.position = transform.position;
            _dummyObject.GetComponent<Rigidbody>().AddForce(force);

            _lineRenderer.positionCount = _maxIterations;

            for (int i = 0; i < _maxIterations; i++)
            {
                //Debug.Log("Iterating");
                _predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
                _lineRenderer.SetPosition(i, _dummyObject.transform.position);
            }
        }

        Destroy(_dummyObject);
    }

    private void ImportObjstacles()
    {
        foreach (GameObject obstacle in _obstacles)
        {
            GameObject dummyObstacle = Instantiate(obstacle, obstacle.transform.position, obstacle.transform.rotation);
            Renderer dummyRenderer = dummyObstacle.GetComponent<Renderer>();
            if (dummyRenderer != null) dummyRenderer.enabled = false;

            SceneManager.MoveGameObjectToScene(dummyObstacle, _predictionScene);
            _dummyObstacles.Add(dummyObstacle);
        }
    }
}
