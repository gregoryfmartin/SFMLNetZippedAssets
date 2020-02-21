using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace SFMLNetZippedAssets {
    class GameCore {
        /// <summary>
        /// Is the Game Core engine running or not?
        /// </summary>
        private Boolean Running;

        /// <summary>
        /// The global clock that runs the game.
        /// </summary>
        private Clock GameClock;

        /// <summary>
        /// The main window where all the action will occur.
        /// </summary>
        private GameWindow MainWindow;

        /// <summary>
        /// A holder for the FPS Delta so that its value can be used in input delegate functions.
        /// </summary>
        private float FPSD;

        private PlayerObject PO;

        private AssetManager am;

        private Sprite a;

        public GameCore () {
            Running = true;
            GameClock = null;
            MainWindow = null;
            PO = null;
            am = null;
            a = null;
        }

        public void Run () {
            Init ();

            while (Running) {
                FPSD = GameClock.Restart ().AsSeconds ();

                Update (FPSD);
                Draw ();
            }

            Deinit ();
        }

        private void Init () {
            MainWindow = new GameWindow ();
            MainWindow.PrincipalWindow.SetFramerateLimit (60);
            MainWindow.PrincipalWindow.SetVerticalSyncEnabled (true);
            MainWindow.KDownHandler += this.CheckGlobalInput;
            MainWindow.KDownHandler += this.CheckPlayerInputPressed;
            MainWindow.KUpHandler += this.CheckPlayerInputReleased;

            am = new AssetManager ();
            am.LoadAssets (@".\GameAssets.zip");

            PO = new PlayerObject (15.0f) {
                FillColor = Color.Red
            };

            a = new Sprite (am.LibraryTextures [10]);
            a.Position = new Vector2f (50.0f, 50.0f);

            MainWindow.ToRender.Add (PO);
            MainWindow.ToRender.Add (a);

            GameClock = new Clock ();
        }

        private void Update (float Delta) {
            MainWindow.Update (Delta);
            PO.Update (Delta);
        }

        private void Draw () {
            MainWindow.Draw ();
        }

        private void Deinit () {
            MainWindow.PrincipalWindow.Close ();
        }

        private void CheckGlobalInput (Object sender, KeyEventArgs e) {
            if (e.Code == Keyboard.Key.Escape) {
                Running = false;
            }
        }

        private void CheckPlayerInputPressed (Object sender, KeyEventArgs e) {
            PO.CheckInputPressed (e);
        }

        private void CheckPlayerInputReleased (Object sender, KeyEventArgs e) {
            PO.CheckInputReleased (e);
        }
    }
}
