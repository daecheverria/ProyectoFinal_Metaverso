using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

namespace PolyStang
{
    public class CarController : MonoBehaviour
    {
        private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction brakeAction;
        public InstanciaCarroSO instanciaCarro;

        public enum ControlMode // this car controller works for both pc and touch devices. You can switch the control mode from the inspector.
        {
            Keyboard,
            Buttons
        };

        public enum Axel // used to identify front and rear wheels.
        {
            Front,
            Rear
        }

        [Serializable]
        public struct Wheel // wheel bits: all fields must be filled to make the wheel work properly.
        {
            public GameObject wheelModel;
            public WheelCollider wheelCollider;
            public GameObject wheelEffectObj;
            public ParticleSystem smokeParticle;
            public Axel axel;
            public GameObject skidSound;
            public int index;
        }

        public ControlMode control;

        [Header("Inputs")]
        public KeyCode brakeKey = KeyCode.Space;

        [Header("Accelerations and deaccelerations")]
        public float maxAcceleration = 30.0f;
        public float brakeAcceleration = 50.0f;
        public float noInputDeacceleration = 10.0f;

        [Header("Steering")]
        public float turnSensitivity = 1.0f;
        public float maxSteerAngle = 30.0f;

        [Header("Speed UI")]
        public TMP_Text speedText;
        public float UISpeedMultiplier = 4;

        [Header("Speed limit")]
        public float frontMaxSpeed = 200;
        public float rearMaxSpeed = 50;
        public float empiricalCoefficient = 0.41f;
        public enum TypeOfSpeedLimit
        {
            noSpeedLimit,
            simple,
            squareRoot
        };
        public TypeOfSpeedLimit typeOfSpeedLimit = TypeOfSpeedLimit.squareRoot;
        [SerializeField] float frontSpeedReducer = 1;
        [SerializeField] float rearSpeedReducer = 1;

        [Header("Skid")]
        public float brakeDriftingSkidLimit = 10f;
        public float lateralFrontDriftingSkidLimit = 0.6f;
        public float lateralRearDriftingSkidLimit = 0.3f;

        [Header("General")]
        public Vector3 _centerOfMass;

        public List<Wheel> wheels;

        float moveInput;
        float steerInput;

        private Rigidbody carRb;

        private CarLights carLights;
        private CarSounds carSounds;

        void Awake() // called the first frame, when the game starts.
        {
            carRb = GetComponent<Rigidbody>();
            carRb.centerOfMass = _centerOfMass;

            carLights = GetComponent<CarLights>();
            carSounds = GetComponent<CarSounds>();
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
            brakeAction = playerInput.actions["Brake"];
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
            brakeAction.performed += OnBrake;
            brakeAction.canceled += OnBrake;
            instanciaCarro.instancia = this.gameObject;
        }

        void LateUpdate() // called after the "Update()" function.
        {
            Move();
            Steer();
            UpdateSpeedUI();
            AnimateWheels();
            WheelEffectsCheck();
            CarLightsControl();
        }

        void OnDestroy()
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            brakeAction.performed -= OnBrake;
            brakeAction.canceled -= OnBrake;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            moveInput = input.y;
            steerInput = input.x;
        }


        void Move()
        {
            foreach (var wheel in wheels)
            {
                // Rotational speed is proportional to radius * frequency: the empirical coefficient is around 0.41
                float currentWheelSpeed = empiricalCoefficient * wheel.wheelCollider.radius * wheel.wheelCollider.rpm;

                if (moveInput != 0) // Permitir torque incluso desde el reposo
                {
                    // Determinar el reductor según el movimiento hacia adelante o hacia atrás
                    float speedReducer = moveInput > 0 ? frontSpeedReducer : rearSpeedReducer;

                    // Aplicar torque al motor
                    wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * speedReducer * Time.deltaTime;
                    wheel.wheelCollider.brakeTorque = 0; // Asegurarse de que no haya freno residual
                }
                else if (carRb.velocity.magnitude > 0.1f) // Sin entrada de usuario pero el carro aún se mueve
                {
                    wheel.wheelCollider.brakeTorque = 300 * noInputDeacceleration * Time.deltaTime;
                    wheel.wheelCollider.motorTorque = 0; // No aplicar par motor si no hay entrada
                }
                else // El carro está completamente detenido y sin entrada
                {
                    wheel.wheelCollider.motorTorque = 0;
                    wheel.wheelCollider.brakeTorque = 0; // No bloquear las ruedas innecesariamente
                }
            }
        }


