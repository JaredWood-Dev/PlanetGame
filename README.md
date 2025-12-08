# Planet-Game
This game is a personal project to fulfill two primary objectives.
* Design a high quality platformer controller.
* Implement the controller in such a way to mimic the gravity in the *Super Mario Galaxy* games.

The player takes on the role of **Houston** a dragonborn astronaut, to explore varied worlds, planets, and planetoids in the galaxy. The gameplay revolves around the previously mentioned platformer controller.

## Usage
Refer to the game's itch.io page where it can be played in the browser. `A` and `D` move the character left and right, `space` causes the character to jump, and `left shift` causes the character to use his gravity breath weapon - which launches the character into the air.

---

## Development
This section documents the development process of the game, including new techniques or lessons learned throughout the development process.

### Gravity and Gravity Objects

In the *Super Mario Galaxy* games, gravity works differently from reality. In these games, gravity has a uniform acceleration regardless of planetary mass. Furthermore, planetoids can have varying gravity fields that range from a uniform one directly down to a 'radial' one that pulls objects toward the center of the planetoid.

In order to achieve this effect in Unity, I had to manually develop a 'gravity' force that gets applied to all objects that should be effected by gravity. In order to achieve this, I developed the `gravityObject.cs` component:
```c#
void FixedUpdate()
{
    _rb.linearVelocity += downDirection * (gravity * Time.fixedDeltaTime);
}
```
This relies on basic physics: `acceleration = (velocity - newVelocity) / time`. The `downDirection` variable represents the current direction of gravity that is being exerted on the object. Changing this vector results in gravity causing the object to 'fall' in the corresponding direction.

#### Why not `_rb.AddForce()` ?
The Unity physics system does have a natural way to apply forces to objects with rigidbodies, and this approach was attempted at first. Unfortunately this implementation caused objects to 'buffer' the momentum from gravity, causing the speed to continue to increase even if the object was on the ground.

The next step was to rotate the object accordingly, but to make an engaging user experience, if the object 'snapped' to the new rotation it would not look polished. 
```c#
var currentUp = transform.up;
var targetUp = -downDirection;
            
float currentAngle = Mathf.Atan2(currentUp.y, currentUp.x) * Mathf.Rad2Deg;
float targetAngle = Mathf.Atan2(targetUp.y, targetUp.x) * Mathf.Rad2Deg;
float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);
transform.rotation = Quaternion.Euler(0, 0, newAngle - 90f);
```
By utilizing a linear interpolation (`lerp`), we can smooth out the rotation of the object based on a settable `rotation` speed.

At this point, objects can now be set to 'fall' in any given direction. In order to make gravity, I need automatically set this down direction depending on if the player is in a gravity field. One consideration is there will be gravity fields that overlap, so which field will be the 'current' field?

To address this question, I devised the following function:
```c#
public void CheckGravitySources()
    {
        if (gravityColliders.Count > 0)
        {
            Collider2D field = gravityColliders[0];
            int higestPriority = 0;
            foreach (var gravityField in gravityColliders)
            {
                if (gravityField.GetComponent<GravitySource>().gravityPriority > higestPriority)
                {
                    field = gravityField;
                    higestPriority = gravityField.GetComponent<GravitySource>().gravityPriority;
                }
            }
            
            gravityObject = field.gameObject;
            gravityType = field.gameObject.GetComponent<GravitySource>().gravityType;
        }

    }
```
`gravityColliders` is a list that contains all the gravity colliders the object is currently overlapping. When `CheckGravitySources()` is called, it performs a linear search to locate the first gravity field that has the highest priority. This means if two gravity fields have the same priority, the first one the object came into contact with will be found. The gravity field found in the search will become the 'current' gravity field.

This search is called whenever the list of gravity colliders change, generally when the object overlaps a new gravity source:
```c#
void OnTriggerEnter2D(Collider2D other)
    {
        CheckGravitySources();
        if (other.gameObject.GetComponent<GravitySource>() != null)
            gravityColliders.Add(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        CheckGravitySources();
        if (other.gameObject.GetComponent<GravitySource>() != null)
            gravityColliders.Remove(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        CheckGravitySources();
        if (other.gameObject.GetComponent<GravitySource>() != null && !gravityColliders.Contains(other))
            gravityColliders.Add(other);
    }
```
In general, when the object overlaps a new trigger, it checks if the trigger is a gravity source. If it is, it's the gravity source list and calls `CheckGravitySources()`. The same happens when the object leaves a gravity field.

These scripts come together to allow objects to be effected by the various gravity fields that can be placed around the level. This implementation was effective, as it allows other physics interactions to occur even if Unity's default gravity was being used.

### Player Controller
One of the most fundamental components of a fun platforming game is the player controller. The reason is simple: if the player is not fun to control, no one will play the game. Knowing this reality, the controller must serve two functions:
* Be fun and fair for a player to control the game with.
* Be unique from other platforming games.

