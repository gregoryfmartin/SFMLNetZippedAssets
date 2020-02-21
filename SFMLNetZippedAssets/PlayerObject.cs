using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace SFMLNetZippedAssets {
    /// <summary>
    /// The entity the player controls. The only major callout here is that it supports eight-directional
    /// movement by translating key states to flag toggles in a dictionary for valid cardinal directions.
    /// A combination of these will yield eight-directional movement patterns.
    /// </summary>
    class PlayerObject : CircleShape {
        public Dictionary<String, Boolean> MovementStates { get; }

        public Vector2f Velocity { get; set; }

        public PlayerObject () : base () {
            MovementStates = new Dictionary<String, Boolean> {
                { "Left", false },
                { "Right", false },
                { "Up", false },
                { "Down", false }
            };
            Velocity = new Vector2f (3.0f, 3.0f);
        }

        public PlayerObject (Single radius) : base (radius) {
            MovementStates = new Dictionary<String, Boolean> {
                { "Left", false },
                { "Right", false },
                { "Up", false },
                { "Down", false }
            };
            Velocity = new Vector2f (3.0f, 3.0f);
        }

        public void CheckInputPressed (KeyEventArgs e) {
            switch (e.Code) {
                case Keyboard.Key.Right:
                    MovementStates ["Right"] = true;
                    break;
                case Keyboard.Key.Left:
                    MovementStates ["Left"] = true;
                    break;
                case Keyboard.Key.Up:
                    MovementStates ["Up"] = true;
                    break;
                case Keyboard.Key.Down:
                    MovementStates ["Down"] = true;
                    break;
            }
        }

        public void CheckInputReleased (KeyEventArgs e) {
            switch (e.Code) {
                case Keyboard.Key.Right:
                    MovementStates ["Right"] = false;
                    break;
                case Keyboard.Key.Left:
                    MovementStates ["Left"] = false;
                    break;
                case Keyboard.Key.Up:
                    MovementStates ["Up"] = false;
                    break;
                case Keyboard.Key.Down:
                    MovementStates ["Down"] = false;
                    break;
            }
        }

        /// <summary>
        /// The scalar on the Delta here is reliant upon the fact that the desired FPS is set to 60 and VSync is on. Otherwise,
        /// the Delta becomes highly irregular and this scalar could easily result in neighbouring values ranging from 1 to 11.
        /// With VSync and nearly locked FPS, the value of Delta becomes considerably more predictable. There's definitely a better
        /// way of doing this, but I'm unsure what it would be at the moment.
        /// </summary>
        /// <param name="Delta">The time between frames, taken from the main game loop through a multidelegate.</param>
        public void Update (float Delta) {
            if (MovementStates ["Right"]) {
                Position += new Vector2f (Velocity.X * (Delta * 100), 0.0f);
            }
            if (MovementStates ["Left"]) {
                Position += new Vector2f (-(Velocity.X * (Delta * 100)), 0.0f);
            }
            if (MovementStates ["Up"]) {
                Position += new Vector2f (0.0f, -(Velocity.Y * (Delta * 100)));
            }
            if (MovementStates ["Down"]) {
                Position += new Vector2f (0.0f, Velocity.Y * (Delta * 100));
            }
        }
    }
}
