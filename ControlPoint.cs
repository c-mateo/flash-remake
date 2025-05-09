using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vector_Meshes
{
    public class ControlPoint : Control
    {
        public Vector Position {
            get => (Vector) Location + (Vector) Size / 2;
            set => Location = value - (Vector) Size / 2;
        }

        public ControlPoint() { }

        public ControlPoint(Vector position, Vector size, Vector? grabPoint = null)
        {
            Location = position - size / 2.0f;
            Size = size;
            if (grabPoint != null) dragPosition = (Vector)grabPoint;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Debug.WriteLine("Paint");
            Graphics g = e.Graphics;
            //g.Clear(Color.);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawEllipse(Pens.Red, 0, 0, Size.Width - 1, Size.Height - 1);
            g.FillEllipse(Brushes.Red, 0, 0, Size.Width - 1, Size.Height - 1);
        }

        Vector dragPosition;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Capture = true;
                Focus();
                dragPosition = e.Location;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Capture)
            {
                Vector newLocation = Location;
                newLocation.X += e.X - dragPosition.X;
                newLocation.Y += e.Y - dragPosition.Y;
                Location = newLocation;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                Capture = false;
            }
        }
    }
}
