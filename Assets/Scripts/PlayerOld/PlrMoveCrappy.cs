//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Player {
//    public enum PlayerStates {
//        Idle,
//        Walking,
//        Running,
//        Jumping,
//        Aerial
//    }

//    public class PlrMove : MonoBehaviour {
//        public PlayerStates state;

//        [Header("Inputs")]
//        public string HorizontalInput = "Horizontal";
//        public string VerticalInput = "Vertical";
//        public string RunInput = "Run";
//        public string JumpInput = "Jump";
//        public string CrouchInput = "Crouch";

//        [Header("Input Axes")]
//        public float hInput;
//        public float vInput;
//        public bool runPressed;
//        public bool jumpPressed;
//        public bool crouchPressed;
        
//        [Header("Player Motor")]
//        [Range(0f, 15f)]
//        public float walkMaxSpeed = 4f;
//        [Range(0f, 15f)]
//        public float walkAccel = 1.5f;
//        [Range(0f, 15f)]
//        public float runMaxSpeed = 8f;
//        [Range(0f, 15f)]
//        public float runAccel = 4.5f;
//        [Range(0f, 1f)]
//        public float speedDecay = 0.33f;
//        [Range(0f, 40f)]
//        public float jumpForce = 8f;
//        [Range(0f, 1f)]
//        public float airControl = 0.3f;
//        [Range(0f, 1f)]
//        public float airAccel = 0.8f;
//        [Range(0f, 1f)]
//        public float airResistance = 0.1f;

//        public float gravity = 20f;

//        public Vector3 moveVector;
//        public Vector3 newMove;

//        public bool grounded;

//        CharacterController charControl;

//        // Start is called before the first frame update
//        void Start() {
//            charControl = GetComponent<CharacterController>();
//        }

//        // Update is called once per frame
//        void Update() {
//            grounded = charControl.isGrounded;
//            HandleInput();
//            HandleState();
//            UpdateMoveVector();
            
//            charControl.Move(UpdateMoveVector() * Time.deltaTime);
//        }

//        Vector3 UpdateMoveVector() {
//            newMove = Vector3.zero;

//            float accel = runPressed ? runAccel : walkAccel;
//            float maxSpeed = runPressed ? runMaxSpeed : walkMaxSpeed;
            
//            if(state == PlayerStates.Aerial) {
//                //If in the air, decrease speed according to air resistance value instead of normal decay value.
//                //This means speed will still decay, but won't suddenly stop.
//                moveVector.x *= 1 - airResistance;
//                moveVector.z *= 1 - airResistance;

//                //Scale speed and acceleration to its air values
//                accel *= airAccel;
//                maxSpeed *= airControl;
//            } else if(jumpPressed) {
//                //StartCoroutine(PerformJumpRoutine());
//                moveVector.y = jumpForce;
//            } else {
//                moveVector.y = 0.0f;
//            }

//            //Decay speed if you're not pushing an input
//            if(hInput == 0) {
//                moveVector.x *= 1 - speedDecay;
//            }

//            if(vInput == 0) {
//                moveVector.z *= 1 - speedDecay;
//            }

//            //If moving one direction but pushing another, decay the speed in the opposite direction very quickly.
//            //This makes controls more responsive by allowing sudden changes in direction.
//            if((hInput > 0 && moveVector.x < 0) || (hInput < 0 && moveVector.x > 0)) {
//                moveVector.x *= 0.6f;
//            }
//            //Same as above
//            if((vInput > 0 && moveVector.z < 0) || (vInput < 0 && moveVector.z > 0)) {
//                moveVector.z *= 0.6f;
//            }

//            //Clamps speed so you can't accelerate forever
//            moveVector.x = Mathf.Clamp(moveVector.x + (accel * hInput), maxSpeed * -1, maxSpeed);
//            moveVector.z = Mathf.Clamp(moveVector.z + (accel * vInput), maxSpeed * -1, maxSpeed);

//            moveVector.y -= gravity * Time.deltaTime;

//            newMove += transform.right * moveVector.x;
//            newMove += transform.up * moveVector.y;
//            newMove += transform.forward * moveVector.z;
//            return newMove;
//        }

//        void HandleInput() {
//            hInput = Input.GetAxisRaw(HorizontalInput);
//            vInput = Input.GetAxisRaw(VerticalInput);
//            runPressed = Input.GetButton(RunInput);
//            jumpPressed = Input.GetButtonDown(JumpInput);
//            crouchPressed = Input.GetButton(CrouchInput);
//        }

//        void HandleState() {
//            if(charControl.isGrounded) {
//                if(hInput != 0 || vInput != 0) {
//                    if(runPressed) {
//                        state = PlayerStates.Running;
//                    } else {
//                        state = PlayerStates.Walking;
//                    }
//                } else {
//                    state = PlayerStates.Idle;
//                }
//            } else {
//                state = PlayerStates.Aerial;
//            }
//        }

//        IEnumerator PerformJumpRoutine() {
//            float _jump = jumpForce;

//            do {
//                charControl.Move(Vector3.up * _jump * Time.deltaTime);
//                _jump -= Time.deltaTime;
//                yield return null;
//            }
//            while(!charControl.isGrounded);
//        }
//    }
//}