        void Steer() // to rotate the front wheels, when steering.
        {
            foreach (var wheel in wheels)
            {
                if (wheel.axel == Axel.Front)
                {
                    var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                    wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
                }
            }
        }
        public void OnBrake(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                // Aplicar freno
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
                }
            }
            else if (context.canceled)
            {
                // Quitar freno
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 0;
                }
            }
        }
        void BrakeAndDeacceleration()
        {
            if (Input.GetKey(brakeKey)) // when pressing space, the brake is used.
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
                }

            }
            else if (moveInput == 0) // with no vertical input, a slight deacceleration is used to slightly slow down the speed of the car.
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 300 * noInputDeacceleration * Time.deltaTime;
                }
            }
            else // with vertical input, no brake or deacceleration is applied.
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 0;
                }
            }
        }


        void AnimateWheels() // to animate wheels accordingly to the car speed.
        {
            foreach (var wheel in wheels)
            {
                Quaternion rot;
                Vector3 pos;
                wheel.wheelCollider.GetWorldPose(out pos, out rot);
                wheel.wheelModel.transform.position = pos;
                wheel.wheelModel.transform.rotation = rot;
            }
        }

        void WheelEffectsCheck() // checking for every wheel if it's slipping: if yes, the "EffectCreate()" function is called.
        {
            foreach (var wheel in wheels)
            {
                // slipping ---> skid
                WheelHit GroundHit; // variable to store hit data
                wheel.wheelCollider.GetGroundHit(out GroundHit); // store hit data into GroundHit
                float lateralDrift = Mathf.Abs(GroundHit.sidewaysSlip);

                if (Input.GetKey(brakeKey) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.velocity.magnitude >= brakeDriftingSkidLimit)
                {
                    EffectCreate(wheel);
                }
                else if (wheel.wheelCollider.isGrounded == true && wheel.axel == Axel.Front && (lateralDrift > lateralFrontDriftingSkidLimit)) // drifting: front wheels
                {
                    EffectCreate(wheel);
                }
                else if (wheel.wheelCollider.isGrounded == true && wheel.axel == Axel.Rear && (lateralDrift > lateralRearDriftingSkidLimit)) // drifting: rear wheels
                {
                    EffectCreate(wheel);
                }
                else
                {
                    wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
                    carSounds.StopSkidSound(wheel.skidSound, wheel.index); // actually decreasing the volume of the skid to 0: see the "CarSound" script.
                }
            }
        }

        private void EffectCreate(Wheel wheel) // actually creating the effects: 1) trail renderer for the skid, 2) smoke particles, 3) skid sound.
        {
            wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
            wheel.smokeParticle.Emit(1);
            carSounds.PlaySkidSound(wheel.skidSound); // actually setting the volume of the skid to 1
        }

        void CarLightsControl() // controlling lights, through the specific script "CarSounds".
        {
            if (Input.GetKey(brakeKey)) // the red lights are activated when the brake is pressed
            {
                carLights.RearRedLightsOn();
            }
            else
            {
                carLights.RearRedLightsOff();
            }

            if (moveInput < 0f) // the rear white lights are activated when the player is pressing "S" or down arrow.
            {
                carLights.RearWhiteLightsOn();
            }
            else
            {
                carLights.RearWhiteLightsOff();
            }
        }

        void UpdateSpeedUI() // UI: speed update.
        {
            int roundedSpeed = (int)Mathf.Round(carRb.velocity.magnitude * UISpeedMultiplier);
            speedText.text = roundedSpeed.ToString();
        }
    }
}