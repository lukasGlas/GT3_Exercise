using PolyStang;
using UnityEngine;

public class CarManager : MonoBehaviour
    {
        public CarController greenCar; // middle car
        public CarController blueCar; // left car
        public CarController redCar; // right car
        
        /* Aufgabe 1: Suche dir ein Auto aus und nutze die Methodenaufrufe in den richtigen Event-Funktionen,
         um mit dem Auto ans Ziel zu gelangen:
            car.ActivateSelf();
            car.AnimateWheels();
            car.DestroySelf();
            car.DisplayCarLights();
            car.InitializeCarComponents();
            car.MoveCarForward();
            car.ProcessMovement();
            car.ProcessPlayerInputs();
            car.UpdateSpeedUI();
            
            Aufgabe 2: Füge auch Methodenaufrufe für die anderen beiden Autos hinzu, aber in unterschiedlicher
            Reihenfolge zu deinem ersten Auto. Experimentiere mit verschiedenen Kombinationen. Was fällt dir auf?
         */
        
        // Awake is called when the script instance is being loaded
        private void Awake() // Initialization logic that should occur before the scene is active
        {
            // GREEN CAR
            
            // BLUE CAR
            
            // RED CAR
        }
        
        // Start is called before the first frame update if the script is enabled
        private void Start() // Initialization logic that depends on all game objects being initialized
        {
            // GREEN CAR
            greenCar.ActivateSelf();
            // BLUE CAR
            blueCar.ActivateSelf();
            // RED CAR
            redCar.ActivateSelf();
        }

        // OnEnable is called when the object becomes enabled and active
        private void OnEnable() // Logic to initialize when the object is activated
        {
            // GREEN CAR
            greenCar.InitializeCarComponents();
            // BLUE CAR
            blueCar.InitializeCarComponents();
            // RED CAR
            redCar.InitializeCarComponents();
        }

        // OnDisable is called when the behaviour becomes disabled or inactive
        private void OnDisable() // Logic for cleanup when the object is deactivated
        {
            // GREEN CAR
            greenCar.DestroySelf();
            // BLUE CAR
            blueCar.DestroySelf();
            // RED CAR
            redCar.DestroySelf();
        }

        // OnDestroy is called when the MonoBehaviour will be destroyed
        private void OnDestroy() // Final cleanup logic before the object is completely destroyed
        {
            // GREEN CAR
            
            // BLUE CAR
            
            // RED CAR
        }

        // Update is called once per frame
        private void Update() // Regular game updates, such as input processing or non-physics related calculations
        {
            // GREEN CAR
            greenCar.ProcessPlayerInputs();
            greenCar.DisplayCarLights();
            greenCar.AnimateWheels();
            // BLUE CAR
            blueCar.ProcessPlayerInputs();
            blueCar.DisplayCarLights();
            blueCar.AnimateWheels();
            // RED CAR
           redCar.ProcessPlayerInputs();
           redCar.DisplayCarLights();
           redCar.AnimateWheels();

           // GREEN CAR
           greenCar.ProcessMovement();
           greenCar.DeactivateOnFinishLineCrossing();
        }

        // FixedUpdate is called at a fixed interval and is independent of frame rate
        private void FixedUpdate() // Physics-related calculations
        {
            // BLUE CAR
           blueCar.ProcessMovement();
           blueCar.DeactivateOnFinishLineCrossing();
           // RED CAR
           redCar.ProcessMovement();
           redCar.DeactivateOnFinishLineCrossing();
        }

        // LateUpdate is called once per frame, after Update has finished
        private void LateUpdate() // Adjustments to the game scene based on updates (e.g., follow camera logic) 
        {
            // GREEN CAR
            greenCar.UpdateSpeedUI(greenCar.calculateRoundedSpeed());
            // BLUE CAR
            blueCar.UpdateSpeedUI(blueCar.calculateRoundedSpeed());
            // RED CAR
            redCar.UpdateSpeedUI(redCar.calculateRoundedSpeed());
        }
    }