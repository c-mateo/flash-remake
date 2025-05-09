namespace Vector_Meshes
{
    public class Bezier
    {
        public Manipulators start, end;

        public ControlPoint P0
        {
            get => start.Main;
            set => start.Main = value;
        }

        public ControlPoint P1
        {
            get => start.Handle ?? start.Main;
            set => start.Handle = value;
        }

        public ControlPoint P2
        {
            get => end.Handle ?? end.Main;
            set => end.Handle = value;
        }

        public ControlPoint P3
        {
            get => end.Main;
            set => end.Main = value;
        }

        public bool DrawStartHandle
        {
            get => start.Handle?.Visible ?? false;
            set
            {
                if (start.Handle != null) start.Handle.Visible = value;
            }
        }

        public bool DrawEndHandle
        {
            get => end.Handle?.Visible ?? false;
            set
            {
                if (end.Handle != null) end.Handle.Visible = value;
            }
        }

        public bool DrawHandles
        {
            get => DrawStartHandle || DrawEndHandle;
            set => (DrawStartHandle, DrawEndHandle) = (value, value);
        }

        public Bezier(Manipulators start, Manipulators end, bool drawHandles = false)
        {
            this.start = start;
            this.end = end;
            DrawHandles = drawHandles;
        }

        public void Draw(Graphics g)
        {
            start.Draw(g);
            end.Draw(g);
            g.DrawBezier(Pens.Black, P0.Position, P1.Position, P2.Position, P3.Position);

        }
    }
}
