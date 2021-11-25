using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryPrediction : MonoBehaviour
{
    public static TrajectoryPrediction Instance;
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
        Instance = this;

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

    }

    private void FixedUpdate()
    {
        if(_currentPhysicsScene.IsValid())
        {
            _currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }

    public void Predict(Vector3 force, GameObject subject)
    {
        ImportObjstacles();

        //Debug.Log("Trying to predict");
        if (_currentPhysicsScene.IsValid() && _predictionScene.IsValid())
        {
            //Debug.Log("Predicting");
            if (_dummyObject == null) //Instantiate dummy object if not available
            {
                _dummyObject = Instantiate(subject);
                SceneManager.MoveGameObjectToScene(_dummyObject, _predictionScene);
            }

            _dummyObject.transform.position = subject.transform.position;
            _dummyObject.GetComponent<Rigidbody>().velocity = subject.GetComponent<Rigidbody>().velocity;
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
        DestroyAllDummyObstacles();
    }

    public void ToggleLineRenderer(bool isOn)
    {
        _lineRenderer.enabled = isOn;
        if(!isOn) _lineRenderer.positionCount = 0;
    }

    private void ImportObjstacles()
    {
        foreach (GameObject obstacle in _obstacles)
        {
            GameObject dummyObstacle = Instantiate(obstacle, obstacle.transform.position, obstacle.transform.rotation);
            Renderer dummyRenderer = dummyObstacle.GetComponent<Renderer>();
            if (dummyRenderer != null) dummyRenderer.enabled = false;

            Rigidbody dummyRG = dummyObstacle.GetComponent<Rigidbody>();
            //if (dummyRG != null) dummyRG.isKinematic = true;

            SceneManager.MoveGameObjectToScene(dummyObstacle, _predictionScene);
            _dummyObstacles.Add(dummyObstacle);
        }
    }

    private void DestroyAllDummyObstacles()
    {
        foreach (GameObject obstacle in _dummyObstacles)
        {
            Destroy(obstacle);
        }

        _dummyObstacles.Clear();
    }
}