Knowing these two principals, there are a number of features that modern platformer controllers have that make the game more fun to play - at the expense of realistic physics.
* Coyote Time
  * Allows players to jump even after leaving a ledge, because human reaction time is too slow.
* Jump Buffering
  * Registers a jump even if the player pressed the jump key too early.
* Variable Jump Height
  * Allows the player to have more control over the character.

Furthermore, jumping fundamentally relies on the direction of gravity, but this script should be loosely coupled to the gravity script. In order to achieve this, the gravity script rotates the gameobject, so the controller script just needs to reference the rotation of the gameobject. While most platformer controllers will refer to the global 'up' (`Vector2.up`), this character controller will always need to refer to the local direction (`transform.up`) instead. This approach will induce more vector math, but will make the system robust; it can be used in normal gravity conditions and this game's unique gravity conditions.

In order to move the character, I applied force to the character whenever the directional keys were pressed. Specifically I used `GetAxis("Horizontal")` and checked if `direction > 0`. I then used physics to calculate the amount of force needed to move the player to a desired speed. The purpose of `direction > 0`, as opposed to setting the desired speed to `0`, was because this would limit the physics interaction possible with the player.
```c#
void Update()
    {
        // Handle Left-Right Inputs
        direction = Input.GetAxis("Horizontal");
    }
```
In order to get the input direction and magnitude.

```c#
void FixedUpdate()
    {
        if (direction != 0)
        {
            moveSpeed = Mathf.Max(speed - Vector2.Dot(_rb.linearVelocity , dir), 0) * Mathf.Abs(direction);
            float acceleration = moveSpeed / Time.fixedDeltaTime;
            float force = acceleration * _rb.mass;
            if (!onGround)
                force *= arialMovementModifer;
            _rb.AddForce(force * dir, ForceMode2D.Force);
            _an.SetBool("isRunning", true);
        }
        else
        {
            _an.SetBool("isRunning", false);
            if (!onGround)
            {
                _rb.AddForce(-_rb.linearVelocity.normalized * 100, ForceMode2D.Force);
            }
                
        }
    }
```
Apply the force if the player is pressing a key. Otherwise, apply a 'friction' force to reduce the character's speed back to zero.

Note: `dir` is a variable that is either `1` or `-1` to handle flipping directions and applying the movement force in the appropriate direction.

This movement code allows the character to move around, with this implementation being effective for switching gravity fields and walking around the edges of planetoids, which is required for this game.

The next objective to implement was jumping. The big challenge was to choose whether to implementing the jumping in `Update()` or `FixedUpdate()`. `Update()` is dependent on framerate, and the best place to implement player input responses, while `FixedUpdate()` is frame independent, which makes it better for physics calculations. Jumping requires both of the events. The solution is to implement a state machine.

This state machine will have four states:
* `Down`, when the player first presses the key
* `Pressed`, when the player holds the key
* `Up`, when the player releases the key
* `Off`, when the player is not interacting with the key

The state will be initiated in `Update()`, allowing for a responsive feeling when pressing the key:
```c#
void Update() 
{
    //Handle the Jump Inputs
    if (Input.GetButtonDown("Jump"))
    {
        jumpState = KeyState.Down;
    }

    if (Input.GetButtonUp("Jump"))
    {
        jumpState = KeyState.Up;
    }
}
```
The state will be changed and physics performed within `FixedUpdate()`, allowing the calculations to be performed independent of the framerate of the game.
```c#
void FixedUpdate()
{
    //Calculate the force needed to jump to the desired height
    //print(Vector2.Dot(_rb.linearVelocity, transform.up));
    float currentVelocity = Vector2.Dot(_rb.linearVelocity, transform.up);
    currentVelocity = Mathf.Max(currentVelocity, 0f);
    float jumpForce = (jumpHeight - 0/ Time.fixedDeltaTime) * _rb.mass;
    jumpForce /= 2;
    //Resolve Jump Inputs
    if (jumpState == KeyState.Down)
    {
        //When the Jump Key is Pressed
        jumpState = KeyState.Pressed;
        if (onGround)
        {
            ZeroUpwardVelocity();
            _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        _an.SetTrigger("jumped");
    }
    if (jumpState == KeyState.Pressed)
    {
        //When the Jump Key is Held
    }
    if (jumpState == KeyState.Up)
    {
        //When the Jump Key is Released
        jumpState = KeyState.Off;
            
        //If we are going up and release, half the upward velocity
        if ((_rb.linearVelocity * transform.up).y > 0)
            _rb.linearVelocity *= 0.5f;
    }    
}
```
Similar to the velocity, the upward force applied is calculated based on the desired jump height, allowing for precise control over the jump ability.

The state diagram approach was highly effective because it allowed the separation between `Update()` and `FixedUpdate()`, as mentioned previously, but also gives a variety of press states for other gameplay effects. Most notably, the ability to check if the key was released, allows the velocity to be cut in half:
```c#
if ((_rb.linearVelocity * transform.up).y > 0)
            _rb.linearVelocity *= 0.5f;
```
This allows the player to control the jump height of the character.