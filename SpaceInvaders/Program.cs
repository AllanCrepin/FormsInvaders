using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// Create an alias for the Timer class.
// Necessary because there are 2 different Timer classes
using WinFormsTimer = System.Windows.Forms.Timer;
using System.Reflection;
using System.Drawing.Drawing2D;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;
using static System.Windows.Forms.AxHost;
using System.Security.Cryptography.X509Certificates;


static class GlobalData
{
    public static int ClientWidth;
    public static int ClientHeight;
    public static int XP = 0;
}


internal static class NativeMethods
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Message
    {
        public IntPtr hWnd;
        public uint msg;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public System.Drawing.Point p;
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint filterMin, uint filterMax, uint removeMsg);
}



namespace SpaceInvaders
{
    public class Invaders : Form
    {

        private bool CPULimitUsage = true;

        private double t = 0.0d;
        private double dt = 1.0d/30.0d; // Game logic update rate / Game tick

        private double TargetFrameTime = 16.6666d;

        private double LastRenderTime = 0;

        private bool cont = true;

        private Stopwatch stopwatch = new Stopwatch();
        private double currentTime;
        private double previousTime;
        private double frametime;
        private double newtime;

        private World GameWorld;

        private double accumulator = 0.0d;

        public Sprite ShooterSprite { get; set; }
        public Sprite BulletSprite { get; set; }
        public Entity Shooter {  get; set; }
        public List<Entity> Bullets { get; set; }

        public bool CooldownBool { get; set; } = true;
        public double Cooldown { get; set; }




        public WinFormsTimer Timer { get; set; }

        public Invaders(int width, int height)
        {

            TimeBeginPeriod(1); // Increase OS time precision - global


            this.Width = width;
            this.Height = height;
            this.DoubleBuffered = true;  // Reduce flickering during rendering
            this.AutoScaleMode = AutoScaleMode.None;

            Bullets = new List<Entity>();

            GameWorld = new World(this.ClientSize.Width, this.ClientSize.Height);
            GlobalData.ClientWidth = this.ClientSize.Width;
            GlobalData.ClientHeight = this.ClientSize.Height;
            
            this.FormClosing += GameForm_FormClosing;

            stopwatch.Start();
            currentTime = stopwatch.Elapsed.TotalSeconds;

            Application.Idle += OnApplicationIdle;



        }

        public void Update(double t, double dt)
        {
            Input.ReadInput();
        }

        public void PreviousIsCurrent(double t, double dt)
        {
            GameWorld.PreviousIsCurrent(t, dt);
        }

        public void FixedUpdate(double t, double dt)
        {
            GameWorld.Step(t, dt);
        }

        // Interpolate between previous and current states based on alpha
        private void Interpolate(double alpha)
        {

            GameWorld.Interpolate(alpha);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor; // clamp to pixel perfect pos
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.SmoothingMode = SmoothingMode.None;

            GameWorld.Render(e);



        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cont = false;
            TimeEndPeriod(1); // Restore OS time precision - global
        }

        private void GameLoop(object sender, EventArgs e)
        {

            double newTime = stopwatch.Elapsed.TotalSeconds;
            double frameTime = newTime - currentTime; // Time since last frame
            currentTime = newTime;


            if (frameTime > 0.25d)
                frameTime = 0.25d; // Avoid spiral of death (large time steps)

            accumulator += frameTime;

            Update(t, frametime);

            // Fixed timestep update loop
            while (accumulator >= dt)
            {
                PreviousIsCurrent(t, dt);
                FixedUpdate(t, dt); // Update game state
                t += dt;          // Move forward in time
                accumulator -= dt; // Decrease the accumulator

            }

            double alpha = accumulator / dt;

            // Interpolate the state for smooth rendering
            Interpolate(alpha);

            this.Invalidate();

        }


        private void OnApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {

                if (CPULimitUsage)
                {
                    double frameStart = stopwatch.Elapsed.TotalMilliseconds;

                    GameLoop(sender, e);

                    double frameEnd = stopwatch.Elapsed.TotalMilliseconds;

                    double elapsedTime = frameEnd - frameStart;

                    double timeToWait = TargetFrameTime - elapsedTime;
                    timeToWait = timeToWait / 2.5d;

                    if (timeToWait > 3)
                    {
                        Thread.Sleep((int)timeToWait);
                    }
                }
                else
                {
                    GameLoop(sender, e);
                }

            }

        }


        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
        public static extern uint TimeBeginPeriod(uint period);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod", SetLastError = true)]
        public static extern uint TimeEndPeriod(uint period);


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int PeekMessage(out Message lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);


        private bool IsApplicationIdle()
        {
            return PeekMessage(out var msg, IntPtr.Zero, 0, 0, 0) == 0;
        }

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        public static void Main()
        {
            // Make the application DPI-aware
            SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Invaders(800, 600));
        }

    }


    public struct Pos
    {
        public double x;
        public double y;

        public Pos(double x, double y) { this.x = x; this.y = y; }
    }

}
