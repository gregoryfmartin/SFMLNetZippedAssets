using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace SFMLNetZippedAssets {
    class GameWindow {
        public RenderWindow PrincipalWindow { get; }

        public List<Drawable> ToRender { get; }

        public delegate void KeyDownHandler (Object sender, KeyEventArgs e);

        public delegate void KeyUpHandler (Object sender, KeyEventArgs e);

        public KeyUpHandler KUpHandler { get; set; }

        public KeyDownHandler KDownHandler { get; set; }

        public GameWindow () {
            PrincipalWindow = new RenderWindow (new VideoMode (800, 600), "SFML.NET Game Core");
            ToRender = new List<Drawable> ();

            PrincipalWindow.KeyPressed += this.Window_KeyPressed;
            PrincipalWindow.KeyReleased += this.Window_KeyReleased;

            // Because delegates are cool.
            KUpHandler += this.Dummy_KeyReleased;
            KDownHandler += this.Dummy_KeyPressed;
        }

        public void Update (float Delta) {
            PrincipalWindow.DispatchEvents ();
        }

        public void Draw () {
            PrincipalWindow.Clear (Color.Black);

            if (ToRender.Count > 0) {
                foreach (Drawable d in ToRender) {
                    PrincipalWindow.Draw (d);
                }
            }

            PrincipalWindow.Display ();
        }

        private void Window_KeyPressed (Object sender, KeyEventArgs e) {
            KDownHandler (sender, e);
        }

        private void Window_KeyReleased (Object sender, KeyEventArgs e) {
            KUpHandler (sender, e);
        }

        /// <summary>
        /// Because delegates are cool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dummy_KeyPressed (Object sender, KeyEventArgs e) { }

        /// <summary>
        /// Because delegates are cool.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dummy_KeyReleased (Object sender, KeyEventArgs e) { }
    }
}
