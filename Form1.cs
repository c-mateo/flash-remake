using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace Vector_Meshes
{
    public partial class Form1 : Form
    {
        public static Form1? context;

        List<Manipulators> newBezierManipulators = new(2);
        List<Bezier> beziers = new();

        public Form1()
        {
            context = this;
            InitializeComponent();
        }

        void LocationChangedHandler(object sender, EventArgs e)
        {
            Invalidate();
        }



        //protected override void OnMouseClick(MouseEventArgs e)
        //{
        //    base.OnMouseClick(e);

        //}



        public ControlPoint AddPoint(Vector position, Vector? grabPosition = null)
        {
            var cp = new ControlPoint(position, new Size(10, 10), grabPosition);
            cp.LocationChanged += (s, e) =>
            {
                Invalidate();
            };
            Controls.Add(cp);
            Invalidate();
            return cp;
        }

        bool draggingHandle = false;
        Manipulators? placingManipulator = null;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Capture = true;
                draggingHandle = true;
                Focus();

                Debug.WriteLine("Add anchor");
                var manipulator = new Manipulators(AddPoint(e.Location));
                newBezierManipulators.Add(manipulator);
                placingManipulator = manipulator;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                Capture = false;
                draggingHandle = false;
                placingManipulator = null;

                if (newBezierManipulators.Count == 2)
                {
                    var bezier = new Bezier(newBezierManipulators[0], newBezierManipulators[1]);
                    beziers.Add(bezier);
                    var lastManipulator = newBezierManipulators[1];
                    newBezierManipulators.Clear();

                    ControlPoint? newHandle = null;
                    if (lastManipulator.Handle != null) {
                        Debug.WriteLine("Last");
                        var relativeHandlePos = lastManipulator.Handle.Position - lastManipulator.Main.Position;
                        newHandle = AddPoint(lastManipulator.Main.Position - relativeHandlePos);
                    }

                    var newStartManipulator = new Manipulators(lastManipulator.Main, newHandle);
                    newBezierManipulators.Add(newStartManipulator);
                    placingManipulator = newStartManipulator;
                    Invalidate();
                }
            }
        }

        //ControlPoint? controlPoint1 = null;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (draggingHandle)
            {
                if (placingManipulator == null) return;

                var distance = Vector.Distance(e.Location, placingManipulator.Main.Position);
                const int threshold = 10;

                if (placingManipulator.Handle == null && distance > threshold)
                {
                    placingManipulator.Handle = AddPoint(e.Location);
                }
                else if (placingManipulator.Handle != null && distance > threshold)
                {
                    placingManipulator.Handle.Position = e.Location;
                    //Invalidate();
                }
                else if (placingManipulator.Handle != null && distance < threshold)
                {
                    Controls.Remove(placingManipulator.Handle);
                    placingManipulator.Handle = null;
                    //Invalidate();
                }

                //Point newLocation = Location;
                //newLocation.X += e.X - controlPoints.Last().Location.X;
                //newLocation.Y += e.Y - controlPoints.Last().Location.Y;
                //controlPoints.Last().Location = newLocation;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Draw bezier being created
            if (newBezierManipulators.Count == 2)
            {
                var bezier = new Bezier(newBezierManipulators[0], newBezierManipulators[1], true);
                bezier.Draw(g);
            }
            else if (placingManipulator?.Handle != null)
            {
                g.DrawLine(Pens.Blue, placingManipulator.Main.Position, placingManipulator.Handle.Position);
            }

            // Draw existing beziers
            foreach (var bezier in beziers)
            {
                bezier.Draw(g);
            }
        }
    }
}
